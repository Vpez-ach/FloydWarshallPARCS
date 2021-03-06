using Parcs;
using System;
using System.Threading;

namespace FloydWarshallParcs
{
    class ModuleFloydWarshall : IModule
    {
        private int number;
        private int[][] chunk;

        public void Run(ModuleInfo info, CancellationToken token = default)
        {
            number = info.Parent.ReadInt();
            Console.WriteLine($"Current number {number}");
            chunk = info.Parent.ReadObject<int[][]>();

            int n = chunk[0].Length; //width
            int c = chunk.Length; //height
            Console.WriteLine($"Chunk {c}x{n}");

            for (int k = 0; k < n; k++) // ->
            {
                int[] currentRow;

                if (k >= number * c && k < number * c + c)
                {
                    currentRow = chunk[k % c]; // iterate through all chunk rows
                    info.Parent.WriteObject(chunk[k % c]);
                }
                else
                {
                    currentRow = info.Parent.ReadObject<int[]>();
                }

                for (int i = 0; i < c; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        chunk[i][j] = MinWeight(chunk[i][j], chunk[i][k], currentRow[j]);
                    }
                }
            }

            info.Parent.WriteObject(chunk);
            Console.WriteLine("Done!");
        }

        static int MinWeight(int a, int b, int c)
        {
            if (a != int.MaxValue)
            {
                if (b != int.MaxValue && c != int.MaxValue)
                    return Math.Min(a, b + c);
                else
                    return a;
            }
            else
            {
                if (b == int.MaxValue || c == int.MaxValue)
                    return a;
                else
                    return b + c;
            }
        }
    }
}
