namespace HarmonyEditor
{
    partial class ChordEditor
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
            this.labelBaseSound = new System.Windows.Forms.Label();
            this.textBoxBaseSound = new System.Windows.Forms.TextBox();
            this.radioMidiMode = new System.Windows.Forms.RadioButton();
            this.radioCentMode = new System.Windows.Forms.RadioButton();
            this.radioHerzMode = new System.Windows.Forms.RadioButton();
            this.buttonCountSpectrum = new System.Windows.Forms.Button();
            this.labelFrequency = new System.Windows.Forms.Label();
            this.labelNotes = new System.Windows.Forms.Label();
            this.groupBoxSpectrum = new System.Windows.Forms.GroupBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.spectrumFrequencies = new HarmonyEditor.Spectrum();
            this.spectrumNotes = new HarmonyEditor.Spectrum();
            this.period6 = new HarmonyEditor.PeriodEditor();
            this.period5 = new HarmonyEditor.PeriodEditor();
            this.period4 = new HarmonyEditor.PeriodEditor();
            this.period3 = new HarmonyEditor.PeriodEditor();
            this.period2 = new HarmonyEditor.PeriodEditor();
            this.period1 = new HarmonyEditor.PeriodEditor();
            this.groupBoxSpectrum.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelBaseSound
            // 
            this.labelBaseSound.AutoSize = true;
            this.labelBaseSound.Location = new System.Drawing.Point(36, 60);
            this.labelBaseSound.Name = "labelBaseSound";
            this.labelBaseSound.Size = new System.Drawing.Size(84, 13);
            this.labelBaseSound.TabIndex = 0;
            this.labelBaseSound.Text = "Dźwięk bazowy:";
            // 
            // textBoxBaseSound
            // 
            this.textBoxBaseSound.Location = new System.Drawing.Point(126, 57);
            this.textBoxBaseSound.Name = "textBoxBaseSound";
            this.textBoxBaseSound.Size = new System.Drawing.Size(49, 20);
            this.textBoxBaseSound.TabIndex = 1;
            // 
            // radioMidiMode
            // 
            this.radioMidiMode.AutoSize = true;
            this.radioMidiMode.Checked = true;
            this.radioMidiMode.Location = new System.Drawing.Point(875, 18);
            this.radioMidiMode.Name = "radioMidiMode";
            this.radioMidiMode.Size = new System.Drawing.Size(48, 17);
            this.radioMidiMode.TabIndex = 10;
            this.radioMidiMode.TabStop = true;
            this.radioMidiMode.Text = "MIDI";
            this.radioMidiMode.UseVisualStyleBackColor = true;
            this.radioMidiMode.CheckedChanged += new System.EventHandler(this.radioMidiMode_CheckedChanged);
            // 
            // radioCentMode
            // 
            this.radioCentMode.AutoSize = true;
            this.radioCentMode.Location = new System.Drawing.Point(875, 41);
            this.radioCentMode.Name = "radioCentMode";
            this.radioCentMode.Size = new System.Drawing.Size(77, 17);
            this.radioCentMode.TabIndex = 11;
            this.radioCentMode.Text = "MIDI centy";
            this.radioCentMode.UseVisualStyleBackColor = true;
            this.radioCentMode.CheckedChanged += new System.EventHandler(this.radioCentMode_CheckedChanged);
            // 
            // radioHerzMode
            // 
            this.radioHerzMode.AutoSize = true;
            this.radioHerzMode.Location = new System.Drawing.Point(875, 64);
            this.radioHerzMode.Name = "radioHerzMode";
            this.radioHerzMode.Size = new System.Drawing.Size(38, 17);
            this.radioHerzMode.TabIndex = 12;
            this.radioHerzMode.Text = "Hz";
            this.radioHerzMode.UseVisualStyleBackColor = true;
            this.radioHerzMode.CheckedChanged += new System.EventHandler(this.radioHerzMode_CheckedChanged);
            // 
            // buttonCountSpectrum
            // 
            this.buttonCountSpectrum.Location = new System.Drawing.Point(586, 49);
            this.buttonCountSpectrum.Name = "buttonCountSpectrum";
            this.buttonCountSpectrum.Size = new System.Drawing.Size(75, 35);
            this.buttonCountSpectrum.TabIndex = 13;
            this.buttonCountSpectrum.Text = "Policz Spektra";
            this.buttonCountSpectrum.UseVisualStyleBackColor = true;
            this.buttonCountSpectrum.Click += new System.EventHandler(this.buttonCountSpectrum_Click);
            // 
            // labelFrequency
            // 
            this.labelFrequency.AutoSize = true;
            this.labelFrequency.Location = new System.Drawing.Point(6, 23);
            this.labelFrequency.Name = "labelFrequency";
            this.labelFrequency.Size = new System.Drawing.Size(74, 13);
            this.labelFrequency.TabIndex = 14;
            this.labelFrequency.Text = "Częstotliwość:";
            // 
            // labelNotes
            // 
            this.labelNotes.AutoSize = true;
            this.labelNotes.Location = new System.Drawing.Point(171, 23);
            this.labelNotes.Name = "labelNotes";
            this.labelNotes.Size = new System.Drawing.Size(32, 13);
            this.labelNotes.TabIndex = 15;
            this.labelNotes.Text = "Nuty:";
            // 
            // groupBoxSpectrum
            // 
            this.groupBoxSpectrum.Controls.Add(this.labelFrequency);
            this.groupBoxSpectrum.Controls.Add(this.labelNotes);
            this.groupBoxSpectrum.Controls.Add(this.spectrumFrequencies);
            this.groupBoxSpectrum.Controls.Add(this.spectrumNotes);
            this.groupBoxSpectrum.Location = new System.Drawing.Point(237, 18);
            this.groupBoxSpectrum.Name = "groupBoxSpectrum";
            this.groupBoxSpectrum.Size = new System.Drawing.Size(343, 88);
            this.groupBoxSpectrum.TabIndex = 16;
            this.groupBoxSpectrum.TabStop = false;
            this.groupBoxSpectrum.Text = "Spektra";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(12, 540);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(108, 30);
            this.buttonAdd.TabIndex = 17;
            this.buttonAdd.Text = "Dodaj";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(836, 540);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(116, 30);
            this.buttonCancel.TabIndex = 18;
            this.buttonCancel.Text = "Anuluj";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Location = new System.Drawing.Point(667, 49);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(75, 35);
            this.buttonPlay.TabIndex = 19;
            this.buttonPlay.Text = "Odtwórz akord";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(748, 49);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(75, 35);
            this.Stop.TabIndex = 20;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // spectrumFrequencies
            // 
            this.spectrumFrequencies.AllowDrag = false;
            this.spectrumFrequencies.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.spectrumFrequencies.CurChord = null;
            this.spectrumFrequencies.FreqNotes = false;
            this.spectrumFrequencies.Location = new System.Drawing.Point(6, 39);
            this.spectrumFrequencies.Name = "spectrumFrequencies";
            this.spectrumFrequencies.Rotated = false;
            this.spectrumFrequencies.Selected = false;
            this.spectrumFrequencies.Size = new System.Drawing.Size(162, 43);
            this.spectrumFrequencies.TabIndex = 8;
            this.spectrumFrequencies.Text = "spectrum1";
            // 
            // spectrumNotes
            // 
            this.spectrumNotes.AllowDrag = false;
            this.spectrumNotes.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.spectrumNotes.CurChord = null;
            this.spectrumNotes.FreqNotes = false;
            this.spectrumNotes.Location = new System.Drawing.Point(174, 39);
            this.spectrumNotes.Name = "spectrumNotes";
            this.spectrumNotes.Rotated = false;
            this.spectrumNotes.Selected = false;
            this.spectrumNotes.Size = new System.Drawing.Size(162, 43);
            this.spectrumNotes.TabIndex = 9;
            this.spectrumNotes.Text = "spectrum2";
            // 
            // period6
            // 
            this.period6.Enabled = false;
            this.period6.Location = new System.Drawing.Point(797, 266);
            this.period6.Name = "period6";
            this.period6.Size = new System.Drawing.Size(151, 255);
            this.period6.TabIndex = 7;
            // 
            // period5
            // 
            this.period5.Enabled = false;
            this.period5.Location = new System.Drawing.Point(640, 266);
            this.period5.Name = "period5";
            this.period5.Size = new System.Drawing.Size(151, 255);
            this.period5.TabIndex = 6;
            // 
            // period4
            // 
            this.period4.Enabled = false;
            this.period4.Location = new System.Drawing.Point(483, 266);
            this.period4.Name = "period4";
            this.period4.Size = new System.Drawing.Size(151, 255);
            this.period4.TabIndex = 5;
            // 
            // period3
            // 
            this.period3.Enabled = false;
            this.period3.Location = new System.Drawing.Point(326, 266);
            this.period3.Name = "period3";
            this.period3.Size = new System.Drawing.Size(151, 255);
            this.period3.TabIndex = 4;
            // 
            // period2
            // 
            this.period2.Enabled = false;
            this.period2.Location = new System.Drawing.Point(169, 266);
            this.period2.Name = "period2";
            this.period2.Size = new System.Drawing.Size(151, 255);
            this.period2.TabIndex = 3;
            // 
            // period1
            // 
            this.period1.Location = new System.Drawing.Point(12, 266);
            this.period1.Name = "period1";
            this.period1.Size = new System.Drawing.Size(151, 255);
            this.period1.TabIndex = 2;
            // 
            // ChordEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 582);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.groupBoxSpectrum);
            this.Controls.Add(this.buttonCountSpectrum);
            this.Controls.Add(this.radioHerzMode);
            this.Controls.Add(this.radioCentMode);
            this.Controls.Add(this.radioMidiMode);
            this.Controls.Add(this.period6);
            this.Controls.Add(this.period5);
            this.Controls.Add(this.period4);
            this.Controls.Add(this.period3);
            this.Controls.Add(this.period2);
            this.Controls.Add(this.period1);
            this.Controls.Add(this.textBoxBaseSound);
            this.Controls.Add(this.labelBaseSound);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "ChordEditor";
            this.Text = "Dodaj Akord...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChordEditor_FormClosing);
            this.groupBoxSpectrum.ResumeLayout(false);
            this.groupBoxSpectrum.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBaseSound;
        private System.Windows.Forms.TextBox textBoxBaseSound;
        private PeriodEditor period1;
        private PeriodEditor period2;
        private PeriodEditor period3;
        private PeriodEditor period4;
        private PeriodEditor period5;
        private PeriodEditor period6;
        private Spectrum spectrumFrequencies;
        private Spectrum spectrumNotes;
        private System.Windows.Forms.RadioButton radioMidiMode;
        private System.Windows.Forms.RadioButton radioCentMode;
        private System.Windows.Forms.RadioButton radioHerzMode;
        private System.Windows.Forms.Button buttonCountSpectrum;
        private System.Windows.Forms.Label labelFrequency;
        private System.Windows.Forms.Label labelNotes;
        private System.Windows.Forms.GroupBox groupBoxSpectrum;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button Stop;
    }
}