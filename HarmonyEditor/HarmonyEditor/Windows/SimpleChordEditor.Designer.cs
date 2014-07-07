namespace HarmonyEditor
{
    partial class SimpleChordEditor
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
            this.textBoxChord = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.radioMidiMode = new System.Windows.Forms.RadioButton();
            this.radioCentMode = new System.Windows.Forms.RadioButton();
            this.radioHerzMode = new System.Windows.Forms.RadioButton();
            this.groupBoxSpectrum = new System.Windows.Forms.GroupBox();
            this.labelNotes = new System.Windows.Forms.Label();
            this.labelFrequencies = new System.Windows.Forms.Label();
            this.buttonCountSpectrum = new System.Windows.Forms.Button();
            this.spectrumNotes = new HarmonyEditor.Spectrum();
            this.spectrumFrequencies = new HarmonyEditor.Spectrum();
            this.groupBoxSpectrum.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxChord
            // 
            this.textBoxChord.Location = new System.Drawing.Point(12, 12);
            this.textBoxChord.Name = "textBoxChord";
            this.textBoxChord.Size = new System.Drawing.Size(511, 20);
            this.textBoxChord.TabIndex = 0;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(12, 137);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(105, 30);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Dodaj";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(421, 137);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(102, 30);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Anuluj";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // radioMidiMode
            // 
            this.radioMidiMode.AutoSize = true;
            this.radioMidiMode.Checked = true;
            this.radioMidiMode.Location = new System.Drawing.Point(12, 48);
            this.radioMidiMode.Name = "radioMidiMode";
            this.radioMidiMode.Size = new System.Drawing.Size(48, 17);
            this.radioMidiMode.TabIndex = 3;
            this.radioMidiMode.TabStop = true;
            this.radioMidiMode.Text = "MIDI";
            this.radioMidiMode.UseVisualStyleBackColor = true;
            // 
            // radioCentMode
            // 
            this.radioCentMode.AutoSize = true;
            this.radioCentMode.Location = new System.Drawing.Point(95, 48);
            this.radioCentMode.Name = "radioCentMode";
            this.radioCentMode.Size = new System.Drawing.Size(77, 17);
            this.radioCentMode.TabIndex = 4;
            this.radioCentMode.Text = "MIDI centy";
            this.radioCentMode.UseVisualStyleBackColor = true;
            // 
            // radioHerzMode
            // 
            this.radioHerzMode.AutoSize = true;
            this.radioHerzMode.Location = new System.Drawing.Point(202, 48);
            this.radioHerzMode.Name = "radioHerzMode";
            this.radioHerzMode.Size = new System.Drawing.Size(38, 17);
            this.radioHerzMode.TabIndex = 5;
            this.radioHerzMode.Text = "Hz";
            this.radioHerzMode.UseVisualStyleBackColor = true;
            // 
            // groupBoxSpectrum
            // 
            this.groupBoxSpectrum.Controls.Add(this.labelNotes);
            this.groupBoxSpectrum.Controls.Add(this.labelFrequencies);
            this.groupBoxSpectrum.Controls.Add(this.spectrumNotes);
            this.groupBoxSpectrum.Controls.Add(this.spectrumFrequencies);
            this.groupBoxSpectrum.Location = new System.Drawing.Point(246, 38);
            this.groupBoxSpectrum.Name = "groupBoxSpectrum";
            this.groupBoxSpectrum.Size = new System.Drawing.Size(277, 93);
            this.groupBoxSpectrum.TabIndex = 6;
            this.groupBoxSpectrum.TabStop = false;
            this.groupBoxSpectrum.Text = "Spektra";
            // 
            // labelNotes
            // 
            this.labelNotes.AutoSize = true;
            this.labelNotes.Location = new System.Drawing.Point(147, 16);
            this.labelNotes.Name = "labelNotes";
            this.labelNotes.Size = new System.Drawing.Size(32, 13);
            this.labelNotes.TabIndex = 3;
            this.labelNotes.Text = "Nuty:";
            // 
            // labelFrequencies
            // 
            this.labelFrequencies.AutoSize = true;
            this.labelFrequencies.Location = new System.Drawing.Point(6, 16);
            this.labelFrequencies.Name = "labelFrequencies";
            this.labelFrequencies.Size = new System.Drawing.Size(76, 13);
            this.labelFrequencies.TabIndex = 2;
            this.labelFrequencies.Text = "Częstotliwości:";
            // 
            // buttonCountSpectrum
            // 
            this.buttonCountSpectrum.Location = new System.Drawing.Point(148, 81);
            this.buttonCountSpectrum.Name = "buttonCountSpectrum";
            this.buttonCountSpectrum.Size = new System.Drawing.Size(92, 30);
            this.buttonCountSpectrum.TabIndex = 7;
            this.buttonCountSpectrum.Text = "Policz spektra";
            this.buttonCountSpectrum.UseVisualStyleBackColor = true;
            this.buttonCountSpectrum.Click += new System.EventHandler(this.buttonCountSpectrum_Click);
            // 
            // spectrumNotes
            // 
            this.spectrumNotes.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.spectrumNotes.CurChord = null;
            this.spectrumNotes.FreqNotes = false;
            this.spectrumNotes.Location = new System.Drawing.Point(150, 32);
            this.spectrumNotes.Name = "spectrumNotes";
            this.spectrumNotes.Rotated = false;
            //this.spectrumNotes.Selected = false;
            this.spectrumNotes.Size = new System.Drawing.Size(121, 53);
            this.spectrumNotes.TabIndex = 1;
            this.spectrumNotes.Text = "spectrum3";
            // 
            // spectrumFrequencies
            // 
            this.spectrumFrequencies.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.spectrumFrequencies.CurChord = null;
            this.spectrumFrequencies.FreqNotes = false;
            this.spectrumFrequencies.Location = new System.Drawing.Point(6, 32);
            this.spectrumFrequencies.Name = "spectrumFrequencies";
            this.spectrumFrequencies.Rotated = false;
            //this.spectrumFrequencies.Selected = false;
            this.spectrumFrequencies.Size = new System.Drawing.Size(121, 53);
            this.spectrumFrequencies.TabIndex = 0;
            this.spectrumFrequencies.Text = "spectrum2";
            // 
            // SimpleChordEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 175);
            this.Controls.Add(this.buttonCountSpectrum);
            this.Controls.Add(this.groupBoxSpectrum);
            this.Controls.Add(this.radioHerzMode);
            this.Controls.Add(this.radioCentMode);
            this.Controls.Add(this.radioMidiMode);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.textBoxChord);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "SimpleChordEditor";
            this.Text = "Dodaj Akord...";
            this.groupBoxSpectrum.ResumeLayout(false);
            this.groupBoxSpectrum.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxChord;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton radioMidiMode;
        private System.Windows.Forms.RadioButton radioCentMode;
        private System.Windows.Forms.RadioButton radioHerzMode;
        private System.Windows.Forms.GroupBox groupBoxSpectrum;
        private System.Windows.Forms.Label labelNotes;
        private System.Windows.Forms.Label labelFrequencies;
        private Spectrum spectrumNotes;
        private Spectrum spectrumFrequencies;
        private System.Windows.Forms.Button buttonCountSpectrum;
    }
}