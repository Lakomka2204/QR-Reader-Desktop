using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QR_Reader_Desktop.QR_Forms
{
    public partial class Phone : Form
    {
        public Phone()
        {
            InitializeComponent();
            FormClosing += (s, a) =>
            {
                if (DialogResult != DialogResult.OK)
                    DialogResult = DialogResult.Cancel;
            };
        }
        public string tel;
        private void button1_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(textBox1.Text, @"^\+?[1-9][0-9]{7,14}$"))
            {
                MessageBox.Show("This is not a phone number.");
                return;
            }
            DialogResult = DialogResult.OK;
            tel = textBox1.Text;
            Close();
        }
    }
}
