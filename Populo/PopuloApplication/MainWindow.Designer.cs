namespace PopuloApplication
{
    partial class MainWindow
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
            this.textBoxModifyAmount = new System.Windows.Forms.TextBox();
            this.labelParameters = new System.Windows.Forms.Label();
            this.textBoxInfluenceAmount = new System.Windows.Forms.TextBox();
            this.buttonStartSimulation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxModifyAmount
            // 
            this.textBoxModifyAmount.Location = new System.Drawing.Point(211, 12);
            this.textBoxModifyAmount.Name = "textBoxModifyAmount";
            this.textBoxModifyAmount.Size = new System.Drawing.Size(320, 20);
            this.textBoxModifyAmount.TabIndex = 0;
            // 
            // labelParameters
            // 
            this.labelParameters.AutoSize = true;
            this.labelParameters.Location = new System.Drawing.Point(12, 9);
            this.labelParameters.Name = "labelParameters";
            this.labelParameters.Size = new System.Drawing.Size(54, 13);
            this.labelParameters.TabIndex = 1;
            this.labelParameters.Text = "Parametry";
            // 
            // textBoxInfluenceAmount
            // 
            this.textBoxInfluenceAmount.Location = new System.Drawing.Point(225, 38);
            this.textBoxInfluenceAmount.Name = "textBoxInfluenceAmount";
            this.textBoxInfluenceAmount.Size = new System.Drawing.Size(100, 20);
            this.textBoxInfluenceAmount.TabIndex = 2;
            // 
            // buttonStartSimulation
            // 
            this.buttonStartSimulation.Location = new System.Drawing.Point(588, 162);
            this.buttonStartSimulation.Name = "buttonStartSimulation";
            this.buttonStartSimulation.Size = new System.Drawing.Size(98, 39);
            this.buttonStartSimulation.TabIndex = 3;
            this.buttonStartSimulation.Text = "Start Simulation";
            this.buttonStartSimulation.UseVisualStyleBackColor = true;
            this.buttonStartSimulation.Click += new System.EventHandler(this.buttonStartSimulation_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 411);
            this.Controls.Add(this.buttonStartSimulation);
            this.Controls.Add(this.textBoxInfluenceAmount);
            this.Controls.Add(this.labelParameters);
            this.Controls.Add(this.textBoxModifyAmount);
            this.Name = "MainWindow";
            this.Text = "Populo Simulator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxModifyAmount;
        private System.Windows.Forms.Label labelParameters;
        private System.Windows.Forms.TextBox textBoxInfluenceAmount;
        private System.Windows.Forms.Button buttonStartSimulation;
    }
}

