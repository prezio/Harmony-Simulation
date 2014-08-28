using System;
using System.Windows.Forms;

namespace PopuloApplication
{
    public class PerCentNumericUpDown : NumericUpDown
    {
        public PerCentNumericUpDown()
        {
        }

        protected override void UpdateEditText()
        {
            // Append '%' to the end of the numeric value
            this.Text = this.Value + " %";
        }
    }
}
