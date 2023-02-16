using System;
using System.Windows.Forms;

namespace QR_Reader_Desktop
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }
        void UpdateQR()
        {
            pictureBox1.Image = QR.Create.FromText("example");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog()
            {
                Color = QR.DarkColor,
            })
            {
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    Settings.DarkColor = cd.Color.ToArgb();
                    UpdateQR();
                }
            }
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = Settings.PPU;
            numericUpDown2.Value = Settings.IconSize;
            numericUpDown3.Value = Settings.BorderRadius;
            checkBox1.Checked = Settings.DrawQuietZones;
            UpdateQR();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog()
            {
                Color = QR.LightColor,
            })
            {
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    Settings.LightColor = cd.Color.ToArgb();
                    UpdateQR();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog op = new OpenFileDialog()
            {
                Title = "Select icon",
                Multiselect = false,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Filter = "Image|*.BMP;*.JPG;*.PNG",
            })
            {
                if (op.ShowDialog() == DialogResult.OK)
                {
                    Settings.IconPath = op.FileName;
                    UpdateQR();
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Settings.PPU = (int)numericUpDown1.Value;
            UpdateQR();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Settings.Save();
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Settings.Load();
            Close();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Settings.IconSize = (int)numericUpDown2.Value;
            UpdateQR();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            Settings.BorderRadius = (int)numericUpDown3.Value;
            UpdateQR();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog()
            {
                Color = QR.IconBorderColor,
            })
            {
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    Settings.IconBorderColor = cd.Color.ToArgb();
                    UpdateQR();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.DrawQuietZones = checkBox1.Checked;
            UpdateQR();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Settings.IconPath = "";
            UpdateQR();
        }
    }
}
