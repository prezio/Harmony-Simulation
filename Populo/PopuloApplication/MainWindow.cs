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

namespace PopuloApplication
{
    public partial class MainWindow : Form
    {
        private void LoadParameters()
        {
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

        private Task _taskSimulation = null;
        private Task _taskPlay = null;

        public MainWindow()
        {
            InitializeComponent();
            LoadParameters();
        }

        #region Events
        private void buttonNextStep_Click(object sender, EventArgs e)
        {
            if (_taskSimulation != null)
            {
                Task.WaitAll(_taskSimulation);
            }

            SaveParameters();
            _taskSimulation = Task.Factory.StartNew(() => Simulation.EvolveUsingThreads());
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void buttonNewPhase_Click(object sender, EventArgs e)
        {
            ChangePhase();
        }
        #endregion
    }
}