using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PeriodicChords;

namespace HarmonyEditor
{
    public partial class SimpleChordEditor : Form
    {
        private bool _okClicked;
        private double[] Peaks
        {
            get
            {
                try
                {
                    return textBoxChord.Text.
                        Trim().TrimEnd(new char[] { ';' }).
                            Split(new char[] { ';' }).Select(StringToDouble).ToArray();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        private SimpleChord _chord;

        public SimpleChord Result
        {
            get
            {
                return _chord;
            }
        }
        public SimpleChordEditor()
        {
            InitializeComponent();
            spectrumFrequencies.FreqNotes = true;
            spectrumNotes.FreqNotes = false;
        }
        public SimpleChordEditor(SimpleChord chord)
        {
            InitializeComponent();
            spectrumFrequencies.FreqNotes = true;
            spectrumNotes.FreqNotes = false;
            textBoxChord.Text = chord.Peaks.Select(a => a.ToString()).Aggregate((s, s1) => s + "; " + s1);
            buttonAdd.Text = "Zmień";
            this.Text = "Zmień Akord...";
        }
        public bool OkClicked
        {
            get
            {
                return _okClicked;
            }
        }

        #region Events
        private double StringToDouble(string a)
        {
            double result;
            double.TryParse(a, out result);
            return result;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            double[] peaks = Peaks;

            if (peaks == null)
            {
                MessageBox.Show("Nie można załadować akordu.");
                return;
            }

            SimpleChord chord;
            if (radioCentMode.Checked)
                chord = new MidiCentSimpleChord();
            else if (radioHerzMode.Checked)
                chord = new HerzSimpleChord();
            else
                chord = new MidiSimpleChord();

            chord.Peaks = peaks;
            _chord = chord;
            _okClicked = true;

            Close();
        }
        private void buttonCountSpectrum_Click(object sender, EventArgs e)
        {
            double[] peaks = Peaks;
            if (peaks == null)
            {
                MessageBox.Show("Nie można załadować akordu.");
                return;
            }

            SimpleChord chord;
            if (radioCentMode.Checked)
                chord = new MidiCentSimpleChord();
            else if (radioHerzMode.Checked)
                chord = new HerzSimpleChord();
            else
                chord = new MidiSimpleChord();

            chord.Peaks = peaks;
            spectrumFrequencies.CurChord = chord;
            spectrumNotes.CurChord = chord;

            spectrumFrequencies.Invalidate();
            spectrumNotes.Invalidate();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            _okClicked = false;
            Close();
        }
        #endregion
    }
}
