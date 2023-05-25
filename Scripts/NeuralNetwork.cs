using Newtonsoft.Json;
using System;

namespace Neural_Network_XOR.Scripts
{
    public class NeuralNetwork
    {
        public int[] layer;
        public Layer[] layers;
        const float LearningRate = 0.001f;

        public NeuralNetwork(int[] layer)
        {
            int layerLength = layer.Length;

            this.layer = new int[layerLength];
            for (int i = 0; i < layerLength; i++)
            {
                this.layer[i] = layer[i];
            }

            int layersLength = layerLength - 1;
            layers = new Layer[layersLength];
            for (int i = 0; i < layersLength; i++)
            {
                layers[i] = new Layer(layer[i], layer[i+1]);
            }
        }

        public string toJson()
        {
            var jsonData = JsonConvert.SerializeObject(this);
            return jsonData;
        }
        public static NeuralNetwork fromJson(string data)
        {
            var jsonData = JsonConvert.DeserializeObject<NeuralNetwork>(data);
            return jsonData;
        }

        public float[] FeedForward(float[] inputs)
        {
            layers[0].FeedForward(inputs);
            int layersLength = layers.Length;
            for (int i = 1; i < layersLength; i++)
            {
                layers[i].FeedForward(layers[i - 1].outputs);
            }
            return layers[layersLength - 1].outputs;
        }

        public void BackProb(float[] expected)
        {
            int layersLength = layers.Length - 1;
            for (int i = layersLength; i >= 0; i--)
            {
                if (i == layersLength) {
                    layers[i].BackPropOutput(expected);
                }
                else
                {
                    layers[i].BackPropHiden(layers[i + 1].gamma, layers[i + 1].weights);
                }
            }

            for (int i = 0; i < layers.Length; i++)
            {
                layers[i].UpdateWeights(LearningRate);
            }
        }


        internal void Reset()
        {
            for (int i = 0; i < layers.Length; i++)
            {
                layers[i].InitilizeWeights();
            }
        }

        public class Layer
        {
            public int numberOfInputs;
            public int numberOfOutputs;

            public float[] inputs;

            public float[,] weights;
            public float[,] weightsDelta;

            public float[] outputs;
            public float[] gamma;
            public float[] error;

            public static Random random = new Random();

            public Layer(int numberOfInputs, int numberOfOutputs)
            {
                this.numberOfInputs = numberOfInputs;
                this.numberOfOutputs = numberOfOutputs;

                inputs = new float[numberOfInputs];

                weights = new float[numberOfOutputs, numberOfInputs];
                weightsDelta = new float[numberOfOutputs, numberOfInputs];

                outputs = new float[numberOfOutputs];
                gamma = new float[numberOfOutputs];
                error = new float[numberOfOutputs];

                InitilizeWeights();
            }

            public void InitilizeWeights()
            {
                for (int i = 0; i < numberOfOutputs; i++)
                {
                    for (int j = 0; j < numberOfInputs; j++)
                    {
                        weights[i, j] = (float)random.NextDouble() - 0.5f;
                    }
                }
            }

            public float[] FeedForward(float[] inputs)
            {
                this.inputs = inputs;
                for (int i = 0; i < numberOfOutputs; i++)
                {
                    outputs[i] = 0;
                    for (int j = 0; j < numberOfInputs; j++)
                    {
                        outputs[i] += inputs[j] * weights[i, j];
                    }
                    outputs[i] = (float)Math.Tanh(outputs[i]);
                }
                return outputs;
            }

            public void UpdateWeights(float LearningRate)
            {
                for (int i = 0; i < numberOfOutputs; i++)
                {
                    for (int j = 0; j < numberOfInputs; j++)
                    {
                        weights[i, j] -= weightsDelta[i, j] * LearningRate;
                    }
                }
            }

            public float TanhDer(float value)
            {
                return 1f - (value * value);
            }

            public void BackPropOutput(float[] expected)
            {
                for (int i = 0; i < numberOfOutputs; i++)
                {
                    error[i] = outputs[i] - expected[i];
                }

                for (int i = 0; i < numberOfOutputs; i++)
                {
                    gamma[i] = error[i] * TanhDer(outputs[i]);
                }

                for (int i = 0; i < numberOfOutputs; i++)
                {
                    for (int j = 0; j < numberOfInputs; j++)
                    {
                        weightsDelta[i, j] = gamma[i] * inputs[j];
                    }
                }

            }

            public void BackPropHiden(float[] gammaForward, float[,] weightsForward)
            {
                for (int i = 0; i < numberOfOutputs; i++)
                {
                    gamma[i] = 0;
                    for (int j = 0; j < gammaForward.Length; j++)
                    {
                        gamma[i] += gammaForward[j] * weightsForward[j, i];
                    }
                    gamma[i] *= TanhDer(outputs[i]);
                }

                for (int i = 0; i < numberOfOutputs; i++)
                {
                    for (int j = 0; j < numberOfInputs; j++)
                    {
                        weightsDelta[i, j] = gamma[i] * inputs[j];
                    }
                }
            }
        }
    }
}
