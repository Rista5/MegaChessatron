using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessEngine
{
    public enum Field : ulong//mozda bi trebalo da bude funkcija ili hash tabela
    {
        notAFile = 0x7f7f7f7f7f7f7f7f,
        notHFile = 0xfefefefefefefefe,
        all1 = 255,
        all8 = 0xff00000000000000,
    }

    public abstract class Figure
    {
        //private long position;
        private ulong bitBoard;
        private bool _white;
        private static Figure inFocus;

        public Figure() { }
        public Figure(ulong bitBoard, bool white) { BitBoard = bitBoard; White = white; }

        #region Methodes
        public abstract ulong AttackedFields(ulong otherFigures=0);//returns bitMap of all fields attacked by this figure
        public List<ulong> PosibleMoves(ulong attacs)//return a list of longs where each long represents one field on a board
        {                                            //!!!treba da se koristi kao staticka metoda,jer radi u opstem slucaju
            ulong tmp = attacs ^ bitBoard;
            List<ulong> array = new List<ulong>();
            ulong comparer = 1;
            for (int i = 0; i < 64; i++, comparer = comparer << 1)
            {
                ulong x;
                if ((x = tmp & comparer) != 0)
                    array.Add(x);
            }
            return array;
        }
        public static List<ulong> PosibleMovesStatic(Figure figure, ulong friends = 0, ulong enemies = 0, ulong enemyAttacks = 0)
        {
            ulong tmp = figure.PosibleMovesBitBoard(friends, enemies, enemyAttacks);
            List<ulong> array = new List<ulong>();
            ulong comparer = 1;
            for (int i = 0; i < 64; i++, comparer = comparer << 1)
            {
                ulong x;
                if ((x = tmp & comparer) != 0)
                    array.Add(x);
            }
            return array;
        }
        public virtual ulong PosibleMovesBitBoard(ulong friends=0,ulong enemies=0,ulong enemyAttacks=0)
        {
            ulong tmp = AttackedFields(friends|enemies);

            //this.PrintDiagnosticBitBoard(tmp);
            //this.PrintDiagnosticBitBoard(friends);
            //this.PrintDiagnosticBitBoard(tmp&friends);
            tmp = tmp - (tmp & friends);
            //this.PrintDiagnosticBitBoard(tmp);
            //this.PrintDiagnosticBitBoard(friends);

            return tmp;
        }
        public bool IsAttacked(ulong enemyAttacks)//returns if figure is attaced by enemy figures, where enemyAttacks is a bitMap of all enemy attacks
        {
            if ((bitBoard & enemyAttacks) != 0)
                return true;
            else return false;
        }
        public static ulong ShiftLeft(ulong input, int number = 1)
        {
            return (input << number);// & (ulong)Field.notHFile;
        }
        public static ulong ShiftRight(ulong input, int number = 1)
        {
            return (input >> number);// & (ulong)Field.notAFile;
        }
        public virtual bool Move(ulong newPosition, ulong friends,ulong enemies, ulong enemyAttacks)
        {
            if (newPosition == BitBoard)
                return false;
            ulong atk = PosibleMovesBitBoard(friends,enemies,enemyAttacks);
            if ((newPosition & atk) == newPosition)
            {
                BitBoard = newPosition;
                PosibleCaptureActivate();
                return true;
            }
            else return false;

        }
        #endregion

        public event EventHandler PosibleCapture;
        protected void PosibleCaptureActivate()
        {
            PosibleCapture?.Invoke(this, new EventArgs());
        }

        #region Properties
        public ulong BitBoard
        {
            get
            {
                return bitBoard;
            }

            set
            {
                bitBoard = value;
            }
        }

        public static Figure InFocus
        {
            get
            {
                return inFocus;
            }

            set
            {
                inFocus = value;
            }
        }

        public bool White { get { return _white; } set { _white = value; } }
        #endregion

        public void PrintBitBoard(ulong board = 0)
        {
            byte[] arr = null;
            if (board == 0)
                arr = BitConverter.GetBytes(BitBoard);
            else arr = BitConverter.GetBytes(board);
            for (int i = arr.Count() - 1; i >= 0; i--)
            {
                Console.WriteLine(Convert.ToString(arr[i], 2).PadLeft(8, '0'));
            }

        }
        public static void PrintDiagnosticBitBoard(ulong board=0)
        {
            System.Diagnostics.Debug.WriteLine("");
            byte[] arr = null;
            //if (board == 0)
            //    arr = BitConverter.GetBytes(BitBoard);
            //else
                arr = BitConverter.GetBytes(board);
            for (int i = arr.Count() - 1; i >= 0; i--)
            {
                System.Diagnostics.Debug.WriteLine(Convert.ToString(arr[i], 2).PadLeft(8, '0'));
            }
            System.Diagnostics.Debug.WriteLine("");
        }

    }
}
//ti kliknes na nesto, i aktiviras event za nesto u fokusu