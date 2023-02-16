using System;
using System.Windows.Forms;

namespace QR_Reader_Desktop.QR_Forms
{
    public partial class Regular : Form
    {
        public Regular()
        {
            InitializeComponent();
            FormClosing += (s, a) =>
            {
                if (DialogResult != DialogResult.OK)
                    DialogResult = DialogResult.Cancel;
            };
        }
        public string text;
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Text cannot be empty");
                return;
            }
            DialogResult = DialogResult.OK;
            text = textBox1.Text;
            Close();
        }
    }
}
