using BenchmarkDotNet.Attributes;

namespace Sort
{
    public class SortingAlgorithms
    {
        private int[] RANDOM__INSERTION_SORT;
        private int[] RANDOM__MERGE_SORT;
        private int[] RANDOM__QUICK_SORT_CLASSICAL;
        private int[] RANDOM__QUICK_SORT;

        private int[] SORTED__INSERTION_SORT;
        private int[] SORTED__MERGE_SORT;
        private int[] SORTED__QUICK_SORT_CLASSICAL;
        private int[] SORTED__QUICK_SORT;

        private int[] REVERSED__INSERTION_SORT;
        private int[] REVERSED__MERGE_SORT;
        private int[] REVERSED__QUICK_SORT_CLASSICAL;
        private int[] REVERSED__QUICK_SORT;

        private int[] ALMOST_SORTED__INSERTION_SORT;
        private int[] ALMOST_SORTED__MERGE_SORT;
        private int[] ALMOST_SORTED__QUICK_SORT_CLASSICAL;
        private int[] ALMOST_SORTED__QUICK_SORT;

        private int[] FEW_UNIQUE__INSERTION_SORT;
        private int[] FEW_UNIQUE__MERGE_SORT;
        private int[] FEW_UNIQUE__QUICK_SORT_CLASSICAL;
        private int[] FEW_UNIQUE__QUICK_SORT;

        [Params(10, 1000, 100000)]
        public int Size { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            RANDOM__INSERTION_SORT = Generators.GenerateRandom(Size, -100000, 100000);
            RANDOM__MERGE_SORT = (int[])RANDOM__INSERTION_SORT.Clone();
            RANDOM__QUICK_SORT_CLASSICAL = (int[])RANDOM__INSERTION_SORT.Clone();
            RANDOM__QUICK_SORT = (int[])RANDOM__INSERTION_SORT.Clone();

            SORTED__INSERTION_SORT = Generators.GenerateSorted(Size, -100000, 100000);
            SORTED__MERGE_SORT = (int[])SORTED__INSERTION_SORT.Clone();
            SORTED__QUICK_SORT_CLASSICAL = (int[])SORTED__INSERTION_SORT.Clone();
            SORTED__QUICK_SORT = (int[])SORTED__INSERTION_SORT.Clone();

            REVERSED__INSERTION_SORT = Generators.GenerateReversed(Size, -100000, 100000);
            REVERSED__MERGE_SORT = (int[])REVERSED__INSERTION_SORT.Clone();
            REVERSED__QUICK_SORT_CLASSICAL = (int[])REVERSED__INSERTION_SORT.Clone();
            REVERSED__QUICK_SORT = (int[])REVERSED__INSERTION_SORT.Clone();

            ALMOST_SORTED__INSERTION_SORT = Generators.Swap20Percent(Generators.GenerateSorted(Size, -100000, 100000));
            ALMOST_SORTED__MERGE_SORT = (int[])ALMOST_SORTED__INSERTION_SORT.Clone();
            ALMOST_SORTED__QUICK_SORT_CLASSICAL = (int[])ALMOST_SORTED__INSERTION_SORT.Clone();
            ALMOST_SORTED__QUICK_SORT = (int[])ALMOST_SORTED__INSERTION_SORT.Clone();

            FEW_UNIQUE__INSERTION_SORT = Generators.GenerateRandom(Size, 0, 10);
            FEW_UNIQUE__MERGE_SORT = (int[])FEW_UNIQUE__INSERTION_SORT.Clone();
            FEW_UNIQUE__QUICK_SORT_CLASSICAL = (int[])FEW_UNIQUE__INSERTION_SORT.Clone();
            FEW_UNIQUE__QUICK_SORT = (int[])FEW_UNIQUE__INSERTION_SORT.Clone();
        }

        public static void InsertionSort(int[] arr)
        {
            if (arr.Length < 2) return;
            for (int i = 1; i < arr.Length; i++)
            {
                int current = arr[i];
                int j = i - 1;

                while (j >= 0 && arr[j] > current)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }

                arr[j + 1] = current;
            }
        }
        public static void MergeSort(int[] arr)
        {
            if (arr.Length < 2) return;
            int[] temp = new int[arr.Length];
            MergeSortHelper(arr, temp, 0, arr.Length - 1);
        }

        private static void MergeSortHelper(int[] arr, int[] temp, int left, int right)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;
                MergeSortHelper(arr, temp, left, mid);
                MergeSortHelper(arr, temp, mid + 1, right);
                Merge(arr, temp, left, mid, right);
            }
        }

        private static void Merge(int[] arr, int[] temp, int left, int mid, int right)
        {
            Array.Copy(arr, left, temp, left, right - left + 1);

            int i = left;
            int j = mid + 1;
            int k = left;

            while (i <= mid && j <= right)
            {
                if (temp[i] <= temp[j])
                {
                    arr[k] = temp[i];
                    i++;
                }
                else
                {
                    arr[k] = temp[j];
                    j++;
                }
                k++;
            }

            while (i <= mid)
            {
                arr[k] = temp[i];
                k++;
                i++;
            }
        }

        public static void QuickSortClassical(int[] arr, int p, int q)
        {
            if (arr.Length < 2) return;
            if (p < q)
            {
                int pivotIndex = QuickSortClassicalPartition(arr, p, q);

                QuickSortClassical(arr, p, pivotIndex - 1);
                QuickSortClassical(arr, pivotIndex + 1, q);
            }
        }
        static int QuickSortClassicalPartition(int[] arr, int p, int q)
        {
            int pivot = arr[q];
            int i = p - 1;

            for (int j = p; j < q; j++)
            {
                if (arr[j] <= pivot)
                {
                    i++;
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            int temp1 = arr[i + 1];
            arr[i + 1] = arr[q];
            arr[q] = temp1;

            return i + 1;
        }
        public static void QuickSort(int[] arr)
        {
            if (arr.Length < 2) return;
            Array.Sort(arr);
        }

        //INSERTION_SORT

        [Benchmark]
        public void BENCHMARK_InsertionSort_Random() => SortingAlgorithms.InsertionSort(RANDOM__INSERTION_SORT);
        [Benchmark]
        public void BENCHMARK_InsertionSort_Sorted() => SortingAlgorithms.InsertionSort(SORTED__INSERTION_SORT);
        [Benchmark]
        public void BENCHMARK_InsertionSort_Reversed() => SortingAlgorithms.InsertionSort(REVERSED__INSERTION_SORT);
        [Benchmark]
        public void BENCHMARK_InsertionSort_AlmostSorted() => SortingAlgorithms.InsertionSort(ALMOST_SORTED__INSERTION_SORT);
        [Benchmark]
        public void BENCHMARK_InsertionSort_FewUnique() => SortingAlgorithms.InsertionSort(FEW_UNIQUE__INSERTION_SORT);

        //MERGE_SORT

        [Benchmark]
        public void BENCHMARK_MergeSort_Random() => SortingAlgorithms.MergeSort(RANDOM__MERGE_SORT);
        [Benchmark]
        public void BENCHMARK_MergeSort_Sorted() => SortingAlgorithms.MergeSort(SORTED__MERGE_SORT);
        [Benchmark]
        public void BENCHMARK_MergeSort_Reversed() => SortingAlgorithms.MergeSort(REVERSED__MERGE_SORT);
        [Benchmark]
        public void BENCHMARK_MergeSort_AlmostSorted() => SortingAlgorithms.MergeSort(ALMOST_SORTED__MERGE_SORT);
        [Benchmark]
        public void BENCHMARK_MergeSort_FewUnique() => SortingAlgorithms.MergeSort(FEW_UNIQUE__MERGE_SORT);

        //QUICK SORT CLASSICAL

        [Benchmark]
        public void BENCHMARK_QuickSortClassical_Random() => SortingAlgorithms.QuickSortClassical(RANDOM__QUICK_SORT_CLASSICAL, 0, RANDOM__QUICK_SORT_CLASSICAL.Length - 1);
        [Benchmark]
        public void BENCHMARK_QuickSortClassical_Sorted() => SortingAlgorithms.QuickSortClassical(SORTED__QUICK_SORT_CLASSICAL, 0, SORTED__QUICK_SORT_CLASSICAL.Length - 1);
        [Benchmark]
        public void BENCHMARK_QuickSortClassical_Reversed() => SortingAlgorithms.QuickSortClassical(REVERSED__QUICK_SORT_CLASSICAL, 0, REVERSED__QUICK_SORT_CLASSICAL.Length - 1);
        [Benchmark]
        public void BENCHMARK_QuickSortClassical_AlmostSorted() => SortingAlgorithms.QuickSortClassical(ALMOST_SORTED__QUICK_SORT_CLASSICAL, 0, ALMOST_SORTED__QUICK_SORT_CLASSICAL.Length - 1);
        [Benchmark]
        public void BENCHMARK_QuickSortClassical_FewUnique() => SortingAlgorithms.QuickSortClassical(FEW_UNIQUE__QUICK_SORT_CLASSICAL, 0, FEW_UNIQUE__QUICK_SORT_CLASSICAL.Length - 1);

        //QUICK SORT

        [Benchmark]
        public void BENCHMARK_QuickSort_Random() => SortingAlgorithms.QuickSort(RANDOM__QUICK_SORT);
        [Benchmark]
        public void BENCHMARK_QuickSort_Sorted() => SortingAlgorithms.QuickSort(SORTED__QUICK_SORT);
        [Benchmark]
        public void BENCHMARK_QuickSort_Reversed() => SortingAlgorithms.QuickSort(REVERSED__QUICK_SORT);
        [Benchmark]
        public void BENCHMARK_QuickSort_AlmostSorted() => SortingAlgorithms.QuickSort(ALMOST_SORTED__QUICK_SORT);
        [Benchmark]
        public void BENCHMARK_QuickSort_FewUnique() => SortingAlgorithms.QuickSort(FEW_UNIQUE__QUICK_SORT);
    }
}
