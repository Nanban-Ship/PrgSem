namespace ok2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int width = int.Parse(Console.ReadLine());
            int height = int.Parse(Console.ReadLine());
            char[,] labyrinth = new char[height, width];
                
            for (int x = 0; x < height; x++) {
                    string line = Console.ReadLine();
                for (int y = 0; y < width; y++)
                {
                    labyrinth[x, y] = line[y];
                }
            }
        }        
        }
    }
 

