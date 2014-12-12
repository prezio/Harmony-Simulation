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



namespace PopuloApplication
{
    /// <summary>
    /// Main window of PopuloApplication
    /// </summary>
    public partial class MainWindow : Form
    {
        
        private delegate int TempoCount(int lastVal, int minVal, int maxVal);

        private void ChangePhase()
        {
            MusicSimulation.Stop();
            Simulation.SimulationBoard.ChangePhase();
            MusicSimulation.Start(this);

            groupBoxFactorsPhase1.Enabled = true;
           
            Melody.ChangePhase(Simulation.SimulationBoard.IndexOfPhase);
            
            //rhythm
            SustainField.Value = Melody.sustainInStages[Melody.phase][Melody.stage];
            common_tempo.Value = Melody.mainTempiInStages[Melody.phase][Melody.stage];
            common_divider.Value = Melody.mainDividersInStages[Melody.phase][Melody.stage];
            commonDividerCheck.Checked = Melody.commonDividersInStages[Melody.phase][Melody.stage];
            commonTempoCheck.Checked = Melody.commonTempiInStages[Melody.phase][Melody.stage];
            tempo1.Value = Melody.tempiInStages[Melody.phase][Melody.stage][0];
            tempo2.Value = Melody.tempiInStages[Melody.phase][Melody.stage][1];
            tempo3.Value = Melody.tempiInStages[Melody.phase][Melody.stage][2];
            tempo4.Value = Melody.tempiInStages[Melody.phase][Melody.stage][3];
            tempo5.Value = Melody.tempiInStages[Melody.phase][Melody.stage][4];
            tempo6.Value = Melody.tempiInStages[Melody.phase][Melody.stage][5];
            tempo7.Value = Melody.tempiInStages[Melody.phase][Melody.stage][6];
            tempo8.Value = Melody.tempiInStages[Melody.phase][Melody.stage][7];
            tempo9.Value = Melody.tempiInStages[Melody.phase][Melody.stage][8];
            tempo10.Value = Melody.tempiInStages[Melody.phase][Melody.stage][9];
            tempo11.Value = Melody.tempiInStages[Melody.phase][Melody.stage][10];
            tempo12.Value = Melody.tempiInStages[Melody.phase][Melody.stage][11];
            tempo13.Value = Melody.tempiInStages[Melody.phase][Melody.stage][12];
            tempo14.Value = Melody.tempiInStages[Melody.phase][Melody.stage][13];
            tempo15.Value = Melody.tempiInStages[Melody.phase][Melody.stage][14];
            tempo16.Value = Melody.tempiInStages[Melody.phase][Melody.stage][15];
            divider1.Value = Melody.dividersInStages[Melody.phase][Melody.stage][0];
            divider2.Value = Melody.dividersInStages[Melody.phase][Melody.stage][1];
            divider3.Value = Melody.dividersInStages[Melody.phase][Melody.stage][2];
            divider4.Value = Melody.dividersInStages[Melody.phase][Melody.stage][3];
            divider5.Value = Melody.dividersInStages[Melody.phase][Melody.stage][4];
            divider6.Value = Melody.dividersInStages[Melody.phase][Melody.stage][5];
            divider7.Value = Melody.dividersInStages[Melody.phase][Melody.stage][6];
            divider8.Value = Melody.dividersInStages[Melody.phase][Melody.stage][7];
            divider9.Value = Melody.dividersInStages[Melody.phase][Melody.stage][8];
            divider10.Value = Melody.dividersInStages[Melody.phase][Melody.stage][9];
            divider11.Value = Melody.dividersInStages[Melody.phase][Melody.stage][10];
            divider12.Value = Melody.dividersInStages[Melody.phase][Melody.stage][11];
            divider13.Value = Melody.dividersInStages[Melody.phase][Melody.stage][12];
            divider14.Value = Melody.dividersInStages[Melody.phase][Melody.stage][13];
            divider15.Value = Melody.dividersInStages[Melody.phase][Melody.stage][14];
            divider16.Value = Melody.dividersInStages[Melody.phase][Melody.stage][15];
            offset1.Value = (decimal)Melody.offset[0];
            offset2.Value = (decimal)Melody.offset[1];
            offset3.Value = (decimal)Melody.offset[2];
            offset4.Value = (decimal)Melody.offset[3];
            offset5.Value = (decimal)Melody.offset[4];
            offset6.Value = (decimal)Melody.offset[5];
            offset7.Value = (decimal)Melody.offset[6];
            offset8.Value = (decimal)Melody.offset[7];
            offset9.Value = (decimal)Melody.offset[8];
            offset10.Value = (decimal)Melody.offset[9];
            offset11.Value = (decimal)Melody.offset[10];
            offset12.Value = (decimal)Melody.offset[11];
            offset13.Value = (decimal)Melody.offset[12];
            offset14.Value = (decimal)Melody.offset[13];
            offset15.Value = (decimal)Melody.offset[14];
            offset16.Value = (decimal)Melody.offset[15];
            this.Invalidate();
        }
        private void LoadParameters()
        {
            //rhythm
            SustainField.Value = MIDIPlayer.sustain;
            //rhythm
            common_tempo.Value = Melody.mainTempiInStages[Melody.phase][Melody.stage];
            common_divider.Value = Melody.mainDividersInStages[Melody.phase][Melody.stage];
            commonDividerCheck.Checked = Melody.commonDividersInStages[Melody.phase][Melody.stage];
            commonTempoCheck.Checked = Melody.commonTempiInStages[Melody.phase][Melody.stage];
            tempo1.Value = Melody.tempiInStages[Melody.phase][Melody.stage][0];
            tempo2.Value = Melody.tempiInStages[Melody.phase][Melody.stage][1];
            tempo3.Value = Melody.tempiInStages[Melody.phase][Melody.stage][2];
            tempo4.Value = Melody.tempiInStages[Melody.phase][Melody.stage][3];
            tempo5.Value = Melody.tempiInStages[Melody.phase][Melody.stage][4];
            tempo6.Value = Melody.tempiInStages[Melody.phase][Melody.stage][5];
            tempo7.Value = Melody.tempiInStages[Melody.phase][Melody.stage][6];
            tempo8.Value = Melody.tempiInStages[Melody.phase][Melody.stage][7];
            tempo9.Value = Melody.tempiInStages[Melody.phase][Melody.stage][8];
            tempo10.Value = Melody.tempiInStages[Melody.phase][Melody.stage][9];
            tempo11.Value = Melody.tempiInStages[Melody.phase][Melody.stage][10];
            tempo12.Value = Melody.tempiInStages[Melody.phase][Melody.stage][11];
            tempo13.Value = Melody.tempiInStages[Melody.phase][Melody.stage][12];
            tempo14.Value = Melody.tempiInStages[Melody.phase][Melody.stage][13];
            tempo15.Value = Melody.tempiInStages[Melody.phase][Melody.stage][14];
            tempo16.Value = Melody.tempiInStages[Melody.phase][Melody.stage][15];
            divider1.Value = Melody.dividersInStages[Melody.phase][Melody.stage][0];
            divider2.Value = Melody.dividersInStages[Melody.phase][Melody.stage][1];
            divider3.Value = Melody.dividersInStages[Melody.phase][Melody.stage][2];
            divider4.Value = Melody.dividersInStages[Melody.phase][Melody.stage][3];
            divider5.Value = Melody.dividersInStages[Melody.phase][Melody.stage][4];
            divider6.Value = Melody.dividersInStages[Melody.phase][Melody.stage][5];
            divider7.Value = Melody.dividersInStages[Melody.phase][Melody.stage][6];
            divider8.Value = Melody.dividersInStages[Melody.phase][Melody.stage][7];
            divider9.Value = Melody.dividersInStages[Melody.phase][Melody.stage][8];
            divider10.Value = Melody.dividersInStages[Melody.phase][Melody.stage][9];
            divider11.Value = Melody.dividersInStages[Melody.phase][Melody.stage][10];
            divider12.Value = Melody.dividersInStages[Melody.phase][Melody.stage][11];
            divider13.Value = Melody.dividersInStages[Melody.phase][Melody.stage][12];
            divider14.Value = Melody.dividersInStages[Melody.phase][Melody.stage][13];
            divider15.Value = Melody.dividersInStages[Melody.phase][Melody.stage][14];
            divider16.Value = Melody.dividersInStages[Melody.phase][Melody.stage][15];
            // Global Parameters
            numericUpDownPercentDeath.Value = SimulationParameters.PercentDeath;
            numericUpDownMaxSteps.Value = SimulationParameters.MaxSteps;
            numericUpDownAlfa.Value = (decimal)SimulationParameters.Alfa;

            // First phase
            numericUpDownOneModifyAmount1.Value = Member.ModifyAmount[0];
            numericUpDownOneModifyAmount2.Value = Member.ModifyAmount[2];
            numericUpDownOneModifyAmount3.Value = Member.ModifyAmount[3];

            numericUpDownOneInfluenceAmount1.Value = (decimal)Member.InfluenceAmount[0];
            numericUpDownOneInfluenceAmount2.Value = (decimal)Member.InfluenceAmount[2];
            numericUpDownOneInfluenceAmount3.Value = (decimal)Member.InfluenceAmount[3];

            numericUpDownOneTransposeChance1.Value = (decimal)Member.TransposeChance[0];
            numericUpDownOneTransposeChance2.Value = (decimal)Member.TransposeChance[2];
            numericUpDownOneTransposeChance3.Value = (decimal)Member.TransposeChance[3];

            numericUpDownOneExchangeChance1.Value = (decimal)Member.ExchangeChance[0];
            numericUpDownOneExchangeChance2.Value = (decimal)Member.ExchangeChance[2];
            numericUpDownOneExchangeChance3.Value = (decimal)Member.ExchangeChance[3];

            numericUpDownOneModifyChance1.Value = (decimal)Member.ModifyChance[0];
            numericUpDownOneModifyChance2.Value = (decimal)Member.ModifyChance[2];
            numericUpDownOneModifyChance3.Value = (decimal)Member.ModifyChance[3];

            numericUpDownOneGrowthChance.Value = (decimal)Member.GrowthChance;
            numericUpDownOneShrinkChance.Value = (decimal)Member.ShrinkChance;
            numericUpDownOnePrefferedLength.Value = (decimal)Member.PrefferedLength;
            offset1.Value = (decimal)Melody.offset[0];
            offset2.Value = (decimal)Melody.offset[1];
            offset3.Value = (decimal)Melody.offset[2];
            offset4.Value = (decimal)Melody.offset[3];
            offset5.Value = (decimal)Melody.offset[4];
            offset6.Value = (decimal)Melody.offset[5];
            offset7.Value = (decimal)Melody.offset[6];
            offset8.Value = (decimal)Melody.offset[7];
            offset9.Value = (decimal)Melody.offset[8];
            offset10.Value = (decimal)Melody.offset[9];
            offset11.Value = (decimal)Melody.offset[10];
            offset12.Value = (decimal)Melody.offset[11];
            offset13.Value = (decimal)Melody.offset[12];
            offset14.Value = (decimal)Melody.offset[13];
            offset15.Value = (decimal)Melody.offset[14];
            offset16.Value = (decimal)Melody.offset[15];
            chord.Value = (decimal)Member1.chord;
            played.Value = (decimal)Member.played;

        }

        /// <summary>
        /// Public property for setting accord
        /// </summary>
      
        /// <summary>
        /// Constructor of MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            
            
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
                MIDIPlayer.sustain = (int)SustainField.Value;
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
                Melody.offset[0] = (int)offset1.Value;
                Melody.offset[1] = (int)offset2.Value;
                Melody.offset[2] = (int)offset3.Value;
                Melody.offset[3] = (int)offset4.Value;
                Melody.offset[4] = (int)offset5.Value;
                Melody.offset[5] = (int)offset6.Value;
                Melody.offset[6] = (int)offset7.Value;
                Melody.offset[7] = (int)offset8.Value;
                Melody.offset[8] = (int)offset9.Value;
                Melody.offset[9] = (int)offset10.Value;
                Melody.offset[10] = (int)offset11.Value;
                Melody.offset[11] = (int)offset12.Value;
                Melody.offset[12] = (int)offset13.Value;
                Melody.offset[13] = (int)offset14.Value;
                Melody.offset[14] = (int)offset15.Value;
                Melody.offset[15] = (int)offset16.Value;
                // Global parameters
                SimulationParameters.PercentDeath = (int)numericUpDownPercentDeath.Value;
                SimulationParameters.MaxSteps = (int)numericUpDownMaxSteps.Value;
                SimulationParameters.Alfa = (double)numericUpDownAlfa.Value;

                // First phase
                Member.ModifyAmount[0] = (int)numericUpDownOneModifyAmount1.Value;
                Member.ModifyAmount[2] = (int)numericUpDownOneModifyAmount2.Value;
                Member.ModifyAmount[3] = (int)numericUpDownOneModifyAmount3.Value;

                Member.InfluenceAmount[0] = (double)numericUpDownOneInfluenceAmount1.Value;
                Member.InfluenceAmount[2] = (double)numericUpDownOneInfluenceAmount2.Value;
                Member.InfluenceAmount[3] = (double)numericUpDownOneInfluenceAmount3.Value;

                Member.TransposeChance[0] = (double)numericUpDownOneTransposeChance1.Value;
                Member.TransposeChance[2] = (double)numericUpDownOneTransposeChance2.Value;
                Member.TransposeChance[3] = (double)numericUpDownOneTransposeChance3.Value;

                Member.ExchangeChance[0] = (double)numericUpDownOneExchangeChance1.Value;
                Member.ExchangeChance[2] = (double)numericUpDownOneExchangeChance2.Value;
                Member.ExchangeChance[3] = (double)numericUpDownOneExchangeChance3.Value;

                Member.ModifyChance[0] = (double)numericUpDownOneModifyChance1.Value;
                Member.ModifyChance[2] = (double)numericUpDownOneModifyChance2.Value;
                Member.ModifyChance[3] = (double)numericUpDownOneModifyChance3.Value;

                Member.GrowthChance = (double)numericUpDownOneGrowthChance.Value;
                Member.ShrinkChance = (double)numericUpDownOneShrinkChance.Value;
                Member.PrefferedLength = (int)numericUpDownOnePrefferedLength.Value;

                Member.played = (int)played.Value;
                Member1.chord = (int)chord.Value;

                
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
            
            buttonStop.Enabled = true;
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void buttonChangePhase_Click(object sender, EventArgs e)
        {
            ChangePhase();
            Odcinek.Text = Melody.phase.ToString();
        }
        private void buttonStop_Click(object sender, EventArgs e)
        {
            MusicSimulation.Stop();

            buttonStartSimulation.Enabled = true;
            buttonStop.Enabled = false;

            buttonChangePhase.Enabled = false;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            MusicSimulation.Stop();
            Melody.StopPlaying();
            Application.Exit();
        }
  
        #endregion

        private void ChangeStage_Click(object sender, EventArgs e)
        {
            Melody.ChangeStage();
            Odcinek.Text = Melody.stage.ToString();
            //rhythm
            SustainField.Value = Melody.sustainInStages[Melody.phase][Melody.stage];
            common_tempo.Value = Melody.mainTempiInStages[Melody.phase][Melody.stage];
            common_divider.Value = Melody.mainDividersInStages[Melody.phase][Melody.stage];
            commonDividerCheck.Checked = Melody.commonDividersInStages[Melody.phase][Melody.stage];
            commonTempoCheck.Checked = Melody.commonTempiInStages[Melody.phase][Melody.stage];
            tempo1.Value = Melody.tempiInStages[Melody.phase][Melody.stage][0];
            tempo2.Value = Melody.tempiInStages[Melody.phase][Melody.stage][1];
            tempo3.Value = Melody.tempiInStages[Melody.phase][Melody.stage][2];
            tempo4.Value = Melody.tempiInStages[Melody.phase][Melody.stage][3];
            tempo5.Value = Melody.tempiInStages[Melody.phase][Melody.stage][4];
            tempo6.Value = Melody.tempiInStages[Melody.phase][Melody.stage][5];
            tempo7.Value = Melody.tempiInStages[Melody.phase][Melody.stage][6];
            tempo8.Value = Melody.tempiInStages[Melody.phase][Melody.stage][7];
            tempo9.Value = Melody.tempiInStages[Melody.phase][Melody.stage][8];
            tempo10.Value = Melody.tempiInStages[Melody.phase][Melody.stage][9];
            tempo11.Value = Melody.tempiInStages[Melody.phase][Melody.stage][10];
            tempo12.Value = Melody.tempiInStages[Melody.phase][Melody.stage][11];
            tempo13.Value = Melody.tempiInStages[Melody.phase][Melody.stage][12];
            tempo14.Value = Melody.tempiInStages[Melody.phase][Melody.stage][13];
            tempo15.Value = Melody.tempiInStages[Melody.phase][Melody.stage][14];
            tempo16.Value = Melody.tempiInStages[Melody.phase][Melody.stage][15];
            divider1.Value = Melody.dividersInStages[Melody.phase][Melody.stage][0];
            divider2.Value = Melody.dividersInStages[Melody.phase][Melody.stage][1];
            divider3.Value = Melody.dividersInStages[Melody.phase][Melody.stage][2];
            divider4.Value = Melody.dividersInStages[Melody.phase][Melody.stage][3];
            divider5.Value = Melody.dividersInStages[Melody.phase][Melody.stage][4];
            divider6.Value = Melody.dividersInStages[Melody.phase][Melody.stage][5];
            divider7.Value = Melody.dividersInStages[Melody.phase][Melody.stage][6];
            divider8.Value = Melody.dividersInStages[Melody.phase][Melody.stage][7];
            divider9.Value = Melody.dividersInStages[Melody.phase][Melody.stage][8];
            divider10.Value = Melody.dividersInStages[Melody.phase][Melody.stage][9];
            divider11.Value = Melody.dividersInStages[Melody.phase][Melody.stage][10];
            divider12.Value = Melody.dividersInStages[Melody.phase][Melody.stage][11];
            divider13.Value = Melody.dividersInStages[Melody.phase][Melody.stage][12];
            divider14.Value = Melody.dividersInStages[Melody.phase][Melody.stage][13];
            divider15.Value = Melody.dividersInStages[Melody.phase][Melody.stage][14];
            divider16.Value = Melody.dividersInStages[Melody.phase][Melody.stage][15];
        }

        private void Odcinek_Click(object sender, EventArgs e)
        {

        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void groupBoxFactorsPhase1_Enter(object sender, EventArgs e)
        {

        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            LoadParameters();
        }

        private void divider1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}