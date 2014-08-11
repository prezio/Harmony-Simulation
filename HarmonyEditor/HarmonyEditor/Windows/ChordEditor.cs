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
using Sanford.Multimedia.Midi;

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
        private bool CountSpectrum()
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

            uint temp = 0;
            uint.TryParse(textBoxBaseSound.Text, out temp);
            chord.BaseNote = temp;

            try
            {
                int left, right;
                if (int.TryParse(textBoxLeft.Text, out left) == false)
                    throw new Exception();
                if (int.TryParse(textBoxRight.Text, out right) == false)
                    throw new Exception();

                chord.Left = left;
                chord.Right = right;
                chord.Periods = periods.ToArray();

                spectrumFrequencies.CurChord = chord;
                spectrumNotes.CurChord = chord;
                _chord = chord;
                spectrumFrequencies.Invalidate();
                spectrumNotes.Invalidate();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
       } 
        private void UploadChord(PeriodicChord chord)
        {
            textBoxBaseSound.Text = chord.BaseNote.ToString();
            textBoxLeft.Text = chord.Left.ToString();
            textBoxRight.Text = chord.Right.ToString();

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
        private void StopSounds()
        {
            Program.sequencer.Stop();
            ChannelMessageBuilder builder = new ChannelMessageBuilder();
            builder.Command = ChannelCommand.Controller;
            builder.MidiChannel = 0;
            builder.Data1 = 120;
            builder.Data2 = 0;
            builder.Build();

            Program.outDevice.Send(builder.Result);
            builder.Command = ChannelCommand.Controller;
            builder.MidiChannel = 1;
            builder.Data1 = 120;
            builder.Data2 = 0;
            builder.Build();

            Program.outDevice.Send(builder.Result);
            builder.Command = ChannelCommand.Controller;
            builder.MidiChannel = 2;
            builder.Data1 = 120;
            builder.Data2 = 0;
            builder.Build();

            Program.outDevice.Send(builder.Result);
            builder.Command = ChannelCommand.Controller;
            builder.MidiChannel = 3;
            builder.Data1 = 120;
            builder.Data2 = 0;
            builder.Build();

            Program.outDevice.Send(builder.Result);
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
            if (CountSpectrum() == false)
            {
                MessageBox.Show("Nie można dodać akordu");
                return;
            }
            _okClicked = true;
            Close();
        }
        private void buttonPlay_Click(object sender, EventArgs e)
        {
            CountSpectrum();
            StopSounds();
            ChannelMessageBuilder builder = new ChannelMessageBuilder();
            double[] notes = _chord.Notes;
            foreach(double note in notes)
            {
                builder.Command = ChannelCommand.NoteOn;
                builder.MidiChannel = (((int) note)%100)/25;
                builder.Data1 =(int) note/100;
                builder.Data2 = 100;
                builder.Build();

                Program.outDevice.Send(builder.Result);
            }

        }
        private void Arpeggio_Click(object sender, EventArgs e)
        {
            int channel = 0;
            int pitch = 0;
            CountSpectrum();
            StopSounds();
            Program.sequencer.Sequence = new Sequence();
            Track track = new Track();
            int i = 0;
            ChannelMessageBuilder builder = new ChannelMessageBuilder();
            double[] notes = _chord.Notes;
            foreach (double note in notes)
            {
                channel = (((int)note) % 100) / 25;
                pitch = (int)note / 100;
                builder.Command = ChannelCommand.NoteOn;
                builder.MidiChannel = channel;
                builder.Data1 = pitch;
                builder.Data2 = 120;
                builder.Build();
                track.Insert(i, builder.Result);
                i += 20;
                builder.Data2 = 0;
                builder.Build();
                track.Insert(i, builder.Result);
            }
            Program.sequencer.Sequence.Add(track);
            Program.sequencer.Start();
        }
        private void ChordEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopSounds();
        }
        private void Stop_Click(object sender, EventArgs e)
        {
            StopSounds();
        }
        #endregion
    }
}
