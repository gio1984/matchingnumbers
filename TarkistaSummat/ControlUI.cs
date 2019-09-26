using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

namespace TarkistaSummat
{
    public partial class ControlUI : UserControl
    {
        //private TarkistaSummat ts = new TarkistaSummat();
        private Color color;
        BackgroundWorker worker;
        public int count;
        DoWorkEventArgs workEventArgs;
        int resultQty;
        public class CellsRemain
        {
            public Range Summa { get; set; }
        }
        List<CellsRemain> resultKomb;
        public ControlUI()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            worker = sender as BackgroundWorker;
            workEventArgs = e;
            Range cellSel = Globals.ThisAddIn.Application.Selection as Range;

            bool komb = false;
            int maxkomb = 0;
            if (radioKomb.Checked == true)
            {
                komb = true;
                if (comboBoxMax.InvokeRequired)
                {
                    comboBoxMax.Invoke(new System.Action(() =>
                    {
                        maxkomb = Convert.ToInt32(comboBoxMax.SelectedItem.ToString());
                    }));
                }
                else
                {
                    maxkomb = Convert.ToInt32(comboBoxMax.SelectedItem.ToString());
                }
            }
            TarkistaPari(cellSel, komb, maxkomb);

        }



        private void ButtonStart_Click(object sender, EventArgs e)
        {
            if (Globals.ThisAddIn.Application.ActiveSheet.UsedRange.Rows.Count < 2 || Globals.ThisAddIn.Application.ActiveSheet == null)
            {
                MessageBox.Show("No data", "Error");
            }
            else
            {
                buttonCancel.Enabled = true;
                buttonStart.Enabled = false;
                System.Threading.SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
                if (backgroundWorker1.IsBusy != true)
                {
                    backgroundWorker1.RunWorkerAsync();
                }
            }
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //Text = e.ProgressPercentage.ToString();
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                MessageBox.Show("Canceled");
            }

            buttonStart.Enabled = true;
            buttonCancel.Enabled = false;
        }

        private void ControlUI_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void ControlUI_Enter(object sender, EventArgs e)
        {
            Range cellSel = Globals.ThisAddIn.Application.Selection as Range;

            if (cellSel.Cells.Count > 1)
            {
                string txt = cellSel.Cells.Address;
                textRange.Text = txt.Replace("$", string.Empty);
            }
            else
            {
                string txt = cellSel.Cells.Address;
                txt = Regex.Replace(txt, @"[\d-]", string.Empty);
                textRange.Text = "Sarake " + txt.Replace("$", string.Empty);

            }
        }

        private void Colorbtn_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            textBoxColor.BackColor = colorDialog1.Color;
            color = colorDialog1.Color;
        }

        private void ControlUI_MouseHover(object sender, EventArgs e)
        {
            Range cellSel = Globals.ThisAddIn.Application.Selection as Range;

            if (cellSel.Cells.Count > 1)
            {
                string txt = cellSel.Cells.Address;
                textRange.Text = txt.Replace("$", string.Empty);
            }
            else
            {
                string txt = cellSel.Cells.Address;
                txt = Regex.Replace(txt, @"[\d -]", string.Empty);
                textRange.Text = "Sarake " + txt.Replace("$", string.Empty);
            }
        }

        private void ControlUI_Load(object sender, EventArgs e)
        {
            colorDialog1.Color = Color.Yellow;
            color = colorDialog1.Color;
            textBoxColor.BackColor = colorDialog1.Color;
            comboBoxMax.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void RadioKomb_CheckedChanged(object sender, EventArgs e)
        {
            if (radioKomb.Checked == true)
            {
                comboBoxMax.Enabled = true;
                comboBoxMax.SelectedIndex = 0;
            }
            else
            {
                comboBoxMax.Enabled = false;
            }
        }

        public void TarkistaPari(Range selRange, bool komb, int maxKomb)
        {
            Worksheet actSheet = Globals.ThisAddIn.Application.ActiveSheet;
            Range cellSel = Globals.ThisAddIn.Application.ActiveCell;
            //Range fromCell;
            //List<Range> remainCell = new List<Range>();
            List<CellsRemain> remainCell = new List<CellsRemain>();
            List<Range> ranges = new List<Range>();

            bool found;
            count = 1;
            resultQty = 0;
            if (actSheet != null && cellSel != null)
            {

                decimal sumCoverted = 0;
                object[,] arrVal;
                List<Range> selCol;
                List<decimal> listValues = new List<decimal>();
                if (selRange.Count > 1)
                {
                    arrVal = selRange.Value;
                    selCol = selRange.Cast<Range>().Select(r => r).ToList();
                }
                else
                {
                    var untilCell = (Range)actSheet.Cells[actSheet.UsedRange.Rows.Count, selRange.Cells.Column];
                    var startCell = (Range)actSheet.Cells[1, selRange.Cells.Column];
                    selCol = actSheet.get_Range(startCell, untilCell).Cast<Range>().Select(r => r).ToList(); ;
                    //arrVal = actSheet.get_Range(startCell, untilCell).Value.XlRangeValueDataType.IsArray;
                }

                foreach(Range item in selCol)
                {
                    decimal addElement = Convert.ToDecimal(item.Value);
                    listValues.Add(addElement);
                }

                if (selCol.Count > 2)
                {
                    textBoxStatus.Invoke(new System.Action(() => textBoxStatus.Text = "Etsi pari"));
                }
                //object[,] arrVal;
                //var arrVal = selRange.Value;
                int index = 0;
                
                //foreach (Range element in selCol)
                for(int j = 0; j < listValues.Count; j++) //test
                {
                    if (worker.CancellationPending)
                    {
                        workEventArgs.Cancel = true;
                    }
                    found = false;

                    worker.ReportProgress(count);

                    count++;
                    //var col = element.Interior.Color;
                    //var col2 = ColorTranslator.ToOle(color);
                    //if (element.Value is string || element.Interior.Color == ColorTranslator.ToOle(color) || element.Value == null || element.Value is DateTime || element.EntireRow.Hidden == true || element.EntireColumn.Hidden == true) //check cell value - Hidden can work with filter
                    if(selCol[j].Value is string || selCol[j].Interior.Color == ColorTranslator.ToOle(color) || selCol[j].Value == null || selCol[j].Value is DateTime || selCol[j].EntireRow.Hidden == true || selCol[j].EntireColumn.Hidden == true) //test
                    {
                        continue;
                    }
                    else
                    {
                        decimal keySearch = listValues[j] * -1; //Convert.ToDecimal(element.Value * -1);
                        //int index = element.
                        index++;
                        for (int i = index; i <= listValues.Count - 1; i++) //selCol -> listValues
                        //foreach(Range el2 in selCol)
                        {

                            if (selCol[i].Value is string || selCol[i].Value == null)
                            {
                                continue;
                            }
                            else
                            {

                                try
                                {
                                    sumCoverted = listValues[i]; //Convert.ToDecimal(selCol[i].Value);
                                }
                                catch (Exception)
                                {
                                    continue;
                                }



                                if (sumCoverted == keySearch && selCol[i].Interior.Color != ColorTranslator.ToOle(color) && selCol[i].EntireRow.Hidden != true)
                                {
                                    selCol[j].Interior.Color = color; //element
                                    selCol[i].Interior.Color = color;
                                    resultQty += 2;
                                    found = true;
                                    break;
                                }

                            }

                        }

                        if (!found)
                        {
                            CellsRemain notFound = new CellsRemain
                            {
                                Summa = selCol[j]
                            };
                            remainCell.Add(notFound);
                        }
                    }

                }
            }
            if (komb)
            {

                if (remainCell.Count > 300 && maxKomb == 10)
                {
                    MessageBox.Show("Ei voi tarkista kombinaation koska se on lian paljion numeroja", "Error");
                }
                else if (remainCell.Count > 150 && maxKomb == 15)
                {
                    MessageBox.Show("Ei voi tarkista kombinaation koska se on lian paljion numeroja", "Error");
                }
                else if (remainCell.Count > 20 && maxKomb == 20)
                {
                    MessageBox.Show("Ei voi tarkista kombinaation koska se on lian paljion numeroja", "Error");
                }
                else
                {
                    textBoxStatus.Invoke(new System.Action(() => textBoxStatus.Text = "Etsi kombinaatio"));
                    HashSet<int> result = FindKomb(remainCell, 0m, maxKomb);
                    resultKomb = new List<CellsRemain>();
                    foreach (int foundIndex in result)
                    {
                        remainCell[foundIndex].Summa.Interior.Color = color;
                    }
                    resultQty += result.Count;
                }
            }
            textBoxStatus.Invoke(new System.Action(() => textBoxStatus.Text = "Loppunut - löytyy kpl " + resultQty.ToString()));
        }

        public HashSet<int> FindKomb(List<CellsRemain> list, decimal sum, int maxKomb)
        {
            //Dictionary<decimal, string> keyValues = new Dictionary<decimal, string>();
            List<decimal> listDecimal = new List<decimal>();
            List<int> listRange = new List<int>(); //string
            List<decimal> resDecimal = new List<decimal>();
            List<IEnumerable<decimal>> resDecimal2 = new List<IEnumerable<decimal>>();
            List<IEnumerable<int>> resIndex = new List<IEnumerable<int>>(); //string
            Dictionary<decimal, int> dicSumma = new Dictionary<decimal, int>();
            int indexList = 0;
            int count = 0;
            foreach (CellsRemain el in list)
            {
                decimal sumDec = Convert.ToDecimal(el.Summa.Value);
                //keyValues.Add(sumDec, el.Summa.Cells.Row.ToString() + el.Summa.Cells.Column.ToString());
                listDecimal.Add(sumDec);
                //string row = el.Summa.Cells.Row.ToString();
                //string col = el.Summa.Cells.Column.ToString();
                //string range = el.Summa.Cells.Row.ToString() + ":" + el.Summa.Cells.Column.ToString();
                listRange.Add(indexList);
                indexList++;
            }

            var subsets = new List<IEnumerable<decimal>> { new List<decimal>() };
            var subsetsIndex = new List<IEnumerable<int>> { new List<int>() }; //string
        
            for (int i = 0; i < listDecimal.Count; i++)
            {
                if (worker.CancellationPending)
                {
                    workEventArgs.Cancel = true;
                }
                int max = maxKomb;
                count = i;
                worker.ReportProgress(count);
                if (i + max > listDecimal.Count - 1)
                {
                    max = (listDecimal.Count) - i;
                }

                List<decimal> subDec = new List<decimal>();
                subDec = listDecimal.GetRange(i, max);
                List<IEnumerable<decimal>> parSubsets = new List<IEnumerable<decimal>>
                    {
                        subDec
                    };
                List<int> subIndex = listRange.GetRange(i, max); //string
                List<IEnumerable<int>> parSubsetsIndex = new List<IEnumerable<int>>
                    {
                        subIndex
                    };

                var powerSetdec = from m in Enumerable.Range(0, 1 << subDec.Count)
                                  select
                                     from n in Enumerable.Range(0, subDec.Count)
                                     where (m & (1 << n)) != 0
                                     select subDec[n];

                subsets.AddRange(powerSetdec.ToList());
                var powerSetIndex = from m in Enumerable.Range(0, 1 << subIndex.Count)
                                    select
                                       from n in Enumerable.Range(0, subIndex.Count)
                                       where (m & (1 << n)) != 0
                                       select subIndex[n];
                subsetsIndex.AddRange(powerSetIndex.ToList());

            }

            HashSet<int> takenIndex = new HashSet<int>();
            for (int i = 1; i < subsets.Count; i++)
            {
                if (worker.CancellationPending)
                {
                    workEventArgs.Cancel = true;
                }
                count = i;
                worker.ReportProgress(count);
                if (subsets[i] != null)
                {
                    bool taken = false;
                    decimal sumDec = subsets[i].Sum();

                    if (sumDec == 0m)
                    {
                        foreach (int resInd in subsetsIndex[i])
                        {
                            if (takenIndex.Contains(resInd))
                            {
                                taken = true;
                                break;
                            }
                        }
                        if (!taken)
                        {
                            resDecimal2.Add(subsets[i]);
                            resIndex.Add(subsetsIndex[i]);
                            foreach (int ind in subsetsIndex[i])
                            {
                                takenIndex.Add(ind);
                            }
                            continue;
                        }
                    }
                    else
                    {
                        if (dicSumma.TryGetValue(sumDec * -1, out int index))
                        {
                            IEnumerable<decimal> intersectSub = subsets[i].Intersect(subsets[index]);
                            if (intersectSub.Count() == 0)
                            {
                                IEnumerable<decimal> addComb = subsets[i].Concat(subsets[index]);
                                var indexComb = subsetsIndex[i].Concat(subsetsIndex[index]);
                                decimal sumComb = addComb.Sum();
                                foreach (int resInd in indexComb)
                                {
                                    if (takenIndex.Contains(resInd))
                                    {
                                        taken = true;
                                        break;
                                    }
                                }

                                if (!taken)
                                {
                                    resDecimal2.Add(addComb);
                                    resIndex.Add(indexComb);
                                    dicSumma.Remove(sumDec * -1);
                                    foreach (int ind in indexComb)
                                    {
                                        takenIndex.Add(ind);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!dicSumma.ContainsKey(sumDec))
                            {
                                dicSumma.Add(sumDec, i);
                            }
                        }
                    }
                }
            }

            return takenIndex;

        }

        private void comboBoxMax_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void buttonStart_MouseHover(object sender, EventArgs e)
        {
            Range cellSel = Globals.ThisAddIn.Application.Selection as Range;

            if (cellSel.Cells.Count > 1)
            {
                string txt = cellSel.Cells.Address;
                textRange.Text = txt.Replace("$", string.Empty);
            }
            else
            {
                string txt = cellSel.Cells.Address;
                txt = Regex.Replace(txt, @"[\d -]", string.Empty);
                textRange.Text = "Sarake " + txt.Replace("$", string.Empty);
            }
        }
    }
}



