using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QR_Reader_Desktop.QR_Forms
{
    public partial class Bookmark : Form
    {
        public Bookmark()
        {
            InitializeComponent();
            FormClosing += (s, a) =>
            {
                if (DialogResult != DialogResult.OK)
                    DialogResult = DialogResult.Cancel;
            };
        }
        public string title, url;

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(textBox2.Text, @"^https?:\/\/(?:www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b(?:[-a-zA-Z0-9()@:%_\+.~#?&\/=]*)$"))
            {
                MessageBox.Show("This is not URL.");
                return;
            }
            title = textBox1.Text;
            url = textBox2.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
