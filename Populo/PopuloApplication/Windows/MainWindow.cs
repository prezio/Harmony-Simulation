using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MusicPopulation;
using Sanford.Multimedia.Midi;
using Ventuz.OSC;
using System.IO;

namespace PopuloApplication
{
    public partial class MainWindow : Form
    {
        private OutputDevice outDevice;
        private Sequencer sequencer;
        private void ChannelMessagePlayed(object sender, ChannelMessageEventArgs e)
        {
            outDevice.Send(e.Message);
        }
        private void LoadParameters()
        {
            // First phase
            numericUpDownPercentDeath.Value = SimulationParameters.PercentDeath;
            numericUpDownMaxSteps.Value = SimulationParameters.MaxSteps;
            numericUpDownAlfa.Value = (decimal)SimulationParameters.Alfa;

            numericUpDownModifyAmount1.Value = Member1.ModifyAmount[0];
            numericUpDownModifyAmount2.Value = Member1.ModifyAmount[1];
            numericUpDownModifyAmount3.Value = Member1.ModifyAmount[2];

            numericUpDownInfluenceAmount1.Value = (decimal)Member1.InfluenceAmount[0];
            numericUpDownInfluenceAmount2.Value = (decimal)Member1.InfluenceAmount[1];
            numericUpDownInfluenceAmount3.Value = (decimal)Member1.InfluenceAmount[2];

            numericUpDownTransposeChance1.Value = (decimal)Member1.TransposeChance[0];
            numericUpDownTransposeChance2.Value = (decimal)Member1.TransposeChance[1];
            numericUpDownTransposeChance3.Value = (decimal)Member1.TransposeChance[2];

            numericUpDownExchangeChance1.Value = (decimal)Member1.ExchangeChance[0];
            numericUpDownExchangeChance2.Value = (decimal)Member1.ExchangeChance[1];
            numericUpDownExchangeChance3.Value = (decimal)Member1.ExchangeChance[2];

            numericUpDownModifyChance1.Value = (decimal)Member1.ModifyChance[0];
            numericUpDownModifyChance2.Value = (decimal)Member1.ModifyChance[1];
            numericUpDownModifyChance3.Value = (decimal)Member1.ModifyChance[2];

            numericUpDownGrowthChance.Value = (decimal)Member1.GrowthChance;
            numericUpDownShrinkChance.Value = (decimal)Member1.ShrinkChance;
        }
        private void SaveParameters()
        {
            // First phase
            SimulationParameters.PercentDeath = (int)numericUpDownPercentDeath.Value;
            SimulationParameters.MaxSteps = (int)numericUpDownMaxSteps.Value;
            SimulationParameters.Alfa = (double)numericUpDownAlfa.Value;

            Member1.ModifyAmount[0] = (int)numericUpDownModifyAmount1.Value;
            Member1.ModifyAmount[1] = (int)numericUpDownModifyAmount2.Value;
            Member1.ModifyAmount[2] = (int)numericUpDownModifyAmount3.Value;

            Member1.InfluenceAmount[0] = (double)numericUpDownInfluenceAmount1.Value;
            Member1.InfluenceAmount[1] = (double)numericUpDownInfluenceAmount2.Value;
            Member1.InfluenceAmount[2] = (double)numericUpDownInfluenceAmount3.Value;

            Member1.TransposeChance[0] = (double)numericUpDownTransposeChance1.Value;
            Member1.TransposeChance[1] = (double)numericUpDownTransposeChance2.Value;
            Member1.TransposeChance[2] = (double)numericUpDownTransposeChance3.Value;

            Member1.ExchangeChance[0] = (double)numericUpDownExchangeChance1.Value;
            Member1.ExchangeChance[1] = (double)numericUpDownExchangeChance2.Value;
            Member1.ExchangeChance[2] = (double)numericUpDownExchangeChance3.Value;

            Member1.ModifyChance[0] = (double)numericUpDownModifyChance1.Value;
            Member1.ModifyChance[1] = (double)numericUpDownModifyChance2.Value;
            Member1.ModifyChance[2] = (double)numericUpDownModifyChance3.Value;

            Member1.GrowthChance = (double)numericUpDownGrowthChance.Value;
            Member1.ShrinkChance = (double)numericUpDownShrinkChance.Value;
        }
        private void ChangePhase()
        {
            Simulation.SimulationBoard.ChangePhase();
            groupBoxFactorsPhase1.Enabled = false;
            groupBoxFactorsPhase2.Enabled = false;
            switch (Simulation.SimulationBoard.IndexOfPhase)
            {
                case 0:
                    groupBoxFactorsPhase1.Enabled = true;
                    break;
            }
        }
        private void PlaySimulation()
        {
            var boardState = Simulation.SimulationBoardState;
            sequencer.Sequence = new Sequence();

            int pitch=0;
            ChannelMessageBuilder builder = new ChannelMessageBuilder();
            for (int channel = 0; channel < 16; channel++)
            {
                Track track = new Track();
                int i = 0;
                for (int index = 0; index < boardState[channel].Item1; index++)
                {
                    //channel = (((int)notes[index,0]) % 100) / 25;
                    pitch = boardState[channel].Item2[index, 0];
                    builder.Command = ChannelCommand.NoteOn;
                    builder.MidiChannel = channel;
                    builder.Data1 = pitch;
                    builder.Data2 = boardState[channel].Item2[index, 3];
                    builder.Build();
                    track.Insert(i, builder.Result);
                    i += boardState[channel].Item2[index, 2] * 5;
                    builder.Data2 = 0;
                    builder.Build();
                    track.Insert(i, builder.Result);
                }
                sequencer.Sequence.Add(track);
            }
            
            sequencer.Start();
        }
        private int _oscPort;
        private string _oscIP;

        private Task _taskSimulation = null;
        private Task _taskPlay = null;
        private int _iEvolveDuration = 100;
        private void DoSimulation()
        {
            _taskSimulation = Task.Factory.StartNew(() => { for (int i = 0; i < _iEvolveDuration; i++) 
                                                                Simulation.EvolveUsingThreads();
                                                            //UdpWriter writer = new UdpWriter(_oscIP, _oscPort);
                                                            //StreamWriter stream = new StreamWriter();
                                                            
                                                            // Send RankTable
                                                            // Simulation.SimulationBoard.RankTable
                                                            });
        }
        private void DoPlay()
        {
            _taskPlay = Task.Factory.StartNew(() => PlaySimulation());
        }

        private enum SimulationState { During, Before };
        private SimulationState _simState;

        public MainWindow(string ip, int port)
        {
            InitializeComponent();
            LoadParameters();
            _simState = SimulationState.Before;
            _oscIP = ip;
            _oscPort = port;
        }

        #region Events
        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            if (_simState == SimulationState.Before)
                return;

            // _simState == SimulationState.During
            SaveParameters();
            if (_taskSimulation == null)
            {
                DoSimulation();
            }
            else if (_taskSimulation.IsCompleted == true)
            {
                if (_taskPlay == null || _taskPlay.IsCompleted)
                {
                    DoPlay();
                    DoSimulation();
                }
            }
        }
        private void buttonStartSimulation_Click(object sender, EventArgs e)
        {
            _simState = SimulationState.During;
            Simulation.ResetSimulation();
            _taskSimulation = null;
            _taskPlay = null;
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void buttonChangePhase_Click(object sender, EventArgs e)
        {
            ChangePhase();
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            sequencer = new Sanford.Multimedia.Midi.Sequencer();
            sequencer.ChannelMessagePlayed += ChannelMessagePlayed;
            outDevice = new OutputDevice(0);

            for (int channel = 0; channel < 16; channel++)
            {
                ChannelMessageBuilder builder = new ChannelMessageBuilder();

                builder.Command = ChannelCommand.ProgramChange;
                builder.MidiChannel = channel;
                builder.Data1 = channel * 3;
                builder.Data2 = 127;
                builder.Build();
                outDevice.Send(builder.Result);
            }
        }
        #endregion
    }
}