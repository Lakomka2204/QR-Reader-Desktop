using System;
using System.Windows.Forms;

namespace QR_Reader_Desktop.QR_Forms
{
    public partial class Wifi : Form
    {
        public Wifi()
        {
            InitializeComponent();
            comboBox1.DataSource = Enum.GetNames(typeof(QRCoder.PayloadGenerator.WiFi.Authentication));
            FormClosing += (s, a) =>
            {
                if (DialogResult != DialogResult.OK)
                    DialogResult = DialogResult.Cancel;
            };
        }
        public string ssid, password;
        public bool hidden;
        public QRCoder.PayloadGenerator.WiFi.Authentication authentication;

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("SSID cannot be empty");
                return;
            }
            ssid = textBox1.Text;
            password = textBox2.Text;
            hidden = checkBox1.Checked;
            authentication = (QRCoder.PayloadGenerator.WiFi.Authentication)Enum.Parse(typeof(QRCoder.PayloadGenerator.WiFi.Authentication), comboBox1.SelectedItem.ToString(), true);
            DialogResult = DialogResult.OK;
        }


    }
}
