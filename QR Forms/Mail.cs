using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QR_Reader_Desktop.QR_Forms
{
    public partial class Mail : Form
    {
        public Mail()
        {
            InitializeComponent();
            FormClosing += (s, a) =>
            {
                if (DialogResult != DialogResult.OK)
                    DialogResult = DialogResult.Cancel;
            };
        }
        public string to, subj, msg;

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(textBox1.Text, @"^\S+@\S+\.\S+$"))
            {
                MessageBox.Show("To should be an email.");
                return;
            }

            to = textBox1.Text;
            subj = textBox2.Text;
            msg = richTextBox1.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
