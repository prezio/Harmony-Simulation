using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PeriodicChords;

namespace HarmonyEditor
{
    public partial class PeriodEditor : UserControl
    {
        private Period ToPeriod()
        {
            uint repeats;

            if (!uint.TryParse(repeatBox.Text, out repeats))
            {
                repeats = 1;
            }

            double period = double.Parse(periodBox.Text);
            double[] divides = new double[8];

            if (!double.TryParse(divide1.Text, out divides[0]))
            {
                return new Period(period, repeats, divides, 0);
            }
            if (!double.TryParse(divide2.Text, out divides[1]))
            {
                return new Period(period, repeats, divides, 1);
            }
            if (!double.TryParse(divide3.Text, out divides[2]))
            {
                return new Period(period, repeats, divides, 2);
            }
            if (!double.TryParse(divide4.Text, out divides[3]))
            {
                return new Period(period, repeats, divides, 3);
            }
            if (!double.TryParse(divide5.Text, out divides[4]))
            {
                return new Period(period, repeats, divides, 4);
            }
            if (!double.TryParse(divide6.Text, out divides[5]))
            {
                return new Period(period, repeats, divides, 5);
            }
            if (!double.TryParse(divide7.Text, out divides[6]))
            {
                return new Period(period, repeats, divides, 6);
            }
            if (!double.TryParse(divide8.Text, out divides[7]))
            {
                return new Period(period, repeats, divides, 7);
            }

            return new Period(period, repeats, divides, 8);
        }
        private void FillPeriod(Period period)
        {
            periodBox.Text = period.PeriodA.ToString();
            repeatBox.Text = period.Repeats.ToString();
            if (period.Divides.Length >= 1)
            {
                divide1.Text = period.Divides[0].ToString();
            }
            if (period.Divides.Length >= 2)
            {
                divide2.Text = period.Divides[1].ToString();
            }
            if (period.Divides.Length >= 3)
            {
                divide3.Text = period.Divides[2].ToString();
            }
            if (period.Divides.Length >= 4)
            {
                divide4.Text = period.Divides[3].ToString();
            }
            if (period.Divides.Length >= 5)
            {
                divide5.Text = period.Divides[4].ToString();
            }
            if (period.Divides.Length >= 6)
            {
                divide6.Text = period.Divides[5].ToString();
            }
            if (period.Divides.Length >= 7)
            {
                divide7.Text = period.Divides[6].ToString();
            }
            if (period.Divides.Length >= 8)
            {
                divide8.Text = period.Divides[7].ToString();
            }
        }

        public PeriodEditor()
        {
            InitializeComponent();
        }
        public bool Valid
        {
            get;
            private set;
        }
        public Period Value
        {
            get
            {
                return ToPeriod();
            }
            set
            {
                FillPeriod(value);
            }
        }
        public void Clear()
        {
            periodBox.Text = "";
            periodBox.Enabled = true;

            repeatBox.Text = "1";
            repeatBox.Enabled = false;

            divide1.Text = "";
            divide1.Enabled = false;

            divide2.Text = "";
            divide2.Enabled = false;

            divide3.Text = "";
            divide3.Enabled = false;

            divide4.Text = "";
            divide4.Enabled = false;

            divide5.Text = "";
            divide5.Enabled = false;

            divide6.Text = "";
            divide6.Enabled = false;

            divide7.Text = "";
            divide7.Enabled = false;

            divide8.Text = "";
            divide8.Enabled = false;
        }
        public bool Default
        {
            get
            {
                return string.IsNullOrEmpty(periodBox.Text) && repeatBox.Text.Equals("1")
                    && string.IsNullOrEmpty(divide1.Text) && string.IsNullOrEmpty(divide2.Text)
                    && string.IsNullOrEmpty(divide3.Text) && string.IsNullOrEmpty(divide4.Text)
                    && string.IsNullOrEmpty(divide5.Text) && string.IsNullOrEmpty(divide6.Text);
            }
        }

        [Category("Action")]
        [Description("Fires when the period is changed")]
        public EventHandler WasValidated  ;
        protected virtual void onValidation()
        {
            EventHandler temp = WasValidated;
            if (temp != null)
            {
                temp(this, new EventArgs());
            }
        }

        #region Events
        private void periodBox_TextChanged(object sender, EventArgs e)
        {
            int temp;
            bool temp2 = int.TryParse(periodBox.Text, out temp);
            repeatBox.Enabled = temp2;
            divide1.Enabled = temp2;
            Valid = temp2;
            onValidation();
        }
        private void divide1_TextChanged(object sender, EventArgs e)
        {
            int temp;
            divide2.Enabled = int.TryParse(divide1.Text, out temp);
        }
        private void divide2_TextChanged(object sender, EventArgs e)
        {
            int temp;
            divide3.Enabled = int.TryParse(divide2.Text, out temp);
        }
        private void divide3_TextChanged(object sender, EventArgs e)
        {
            int temp;
            divide4.Enabled = int.TryParse(divide3.Text, out temp);
        }
        private void divide4_TextChanged(object sender, EventArgs e)
        {
            int temp;
            divide5.Enabled = int.TryParse(divide4.Text, out temp);
        }
        private void divide5_TextChanged(object sender, EventArgs e)
        {
            int temp;
            divide6.Enabled = int.TryParse(divide5.Text, out temp);
        }
        private void divide6_TextChanged(object sender, EventArgs e)
        {
            int temp;
            divide7.Enabled = int.TryParse(divide6.Text, out temp);
        }
        private void divide7_TextChanged(object sender, EventArgs e)
        {
            int temp;
            divide8.Enabled = int.TryParse(divide7.Text, out temp);
        }
        #endregion
    }
}
