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
    /// <summary>
    /// Main window of PopuloApplication
    /// </summary>
    public partial class MainWindow : Form
    {
        private int _perCentKnob;
        private delegate int TempoCount(int lastVal, int minVal, int maxVal);
        private void ChangePhase()
        {
            MusicSimulation.Stop();
            Simulation.SimulationBoard.ChangePhase();
            MusicSimulation.Start(this);

            groupBoxFactorsPhase1.Enabled = false;
            groupBoxFactorsPhase2.Enabled = false;
            groupBoxFactorsPhase3.Enabled = false;
            Melody.phase = Simulation.SimulationBoard.IndexOfPhase;
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

            this.Invalidate();
        }
        private void LoadParameters()
        {
            //rhythm
            common_tempo.Value = Melody.tempo;
            common_divider.Value = Melody.divider;
            commonDividerCheck.Checked = Melody.common_divider;
            commonTempoCheck.Checked = Melody.common_tempo;
            tempo1.Value = Melody.tempi[0];
            tempo2.Value = Melody.tempi[1];
            tempo3.Value = Melody.tempi[2];
            tempo4.Value = Melody.tempi[3];
            tempo5.Value = Melody.tempi[4];
            tempo6.Value = Melody.tempi[5];
            tempo7.Value = Melody.tempi[6];
            tempo8.Value = Melody.tempi[7];
            tempo9.Value = Melody.tempi[8];
            tempo10.Value = Melody.tempi[9];
            tempo11.Value = Melody.tempi[10];
            tempo12.Value = Melody.tempi[11];
            tempo13.Value = Melody.tempi[12];
            tempo14.Value = Melody.tempi[13];
            tempo15.Value = Melody.tempi[14];
            tempo16.Value = Melody.tempi[15];
            divider1.Value = Melody.dividers[0];
            divider2.Value = Melody.dividers[1];
            divider3.Value = Melody.dividers[2];
            divider4.Value = Melody.dividers[3];
            divider5.Value = Melody.dividers[4];
            divider6.Value = Melody.dividers[5];
            divider7.Value = Melody.dividers[6];
            divider8.Value = Melody.dividers[7];
            divider9.Value = Melody.dividers[8];
            divider10.Value = Melody.dividers[9];
            divider11.Value = Melody.dividers[10];
            divider12.Value = Melody.dividers[11];
            divider13.Value = Melody.dividers[12];
            divider14.Value = Melody.dividers[13];
            divider15.Value = Melody.dividers[14];
            divider16.Value = Melody.dividers[15];
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
            numericUpDownOnePrefferedLength.Value = (decimal)Member1.PrefferedLength;

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

        /// <summary>
        /// Public property for setting accord
        /// </summary>
        public List<List<PitchData>> Accord { get; set; }
        /// <summary>
        /// Constructor of MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            LoadParameters();
            _perCentKnob = (int)perCentNumericUpDownKnob.Value;
        }
        /// <summary>
        /// Method used for refreshing controls in main window. Can be used outside the class.
        /// Sets the color of progress control: green- if simulation is running, red- otherwise
        /// </summary>
        public void RefreshSimulationControls()
        {
            if (Simulation.SimulationStatus == TaskStatus.Running)
                groupBoxTaskSimulation.BackColor = Color.Green;
            else
                groupBoxTaskSimulation.BackColor = Color.Red;
            groupBoxTaskSimulation.Invalidate();
        }
        /// <summary>
        /// Method used for saving parameters from application to Simulation
        /// </summary>
        public void SaveParameters()
        {
            try
            {
                //rhythm
                Melody.tempo = (int)common_tempo.Value;
                Melody.divider = (int)common_divider.Value;
                Melody.common_tempo = commonTempoCheck.Checked;
                Melody.common_divider = commonDividerCheck.Checked;
                Melody.tempi[0] = (int)tempo1.Value;
                Melody.tempi[1] = (int)tempo2.Value;
                Melody.tempi[2] = (int)tempo3.Value;
                Melody.tempi[3] = (int)tempo4.Value;
                Melody.tempi[4] = (int)tempo5.Value;
                Melody.tempi[5] = (int)tempo6.Value;
                Melody.tempi[6] = (int)tempo7.Value;
                Melody.tempi[7] = (int)tempo8.Value;
                Melody.tempi[8] = (int)tempo9.Value;
                Melody.tempi[9] = (int)tempo10.Value;
                Melody.tempi[10] = (int)tempo11.Value;
                Melody.tempi[11] = (int)tempo12.Value;
                Melody.tempi[12] = (int)tempo13.Value;
                Melody.tempi[13] = (int)tempo14.Value;
                Melody.tempi[14] = (int)tempo15.Value;
                Melody.tempi[15] = (int)tempo16.Value;
                Melody.dividers[0] = (int)divider1.Value;
                Melody.dividers[1] = (int)divider2.Value;
                Melody.dividers[2] = (int)divider3.Value;
                Melody.dividers[3] = (int)divider4.Value;
                Melody.dividers[4] = (int)divider5.Value;
                Melody.dividers[5] = (int)divider6.Value;
                Melody.dividers[6] = (int)divider7.Value;
                Melody.dividers[7] = (int)divider8.Value;
                Melody.dividers[8] = (int)divider9.Value;
                Melody.dividers[9] = (int)divider10.Value;
                Melody.dividers[10] = (int)divider11.Value;
                Melody.dividers[11] = (int)divider12.Value;
                Melody.dividers[12] = (int)divider13.Value;
                Melody.dividers[13] = (int)divider14.Value;
                Melody.dividers[14] = (int)divider15.Value;
                Melody.dividers[15] = (int)divider16.Value;
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
                Member1.PrefferedLength = (int)numericUpDownOnePrefferedLength.Value;

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
            catch (System.InvalidOperationException)
            {
                
            }
        }

        #region Events
        private void buttonStartSimulation_Click(object sender, EventArgs e)
        {
            MusicSimulation.Start(this);

            buttonStartSimulation.Enabled = false;
            buttonChangePhase.Enabled = true;
            buttonSetAccord.Enabled = false;
            buttonStop.Enabled = true;
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
            MusicSimulation.Stop();

            buttonStartSimulation.Enabled = true;
            buttonStop.Enabled = false;
            buttonSetAccord.Enabled = true;
            buttonChangePhase.Enabled = false;
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
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            MusicSimulation.Stop();
        }
        private void perCentNumericUpDownKnob_ValueChanged(object sender, EventArgs e)
        {
            TempoCount count = delegate(int lastVal, int minVal, int maxVal) 
            { 
                        int res = (int)Math.Ceiling((double)lastVal * (double)perCentNumericUpDownKnob.Value / _perCentKnob);
                        if (res > maxVal)
                            return maxVal;
                        if (res < minVal)
                            return minVal;
                        return res;
            };

            tempo1.Value = count((int)tempo1.Value, (int)tempo1.Minimum, (int)tempo1.Maximum);
            tempo2.Value = count((int)tempo2.Value, (int)tempo2.Minimum, (int)tempo2.Maximum);
            tempo3.Value = count((int)tempo3.Value, (int)tempo3.Minimum, (int)tempo3.Maximum);
            tempo4.Value = count((int)tempo4.Value, (int)tempo4.Minimum, (int)tempo4.Maximum);
            tempo5.Value = count((int)tempo5.Value, (int)tempo5.Minimum, (int)tempo5.Maximum);
            tempo6.Value = count((int)tempo6.Value, (int)tempo6.Minimum, (int)tempo6.Maximum);
            tempo7.Value = count((int)tempo7.Value, (int)tempo7.Minimum, (int)tempo7.Maximum);
            tempo8.Value = count((int)tempo8.Value, (int)tempo8.Minimum, (int)tempo8.Maximum);
            tempo9.Value = count((int)tempo9.Value, (int)tempo9.Minimum, (int)tempo9.Maximum);
            tempo10.Value = count((int)tempo10.Value, (int)tempo10.Minimum, (int)tempo10.Maximum);
            tempo11.Value = count((int)tempo11.Value, (int)tempo11.Minimum, (int)tempo11.Maximum);
            tempo12.Value = count((int)tempo12.Value, (int)tempo12.Minimum, (int)tempo12.Maximum);
            tempo13.Value = count((int)tempo13.Value, (int)tempo13.Minimum, (int)tempo13.Maximum);
            tempo14.Value = count((int)tempo14.Value, (int)tempo14.Minimum, (int)tempo14.Maximum);
            tempo15.Value = count((int)tempo15.Value, (int)tempo15.Minimum, (int)tempo15.Maximum);
            tempo16.Value = count((int)tempo16.Value, (int)tempo16.Minimum, (int)tempo16.Maximum);

            _perCentKnob = (int)perCentNumericUpDownKnob.Value;
        }
        #endregion
    }
}