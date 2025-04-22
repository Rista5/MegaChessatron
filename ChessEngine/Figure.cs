using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessEngine
{
    public enum Field : ulong
    {
        notAFile = 0x7f7f7f7f7f7f7f7f,
        notHFile = 0xfefefefefefefefe,
        all1 = 255,
        all8 = 0xff00000000000000,
    }

    public abstract class Figure
    {
        private ulong bitBoard;
        private bool _white;
        private static Figure inFocus;

        public Figure() { }
        public Figure(ulong bitBoard, bool white) { BitBoard = bitBoard; White = white; }

        #region Methodes
        /// <summary>
        /// Returns BitMap of all fields attacked by this figure
        /// </summary>
        /// <param name="otherFigures"></param>
        /// <returns></returns>
        public abstract ulong AttackedFields(ulong otherFigures = 0);

        /// <summary>
        /// Gets list of possible moves
        /// </summary>
        /// <param name="attacs"></param>
        /// <returns>Return a list of longs where each long represents one field on a board</returns>
        public List<ulong> PosibleMoves(ulong attacs)
        {
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
        public virtual ulong PosibleMovesBitBoard(ulong friends = 0, ulong enemies = 0, ulong enemyAttacks = 0)
        {
            ulong bitBoard = AttackedFields(friends | enemies);

            bitBoard = bitBoard - (bitBoard & friends);

            return bitBoard;
        }

        /// <summary>
        /// Checks if figure is attaced by enemy figures, where enemyAttacks is a bitMap of all enemy attacks
        /// </summary>
        /// <param name="enemyAttacks"></param>
        /// <returns></returns>
        public bool IsAttacked(ulong enemyAttacks)
        {
            if ((bitBoard & enemyAttacks) != 0)
                return true;
            else return false;
        }

        public static ulong ShiftLeft(ulong input, int number = 1)
        {
            return (input << number);
        }

        public static ulong ShiftRight(ulong input, int number = 1)
        {
            return (input >> number);
        }

        public virtual bool Move(ulong newPosition, ulong friends, ulong enemies, ulong enemyAttacks)
        {
            if (newPosition == BitBoard)
                return false;
            ulong atk = PosibleMovesBitBoard(friends, enemies, enemyAttacks);
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

        public static void PrintDiagnosticBitBoard(ulong board = 0)
        {
            System.Diagnostics.Debug.WriteLine("");
            byte[] arr = null;
            arr = BitConverter.GetBytes(board);
            for (int i = arr.Count() - 1; i >= 0; i--)
            {
                System.Diagnostics.Debug.WriteLine(Convert.ToString(arr[i], 2).PadLeft(8, '0'));
            }
            System.Diagnostics.Debug.WriteLine("");
        }

    }
}