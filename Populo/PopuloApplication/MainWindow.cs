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

            numericUpDownModifyAmount1.Value = SimulationParameters.ModifyAmount[0];
            numericUpDownModifyAmount2.Value = SimulationParameters.ModifyAmount[1];
            numericUpDownModifyAmount3.Value = SimulationParameters.ModifyAmount[2];

            numericUpDownInfluenceAmount1.Value = (decimal)SimulationParameters.InfluenceAmount[0];
            numericUpDownInfluenceAmount2.Value = (decimal)SimulationParameters.InfluenceAmount[1];
            numericUpDownInfluenceAmount3.Value = (decimal)SimulationParameters.InfluenceAmount[2];

            numericUpDownTransposeChance1.Value = (decimal)SimulationParameters.TransposeChance[0];
            numericUpDownTransposeChance2.Value = (decimal)SimulationParameters.TransposeChance[1];
            numericUpDownTransposeChance3.Value = (decimal)SimulationParameters.TransposeChance[2];

            numericUpDownExchangeChance1.Value = (decimal)SimulationParameters.ExchangeChance[0];
            numericUpDownExchangeChance2.Value = (decimal)SimulationParameters.ExchangeChance[1];
            numericUpDownExchangeChance3.Value = (decimal)SimulationParameters.ExchangeChance[2];

            numericUpDownModifyChance1.Value = (decimal)SimulationParameters.ModifyChance[0];
            numericUpDownModifyChance2.Value = (decimal)SimulationParameters.ModifyChance[1];
            numericUpDownModifyChance3.Value = (decimal)SimulationParameters.ModifyChance[2];

            numericUpDownGrowthChance.Value = (decimal)SimulationParameters.GrowthChance;
            numericUpDownShrinkChance.Value = (decimal)SimulationParameters.ShrinkChance;
        }
        private void SaveParameters()
        {
            SimulationParameters.PercentDeath = (int)numericUpDownPercentDeath.Value;
            SimulationParameters.MaxSteps = (int)numericUpDownMaxSteps.Value;
            SimulationParameters.Alfa = (double)numericUpDownAlfa.Value;

            SimulationParameters.ModifyAmount[0] = (int)numericUpDownModifyAmount1.Value;
            SimulationParameters.ModifyAmount[1] = (int)numericUpDownModifyAmount2.Value;
            SimulationParameters.ModifyAmount[2] = (int)numericUpDownModifyAmount3.Value;

            SimulationParameters.InfluenceAmount[0] = (double)numericUpDownInfluenceAmount1.Value;
            SimulationParameters.InfluenceAmount[1] = (double)numericUpDownInfluenceAmount2.Value;
            SimulationParameters.InfluenceAmount[2] = (double)numericUpDownInfluenceAmount3.Value;

            SimulationParameters.TransposeChance[0] = (double)numericUpDownTransposeChance1.Value;
            SimulationParameters.TransposeChance[1] = (double)numericUpDownTransposeChance2.Value;
            SimulationParameters.TransposeChance[2] = (double)numericUpDownTransposeChance3.Value;

            SimulationParameters.ExchangeChance[0] = (double)numericUpDownExchangeChance1.Value;
            SimulationParameters.ExchangeChance[1] = (double)numericUpDownExchangeChance2.Value;
            SimulationParameters.ExchangeChance[2] = (double)numericUpDownExchangeChance3.Value;

            SimulationParameters.ModifyChance[0] = (double)numericUpDownModifyChance1.Value;
            SimulationParameters.ModifyChance[1] = (double)numericUpDownModifyChance2.Value;
            SimulationParameters.ModifyChance[2] = (double)numericUpDownModifyChance3.Value;

            SimulationParameters.GrowthChance = (double)numericUpDownGrowthChance.Value;
            SimulationParameters.ShrinkChance = (double)numericUpDownShrinkChance.Value;
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
        #endregion
    }
}