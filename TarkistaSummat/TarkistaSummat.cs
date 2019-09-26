using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TarkistaSummat
{
    class TarkistaSummat
    {
        public int count;

        public class CellsRemain
        {
            public Range Summa { get; set; }
            //public Range Desc { get; set; }
        }

        public List<CellsRemain> Main(BackgroundWorker worker, DoWorkEventArgs e, System.Drawing.Color color, Range selRange, bool komb, ProgressBar progressBar, int maxKomb)
        {
            Worksheet actSheet = Globals.ThisAddIn.Application.ActiveSheet;
            Range cellSel = Globals.ThisAddIn.Application.ActiveCell;
            //Range fromCell;
            //List<Range> remainCell = new List<Range>();
            List<CellsRemain> remainCell = new List<CellsRemain>();
            List<Range> ranges = new List<Range>();
            
            bool found;
            count = 1;
            if (actSheet != null && cellSel != null)
            {

                //int column = .Column;
                //int rowAdd;
                //if (actSheet.Cells[1, column].Value is string) //check header
                //{
                //    fromCell = (Range)actSheet.Cells[2, column];
                //    rowAdd = 1;
                //}
                //else
                //{
                //    fromCell = (Range)actSheet.Cells[1, column];
                //    rowAdd = 0;
                //}
                decimal sumCoverted = 0;
                object[,] arrVal;
                List<Range> selCol;
                if (selRange.Count > 1)
                {
                    arrVal = selRange.Value;
                    selCol = selRange.Cast<Range>().Select( r => r).ToList();
                }
                else
                {
                    var untilCell = (Range)actSheet.Cells[actSheet.UsedRange.Rows.Count, selRange.Cells.Column];
                    var startCell = (Range)actSheet.Cells[1, selRange.Cells.Column];
                    selCol = actSheet.get_Range(startCell, untilCell).Cast<Range>().Select(r => r).ToList(); ;
                    arrVal = actSheet.get_Range(startCell, untilCell).Value;
                }

                if(selCol.Count >2)
                {
                    if(progressBar.InvokeRequired)
                    {
                        progressBar.Invoke(new System.Action(() =>
                        {
                            progressBar.Minimum = 1;
                            progressBar.Maximum = selCol.Count;
                            progressBar.Value = 2;
                            progressBar.Refresh();
                        }
                        ));
                    }
                    else
                    {
                        progressBar.Minimum = 1;
                        progressBar.Maximum = selCol.Count;
                        progressBar.Step = 1;
                        progressBar.Value = 2;
                        progressBar.Refresh();
                    }

                }
                //object[,] arrVal;
                //var arrVal = selRange.Value;
                int index = 0;
                foreach (Range element in selCol)
                {
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                    }
                    found = false;
                    //fr.progressBar1.PerformStep();
                    //fr.label4.Text = count.ToString();
                    //fr.Refresh();
                    //var hid = element.EntireRow.Hidden;
                    worker.ReportProgress(count);
                    
                    count++;
                    if (element.Value is string || element.Interior.ColorIndex == 6 || element.Value == null || element.Value is DateTime || element.EntireRow.Hidden == true || element.EntireColumn.Hidden == true) //check cell value - Hidden can work with filter
                    {
                        continue;
                    }
                    else
                    {
                        decimal keySearch = Convert.ToDecimal(element.Value * -1);
                        //int index = element.
                        for (int i = index; i <= arrVal.Length - 1; i++)
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
                                    sumCoverted = Convert.ToDecimal(arrVal[i, 1]);
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                                if (sumCoverted == keySearch && selCol[i].Interior.ColorIndex != 6 && selCol[i].EntireRow.Hidden != true)
                                {
                                    element.Interior.ColorIndex = 6;
                                    selCol[i].Interior.ColorIndex = 6;
                                    found = true;
                                    break;
                                }
                            }
                        }
                        
                        if (!found)
                        {
                            CellsRemain notFound = new CellsRemain
                            {
                                Summa = element,
                            };
                            remainCell.Add(notFound);
                        }
                    }
                    index++;
                }
            }
            if(komb)
            {
                FindKomb(remainCell, 0m, maxKomb, worker, e, progressBar);
            }
            return remainCell;
        }

        public void FindKomb(List<CellsRemain> list, decimal sum, int maxKomb, BackgroundWorker worker, DoWorkEventArgs e, ProgressBar progressBar)
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

            //if (progressBar.InvokeRequired)
            //{
            //    progressBar.Invoke(new System.Action(() =>
            //    {
            //        progressBar.Minimum = 1;
            //        progressBar.Maximum = listDecimal.Count;
            //        progressBar.Value = 2;
            //        progressBar.Refresh();
            //    }
            //    ));
            //}
            //else
            //{
            //    progressBar.Minimum = 1;
            //    progressBar.Maximum = listDecimal.Count;
            //    progressBar.Step = 1;
            //    progressBar.Value = 2;
            //    progressBar.Refresh();
            //}
            
            //Parallel.For(0, listDecimal.Count - 1, i =>
            for (int i = 0; i < listDecimal.Count; i++) // check <=
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                }
                int max = maxKomb;
                count = i;
                worker.ReportProgress(count);
                if (i + max > listDecimal.Count - 1)
                {
                    max = (listDecimal.Count - 1) - i;
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

            }//);

            //var lastSub = subsets[subsets.Count - 10];

            //if (progressBar.InvokeRequired)
            //{
            //    progressBar.Invoke(new System.Action(() =>
            //    {
            //        progressBar.Minimum = 1;
            //        progressBar.Maximum = subsets.Count;
            //        progressBar.Value = 2;
            //        progressBar.Refresh();
            //    }
            //    ));
            //}
            //else
            //{
            //    progressBar.Minimum = 1;
            //    progressBar.Maximum = subsets.Count;
            //    progressBar.Step = 1;
            //    progressBar.Value = 2;
            //    progressBar.Refresh();
            //}

            HashSet<int> takenIndex = new HashSet<int>();
            for (int i = 1; i < subsets.Count; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
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
            foreach (int foundIndex in takenIndex)
            {
                list[foundIndex].Summa.Interior.ColorIndex = 5;
            }
        }
    }
}
