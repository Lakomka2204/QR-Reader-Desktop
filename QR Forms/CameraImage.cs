using AForge.Video.DirectShow;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ZXing;

namespace QR_Reader_Desktop.QR_Forms
{
    public partial class CameraImage : Form
    {
        public CameraImage()
        {
            InitializeComponent();
            FormClosing += (s, a) =>
            {
                if (DialogResult != DialogResult.OK)
                    DialogResult = DialogResult.Cancel;
            };
        }
        FilterInfo[] devices;
        VideoCaptureDevice currentDevice;
        public Image lastFrame;
        public Result QRResult;
        CamStatus cstatus = CamStatus.Disabled;
        void RefreshCameras()
        {
            devices = new FilterInfoCollection(FilterCategory.VideoInputDevice).Cast<FilterInfo>().ToArray();
            comboBox1.Items.Clear();
            if (devices.Length == 0)
            {
                MessageBox.Show("No cameras detected.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (var device in devices)
                comboBox1.Items.Add(device.Name);
            comboBox1.SelectedIndex = 0;

        }
        void StartCapture()
        {
            currentDevice = new VideoCaptureDevice(devices[comboBox1.SelectedIndex].MonikerString);
            currentDevice.NewFrame += CurrentDevice_NewFrame;
            currentDevice.Start();
            cstatus = CamStatus.Waiting_for_signal;
            DrawText();
        }
        void DrawText(Graphics g = null)
        {
            if (g == null)
                g = pictureBox1.CreateGraphics();
            //if (currentDevice != null && !currentDevice.IsRunning)
                g.Clear(Color.Black);
            g.DrawString(cstatus.ToString().Replace('_', ' '), new Font(SystemFonts.DefaultFont.FontFamily,
                24, FontStyle.Regular), Brushes.White, 10, 10);
        }
        void StopCapture()
        {
            cstatus = CamStatus.Disabled;
            currentDevice?.SignalToStop();
            //currentDevice?.WaitForStop();
            currentDevice = null;
            DrawText();
        }
        void CurrentDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs e)
        {
            if (cstatus != CamStatus.Waiting_for_scan)
                cstatus = CamStatus.Waiting_for_scan;
            Bitmap img = e.Frame.Clone() as Bitmap;
            Graphics g = Graphics.FromImage(img);
            g.DrawString(cstatus.ToString().Replace('_', ' '), new Font(SystemFonts.DefaultFont.FontFamily,
                32, FontStyle.Regular), Brushes.White, 10, 10);
            pictureBox1.Image = img;
            var res = QR.Scan.FromImageFast((Bitmap)e.Frame.Clone());
            if (res == null) return;
            cstatus = CamStatus.Scanning;
            QRResult = res;
            lastFrame = (Image)e.Frame.Clone();
            DialogResult = DialogResult.OK;
            StopCapture();
            Close();

        }
        private void CameraImage_Load(object sender, EventArgs e)
        {
            RefreshCameras();
            DrawText();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentDevice != null && currentDevice.IsRunning)
            {
                StopCapture();
                button2.Text = "Start";
            }
            else
            {
                if (devices != null && devices.Length == 0)
                {
                    MessageBox.Show("There are no cameras available");
                    return;
                }
                StartCapture();
                button2.Text = "Stop";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshCameras();
        }

        private void CameraImage_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentDevice != null && currentDevice.IsRunning)
                StopCapture();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (cstatus == CamStatus.Waiting_for_scan) return;
            DrawText(e.Graphics);
        }
        enum CamStatus
        {
            Disabled,
            Waiting_for_signal,
            Waiting_for_scan,
            Scanning,
        }
    }
}
