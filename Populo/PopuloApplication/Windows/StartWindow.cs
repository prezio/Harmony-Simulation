using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PopuloApplication
{
    public partial class StartWindow : Form
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxIP.Text) || string.IsNullOrEmpty(textBoxPort.Text))
            {
                MessageBox.Show("Nie wypełniłeś wszystkich pól.");
                return;
            }
            int port;
            bool parseError = !int.TryParse(textBoxPort.Text, out port);

            if (parseError)
            {
                MessageBox.Show("Pole port nie jest liczbą");
                return;
            }

            Hide();
            MainWindow window = new MainWindow();
            window.ShowDialog();

            Show();
        }
    }
}
