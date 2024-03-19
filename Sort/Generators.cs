namespace Sort
{
    public static class Generators
    {
        public static int[] GenerateRandom(int size, int minVal, int maxVal)
        {
            if (size <= 0 || minVal >= maxVal)
            {
                throw new ArgumentException("Invalid parameters");
            }

            Random random = new Random();
            int[] a = new int[size];

            for (int i = 0; i < size; i++)
            {
                a[i] = random.Next(minVal, maxVal + 1);
            }

            return a;
        }

        public static int[] GenerateSorted(int size, int minVal, int maxVal)
        {
            int[] a = GenerateRandom(size, minVal, maxVal);
            Array.Sort(a);
            return a;
        }

        public static int[] GenerateReversed(int size, int minVal, int maxVal)
        {
            int[] a = GenerateSorted(size, minVal, maxVal);
            Array.Reverse(a);
            return a;
        }

        public static int[] Swap20Percent(int[] arr)
        {
            if (arr.Length < 10)
            {
                throw new ArgumentException("Invalid parameter");
            }

            int numOfElementsToSwap = (int)(arr.Length * 0.2);
            int[] swapIndexes = new int[numOfElementsToSwap];

            Random random = new Random();

            for (int i = 0; i < numOfElementsToSwap; i++)
            {
                int index;
                do
                {
                    index = random.Next(0, arr.Length);
                } while (Array.IndexOf(swapIndexes, index) != -1);

                swapIndexes[i] = index;
            }

            foreach (int index in swapIndexes)
            {
                int swapIndex = random.Next(0, arr.Length);
                int temp = arr[index];
                arr[index] = arr[swapIndex];
                arr[swapIndex] = temp;
            }

            return arr;
        }
    }
}