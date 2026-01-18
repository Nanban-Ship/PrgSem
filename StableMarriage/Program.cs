using System;
using System.Collections.Generic;
using System.Linq;

namespace StableMarriage
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) return;
            int n = int.Parse(line);

        
            int[][] womenPrefs = new int[n][];
            

            int[][] menRanking = new int[n][];


            for (int i = 0; i < n; i++)
            {

                womenPrefs[i] = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => int.Parse(x) - 1)
                    .ToArray();
            }

            for (int i = 0; i < n; i++)
            {
                int[] prefs = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => int.Parse(x) - 1)
                    .ToArray();

                menRanking[i] = new int[n];
                for (int rank = 0; rank < n; rank++)
                {
                    int womanId = prefs[rank];
                    menRanking[i][womanId] = rank; 
                }
            }

            Queue<int> freeWomen = new Queue<int>();
            for (int i = 0; i < n; i++)
            {
                freeWomen.Enqueue(i);
            }

            int[] womenNextProposal = new int[n]; 

            int[] menPartner = Enumerable.Repeat(-1, n).ToArray();

            while (freeWomen.Count > 0)
            {
                int w = freeWomen.Dequeue();
                
                int m = womenPrefs[w][womenNextProposal[w]];
                womenNextProposal[w]++;
                if (menPartner[m] == -1)
                {
                    menPartner[m] = w;
                }
                else
                {
                    int currentWife = menPartner[m];

                    if (menRanking[m][w] < menRanking[m][currentWife])
                    {
                        menPartner[m] = w;    
                        freeWomen.Enqueue(currentWife);
                    }
                    else
                    {
                        freeWomen.Enqueue(w);
                    }
                }
            }

            int[] result = new int[n];
            for (int m = 0; m < n; m++)
            {
                int w = menPartner[m];
                result[w] = m;
            }

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(result[i] + 1);
            }
        }
    }
}