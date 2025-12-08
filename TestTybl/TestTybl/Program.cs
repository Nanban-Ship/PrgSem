
using System.ComponentModel;
using System.Reflection.Emit;

namespace TestTybl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChessBoard board = new ChessBoard();
            board.ReadInput();

        }
    }

    class ChessBoard
    {
        public void InitBoard()
        {
            // Kdybych si vzpomněl jak, tak bych vytvořil pole s 8 sloupci i řady     
            // 

        }
        
        
        public void ReadInput() 
        {
            using(StreamReader sr = new StreamReader("..\\TestTybl\\bin\\Debug\\net8.0\\vstupni_soubory\\1.txt"))
            {
                int xCount = Convert.ToInt32(sr.ReadLine());
                List<string> xCoords = new();
                for (int i = 0; i < xCount; i++) 
                {
                    xCoords.Add(sr.ReadLine());
                }
                List<string> sCoords = new();
                sCoords.Add(sr.ReadLine());
                List<string> cCoords = new();
                cCoords.Add(sr.ReadLine());
            }
            
        } // void by to být něměl, jen si nepamatuji na jiný return type

        public void Jump()
        {
            // zkontrolovat, že x+-2, y+-1 a x+-1, y+-2 neobsahují překážky
            // řešit to stejné pro body, které neobsahují překážku
        }
    }

}