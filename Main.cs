using Neural_Network_XOR.Scripts;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Neural_Network_XOR
{
    public partial class Main : Form
    {
        NeuralNetwork neuralNetwork = new NeuralNetwork(new int[] { 3, 25, 25, 1 });
        const int trainIterationCount = 1000;

        private Thread trainingThread;
        private bool isTraining = false;

        public Main()
        {
            InitializeComponent();
            Result_TextBox.ScrollBars = ScrollBars.Vertical;
            Result_TextBox.ReadOnly = true;

            Train_ProgressBar.Hide();
            Train_ProgressBar.Minimum = 0;
            Train_ProgressBar.Maximum = trainIterationCount;
            Train_ProgressBar.Step = 1;

            // Get current build path and create new custom folder to save neural data
            string path = AppDomain.CurrentDomain.BaseDirectory + "Neural Network Models\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        // Train Neural Network and show results on result screen
        private void Train()
        {
            MethodInvoker prograss = new MethodInvoker(() => Train_ProgressBar.Value++);
            for (int i = 0; i < trainIterationCount; i++)
            {
                Train_ProgressBar.Invoke(prograss);

                neuralNetwork.FeedForward(new float[]{ 0f, 0f, 0f });
                neuralNetwork.BackProb(new float[] { 0 });

                neuralNetwork.FeedForward(new float[] { 0f, 0f, 1f });
                neuralNetwork.BackProb(new float[] { 1 });

                neuralNetwork.FeedForward(new float[] { 0f, 1f, 0f });
                neuralNetwork.BackProb(new float[] { 1 });

                neuralNetwork.FeedForward(new float[] { 0f, 1f, 1f });
                neuralNetwork.BackProb(new float[] { 0 });

                neuralNetwork.FeedForward(new float[] { 1f, 0f, 0f });
                neuralNetwork.BackProb(new float[] { 1 });

                neuralNetwork.FeedForward(new float[] { 1f, 0f, 1f });
                neuralNetwork.BackProb(new float[] { 0 });

                neuralNetwork.FeedForward(new float[] { 1f, 1f, 0f });
                neuralNetwork.BackProb(new float[] { 0 });

                neuralNetwork.FeedForward(new float[] { 1f, 1f, 1f });
                neuralNetwork.BackProb(new float[] { 1 });
            }

            isTraining = false;

            Train_ProgressBar.Invoke(new MethodInvoker(() => Train_ProgressBar.Hide()));

            Train_Button.Invoke(new MethodInvoker(() => Train_Button.Text = "Train"));

            Result_TextBox.Invoke(new MethodInvoker(()=> ShowMessage(GetNNResult())));
        }

        // Show Neural Network Output results on result screen
        private string GetNNResult()
        {

            var outputs = "Neural Outputs:";
            float output = neuralNetwork.FeedForward(new float[] { 0f, 0f, 0f })[0];
            outputs += $"\r\nInputs: [0, 0, 0] - Expeacted Output: 0 - Outputs: {Math.Round(output)} - Linear Output: {output}";
            output = neuralNetwork.FeedForward(new float[] { 0f, 0f, 1f })[0];
            outputs += $"\r\nInputs: [0, 0, 1] - Expeacted output: 1 - Outputs: {Math.Round(output)} - Linear Output: {output}";
            output = neuralNetwork.FeedForward(new float[] { 0f, 1f, 0f })[0];
            outputs += $"\r\nInputs: [0, 1, 0] - Expeacted output: 1 - Outputs: {Math.Round(output)} - Linear Output: {output}";
            output = neuralNetwork.FeedForward(new float[] { 0f, 1f, 1f })[0];
            outputs += $"\r\nInputs: [0, 1, 1] - Expeacted Output: 0 - Outputs: {Math.Round(output)} - Linear Output: {output}";
            output = neuralNetwork.FeedForward(new float[] { 1f, 0f, 0f })[0];
            outputs += $"\r\nInputs: [1, 0, 0] - Expeacted output: 1 - Outputs: {Math.Round(output)} - Linear Output: {output}";
            output = neuralNetwork.FeedForward(new float[] { 1f, 0f, 1f })[0];
            outputs += $"\r\nInputs: [1, 0, 1] - Expeacted Output: 0 - Outputs: {Math.Round(output)} - Linear Output: {output}";
            output = neuralNetwork.FeedForward(new float[] { 1f, 1f, 0f })[0];
            outputs += $"\r\nInputs: [1, 1, 0] - Expeacted Output: 0 - Outputs: {Math.Round(output)} - Linear Output: {output}";
            output = neuralNetwork.FeedForward(new float[] { 1f, 1f, 1f })[0];
            outputs += $"\r\nInputs: [1, 1, 1] - Expeacted output: 1 - Outputs: {Math.Round(output)} - Linear Output: {output}";

            return outputs;
        }

        //Show any message on result screen
        private void ShowMessage(String message)
        {
            Result_TextBox.Text += $"\r\n{message}\r\n";
            Result_TextBox.SelectionStart = Result_TextBox.Text.Length;
            Result_TextBox.ScrollToCaret();
        }


        #region Button Events
        private void Train_Button_Click(object sender, EventArgs e)
        {
            if (!isTraining) {
                ShowMessage("Training....");
                trainingThread = new Thread(new ThreadStart(Train));
                trainingThread.Start();
                Train_ProgressBar.Show();
                Train_ProgressBar.Value = 0;
                Train_Button.Text = "Cancel";
                isTraining = true;
            }
            else
            {
                ShowMessage("Training has been canceled.");
                trainingThread.Abort();
                Train_ProgressBar.Hide();
                Train_Button.Text = "Train";
                isTraining = false;
            }
        }

        private void Save_Button_Click(object sender, EventArgs e)
        {
            // Get current build path and create new custom folder
            var path = AppDomain.CurrentDomain.BaseDirectory + "Neural Network Models\\";
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
            // Create unique save file name
            DateTime date = DateTime.Now;
            var fileId = $"{ date.Day }{ date.Month }{ date.Year }{ date.Hour }{ date.Minute }{ date.Second }";
            var fileName = $@"NN Model_{ fileId }.txt";
            // Convert neural network to JSON format and save it. 
            var data = neuralNetwork.toJson();
            File.WriteAllText(path + fileName, data);
            ShowMessage($"File Saved at: [{ path }] With name: [{ fileName }]");
        }

        private void Load_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            // Set intial directory to default path
            string initialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Neural Network Models";
            ofd.InitialDirectory = initialDirectory;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.OpenFile() != null)
                {
                    try
                    {
                        string file = ofd.FileName;
                        string str = File.ReadAllText(file);

                        neuralNetwork = NeuralNetwork.fromJson(str);

                        ShowMessage("Loaded Neural Network Model.");

                        ShowMessage(GetNNResult());
                    }
                    catch (Exception ex)
                    {
                        // SHESH don't say anything, and yes i know :)
                        MessageBox.Show("Unknown Error!");
                    }
                }
            }
        }

        private void ResetNN_Button_Click(object sender, EventArgs e)
        {
            neuralNetwork.Reset();
            ShowMessage("Neural network has been resetted.");
        }
        private void ShowNNResult_Button_Click(object sender, EventArgs e)
        {
            ShowMessage(GetNNResult());
        }
        #endregion

    }
}
