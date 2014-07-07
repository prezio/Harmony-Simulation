namespace HarmonyEditor
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
            this.buttonAddSimpleAccord = new System.Windows.Forms.Button();
            this.buttonAddPeriodicAccord = new System.Windows.Forms.Button();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonFrequencies = new System.Windows.Forms.RadioButton();
            this.radioButtonNotes = new System.Windows.Forms.RadioButton();
            this.numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHeight = new System.Windows.Forms.NumericUpDown();
            this.labelHeight = new System.Windows.Forms.Label();
            this.labelWidth = new System.Windows.Forms.Label();
            this.buttonRemoveAccord = new System.Windows.Forms.Button();
            this.buttonEditAccord = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonAddSimpleAccord
            // 
            this.buttonAddSimpleAccord.Location = new System.Drawing.Point(12, 12);
            this.buttonAddSimpleAccord.Name = "buttonAddSimpleAccord";
            this.buttonAddSimpleAccord.Size = new System.Drawing.Size(117, 38);
            this.buttonAddSimpleAccord.TabIndex = 0;
            this.buttonAddSimpleAccord.Text = "Dodaj prosty akord";
            this.buttonAddSimpleAccord.UseVisualStyleBackColor = true;
            this.buttonAddSimpleAccord.Click += new System.EventHandler(this.buttonAddSimpleAccord_Click);
            // 
            // buttonAddPeriodicAccord
            // 
            this.buttonAddPeriodicAccord.Location = new System.Drawing.Point(135, 12);
            this.buttonAddPeriodicAccord.Name = "buttonAddPeriodicAccord";
            this.buttonAddPeriodicAccord.Size = new System.Drawing.Size(134, 38);
            this.buttonAddPeriodicAccord.TabIndex = 1;
            this.buttonAddPeriodicAccord.Text = "Dodaj okresowy akord";
            this.buttonAddPeriodicAccord.UseVisualStyleBackColor = true;
            this.buttonAddPeriodicAccord.Click += new System.EventHandler(this.buttonAddPeriodicAccord_Click);
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AllowDrop = true;
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.BackColor = System.Drawing.SystemColors.Info;
            this.flowLayoutPanel.Location = new System.Drawing.Point(12, 100);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(845, 616);
            this.flowLayoutPanel.TabIndex = 2;
            this.flowLayoutPanel.Click += new System.EventHandler(this.flowLayoutPanel_Click);
            this.flowLayoutPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowLayoutPanel_DragDrop);
            this.flowLayoutPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.flowLayoutPanel_DragEnter);
            // 
            // radioButtonFrequencies
            // 
            this.radioButtonFrequencies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonFrequencies.AutoSize = true;
            this.radioButtonFrequencies.Checked = true;
            this.radioButtonFrequencies.Location = new System.Drawing.Point(711, 67);
            this.radioButtonFrequencies.Name = "radioButtonFrequencies";
            this.radioButtonFrequencies.Size = new System.Drawing.Size(91, 17);
            this.radioButtonFrequencies.TabIndex = 3;
            this.radioButtonFrequencies.TabStop = true;
            this.radioButtonFrequencies.Text = "Częstotliwości";
            this.radioButtonFrequencies.UseVisualStyleBackColor = true;
            this.radioButtonFrequencies.CheckedChanged += new System.EventHandler(this.radioButtonFrequencies_CheckedChanged);
            // 
            // radioButtonNotes
            // 
            this.radioButtonNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonNotes.AutoSize = true;
            this.radioButtonNotes.Location = new System.Drawing.Point(808, 67);
            this.radioButtonNotes.Name = "radioButtonNotes";
            this.radioButtonNotes.Size = new System.Drawing.Size(47, 17);
            this.radioButtonNotes.TabIndex = 4;
            this.radioButtonNotes.Text = "Nuty";
            this.radioButtonNotes.UseVisualStyleBackColor = true;
            this.radioButtonNotes.CheckedChanged += new System.EventHandler(this.radioButtonNotes_CheckedChanged);
            // 
            // numericUpDownWidth
            // 
            this.numericUpDownWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownWidth.Location = new System.Drawing.Point(808, 23);
            this.numericUpDownWidth.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDownWidth.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownWidth.TabIndex = 5;
            this.numericUpDownWidth.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.numericUpDownWidth.ValueChanged += new System.EventHandler(this.numericUpDownWidth_ValueChanged);
            // 
            // numericUpDownHeight
            // 
            this.numericUpDownHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownHeight.Location = new System.Drawing.Point(687, 23);
            this.numericUpDownHeight.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownHeight.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownHeight.Name = "numericUpDownHeight";
            this.numericUpDownHeight.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownHeight.TabIndex = 6;
            this.numericUpDownHeight.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numericUpDownHeight.ValueChanged += new System.EventHandler(this.numericUpDownHeight_ValueChanged);
            // 
            // labelHeight
            // 
            this.labelHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(621, 25);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(60, 13);
            this.labelHeight.TabIndex = 7;
            this.labelHeight.Text = "Wysokość:";
            // 
            // labelWidth
            // 
            this.labelWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(742, 25);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(60, 13);
            this.labelWidth.TabIndex = 8;
            this.labelWidth.Text = "Szerokość:";
            // 
            // buttonRemoveAccord
            // 
            this.buttonRemoveAccord.Enabled = false;
            this.buttonRemoveAccord.Location = new System.Drawing.Point(12, 56);
            this.buttonRemoveAccord.Name = "buttonRemoveAccord";
            this.buttonRemoveAccord.Size = new System.Drawing.Size(117, 38);
            this.buttonRemoveAccord.TabIndex = 9;
            this.buttonRemoveAccord.Text = "Usuń akord";
            this.buttonRemoveAccord.UseVisualStyleBackColor = true;
            this.buttonRemoveAccord.Click += new System.EventHandler(this.buttonRemoveAccord_Click);
            // 
            // buttonEditAccord
            // 
            this.buttonEditAccord.Enabled = false;
            this.buttonEditAccord.Location = new System.Drawing.Point(135, 56);
            this.buttonEditAccord.Name = "buttonEditAccord";
            this.buttonEditAccord.Size = new System.Drawing.Size(134, 38);
            this.buttonEditAccord.TabIndex = 10;
            this.buttonEditAccord.Text = "Edytuj akord...";
            this.buttonEditAccord.UseVisualStyleBackColor = true;
            this.buttonEditAccord.Click += new System.EventHandler(this.buttonEditAccord_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 728);
            this.Controls.Add(this.buttonEditAccord);
            this.Controls.Add(this.buttonRemoveAccord);
            this.Controls.Add(this.numericUpDownWidth);
            this.Controls.Add(this.labelWidth);
            this.Controls.Add(this.labelHeight);
            this.Controls.Add(this.numericUpDownHeight);
            this.Controls.Add(this.radioButtonNotes);
            this.Controls.Add(this.radioButtonFrequencies);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.buttonAddPeriodicAccord);
            this.Controls.Add(this.buttonAddSimpleAccord);
            this.Name = "MainWindow";
            this.Text = "Generator Muzyki";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAddSimpleAccord;
        private System.Windows.Forms.Button buttonAddPeriodicAccord;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.RadioButton radioButtonFrequencies;
        private System.Windows.Forms.RadioButton radioButtonNotes;
        private System.Windows.Forms.NumericUpDown numericUpDownWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownHeight;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.Button buttonRemoveAccord;
        private System.Windows.Forms.Button buttonEditAccord;

    }
}

