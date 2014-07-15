using PeriodicChords;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HarmonyEditor
{
    public partial class ChordEditor : Form
    {
        private bool _okClicked;
        private void InitData()
        {
            period1.WasValidated += period1_validated;
            period2.WasValidated += period2_validated;
            period3.WasValidated += period3_validated;
            period4.WasValidated += period4_validated;
            period5.WasValidated += period5_validated;
            spectrumFrequencies.FreqNotes = true;
            spectrumNotes.FreqNotes = false;
        }
        private void ClearData()
        {
            if (Default == true)
                return;
            DialogResult dialogResult = MessageBox.Show("Czy chcesz usunąć wszystkie dane przed zmianą trybu?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }
            textBoxBaseSound.Text = "";
            period1.Clear();
            period2.Clear();
            period3.Clear();
            period4.Clear();
            period5.Clear();
            period6.Clear();
        }
        private void CountSpectrum()
        {
            PeriodicChord chord = null;

            if (radioMidiMode.Checked)
            {
                chord = new MidiPeriodicChord();
            }
            else if (radioCentMode.Checked)
            {
                chord = new MidiCentPeriodicChord();
            }
            else
            {
                chord = new HerzPeriodicChord();
            }

            List<Period> periods = new List<Period>();
            if (period1.Valid)
                periods.Add(period1.Value);
            if (period2.Valid)
                periods.Add(period2.Value);
            if (period3.Valid)
                periods.Add(period3.Value);
            if (period4.Valid)
                periods.Add(period4.Value);
            if (period5.Valid)
                periods.Add(period5.Value);
            if (period6.Valid)
                periods.Add(period6.Value);

            chord.Periods = periods.ToArray();
            uint temp = 0;
            uint.TryParse(textBoxBaseSound.Text, out temp);
            chord.BaseNote = temp;

            try
            {
                spectrumFrequencies.CurChord = chord;
                spectrumNotes.CurChord = chord;
                _chord = chord;
                spectrumFrequencies.Invalidate();
                spectrumNotes.Invalidate();
            }
            catch (SoundOutOfRangeException)
            {
                MessageBox.Show("Podane dźwięki nie mieszczą się w zakresie obsługiwanym przez program.");
            }
        }
        private void UploadChord(PeriodicChord chord)
        {
            textBoxBaseSound.Text = chord.BaseNote.ToString();
            if (chord.Periods.Length >= 1)
            {
                Period p = chord.Periods[0];
                period1.Value = p;
            }
            if (chord.Periods.Length >= 2)
            {
                Period p = chord.Periods[1];
                period2.Value = p;
            }
            if (chord.Periods.Length >= 3)
            {
                Period p = chord.Periods[2];
                period3.Value = p;
            }
            if (chord.Periods.Length >= 4)
            {
                Period p = chord.Periods[3];
                period4.Value = p;
            }
            if (chord.Periods.Length >= 5)
            {
                Period p = chord.Periods[4];
                period5.Value = p;
            }
            if (chord.Periods.Length >= 6)
            {
                Period p = chord.Periods[5];
                period6.Value = p;
            }
        }
        private PeriodicChord _chord;

        public PeriodicChord Result
        {
            get
            {
                return _chord;
            }
        }
        public ChordEditor()
        {
            InitializeComponent();
            InitData();
        }
        public ChordEditor(PeriodicChord chord)
        {
            InitializeComponent();
            InitData();
            UploadChord(chord);
            buttonAdd.Text = "Zmień";
            this.Text = "Zmień Akord...";
        }
        public bool Default
        {
            get
            {
                return string.IsNullOrEmpty(textBoxBaseSound.Text) && period1.Default && period2.Default
                    && period3.Default && period4.Default && period5.Default && period6.Default;
            }
        }
        public bool OkClicked
        {
            get
            {
                return _okClicked;
            }
        }

        #region Events
        private void period1_validated(object sender, EventArgs e)
        {
            period2.Enabled = period1.Valid;
        }
        private void period2_validated(object sender, EventArgs e)
        {
            period3.Enabled = period2.Valid;
        }
        private void period3_validated(object sender, EventArgs e)
        {
            period4.Enabled = period3.Valid;
        }
        private void period4_validated(object sender, EventArgs e)
        {
            period5.Enabled = period4.Valid;
        }
        private void period5_validated(object sender, EventArgs e)
        {
            period6.Enabled = period5.Valid;
        }
        private void radioMidiMode_CheckedChanged(object sender, EventArgs e)
        {
            if (radioMidiMode.Checked)
            {
                ClearData();
            }
        }
        private void radioCentMode_CheckedChanged(object sender, EventArgs e)
        {
            if (radioCentMode.Checked)
            {
                ClearData();
            }
        }
        private void radioHerzMode_CheckedChanged(object sender, EventArgs e)
        {
            if (radioHerzMode.Checked)
            {
                ClearData();
            }
        }
        private void buttonCountSpectrum_Click(object sender, EventArgs e)
        {
            CountSpectrum();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            _okClicked = false;
            Close();
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            CountSpectrum();
            _okClicked = true;
            Close();
        }
        private void buttonPlay_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
