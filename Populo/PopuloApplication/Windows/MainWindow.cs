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
using System.IO;
using System.Threading;
using PeriodicChords;


namespace PopuloApplication
{
    public partial class MainWindow : Form
    {
        private bool _isSimRunning = false;

        private void LoadParameters()
        {
            // Global Parameters
            numericUpDownPercentDeath.Value = SimulationParameters.PercentDeath;
            numericUpDownMaxSteps.Value = SimulationParameters.MaxSteps;
            numericUpDownAlfa.Value = (decimal)SimulationParameters.Alfa;

            // First phase
            numericUpDownOneModifyAmount1.Value = Member1.ModifyAmount[0];
            numericUpDownOneModifyAmount2.Value = Member1.ModifyAmount[2];
            numericUpDownOneModifyAmount3.Value = Member1.ModifyAmount[3];

            numericUpDownOneInfluenceAmount1.Value = (decimal)Member1.InfluenceAmount[0];
            numericUpDownOneInfluenceAmount2.Value = (decimal)Member1.InfluenceAmount[2];
            numericUpDownOneInfluenceAmount3.Value = (decimal)Member1.InfluenceAmount[3];

            numericUpDownOneTransposeChance1.Value = (decimal)Member1.TransposeChance[0];
            numericUpDownOneTransposeChance2.Value = (decimal)Member1.TransposeChance[2];
            numericUpDownOneTransposeChance3.Value = (decimal)Member1.TransposeChance[3];

            numericUpDownOneExchangeChance1.Value = (decimal)Member1.ExchangeChance[0];
            numericUpDownOneExchangeChance2.Value = (decimal)Member1.ExchangeChance[2];
            numericUpDownOneExchangeChance3.Value = (decimal)Member1.ExchangeChance[3];

            numericUpDownOneModifyChance1.Value = (decimal)Member1.ModifyChance[0];
            numericUpDownOneModifyChance2.Value = (decimal)Member1.ModifyChance[2];
            numericUpDownOneModifyChance3.Value = (decimal)Member1.ModifyChance[3];

            numericUpDownOneGrowthChance.Value = (decimal)Member1.GrowthChance;
            numericUpDownOneShrinkChance.Value = (decimal)Member1.ShrinkChance;

            // Second Phase
            numericUpDownTwoPitchInfluence.Value = (decimal)Member2.PitchInfluenceAmount;
            numericUpDownTwoRythmInfluence.Value = (decimal)Member2.RhythmInfluenceAmount;
            numericUpDownTwoDynamicsInfluence.Value = (decimal)Member2.DynamicsInfluenceAmount;
            numericUpDownTwoPauseInfluenceAmount.Value = (decimal)Member2.PauseInfluenceAmount;
            numericUpDownTwoRythmDistortion.Value = (decimal)Member2.RhythmDistortionInfluenceChance;
            numericUpDownTwoDynamicsDistortion.Value = (decimal)Member2.DynamicsDistortionInfluenceChance;
            numericUpDownTwoTypeChance.Value = (decimal)Member2.TypeInfluenceChance;
            numericUpDownTwoGrowthChance.Value = (decimal)Member2.GrowthChance;
            numericUpDownTwoShrinkChance.Value = (decimal)Member2.ShrinkChance;
            numericUpDownTwoPeakMoveChance.Value = (decimal)Member2.PeakMoveChance;
            numericUpDownTwoPeakMaxMove.Value = (decimal)Member2.PeakMaxMove;
            numericUpDownTwoPauseChangeChance.Value = (decimal)Member2.PauseChangeChance;
            numericUpDownTwoPauseMaxChange.Value = (decimal)Member2.PauseMaxChange;
            numericUpDownTwoInitialRythmChance.Value = (decimal)Member2.InitialRhythmChangeChance;
            numericUpDownTwoInitialRythmMaxChange.Value = (decimal)Member2.InitialRhythmMaxChange;
            numericUpDownTwoInitialDynamicsChance.Value = (decimal)Member2.InitialDynamicsChangeChance;
            numericUpDownTwoInitialDynamicsMaxChange.Value = (decimal)Member2.InitialDynamicsMaxChange;
            numericUpDownTwoInitialChordChance.Value = (decimal)Member2.InitialChordChangeChance;
            numericUpDownTwoChordChangeChance.Value = (decimal)Member2.ChordChangeChance;
            numericUpDownTwoRythmDistortionChange.Value = (decimal)Member2.RhythmDistortionChangeChance;
            numericUpDownTwoDynamicsDistortionChangeChance.Value = (decimal)Member2.DynamicsDistortionChangeChance;
            numericUpDownTwoPitchChangeChance.Value = (decimal)Member2.PitchChangeChance;
            numericUpDownTwoPitchMaxChange.Value = (decimal)Member2.PitchMaxChange;
            numericUpDownTwoRythmChangeChance.Value = (decimal)Member2.RhythmChangeChance;
            numericUpDownTwoRythmMaxChange.Value = (decimal)Member2.RhythmMaxChange;
            numericUpDownTwoDynamicsChangeChance.Value = (decimal)Member2.DynamicsChangeChance;
            numericUpDownTwoDynamicsMaxChange.Value = (decimal)Member2.DynamicsMaxChange;
            numericUpDownTwoPrefferedLength.Value = (decimal)Member2.PrefferedLength;
            numericUpDownTwoPauseLength.Value = (decimal)Member2.PrefferedPauseLength;
            numericUpDownTwoTypeChangeChance.Value = (decimal)Member2.TypeChangeChance;

            // Third phase
            numericUpDownThreeModifyAmount1.Value = (decimal)Member3.ModifyAmount[0];
            numericUpDownThreeModifyAmount2.Value = (decimal)Member3.ModifyAmount[1];
            numericUpDownThreeModifyAmount3.Value = (decimal)Member3.ModifyAmount[2];
            numericUpDownThreeModifyAmount4.Value = (decimal)Member3.ModifyAmount[3];
            numericUpDownThreeModifyAmount5.Value = (decimal)Member3.ModifyAmount[4];
            numericUpDownThreeModifyAmount6.Value = (decimal)Member3.ModifyAmount[5];
            numericUpDownThreeModifyAmount7.Value = (decimal)Member3.ModifyAmount[6];

            numericUpDownThreeInfluenceAmount1.Value = (decimal)Member3.InfluenceAmount[0];
            numericUpDownThreeInfluenceAmount2.Value = (decimal)Member3.InfluenceAmount[1];
            numericUpDownThreeInfluenceAmount3.Value = (decimal)Member3.InfluenceAmount[2];
            numericUpDownThreeInfluenceAmount4.Value = (decimal)Member3.InfluenceAmount[3];
            numericUpDownThreeInfluenceAmount5.Value = (decimal)Member3.InfluenceAmount[4];
            numericUpDownThreeInfluenceAmount6.Value = (decimal)Member3.InfluenceAmount[5];
            numericUpDownThreeInfluenceAmount7.Value = (decimal)Member3.InfluenceAmount[6];

            numericUpDownThreeTransposeChance1.Value = (decimal)Member3.TransposeChance[0];
            numericUpDownThreeTransposeChance2.Value = (decimal)Member3.TransposeChance[1];
            numericUpDownThreeTransposeChance3.Value = (decimal)Member3.TransposeChance[2];
            numericUpDownThreeTransposeChance4.Value = (decimal)Member3.TransposeChance[3];
            numericUpDownThreeTransposeChance5.Value = (decimal)Member3.TransposeChance[4];
            numericUpDownThreeTransposeChance6.Value = (decimal)Member3.TransposeChance[5];
            numericUpDownThreeTransposeChance7.Value = (decimal)Member3.TransposeChance[6];

            numericUpDownThreeExchangeChance1.Value = (decimal)Member3.ExchangeChance[0];
            numericUpDownThreeExchangeChance2.Value = (decimal)Member3.ExchangeChance[1];
            numericUpDownThreeExchangeChance3.Value = (decimal)Member3.ExchangeChance[2];
            numericUpDownThreeExchangeChance4.Value = (decimal)Member3.ExchangeChance[3];
            numericUpDownThreeExchangeChance5.Value = (decimal)Member3.ExchangeChance[4];
            numericUpDownThreeExchangeChance6.Value = (decimal)Member3.ExchangeChance[5];
            numericUpDownThreeExchangeChance7.Value = (decimal)Member3.ExchangeChance[6];

            numericUpDownThreeModifyChance1.Value = (decimal)Member3.ModifyChance[0];
            numericUpDownThreeModifyChance2.Value = (decimal)Member3.ModifyChance[1];
            numericUpDownThreeModifyChance3.Value = (decimal)Member3.ModifyChance[2];
            numericUpDownThreeModifyChance4.Value = (decimal)Member3.ModifyChance[3];
            numericUpDownThreeModifyChance5.Value = (decimal)Member3.ModifyChance[4];
            numericUpDownThreeModifyChance6.Value = (decimal)Member3.ModifyChance[5];
            numericUpDownThreeModifyChance7.Value = (decimal)Member3.ModifyChance[6];

            numericUpDownThreeGrowthChance.Value = (decimal)Member3.GrowthChance;
            numericUpDownThreeShrinkChance.Value = (decimal)Member3.ShrinkChance;
            numericUpDownThreePrefferedLength.Value = (decimal)Member3.PrefferedLength;
            numericUpDownThreePrefferedGroups.Value = (decimal)Member3.PrefferedGroups;
            numericUpDownThreePrefferedNotes.Value = (decimal)Member3.PrefferedNotes;
            numericUpDownThreePlayedGroup.Value = (decimal)Member3.PlayedGroup;
        }
        private void SaveParameters()
        {
            // Global parameters
            SimulationParameters.PercentDeath = (int)numericUpDownPercentDeath.Value;
            SimulationParameters.MaxSteps = (int)numericUpDownMaxSteps.Value;
            SimulationParameters.Alfa = (double)numericUpDownAlfa.Value;

            // First phase
            Member1.ModifyAmount[0] = (int)numericUpDownOneModifyAmount1.Value;
            Member1.ModifyAmount[2] = (int)numericUpDownOneModifyAmount2.Value;
            Member1.ModifyAmount[3] = (int)numericUpDownOneModifyAmount3.Value;

            Member1.InfluenceAmount[0] = (double)numericUpDownOneInfluenceAmount1.Value;
            Member1.InfluenceAmount[2] = (double)numericUpDownOneInfluenceAmount2.Value;
            Member1.InfluenceAmount[3] = (double)numericUpDownOneInfluenceAmount3.Value;

            Member1.TransposeChance[0] = (double)numericUpDownOneTransposeChance1.Value;
            Member1.TransposeChance[2] = (double)numericUpDownOneTransposeChance2.Value;
            Member1.TransposeChance[3] = (double)numericUpDownOneTransposeChance3.Value;

            Member1.ExchangeChance[0] = (double)numericUpDownOneExchangeChance1.Value;
            Member1.ExchangeChance[2] = (double)numericUpDownOneExchangeChance2.Value;
            Member1.ExchangeChance[3] = (double)numericUpDownOneExchangeChance3.Value;

            Member1.ModifyChance[0] = (double)numericUpDownOneModifyChance1.Value;
            Member1.ModifyChance[2] = (double)numericUpDownOneModifyChance2.Value;
            Member1.ModifyChance[3] = (double)numericUpDownOneModifyChance3.Value;

            Member1.GrowthChance = (double)numericUpDownOneGrowthChance.Value;
            Member1.ShrinkChance = (double)numericUpDownOneShrinkChance.Value;

            // Second phase
            Member2.PitchInfluenceAmount = (double)numericUpDownTwoPitchInfluence.Value;
            Member2.RhythmInfluenceAmount = (double)numericUpDownTwoRythmInfluence.Value;
            Member2.DynamicsInfluenceAmount = (double)numericUpDownTwoDynamicsInfluence.Value;
            Member2.PauseInfluenceAmount = (double)numericUpDownTwoPauseInfluenceAmount.Value;
            Member2.RhythmDistortionInfluenceChance = (double)numericUpDownTwoRythmDistortion.Value;
            Member2.DynamicsDistortionInfluenceChance = (double)numericUpDownTwoDynamicsDistortion.Value;
            Member2.TypeInfluenceChance = (double)numericUpDownTwoTypeChance.Value;
            Member2.GrowthChance = (double)numericUpDownTwoGrowthChance.Value;
            Member2.ShrinkChance = (double)numericUpDownTwoShrinkChance.Value;
            Member2.PeakMoveChance = (double)numericUpDownTwoPeakMoveChance.Value;
            Member2.PeakMaxMove = (int)numericUpDownTwoPeakMaxMove.Value;
            Member2.PauseChangeChance = (double)numericUpDownTwoPauseChangeChance.Value;
            Member2.PauseMaxChange = (int)numericUpDownTwoPauseMaxChange.Value;
            Member2.InitialRhythmChangeChance = (double)numericUpDownTwoInitialRythmChance.Value;
            Member2.InitialRhythmMaxChange = (int)numericUpDownTwoInitialRythmMaxChange.Value;
            Member2.InitialChordChangeChance = (double)numericUpDownTwoInitialDynamicsChance.Value;
            Member2.InitialDynamicsMaxChange = (int)numericUpDownTwoInitialDynamicsMaxChange.Value;
            Member2.InitialChordChangeChance = (double)numericUpDownTwoInitialChordChance.Value;
            Member2.ChordChangeChance = (double)numericUpDownTwoChordChangeChance.Value;
            Member2.RhythmDistortionChangeChance = (double)numericUpDownTwoRythmDistortionChange.Value;
            Member2.DynamicsDistortionChangeChance = (double)numericUpDownTwoDynamicsDistortionChangeChance.Value;
            Member2.PitchChangeChance = (double)numericUpDownTwoPitchChangeChance.Value;
            Member2.PitchMaxChange = (int)numericUpDownTwoPitchMaxChange.Value;
            Member2.RhythmChangeChance = (double)numericUpDownTwoRythmChangeChance.Value;
            Member2.RhythmMaxChange = (int)numericUpDownTwoRythmMaxChange.Value;
            Member2.DynamicsChangeChance = (double)numericUpDownTwoDynamicsChangeChance.Value;
            Member2.DynamicsMaxChange = (int)numericUpDownTwoDynamicsMaxChange.Value;
            Member2.PrefferedLength = (int)numericUpDownTwoPeakMoveChance.Value;
            Member2.PrefferedPauseLength = (int)numericUpDownTwoPauseLength.Value;
            Member2.TypeChangeChance = (double)numericUpDownTwoTypeChangeChance.Value;

            // Third phase
            Member3.ModifyAmount[0] = (int)numericUpDownThreeModifyAmount1.Value;
            Member3.ModifyAmount[1] = (int)numericUpDownThreeModifyAmount2.Value;
            Member3.ModifyAmount[2] = (int)numericUpDownThreeModifyAmount3.Value;
            Member3.ModifyAmount[3] = (int)numericUpDownThreeModifyAmount4.Value;
            Member3.ModifyAmount[4] = (int)numericUpDownThreeModifyAmount5.Value;
            Member3.ModifyAmount[5] = (int)numericUpDownThreeModifyAmount6.Value;
            Member3.ModifyAmount[6] = (int)numericUpDownThreeModifyAmount7.Value;

            Member3.InfluenceAmount[0] = (double)numericUpDownThreeInfluenceAmount1.Value;
            Member3.InfluenceAmount[1] = (double)numericUpDownThreeInfluenceAmount2.Value;
            Member3.InfluenceAmount[2] = (double)numericUpDownThreeInfluenceAmount3.Value;
            Member3.InfluenceAmount[3] = (double)numericUpDownThreeInfluenceAmount4.Value;
            Member3.InfluenceAmount[4] = (double)numericUpDownThreeInfluenceAmount5.Value;
            Member3.InfluenceAmount[5] = (double)numericUpDownThreeInfluenceAmount6.Value;
            Member3.InfluenceAmount[6] = (double)numericUpDownThreeInfluenceAmount7.Value;

            Member3.TransposeChance[0] = (double)numericUpDownThreeTransposeChance1.Value;
            Member3.TransposeChance[1] = (double)numericUpDownThreeTransposeChance2.Value;
            Member3.TransposeChance[2] = (double)numericUpDownThreeTransposeChance3.Value;
            Member3.TransposeChance[3] = (double)numericUpDownThreeTransposeChance4.Value;
            Member3.TransposeChance[4] = (double)numericUpDownThreeTransposeChance5.Value;
            Member3.TransposeChance[5] = (double)numericUpDownThreeTransposeChance6.Value;
            Member3.TransposeChance[6] = (double)numericUpDownThreeTransposeChance7.Value;

            Member3.ExchangeChance[0] = (double)numericUpDownThreeExchangeChance1.Value;
            Member3.ExchangeChance[1] = (double)numericUpDownThreeExchangeChance2.Value;
            Member3.ExchangeChance[2] = (double)numericUpDownThreeExchangeChance3.Value;
            Member3.ExchangeChance[3] = (double)numericUpDownThreeExchangeChance4.Value;
            Member3.ExchangeChance[4] = (double)numericUpDownThreeExchangeChance5.Value;
            Member3.ExchangeChance[5] = (double)numericUpDownThreeExchangeChance6.Value;
            Member3.ExchangeChance[6] = (double)numericUpDownThreeExchangeChance7.Value;

            Member3.ModifyChance[0] = (double)numericUpDownThreeModifyChance1.Value;
            Member3.ModifyChance[1] = (double)numericUpDownThreeModifyChance2.Value;
            Member3.ModifyChance[2] = (double)numericUpDownThreeModifyChance3.Value;
            Member3.ModifyChance[3] = (double)numericUpDownThreeModifyChance4.Value;
            Member3.ModifyChance[4] = (double)numericUpDownThreeModifyChance5.Value;
            Member3.ModifyChance[5] = (double)numericUpDownThreeModifyChance6.Value;
            Member3.ModifyChance[6] = (double)numericUpDownThreeModifyChance7.Value;

            Member3.GrowthChance = (double)numericUpDownThreeGrowthChance.Value;
            Member3.ShrinkChance = (double)numericUpDownThreeShrinkChance.Value;
            Member3.PrefferedLength = (int)numericUpDownThreePrefferedLength.Value;
            Member3.PrefferedGroups = (int)numericUpDownThreePrefferedGroups.Value;
            Member3.PrefferedNotes = (int)numericUpDownThreePrefferedNotes.Value;
            Member3.PlayedGroup = (int)numericUpDownThreePlayedGroup.Value;
        }
        private void ChangePhase()
        {
            Simulation.StopSimulation();
            Melody.StopPlaying();
            Simulation.SimulationBoard.ChangePhase();

            groupBoxFactorsPhase1.Enabled = false;
            groupBoxFactorsPhase2.Enabled = false;
            groupBoxFactorsPhase3.Enabled = false;

            switch (Simulation.SimulationBoard.IndexOfPhase)
            {
                case 0:
                    groupBoxFactorsPhase1.Enabled = true;
                    break;
                case 1:
                    groupBoxFactorsPhase2.Enabled = true;
                    break;
                case 2:
                    groupBoxFactorsPhase3.Enabled = true;
                    break;
            }
        }

        public List<List<PitchData>> Accord { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            LoadParameters();
        }

        #region Events
        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            if (Simulation.SimulationStatus == TaskStatus.Running)
                groupBoxTaskSimulation.BackColor = Color.Green;
            else
                groupBoxTaskSimulation.BackColor = Color.Red;
            groupBoxTaskSimulation.Invalidate();

            if (_isSimRunning == true)
            {
                
            }

            SaveParameters();
            Simulation.StartSimulation(1000);
        }
        private void buttonStartSimulation_Click(object sender, EventArgs e)
        {
            _isSimRunning = true;
            Simulation.StartSimulation(1000);
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void buttonChangePhase_Click(object sender, EventArgs e)
        {
            ChangePhase();
        }
        private void buttonStop_Click(object sender, EventArgs e)
        {
            Melody.StopPlaying();
            Simulation.StopSimulation();
        }
        private void buttonSetAccord_Click(object sender, EventArgs e)
        {
            OpenFileDialog wnd = new OpenFileDialog();
            wnd.RestoreDirectory = true;

            DialogResult result = wnd.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            try
            {
                Accord = Serialization.ReadFromPitch(wnd.FileName);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to read accord");
            }
        }
        #endregion
    }
}