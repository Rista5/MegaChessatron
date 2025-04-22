using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessEngine
{
    public class Program
    {
        static void Main(string[] args)
        {

            //King k = new King(0x100000000000);
            //k.PrintBitBoard();
            //Console.WriteLine();
            //k.PrintBitBoard(k.AttackedFields());
            //Console.WriteLine();
            //Bishop b = new Bishop(0x1000000000);
            //b.PrintBitBoard();
            //Console.WriteLine();
            //b.PrintBitBoard(b.AttackedFields());
            //k.PrintBitBoard(Board.FieldOnBoard("4", 'C'));

            ulong posk = Board.FieldOnBoard('4', 'C');
            ulong posb = Board.FieldOnBoard('3', 'D');
            Bishop b = new Bishop(posb, true);
            b.PrintBitBoard(b.AttackedFields(posk));
        }
    }
}
