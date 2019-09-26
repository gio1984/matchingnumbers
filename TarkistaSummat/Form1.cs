using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TarkistaSummat
{
    public partial class Form1 : Form
    {
        //public int count = 0;
        private TarkistaSummat ts = new TarkistaSummat();
        private int selComBox = 0;
        //private int selCol;

        public class CellsRemain
        {
            public Range Value { get; set; }
            public Range Desc { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void Main()
        {

        }

        private void BtnStart_Click(object sender, EventArgs e)
        {

            if (Globals.ThisAddIn.Application.ActiveSheet.UsedRange.Rows.Count < 2 || Globals.ThisAddIn.Application.ActiveSheet == null)
            {
                MessageBox.Show("No data", "Error");
            }
            else
            {
                label3.Visible = true;
                label4.Visible = true;
                progressBar1.Visible = true;
                btnCancel.Visible = true;
                btnCancel.Enabled = true;
                btnStart.Enabled = false;
                comboBox1.Enabled = false;
                progressBar1.Minimum = 2;
                progressBar1.Maximum = Globals.ThisAddIn.Application.ActiveSheet.UsedRange.Rows.Count;
                progressBar1.Step = 1;
                progressBar1.Value = 2;
                Refresh();
                //label6.Text = "Täsmäytys";
                selComBox = Convert.ToInt16(comboBox1.SelectedItem);
                //char c = Convert.ToChar(textBox1.Text);
                //selCol = ((int) char.ToUpper(c)) - 64;
                System.Threading.SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
                if (backgroundWorker1.IsBusy != true)
                {
                    backgroundWorker1.RunWorkerAsync();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Range cellSel = Globals.ThisAddIn.Application.ActiveCell;
            label2.Visible = true;
            label2.Text = cellSel.Column.ToString();
        }

        private void Form1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            Range cellSel = Globals.ThisAddIn.Application.ActiveCell;
            if (cellSel.Column.ToString() != null)
            {
                label2.Text = cellSel.Column.ToString();
                Refresh();
            }
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            label2.Text = "";
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                backgroundWorker1.CancelAsync();
                btnCancel.Enabled = false;
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                label5.Visible = true;
                comboBox1.SelectedIndex = 0;
                comboBox1.Visible = true;
                //checkBox2.Visible = true;
                //textBox1.Visible = true;
                //label7.Visible = true;
            }
            else
            {
                label5.Visible = false;
                comboBox1.Visible = false;
                //checkBox2.Visible = false;
                //textBox1.Visible = false;
                //label7.Visible = false;
            }
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //BackgroundWorker worker = sender as BackgroundWorker;

            ////var remainCell = ts.Main(worker, e, selCol);

            //if (checkBox1.Checked && remainCell != null) //comboBox1.SelectedItem != null
            //{
            //    if (progressBar1.InvokeRequired)
            //    {
            //        progressBar1.Invoke(new System.Action(() =>
            //        {
            //            progressBar1.Minimum = 1;
            //            progressBar1.Maximum = remainCell.Count;
            //            progressBar1.Step = 1;
            //            progressBar1.Value = 1;
            //        }
            //                       ));
            //    }
            //    else
            //    {
            //        progressBar1.Minimum = 1;
            //        progressBar1.Maximum = remainCell.Count;
            //        progressBar1.Step = 1;
            //        progressBar1.Value = 1;
            //    }
            //    //label6.Text = "Kombinaatio";
            //    TarkistaKomb tk = new TarkistaKomb();
            //    tk.Main(selComBox, remainCell, worker, e);
            //}
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.PerformStep();
            label4.Text = e.ProgressPercentage.ToString();
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                MessageBox.Show("Canceled");
            }
            btnStart.Enabled = true;
            btnCancel.Enabled = false;
            comboBox1.Enabled = true;
            //progressBar1.Value = 1;
            label6.Text = "";
        }
    }
}
