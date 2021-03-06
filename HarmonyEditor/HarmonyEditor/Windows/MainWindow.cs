﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PeriodicChords;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;
using System.Collections;
using Sanford.Multimedia.Midi;

namespace HarmonyEditor
{
    /// <summary>
    /// Main window of application.
    /// </summary>
    public partial class MainWindow : Form
    {
        private double _fMin = AppConfiguration.GetFreqMin(), _fMax = AppConfiguration.GetFreqMax(), 
            _nMin = AppConfiguration.GetNoteMin(), _nMax = AppConfiguration.GetNoteMax();
        private int _selectedIndex = -1;
        private void RefreshData()
        {
            foreach (DraggableComponent dc in flowLayoutPanel.Controls)
            {
                if (dc is Spectrum)
                {
                    Spectrum sp = dc as Spectrum;
                    sp.FreqNotes = radioButtonFrequencies.Checked;
                    sp.Width = (int)numericUpDownWidth.Value;
                    sp.Height = (int)numericUpDownHeight.Value;
                    sp.Invalidate();
                }
                else if (dc is EndPoint)
                {
                    EndPoint ep = dc as EndPoint;
                    ep.Width = (int)((int)numericUpDownWidth.Value * 0.5);
                    ep.Height = (int)numericUpDownHeight.Value;
                    ep.Invalidate();
                }
            }
            if (_selectedIndex != -1)
            {
                buttonRemoveAccord.Enabled = true;

                DraggableComponent dc = flowLayoutPanel.Controls[_selectedIndex] as DraggableComponent;
                if (dc is EndPoint)
                {
                    buttonRemoveAccord.Text = "Usuń punkt...";
                    buttonEditAccord.Enabled = false;
                }
                else if (dc is Spectrum)
                {
                    buttonRemoveAccord.Text = "Usuń akord...";
                    buttonEditAccord.Enabled = true;
                }
            }
            else
            {
                buttonRemoveAccord.Enabled = false;
                buttonEditAccord.Enabled = false;
            }
        }
        private void ResetData()
        {
            flowLayoutPanel.Controls.Clear();
            _selectedIndex = -1;
        }
        private void LoadSettings()
        {
            try
            {
                int boardWidth, boardHeight, tileWidth, tileHeight;
                bool freqNotes;

                string path = Registry.LocalMachine.Name + @"\SOFTWARE\Harmony-Simulation";

                boardWidth = (int)Registry.GetValue(path, "BoardWidth", null);
                boardHeight = (int)Registry.GetValue(path, "BoardHeight", null);
                tileWidth = (int)Registry.GetValue(path, "TileWidth", null);
                tileHeight = (int)Registry.GetValue(path, "TileHeight", null);
                freqNotes = (string)Registry.GetValue(path, "FreqNotes", null) == "true" ? true : false;

                this.Width = boardWidth;
                this.Height = boardHeight;
                numericUpDownWidth.Value = tileWidth;
                numericUpDownHeight.Value = tileHeight;

                if (!freqNotes)
                {
                    radioButtonFrequencies.Checked = false;
                    radioButtonNotes.Checked = true;
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        private void SaveSettings()
        {
            try
            {
                string path = Registry.LocalMachine.Name + @"\SOFTWARE\Harmony-Simulation";
                Registry.SetValue(path, "BoardWidth", (int)this.Width);
                Registry.SetValue(path, "BoardHeight", (int)this.Height);
                Registry.SetValue(path, "TileWidth", (int)numericUpDownWidth.Value);
                Registry.SetValue(path, "TileHeight", (int)numericUpDownHeight.Value);
                Registry.SetValue(path, "FreqNotes", radioButtonFrequencies.Checked ? "true" : "false");
            }
            catch (Exception)
            {
                return;
            }
        }
        private string GetSaveDirectory()
        {
            SaveFileDialog wnd = new SaveFileDialog();
            wnd.RestoreDirectory = true;

            DialogResult result = wnd.ShowDialog();
            if (result != DialogResult.OK)
            {
                return null;
            }

            return wnd.FileName;
        }
        private void AddSpectrum(Spectrum sp)
        {
            sp.Rotated = true;
            sp.MouseClick += component_MouseClick;
            sp.DoubleClick += component_DoubleClick;

            flowLayoutPanel.Controls.Add(sp);
        }
        private void AddEndPoint(EndPoint ep)
        {
            ep.MouseClick += component_MouseClick;
            ep.DoubleClick += component_DoubleClick;

            flowLayoutPanel.Controls.Add(ep);
        }
        private void EditCurrentItem()
        {
            DraggableComponent dc = flowLayoutPanel.Controls[_selectedIndex] as DraggableComponent;
            if (dc is Spectrum)
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
            }
            RefreshData();
        }
        private List<List<Chord>> ToChords()
        {
            List<List<Chord>> result = new List<List<Chord>>();
            result.Add(new List<Chord>());
            int index = 0;

            foreach (DraggableComponent dc in flowLayoutPanel.Controls)
            {
                if (dc is Spectrum)
                {
                    result[index].Add((dc as Spectrum).CurChord);
                }
                else if (dc is EndPoint)
                {
                    index++;
                    result.Add(new List<Chord>());
                }
            }
            return result;
        }

        public MainWindow()
        {
            InitializeComponent();
            LoadSettings();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            SaveSettings();
        }

        #region Events
        private void flowLayoutPanel_Click(object sender, EventArgs e)
        {
            if (_selectedIndex == -1)
            {
                return;
            }
            DraggableComponent dc = flowLayoutPanel.Controls[_selectedIndex] as DraggableComponent;
            dc.Selected = false;
            _selectedIndex = -1;
            RefreshData();
        }
        private void component_DoubleClick(object sender, EventArgs e)
        {
            DraggableComponent dc = sender as DraggableComponent;
            if (_selectedIndex == -1)
            {
                dc.Selected = true;
                _selectedIndex = flowLayoutPanel.Controls.IndexOf(dc);
            }
            else
            {
                DraggableComponent tmp = flowLayoutPanel.Controls[_selectedIndex] as DraggableComponent;
                tmp.Selected = false;
                _selectedIndex = flowLayoutPanel.Controls.IndexOf(dc);
                dc.Selected = true;
            }

            RefreshData();
            EditCurrentItem();
        }
        private void component_MouseClick(object sender, MouseEventArgs e)
        {
            DraggableComponent dc = sender as DraggableComponent;
            if (_selectedIndex == -1)
            {
                dc.Selected = true;
                _selectedIndex = flowLayoutPanel.Controls.IndexOf(dc);
            }
            else
            {
                DraggableComponent tmp = flowLayoutPanel.Controls[_selectedIndex] as DraggableComponent;
                tmp.Selected = false;
                _selectedIndex = flowLayoutPanel.Controls.IndexOf(dc);
                dc.Selected = true;
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
            AddSpectrum(sp);
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
            AddSpectrum(sp);
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
            EditCurrentItem();
        }
        private void flowLayoutPanel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void flowLayoutPanel_DragDrop(object sender, DragEventArgs e)
        {
            DraggableComponent data = null;
            data = e.Data.GetData(typeof(Spectrum)) as DraggableComponent;
            if (data == null)
                data = e.Data.GetData(typeof(EndPoint)) as DraggableComponent;

            Point p = flowLayoutPanel.PointToClient(new Point(e.X, e.Y));
            var item = flowLayoutPanel.GetChildAtPoint(p);
            int index = flowLayoutPanel.Controls.GetChildIndex(item, false);
            if (_selectedIndex != -1 && !data.Selected)
            {
                DraggableComponent tmp = flowLayoutPanel.Controls[_selectedIndex] as DraggableComponent;
                tmp.Selected = false;
            }
            flowLayoutPanel.Controls.SetChildIndex(data, index);
            _selectedIndex = index;
            data.Selected = true;
            RefreshData();
        }
        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // do zmiany
            OpenFileDialog wnd = new OpenFileDialog();
            wnd.RestoreDirectory = true;

            DialogResult result = wnd.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            ResetData();
            List<List<Chord>> data = Serialization.LoadResultsFromJson(wnd.FileName);
            foreach (List<Chord> list in data)
            {
                foreach (Chord ch in list)
                {
                    Spectrum sp = new Spectrum();
                    sp.CurChord = ch;

                    AddSpectrum(sp);
                }

                EndPoint ep = new EndPoint();
                AddEndPoint(ep);
            }
            RefreshData();
        }
        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = GetSaveDirectory();
            if (fileName != null)
                ToChords().SaveResultsToJson(fileName);
        }
        private void eksportujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = GetSaveDirectory();
            if (fileName != null)
                ToChords().WriteToPitch(fileName);
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            int instrument = 0;
            Program.outDevice = new OutputDevice(0);

            ChannelMessageBuilder builder = new ChannelMessageBuilder();

            builder.Command = ChannelCommand.ProgramChange;
            builder.MidiChannel = 0;
            builder.Data1 = instrument;
            builder.Data2 = 127;
            builder.Build();
            Program.outDevice.Send(builder.Result);
            builder.Command = ChannelCommand.ProgramChange;
            builder.MidiChannel = 1;
            builder.Data1 = instrument;
            builder.Data2 = 127;
            builder.Build();
            Program.outDevice.Send(builder.Result);
            builder.Command = ChannelCommand.ProgramChange;
            builder.MidiChannel = 2;
            builder.Data1 = instrument;
            builder.Data2 = 127;
            builder.Build();
            Program.outDevice.Send(builder.Result);
            builder.Command = ChannelCommand.ProgramChange;
            builder.MidiChannel = 3;
            builder.Data1 = instrument;
            builder.Data2 = 127;
            builder.Build();
            Program.outDevice.Send(builder.Result);

            builder.Command = ChannelCommand.Controller;
            builder.MidiChannel = 1;
            builder.Data1 = 101;
            builder.Data2 = 0;
            builder.Build();
            Program.outDevice.Send(builder.Result);
            builder.Command = ChannelCommand.Controller;
            builder.MidiChannel = 1;
            builder.Data1 = 100;
            builder.Data2 = 1;
            builder.Build();
            Program.outDevice.Send(builder.Result);
            builder.Command = ChannelCommand.Controller;
            builder.MidiChannel = 1;
            builder.Data1 = 6;
            builder.Data2 = 0x50;
            builder.Build();
            Program.outDevice.Send(builder.Result);
            builder.Command = ChannelCommand.Controller;
            builder.MidiChannel = 1;
            builder.Data1 = 38;
            builder.Data2 = 0x00;
            builder.Build();
            Program.outDevice.Send(builder.Result);

            builder.Command = ChannelCommand.Controller;
            builder.MidiChannel = 2;
            builder.Data1 = 101;
            builder.Data2 = 0;
            builder.Build();
            Program.outDevice.Send(builder.Result);
            builder.Command = ChannelCommand.Controller;
            builder.MidiChannel = 2;
            builder.Data1 = 100;
            builder.Data2 = 1;
            builder.Build();
            Program.outDevice.Send(builder.Result);
            builder.Command = ChannelCommand.Controller;
            builder.MidiChannel = 2;
            builder.Data1 = 6;
            builder.Data2 = 0x60;
            builder.Build();
            Program.outDevice.Send(builder.Result);
            builder.Command = ChannelCommand.Controller;
            builder.MidiChannel = 2;
            builder.Data1 = 38;
            builder.Data2 = 0x00;
            builder.Build();
            Program.outDevice.Send(builder.Result);

            builder.Command = ChannelCommand.Controller;
            builder.MidiChannel = 3;
            builder.Data1 = 101;
            builder.Data2 = 0;
            builder.Build();
            Program.outDevice.Send(builder.Result);
            builder.Command = ChannelCommand.Controller;
            builder.MidiChannel = 3;
            builder.Data1 = 100;
            builder.Data2 = 1;
            builder.Build();
            Program.outDevice.Send(builder.Result);
            builder.Command = ChannelCommand.Controller;
            builder.MidiChannel = 3;
            builder.Data1 = 6;
            builder.Data2 = 0x70;
            builder.Build();
            Program.outDevice.Send(builder.Result);
            builder.Command = ChannelCommand.Controller;
            builder.MidiChannel = 3;
            builder.Data1 = 38;
            builder.Data2 = 0x00;
            builder.Build();
            Program.outDevice.Send(builder.Result);

        }
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.outDevice.Close();
        }
        private void buttonAddEndPoint_Click(object sender, EventArgs e)
        {
            int width = (int)((int)numericUpDownWidth.Value * 0.5);
            int height = (int)numericUpDownHeight.Value;

            EndPoint ep = new EndPoint(width, height);
            AddEndPoint(ep);
        }
        #endregion
    }
}
