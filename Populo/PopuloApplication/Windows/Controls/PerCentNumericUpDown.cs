using System;
using System.Windows.Forms;

namespace PopuloApplication
{
    /// <summary>
    /// Defined Percent numericupdown control
    /// </summary>
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
