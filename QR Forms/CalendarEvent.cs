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
    public partial class CalendarEvent : Form
    {
        public CalendarEvent()
        {
            InitializeComponent();
            FormClosing += (s, a) =>
            {
                if (DialogResult != DialogResult.OK)
                    DialogResult = DialogResult.Cancel;
            };
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(1);
        }
        public string subj, desc, loc;
        public DateTime start, end;
        public bool allday;
        private void button1_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value == dateTimePicker2.Value)
            {
                MessageBox.Show("Start and end time shouldn't be same.");
                return;
            }
            subj = textBox1.Text;
            desc = textBox2.Text;
            loc = textBox3.Text;
            start = dateTimePicker1.Value;
            end = dateTimePicker2.Value;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
