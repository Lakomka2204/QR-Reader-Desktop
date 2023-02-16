using QR_Reader_Desktop.QR_Forms;
using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing.OneD;

namespace QR_Reader_Desktop
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            Settings.Load();
        }
        Bitmap GetAtTab(int index)
        {
            return (Bitmap)((PictureBox)tabControl1.TabPages[index].Controls[0]).Image;
        }
        PictureBox CreatePictureBox(Bitmap origin)
            => new PictureBox
            {
                Image = origin,
                SizeMode = PictureBoxSizeMode.Zoom,
                ContextMenu = new ContextMenu(new MenuItem[2]
            {
                new MenuItem("Copy", (_,__) => Clipboard.SetImage(origin), Shortcut.CtrlC),
                new MenuItem("Save",(_,__) => SaveImage(origin), Shortcut.CtrlS)
            }),
                Dock = DockStyle.Fill
            };

        void AddQRTab(string title, Bitmap image, string result)
        {
            TabPage tp = new TabPage($"#{tabControl1.TabCount + 1} {title}");
            tp.Controls.Add(CreatePictureBox(image));
            tp.Tag = result;
            tabControl1.Controls.Add(tp);
            //QRs.Add(image);
            tabControl1_SelectedIndexChanged(null, EventArgs.Empty);
            tabControl1.SelectedIndex = tabControl1.TabCount - 1;
        }
        void SaveImage(Bitmap image)
        {
            using (SaveFileDialog sf = new SaveFileDialog()
            {
                Title = "Save image",
                FileName = Path.ChangeExtension(Path.GetRandomFileName(), "png"),
                Filter = "Image|*.png",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            })
            {
                if (sf.ShowDialog() == DialogResult.OK)
                    image.Save(sf.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = tabControl1.SelectedIndex >= 0 ? string.Format("Result: {0}", tabControl1.TabPages[tabControl1.SelectedIndex].Tag.ToString()) : "Result:";
        }

        private void Main_KeyUp(object sender, KeyEventArgs e)
        {
            if (!e.Control) return;
            switch (e.KeyCode)
            {
                case Keys.S:
                    if (tabControl1.TabCount > 0)
                        SaveImage(GetAtTab(tabControl1.SelectedIndex));
                    break;
                case Keys.C:
                    if (tabControl1.TabCount > 0)
                        Clipboard.SetImage(GetAtTab(tabControl1.SelectedIndex));
                    break;
                case Keys.V:
                    InsertData();
                    break;
            }

        }

        private void InsertData()
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Bitmap))
                InsertImage((Bitmap)Clipboard.GetImage(), "Clipboard");
            else if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                string text = Clipboard.GetText();
                var m = MessageBox.Show($"Do you want to convert clipboard text to QR code?\nText: {text}", "Clipboard", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (m == DialogResult.Yes)
                    AddQRTab("Clipboard text", QR.Create.FromText(text), text);
            }
        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            var tabControl = sender as TabControl;
            var tabs = tabControl.TabPages;

            if (e.Button == MouseButtons.Middle)
            {
                tabs.Remove(tabs.Cast<TabPage>()
                        .Where((t, i) => tabControl.GetTabRect(i).Contains(e.Location))
                        .First());
            }
            GC.Collect();
        }
        private void regularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Regular reg = new Regular())
            {
                if (reg.ShowDialog() == DialogResult.OK)
                {
                    var img = QR.Create.FromText(reg.text);
                    AddQRTab(reg.Text, img, reg.text);
                }
            }
        }
        private void wifiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Wifi wifi = new Wifi())
            {
                if (wifi.ShowDialog() == DialogResult.OK)
                {
                    var img = QR.Create.FromWifi(wifi.ssid, wifi.password, wifi.hidden, wifi.authentication);
                    AddQRTab(wifi.Text, img, QR.Scan.FromImage(img));
                }
            }
        }

        private void phoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Phone phone = new Phone())
            {
                if (phone.ShowDialog() == DialogResult.OK)
                {
                    var img = QR.Create.FromPhone(phone.tel);
                    AddQRTab(phone.Text, img, QR.Scan.FromImage(img));
                }
            }
        }

        private void sMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Phone sms = new Phone()
            {
                Text = "SMS"
            })
            {
                if (sms.ShowDialog() == DialogResult.OK)
                {
                    var img = QR.Create.FromSMS(sms.tel);
                    AddQRTab(sms.Text, img, QR.Scan.FromImage(img));
                }
            }
        }

        private void uRLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (URL url = new URL())
            {
                if (url.ShowDialog() == DialogResult.OK)
                {
                    var img = QR.Create.FromUrl(url.url);
                    AddQRTab(url.Text, img, QR.Scan.FromImage(img));
                }
            }
        }

        private void calendarEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (CalendarEvent ce = new CalendarEvent())
            {
                if (ce.ShowDialog() == DialogResult.OK)
                {
                    var img = QR.Create.FromCalendarEvent(ce.subj, ce.desc, ce.loc, ce.start, ce.end, ce.allday);
                    AddQRTab(ce.Text, img, QR.Scan.FromImage(img));
                }
            }
        }

        private void bookmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Bookmark bm = new Bookmark())
            {
                if (bm.ShowDialog() == DialogResult.OK)
                {
                    var img = QR.Create.FromBookmark(bm.title, bm.url);
                    AddQRTab(bm.Text, img, QR.Scan.FromImage(img));
                }
            }
        }

        private void mailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Mail mail = new Mail())
            {
                if (mail.ShowDialog() == DialogResult.OK)
                {
                    var img = QR.Create.FromMail(mail.to, mail.subj, mail.msg);
                    AddQRTab(mail.Text, img, QR.Scan.FromImage(img));
                }
            }
        }
        void InsertImage(Bitmap img, string source)
        {
            var res = QR.Scan.FromImageMultiple(img);
            string resnormal = null;
            if (res == null) resnormal = "No result";
            else
                for (int i = 0; i < res.Length; i++)
                    resnormal += $"\r\n#{i + 1}: {res[i].Text}";
            AddQRTab(source, img, resnormal);
        }
        private void fromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press Ctrl + V to insert image.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void fromImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog op = new OpenFileDialog()
            {
                Title = "Select image",
                Multiselect = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Filter = "Image|*.BMP;*.JPG;*.PNG",
            })
            {
                if (op.ShowDialog() == DialogResult.OK)
                {
                    foreach (var img in op.FileNames)
                    {
                        try
                        {
                            var bm = Image.FromFile(img);
                            InsertImage((Bitmap)bm, "File");
                        }
                        catch (OutOfMemoryException)
                        {
                            MessageBox.Show($"File \"{img}\" does not have a valid image format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }

        private void fromCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (CameraImage ci = new CameraImage())
                if (ci.ShowDialog() == DialogResult.OK)
                    AddQRTab(ci.Text, (Bitmap)ci.lastFrame, ci.QRResult.Text);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SettingsForm s = new SettingsForm())
                s.ShowDialog();
            GC.Collect();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (About a = new About())
                a.ShowDialog();
        }
    }
}
