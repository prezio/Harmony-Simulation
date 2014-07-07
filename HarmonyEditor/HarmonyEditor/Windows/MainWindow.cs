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
    public partial class MainWindow : Form
    {
        private double _fMin = AppConfiguration.GetFreqMin(), _fMax = AppConfiguration.GetFreqMax(), 
            _nMin = AppConfiguration.GetNoteMin(), _nMax = AppConfiguration.GetNoteMax();
        private int _selectedIndex = -1;
        private void RefreshData()
        {
            foreach (Spectrum sp in flowLayoutPanel.Controls)
            {
                sp.FreqNotes = radioButtonFrequencies.Checked;
                sp.Width = (int)numericUpDownWidth.Value;
                sp.Height = (int)numericUpDownHeight.Value;
                sp.Invalidate();
            }
            buttonRemoveAccord.Enabled = _selectedIndex != -1;
            buttonEditAccord.Enabled = _selectedIndex != -1;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Events
        private void flowLayoutPanel_Click(object sender, EventArgs e)
        {
            if (_selectedIndex == -1)
            {
                return;
            }
            Spectrum sp = (Spectrum)flowLayoutPanel.Controls[_selectedIndex];
            sp.Selected = false;
            _selectedIndex = -1;
            RefreshData();
        }
        private void spectrum_MouseClick(object sender, MouseEventArgs e)
        {
            Spectrum sp = (Spectrum) sender;
            if (_selectedIndex == -1)
            {
                sp.Selected = true;
                _selectedIndex = flowLayoutPanel.Controls.IndexOf(sp);
            }
            else
            {
                Spectrum tmp = (Spectrum)flowLayoutPanel.Controls[_selectedIndex];
                tmp.Selected = false;
                _selectedIndex = flowLayoutPanel.Controls.IndexOf(sp);
                sp.Selected = true;
            }
            RefreshData();
        }
        private void buttonAddSimpleAccord_Click(object sender, EventArgs e)
        {
            SimpleChordEditor window = new SimpleChordEditor();
            window.ShowDialog();

            if (!window.OkClicked)
            {
                return;
            }

            Spectrum sp = new Spectrum(true, radioButtonFrequencies.Checked, window.Result,
                                                         (int)numericUpDownWidth.Value, (int)numericUpDownHeight.Value);
            sp.MouseClick += spectrum_MouseClick;

            flowLayoutPanel.Controls.Add(sp);
        }

        private void buttonAddPeriodicAccord_Click(object sender, EventArgs e)
        {
            ChordEditor window = new ChordEditor();
            window.ShowDialog();

            if (!window.OkClicked)
            {
                return;
            }

            Spectrum sp = new Spectrum(true, radioButtonFrequencies.Checked, window.Result,
                                                        (int)numericUpDownWidth.Value, (int)numericUpDownHeight.Value);
            sp.MouseClick += spectrum_MouseClick;

            flowLayoutPanel.Controls.Add(sp);
        }
        private void radioButtonFrequencies_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButtonFrequencies.Checked)
            {
                return;
            }
            RefreshData();
        }
        private void radioButtonNotes_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButtonNotes.Checked)
            {
                return;
            }
            RefreshData();
        }
        private void numericUpDownHeight_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
        private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
        private void buttonRemoveAccord_Click(object sender, EventArgs e)
        {
            flowLayoutPanel.Controls.RemoveAt(_selectedIndex);
            _selectedIndex = -1;
            RefreshData();
        }
        private void buttonEditAccord_Click(object sender, EventArgs e)
        {
            Spectrum sp = (Spectrum)flowLayoutPanel.Controls[_selectedIndex];
            if (sp.CurChord is PeriodicChord)
            {
                PeriodicChord pc = (PeriodicChord)sp.CurChord;
                ChordEditor window = new ChordEditor(pc);
                window.ShowDialog();

                if (window.OkClicked)
                {
                    sp.CurChord = window.Result;
                }
            }
            else if (sp.CurChord is SimpleChord)
            {
                SimpleChord sc = (SimpleChord)sp.CurChord;
                SimpleChordEditor window = new SimpleChordEditor(sc);
                window.ShowDialog();

                if (window.OkClicked)
                {
                    sp.CurChord = window.Result;
                }
            }
            RefreshData();
        }
        private void flowLayoutPanel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void flowLayoutPanel_DragDrop(object sender, DragEventArgs e)
        {
            Spectrum data = (Spectrum)e.Data.GetData(typeof(Spectrum));
            Point p = flowLayoutPanel.PointToClient(new Point(e.X, e.Y));
            var item = flowLayoutPanel.GetChildAtPoint(p);
            int index = flowLayoutPanel.Controls.GetChildIndex(item, false);

            if (_selectedIndex != -1 && !data.Selected)
            {
                Spectrum tmp = (Spectrum)flowLayoutPanel.Controls[_selectedIndex];
                tmp.Selected = false;
            }

            flowLayoutPanel.Controls.SetChildIndex(data, index);
            _selectedIndex = index;
            data.Selected = true;
            RefreshData();
        }
        #endregion
    }
}
