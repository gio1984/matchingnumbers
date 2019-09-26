using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace TarkistaSummat
{
    class TarkistaKomb
    {
        //int count = 1;
        Worksheet actSheet = Globals.ThisAddIn.Application.ActiveSheet;


        public void Main(int selComBox, List<TarkistaSummat.CellsRemain> remainCell, BackgroundWorker worker, DoWorkEventArgs e)
        {
            //remainCell.OrderBy(x => x.Summa);
            switch (selComBox)
            {
                case 3:
                    Calc3Comb(remainCell);
                    //List<double> list = new List<double>();
                    //foreach (TarkistaSummat.CellsRemain cell in remainCell)
                    //{
                    //    list.Add(Convert.ToInt32(cell.Summa.Value));
                    //}
                    //List<double[]> results = Knapsack.MatchTotal(0, remainCell);
                    //bool result = SubsetSum(list, 0);

                    //List<int> list = new List<int>();
                    //foreach (TarkistaSummat.CellsRemain cell in remainCell)
                    //{
                    //    list.Add(Convert.ToInt32(cell.Summa.Value * 100));
                    //}
                    //bool found = SubSetSum.Find(list, 100 * 100);

                    break;
                case 4:
                    //List<Range> cell4Comb = Calc3Comb(remainCell);
                    //var remains = Calc3Comb(remainCell);
                    //CalcComb(remainCell);
                    //LastCalc(remainCell);
                    Find(remainCell, 0m);
                    break;

            }
        }


        static void LastCalc(List<TarkistaSummat.CellsRemain> cellsRemains)
        {
            List<KeyValuePair<decimal, List<TarkistaSummat.CellsRemain>>> listDynamic = new List<KeyValuePair<decimal, List<TarkistaSummat.CellsRemain>>>();
            Dictionary<decimal, List<TarkistaSummat.CellsRemain>> dictDynamic = new Dictionary<decimal, List<TarkistaSummat.CellsRemain>>();

            for (int i = 0; i < cellsRemains.Count; i++)
            {
                int max = 50;
                if (max > cellsRemains.Count)
                {
                    max = cellsRemains.Count;
                }
                if (max > (cellsRemains.Count - i))
                {
                    max = cellsRemains.Count - i;
                }
                if (dictDynamic.ContainsKey(Convert.ToDecimal(cellsRemains[i].Summa.Value) * -1))
                {
                    KeyValuePair<decimal, List<TarkistaSummat.CellsRemain>> found = dictDynamic.Select(x => x).Where(w => w.Key == Convert.ToDecimal(cellsRemains[i].Summa.Value) * -1).FirstOrDefault();
                    if (found.Key != 0)
                    {
                        var takenCells = found.Value.Select(s => s.Summa.Interior.ColorIndex == 0);
                        if (takenCells != null)
                        {
                            cellsRemains[i].Summa.Interior.ColorIndex = 6;
                            foreach (TarkistaSummat.CellsRemain cell in found.Value)
                            {
                                cell.Summa.Interior.ColorIndex = 6;
                            }
                            continue;
                        }
                    }

                }
                if (!dictDynamic.ContainsKey(Convert.ToDecimal(cellsRemains[i].Summa.Value)))
                {
                    dictDynamic.Add(Convert.ToDecimal(cellsRemains[i].Summa.Value), new List<TarkistaSummat.CellsRemain> { cellsRemains[i] });

                }
                for (int j = i + 1; j < max; j++)
                {
                    if (dictDynamic.ContainsKey(Convert.ToDecimal(cellsRemains[j].Summa.Value) * -1))
                    {
                        List<TarkistaSummat.CellsRemain> foundList = new List<TarkistaSummat.CellsRemain>();
                        dictDynamic.TryGetValue(Convert.ToDecimal(cellsRemains[j].Summa.Value) * -1, out foundList);
                        cellsRemains[j].Summa.Interior.ColorIndex = 6;
                        foreach (TarkistaSummat.CellsRemain el in foundList)
                        {
                            el.Summa.Interior.ColorIndex = 6;
                        }
                        break;
                    }
                    //var foundLinq = dictDynamic.Select(x => x.Key).Where(d => d.)
                    List<TarkistaSummat.CellsRemain> newList = cellsRemains.GetRange(i, j);
                    //var newSum = (from summa in newList select summa.Summa.Value).Sum();
                    decimal newSum = 0;
                    foreach (TarkistaSummat.CellsRemain el in newList)
                    {
                        newSum += Convert.ToDecimal(el.Summa.Value);
                    }
                    if (!dictDynamic.ContainsKey(newSum))
                    {
                        dictDynamic.Add(Convert.ToDecimal(newSum), newList);
                    }
                }
            }
        }

        public void Find(List<TarkistaSummat.CellsRemain> list, decimal sum)
        {
            //Dictionary<decimal, string> keyValues = new Dictionary<decimal, string>();
            List<decimal> listDecimal = new List<decimal>();
            List<int> listRange = new List<int>(); //string
            List<decimal> resDecimal = new List<decimal>();
            List<IEnumerable<decimal>> resDecimal2 = new List<IEnumerable<decimal>>();
            List<IEnumerable<int>> resIndex = new List<IEnumerable<int>>(); //string
            Dictionary<decimal, int> dicSumma = new Dictionary<decimal, int>();
            int indexList = 0;
            foreach (TarkistaSummat.CellsRemain el in list)
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

            for (int i = 0; i < listDecimal.Count; i++) // check <=
            {
                int max = 15;

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

            }
            var lastSub = subsets[subsets.Count - 10];

            HashSet<int> takenIndex = new HashSet<int>();
            for (int i = 1; i < subsets.Count; i++)
            {
                if(subsets[i] != null)
                {
                    bool taken = false;
                    decimal sumDec = subsets[i].Sum();

                    if (sumDec == 0m)
                    {
                        foreach(int resInd in subsetsIndex[i])
                        {
                            if (takenIndex.Contains(resInd))
                            {
                                taken = true;
                                break;
                            }
                        }
                        if(!taken)
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
            foreach(int foundIndex in takenIndex)
            {
                list[foundIndex].Summa.Interior.ColorIndex = 6;
            }
        }


        static bool[,] dp;

        static void printSubsetsRec(List<TarkistaSummat.CellsRemain> arr, int i, decimal sum, List<TarkistaSummat.CellsRemain> p)
        {
            // If we reached end and sum is non-zero. We print 
            // p[] only if arr[0] is equal to sun OR dp[0][sum] 
            // is true.
            if (i == 0 && sum != 0 && dp[0, Convert.ToInt32(sum * 100)])
            {
                p.Add(arr[i]);
                foreach (TarkistaSummat.CellsRemain cell in p)
                {
                    cell.Summa.Interior.ColorIndex = 6;
                }
                p.Clear();
                return;
            }

            //If sum becomes 0
            if (i == 0 && sum == 0)
            {
                foreach (TarkistaSummat.CellsRemain cell in p)
                {
                    cell.Summa.Interior.ColorIndex = 6;
                }
                p.Clear();
                return;
            }

            //If given sum can be achieved after ignoring current element
            if (dp[i - 1, Convert.ToInt32(sum * 100)])
            {
                List<TarkistaSummat.CellsRemain> b = new List<TarkistaSummat.CellsRemain>();
                b.AddRange(p);
                printSubsetsRec(arr, i - 1, sum, b);
            }

            //If given sum can be achieved after considering current element
            if (sum >= Convert.ToDecimal(arr[i].Summa.Value) && dp[i - 1, Convert.ToInt32((sum * 100) - (Convert.ToDecimal(arr[i].Summa.Value) * 100))])
            {
                p.Add(arr[i]);
                printSubsetsRec(arr, i - 1, sum - Convert.ToDecimal(arr[i].Summa.Value), p);
            }
        }

        static void printAllSubsets(List<TarkistaSummat.CellsRemain> arr, int n, decimal sum)
        {
            if (n == 0 && sum < 0) return;

            //Sum 0 can always be achieved with 0 element
            dp = new bool[n, Convert.ToInt32((sum + 1) * 100)];
            for (int i = 0; i < n; i++)
            {
                dp[i, 0] = true;
            }

            //Sum arr[0] can be achieved with single elements ???
            //if(Convert.ToDecimal(arr[0].Summa.Value) <= sum)
            //{
            //    dp[0, arr[0].Summa.Value] = true;
            //}

            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j < Convert.ToInt32(sum) + 1; j++)
                {
                    dp[i, j] = (Convert.ToInt32(arr[i].Summa.Value * 100) <= j) ? (dp[i - 1, j] || dp[i - 1, j - Convert.ToInt32(arr[i].Summa.Value * 100)]) : dp[i - 1, j];
                }
                if (dp[n - 1, (Convert.ToInt32(arr[i].Summa.Value * 100))]) return;

                List<TarkistaSummat.CellsRemain> p = new List<TarkistaSummat.CellsRemain>();
                printSubsetsRec(arr, n - 1, sum, p);
            }
        }


        private void RandomAlg(List<TarkistaSummat.CellsRemain> cellsRemains)
        {
            List<TarkistaSummat.CellsRemain> usedNums = new List<TarkistaSummat.CellsRemain>();
            List<TarkistaSummat.CellsRemain> unusedNums = new List<TarkistaSummat.CellsRemain>();
            unusedNums = cellsRemains;
            decimal targetSum = 0;
            for (int index = 0; index < cellsRemains.Count; index++)
            {
                TarkistaSummat.CellsRemain currElement = cellsRemains[index];
                decimal usedSum = Convert.ToDecimal(currElement.Summa.Value);
                int index2 = index;
                bool first = true;
                while (targetSum != usedSum)
                {
                    currElement = cellsRemains[index2];
                    if (targetSum > usedSum)
                    {
                        if (!first)
                        {
                            usedSum += Convert.ToDecimal(currElement.Summa.Value);
                            first = false;
                        }
                        usedNums.Add(currElement);
                        cellsRemains.Remove(currElement);
                    }
                    else
                    {
                        if (!first)
                        {
                            usedSum += Convert.ToDecimal(currElement.Summa.Value);
                            first = false;
                        }
                        bool remUsed = usedNums.Remove(currElement);
                        if (remUsed)
                        {
                            cellsRemains.Add(currElement);
                        }
                    }
                    index2++;
                    if (index2 >= cellsRemains.Count)
                    {
                        break;
                    }
                }
                if (targetSum == usedSum)
                {
                    foreach (TarkistaSummat.CellsRemain cells in usedNums)
                    {
                        cells.Summa.Interior.ColorIndex = 6;
                        cellsRemains.Remove(cells);
                    }
                }
            }
        }

        static class SubSetSum
        {
            private static Dictionary<int, bool> memo;
            private static Dictionary<int, KeyValuePair<int, int>> prev;

            static SubSetSum()
            {
                memo = new Dictionary<int, bool>();
                prev = new Dictionary<int, KeyValuePair<int, int>>();
            }

            public static bool Find(List<int> inputArray, int sum)
            {
                memo.Clear();
                prev.Clear();
                memo[0] = true;
                prev[0] = new KeyValuePair<int, int>(-1, 0);

                for (int i = 0; i < inputArray.Count; i++)
                {
                    int num = inputArray[i];
                    for (int s = sum; s >= num; --s)
                    {
                        if (memo.ContainsKey(s - num) && memo[s - num] == true)
                        {
                            memo[s] = true;
                            if (!prev.ContainsKey(s))
                            {
                                prev[s] = new KeyValuePair<int, int>(i, num);
                            }
                        }
                    }
                }
                return memo.ContainsKey(sum) && memo[sum];
            }
        }

        private static List<TarkistaSummat.CellsRemain> Calc3Comb(List<TarkistaSummat.CellsRemain> cells)
        {
            for (int i = 0; i <= cells.Count - 1; i++)
            {
                int j = i + 1;
                int k = cells.Count - 1;

                while (k >= j)
                {
                    var somma = Convert.ToDecimal(cells[i].Summa.Value) + Convert.ToDecimal(cells[j].Summa.Value) + Convert.ToDecimal(cells[k].Summa.Value);
                    if (Convert.ToDecimal(cells[i].Summa.Value) + Convert.ToDecimal(cells[j].Summa.Value) + Convert.ToDecimal(cells[k].Summa.Value) == 0)
                    {
                        cells[i].Summa.Interior.ColorIndex = 6;
                        cells[j].Summa.Interior.ColorIndex = 6;
                        cells[k].Summa.Interior.ColorIndex = 6;
                        cells.Remove(cells[i]);
                        cells.Remove(cells[j - 1]);
                        cells.Remove(cells[k - 2]);
                        break;
                    }
                    else
                    {
                        if (cells[i].Summa.Value + cells[j].Summa.Value + cells[k].Summa.Value > 0)
                        {
                            k--;
                        }
                        else
                        {
                            j++;
                        }
                    }
                }
            }
            return cells;
        }


        static List<TarkistaSummat.CellsRemain> listLeft;
        static List<TarkistaSummat.CellsRemain> listRight;
        static List<TarkistaSummat.CellsRemain> cells;
        private static void CalcComb(List<TarkistaSummat.CellsRemain> remainCell)
        {
            listLeft = remainCell.FindAll(c => c.Summa.Value < 0);
            listRight = remainCell.FindAll(c => c.Summa.Value > 0);
            //Dictionary<decimal, TarkistaSummat.CellsRemain[]> rightDic = new Dictionary<decimal, TarkistaSummat.CellsRemain[]>();
            //Dictionary<decimal, TarkistaSummat.CellsRemain[]> leftDic = new Dictionary<decimal, TarkistaSummat.CellsRemain[]>();
            cells = remainCell;
            Solver sv = new Solver();
            List<List<TarkistaSummat.CellsRemain>> foundCellComb = sv.Solve(0);

            foreach (List<TarkistaSummat.CellsRemain> summats in foundCellComb)
            {
                foreach (TarkistaSummat.CellsRemain cellFound in summats)
                {
                    double sum = cellFound.Summa.Value;
                    cellFound.Summa.Interior.ColorIndex = 6;
                }
            }

        }

        public class Solver
        {

            private List<List<TarkistaSummat.CellsRemain>> mResults;
            private List<List<TarkistaSummat.CellsRemain>> currResults;
            private List<List<TarkistaSummat.CellsRemain>> ResultsReturn;
            int currIndex;
            Worksheet actSheet = Globals.ThisAddIn.Application.ActiveSheet;
            int count;
            public List<List<TarkistaSummat.CellsRemain>> Solve(decimal goal) //List<TarkistaSummat.CellsRemain> elements
            {
                ResultsReturn = new List<List<TarkistaSummat.CellsRemain>>();
                mResults = new List<List<TarkistaSummat.CellsRemain>>();
                currResults = new List<List<TarkistaSummat.CellsRemain>>();
                bool finish = false;
                currIndex = 0;

                while (!finish)
                {
                    count = 0;
                    RecursiveSolve(goal, 0.0m, new List<TarkistaSummat.CellsRemain>(), cells, currIndex);

                    if (currResults.Count > 0)
                    {
                        mResults.AddRange(currResults);
                        currResults.Clear();
                        //currIndex = 0;
                    }
                    else if (currResults.Count == 0)
                    {
                        finish = true;
                    }

                }

                return mResults;
            }

            private void RecursiveSolve(decimal goal, decimal currentSum,
                List<TarkistaSummat.CellsRemain> included, List<TarkistaSummat.CellsRemain> notIncluded, int startIndex)
            {

                if (currResults.Count > 0) return;
                if (listRight.Count == 0 || listLeft.Count == 0) return;

                //if (included.Count > 10)
                //{
                //    included.Clear();
                //    startIndex++;
                //    count = 0;
                //    currentSum = 0;
                //    return;
                //}

                for (int index = startIndex; index < notIncluded.Count; index++)
                {

                    if (count > 500)
                    {
                        count = 0;
                        notIncluded.RemoveAt(0);
                        included.Remove(notIncluded[0]);
                        startIndex++;
                        break;
                    }

                    TarkistaSummat.CellsRemain nextValue;

                    nextValue = notIncluded[index];

                    if (currentSum + Convert.ToDecimal(nextValue.Summa.Value) == goal)
                    {
                        List<TarkistaSummat.CellsRemain> newResult = new List<TarkistaSummat.CellsRemain>(included);
                        newResult.Add(nextValue);
                        currResults.Add(newResult);

                        foreach (TarkistaSummat.CellsRemain el in newResult)
                        {
                            double sum = el.Summa.Value;
                            bool remI = included.Remove(el);
                            bool rem = notIncluded.Remove(el);
                            cells.Remove(el);
                            listRight.Remove(el);
                            listLeft.Remove(el);
                        }
                        currentSum = 0;
                        currentSum = 0;
                        currIndex = startIndex - currResults.Count + 1;
                        count = 0;
                        return;
                    }
                    else if (currentSum + Convert.ToDecimal(nextValue.Summa.Value) < goal)
                    {
                        List<TarkistaSummat.CellsRemain> nextIncluded = new List<TarkistaSummat.CellsRemain>(included);
                        nextIncluded.Add(nextValue);
                        List<TarkistaSummat.CellsRemain> nextNotIncluded = new List<TarkistaSummat.CellsRemain>(notIncluded);
                        nextNotIncluded.Remove(nextValue);
                        RecursiveSolve(goal, currentSum + Convert.ToDecimal(nextValue.Summa.Value),
                            nextIncluded, nextNotIncluded, startIndex++);
                    }
                    count++;
                }
            }
        }


        public static List<T[]> SubsetSums<T>(T[] items, int target, Func<T, int> amountGetter)
        {
            Stack<T> unusedItems = new Stack<T>(items.OrderByDescending(amountGetter));
            Stack<T> usedItems = new Stack<T>();
            List<T[]> results = new List<T[]>();
            SubsetSumsRec(unusedItems, usedItems, target, results, amountGetter);
            return results;
        }
        public static void SubsetSumsRec<T>(Stack<T> unusedItems, Stack<T> usedItems, int targetSum, List<T[]> results, Func<T, int> amountGetter)
        {
            if (targetSum == 0)
                results.Add(usedItems.ToArray());
            if (targetSum < 0 || unusedItems.Count == 0)
                return;
            var item = unusedItems.Pop();
            int currentAmount = amountGetter(item);
            if (targetSum >= currentAmount)
            {
                // case 1: use current element
                usedItems.Push(item);
                SubsetSumsRec(unusedItems, usedItems, targetSum - currentAmount, results, amountGetter);
                usedItems.Pop();
                // case 2: skip current element
                SubsetSumsRec(unusedItems, usedItems, targetSum, results, amountGetter);
            }
            unusedItems.Push(item);
        }

        static List<TarkistaSummat.CellsRemain> resList;
        private static List<TarkistaSummat.CellsRemain> items;
        private static int nSub = 0;
        private static readonly int LIMIT = 1000;
        private static int[] indices;
        private static int countSub = 0;
        private static void ZeroSum(int i, double w)
        {
            if (i != 0 && w == 0)
            {
                for (int j = 0; j < i; j++)
                {
                    //Console.Write("{0} ", items[indices[j]]);
                    resList.Add(items[indices[j]]);
                    items[indices[j]].Summa.Interior.ColorIndex = 6;
                    //items.Remove(items[indices[j]]);
                }
                int c = 0;
                for (int j = 0; j < i; j++)
                {
                    items.Remove(items[indices[j - c]]);
                    c++;
                }
                if (i < items.Count)
                {
                    if (countSub < LIMIT) countSub++;
                    else return;

                    nSub = items.Count;
                    indices = new int[nSub];

                    ZeroSum(0, 0);
                }
                else
                {
                    return;
                }
                //Console.WriteLine("\n");
                //if (countSub < LIMIT) countSub++;
                //else return;
            }
            if (i == nSub)
            {
                return;
            }
            else
            {
                int k = (i != 0) ? indices[i - 1] + 1 : 0;
                for (int j = k; j < nSub; j++)
                {
                    indices[i] = j;
                    ZeroSum(i + 1, w + items[j].Summa.Value);
                    if (countSub == LIMIT) return;
                }
            }
        }




        //static bool SubsetSum(List<TarkistaSummat.CellsRemain> nums, decimal target)
        //{
        //    //var left = new List<int> { 0 };
        //    //var right = new List<int> { 0 };
        //    List<TarkistaSummat.CellsRemain> left = nums.FindAll(c => c.Summa.Value < 0);
        //    List<TarkistaSummat.CellsRemain> right = nums.FindAll(c => c.Summa.Value > 0);
        //    var remainingSum = nums.Sum();
        //    foreach (var n in nums)
        //    {
        //        if (left.Count == 0 || right.Count == 0) return false;
        //        remainingSum -= n;
        //        if (left.Count < right.Count) left = Insert(n, left, target - remainingSum - right.Last(), target);
        //        else right = Insert(n, right, target - remainingSum - left.Last(), target);
        //    }
        //    int lefti = 0, righti = right.Count - 1;
        //    while (lefti < left.Count && righti >= 0)
        //    {
        //        decimal s = Convert.ToDecimal(left[lefti].Summa.Value) + Convert.ToDecimal(right[righti].Summa.Value);
        //        if (s < target) lefti++;
        //        else if (s > target) righti--;
        //        else
        //        {
        //            return true;
        //        }

        //    }
        //    return false;

        //}

        static List<int> Insert(int num, List<int> nums)
        {
            var result = new List<int>();
            int lefti = 0, left = nums[0] + num;
            for (var righti = 0; righti < nums.Count - 1; righti++)
            {

                int right = nums[righti];
                while (left < right)
                {
                    result.Add(left);
                    left = nums[++lefti] + num;
                }
                if (right != left) result.Add(right);
            }
            while (lefti < nums.Count) result.Add(nums[lefti++] + num);
            return result;
        }
    }

    public static class Combination
    {
        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> elements, int choose)
        {
            return choose == 0 ?                        // if choose = 0
                new[] { new T[0] } :                    // return empty Type array
                elements.SelectMany((element, i) =>     // else recursively iterate over array to create combinations
                elements.Skip(i + 1).Combinations(choose - 1).Select(combo => (new[] { element }).Concat(combo)));
        }
    }
}

