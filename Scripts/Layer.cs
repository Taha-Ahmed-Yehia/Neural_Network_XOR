using System;

namespace Neural_Network_XOR.Scripts
{
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
        /// <summary>
        /// The derivative of Tanh value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public float TanhDerivative(float value)
        {
            return 1f - (value * value);
        }

        /// <summary>
        /// Perform backpropagation for output layer with expected values to calculate erros and adjust delta weights.
        /// </summary>
        /// <param name="expected">The correct values the network should output.</param>
        public void BackPropOutput(float[] expected)
        {
            for (int i = 0; i < numberOfOutputs; i++)
            {
                error[i] = outputs[i] - expected[i];
            }

            for (int i = 0; i < numberOfOutputs; i++)
            {
                gamma[i] = error[i] * TanhDerivative(outputs[i]);
            }

            for (int i = 0; i < numberOfOutputs; i++)
            {
                for (int j = 0; j < numberOfInputs; j++)
                {
                    weightsDelta[i, j] = gamma[i] * inputs[j];
                }
            }

        }
        /// <summary>
        /// Perform backpropagation for the hidden layer with expected values to calculate erros and adjust delta weights.
        /// </summary>
        /// <param name="gammaForward"></param>
        /// <param name="weightsForward"></param>
        public void BackPropHidden(float[] gammaForward, float[,] weightsForward)
        {
            for (int i = 0; i < numberOfOutputs; i++)
            {
                gamma[i] = 0;
                for (int j = 0; j < gammaForward.Length; j++)
                {
                    gamma[i] += gammaForward[j] * weightsForward[j, i];
                }
                gamma[i] *= TanhDerivative(outputs[i]);
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
