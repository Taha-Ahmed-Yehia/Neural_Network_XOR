using Newtonsoft.Json;
using System;

namespace Neural_Network_XOR.Scripts
{
    public class NeuralNetwork
    {
        public int[] layer;
        public Layer[] layers;
        const float LearningRate = 0.001f;
        /// <summary>
        /// Constarctor 
        /// </summary>
        /// <param name="layer">
        /// How many layers the network will have.
        /// <br>First element in the array is for the inputs, and the last element is for the outputs.</br>
        /// <br>The elements between first and last are for heddin layers.</br>
        /// </param>
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

        #region Json Convertions
        /// <summary>
        /// Convert Neural Network data to JSON Format and return it as String.
        /// </summary>
        /// <returns>JSON data as String.</returns>
        public string toJson()
        {
            var jsonData = JsonConvert.SerializeObject(this);
            return jsonData;
        }
        /// <summary>
        /// Convert JSON data to NeuralNetwork.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>NeuralNetwork from JSON Format.</returns>
        public static NeuralNetwork fromJson(string data)
        {
            var jsonData = JsonConvert.DeserializeObject<NeuralNetwork>(data);
            return jsonData;
        }
        #endregion

        /// <summary>
        /// Inject neural network with inputs and get the outputs from it.
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns>outputs of neural network.</returns>
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
        /// <summary>
        /// Perform backpropagation with expected values to calculate erros and train network.
        /// </summary>
        /// <param name="expected">The correct values the network should output.</param>
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
                    layers[i].BackPropHidden(layers[i + 1].gamma, layers[i + 1].weights);
                }
            }

            for (int i = 0; i < layers.Length; i++)
            {
                layers[i].UpdateWeights(LearningRate);
            }
        }
        /// <summary>
        /// Reset neural network by initilize new weights.
        /// </summary>
        internal void Reset()
        {
            for (int i = 0; i < layers.Length; i++)
            {
                layers[i].InitilizeWeights();
            }
        }
    }
}
