using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessEngine
{
    public class Bishop : Figure
    {
        public Bishop() { }
        public Bishop(ulong position, bool white) : base(position, white) { }
        public Bishop(Bishop b)
        {
            BitBoard = b.BitBoard;
            White = b.White;
        }


        public override ulong AttackedFields(ulong otherFigures = 0)//mozda da se doda samo jedno polje prilikom shifta, a ne sva polja odjednom
        {
            ulong available = BitBoard;
            ulong previous = BitBoard;
            ulong tmp = BitBoard;
            ulong result = BitBoard;
            //this.PrintDiagnosticBitBoard(otherFigures);
            #region Propao kod
            //int k = 1;
            //do
            //{
            //    previous = available;
            //    tmp = (BitBoard << (9*k)) & (ulong)Field.notHFile;
            //    if (tmp!=0 && (tmp & otherFigures) == 0)
            //        available = available | tmp;
            //    Console.WriteLine();
            //    PrintBitBoard(available);            

            //    tmp = (BitBoard << (7 * k)) & (ulong)Field.notAFile;
            //    if (tmp != 0 && (tmp & otherFigures) == 0)
            //        available = available | tmp;
            //    Console.WriteLine();
            //    PrintBitBoard(available);

            //    tmp = (BitBoard >> (9 * k)) & (ulong)Field.notAFile;
            //    if (tmp != 0 && (tmp & otherFigures) == 0)
            //        available = available | tmp;
            //    Console.WriteLine();
            //    PrintBitBoard(available);

            //    tmp = (BitBoard >> (7 * k)) & (ulong)Field.notHFile;
            //    if (tmp != 0 && (tmp & otherFigures) == 0)
            //        available = available | tmp;
            //    Console.WriteLine();
            //    PrintBitBoard(available);

            //    k++;
            //} while (previous != available);
            #endregion
            do
            {
               // this.PrintDiagnosticBitBoard(tmp);
                previous = available;
                tmp = (previous << 9) & (ulong)Field.notHFile;
                if (tmp != 0 /*&& (tmp & otherFigures) == 0*/)
                    available = available | tmp;
                if ((tmp & otherFigures) != 0)
                    break;
            } while (previous != available);
            result = result | available;
           // this.PrintDiagnosticBitBoard(result);
            available = BitBoard;
            previous = available;
            do
            {
           //     this.PrintDiagnosticBitBoard(tmp);
                previous = available;
                tmp = (previous << 7) & (ulong)Field.notAFile;
                if (tmp != 0 /*&& (tmp & otherFigures) == 0*/)
                    available = available | tmp;
                if ((tmp & otherFigures) != 0)
                    break;
            } while (previous != available);
            result = result | available;
          //  this.PrintDiagnosticBitBoard(result);
            available = BitBoard;
            previous = available;
            do
            {
            //    this.PrintDiagnosticBitBoard(tmp);
                previous = available;
                tmp = (previous >> 9) & (ulong)Field.notAFile;
                if (tmp != 0 /*&& (tmp & otherFigures) == 0*/)
                    available = available | tmp;
                if ((tmp & otherFigures) != 0)
                    break;
            } while (previous != available);
            result = result | available;
          //  this.PrintDiagnosticBitBoard(result);
            available = BitBoard;
            previous = available;
            do
            {
           //     this.PrintDiagnosticBitBoard(tmp);
                previous = available;
                tmp = (previous >> 7) & (ulong)Field.notHFile;
                if (tmp != 0 /*&& (tmp & otherFigures) == 0*/)
                    available = available | tmp;
                if ((tmp & otherFigures) != 0)
                    break;
            } while (previous != available);
            result = result | available;
            //this.PrintDiagnosticBitBoard(result);
            //this.PrintDiagnosticBitBoard(result^BitBoard);
            return result^BitBoard;
        }
    }
}

