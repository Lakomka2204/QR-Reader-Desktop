using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QR_Reader_Desktop.QR_Forms
{
    public partial class URL : Form
    {
        public URL()
        {
            InitializeComponent();
            FormClosing += (s, a) =>
            {
                if (DialogResult != DialogResult.OK)
                    DialogResult = DialogResult.Cancel;
            };
        }
        public string url;
        private void button1_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(textBox1.Text, @"^https?:\/\/(?:www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b(?:[-a-zA-Z0-9()@:%_\+.~#?&\/=]*)$"))
            {
                MessageBox.Show("This is not URL.");
                return;
            }
            DialogResult = DialogResult.OK;
            url = textBox1.Text;
            Close();
        }
    }
}
