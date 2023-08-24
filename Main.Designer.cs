
namespace Neural_Network_XOR
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.Train_ProgressBar = new System.Windows.Forms.ProgressBar();
            this.Train_Button = new System.Windows.Forms.Button();
            this.ResetNN_Button = new System.Windows.Forms.Button();
            this.Save_Button = new System.Windows.Forms.Button();
            this.Load_Button = new System.Windows.Forms.Button();
            this.Result_TextBox = new System.Windows.Forms.TextBox();
            this.ShowNNResult_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Train_ProgressBar
            // 
            resources.ApplyResources(this.Train_ProgressBar, "Train_ProgressBar");
            this.Train_ProgressBar.Name = "Train_ProgressBar";
            // 
            // Train_Button
            // 
            this.Train_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(255)))), ((int)(((byte)(217)))));
            resources.ApplyResources(this.Train_Button, "Train_Button");
            this.Train_Button.Name = "Train_Button";
            this.Train_Button.UseVisualStyleBackColor = false;
            this.Train_Button.Click += new System.EventHandler(this.Train_Button_Click);
            // 
            // ResetNN_Button
            // 
            this.ResetNN_Button.BackColor = System.Drawing.Color.MistyRose;
            resources.ApplyResources(this.ResetNN_Button, "ResetNN_Button");
            this.ResetNN_Button.Name = "ResetNN_Button";
            this.ResetNN_Button.UseVisualStyleBackColor = false;
            this.ResetNN_Button.Click += new System.EventHandler(this.ResetNN_Button_Click);
            // 
            // Save_Button
            // 
            this.Save_Button.BackColor = System.Drawing.Color.LemonChiffon;
            resources.ApplyResources(this.Save_Button, "Save_Button");
            this.Save_Button.Name = "Save_Button";
            this.Save_Button.UseVisualStyleBackColor = false;
            this.Save_Button.Click += new System.EventHandler(this.Save_Button_Click);
            // 
            // Load_Button
            // 
            this.Load_Button.BackColor = System.Drawing.Color.LemonChiffon;
            resources.ApplyResources(this.Load_Button, "Load_Button");
            this.Load_Button.Name = "Load_Button";
            this.Load_Button.UseVisualStyleBackColor = false;
            this.Load_Button.Click += new System.EventHandler(this.Load_Button_Click);
            // 
            // Result_TextBox
            // 
            resources.ApplyResources(this.Result_TextBox, "Result_TextBox");
            this.Result_TextBox.Name = "Result_TextBox";
            // 
            // ShowNNResult_Button
            // 
            this.ShowNNResult_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(248)))), ((int)(((byte)(254)))));
            resources.ApplyResources(this.ShowNNResult_Button, "ShowNNResult_Button");
            this.ShowNNResult_Button.Name = "ShowNNResult_Button";
            this.ShowNNResult_Button.UseVisualStyleBackColor = false;
            this.ShowNNResult_Button.Click += new System.EventHandler(this.ShowNNResult_Button_Click);
            // 
            // Main
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.ShowNNResult_Button);
            this.Controls.Add(this.Result_TextBox);
            this.Controls.Add(this.Save_Button);
            this.Controls.Add(this.ResetNN_Button);
            this.Controls.Add(this.Load_Button);
            this.Controls.Add(this.Train_Button);
            this.Controls.Add(this.Train_ProgressBar);
            this.Name = "Main";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar Train_ProgressBar;
        private System.Windows.Forms.Button Train_Button;
        private System.Windows.Forms.Button ResetNN_Button;
        private System.Windows.Forms.Button Save_Button;
        private System.Windows.Forms.Button Load_Button;
        private System.Windows.Forms.TextBox Result_TextBox;
        private System.Windows.Forms.Button ShowNNResult_Button;
    }
}

