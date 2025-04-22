namespace ChessEngine
{
    public class King : Figure
    {

        public King() { }
        public King(ulong position, bool white) : base(position, white) { }
        public King(King k)
        {
            BitBoard = k.BitBoard;
            White = k.White;
        }

        #region Methodes

        public int NumberOfAvailableFileds(ulong enemyAttacks)
        {
            return CountOnes(this.AvailableFields(enemyAttacks));
        }

        public ulong AvailableFields(ulong enemyAttacks)
        {
            ulong available = BitBoard;
            ulong previous = BitBoard;
            do
            {
                previous = available;

                available = (previous << 1) & (ulong)Field.notHFile | (previous >> 1) & (ulong)Field.notAFile | previous;
                available = (available << 8) | (available >> 8) | available;
                available = available - (available & enemyAttacks);

            } while (previous != available);
            return available;
        }

        public override ulong AttackedFields(ulong otherFigures = 0)
        {
            ulong bitBoard = this.BitBoard;

            bitBoard = (bitBoard << 1) & (ulong)Field.notHFile | (bitBoard >> 1) & (ulong)Field.notAFile | bitBoard;

            bitBoard = (bitBoard << 8) | (bitBoard >> 8) | bitBoard;

            return bitBoard ^ BitBoard;

        }

        public override ulong PosibleMovesBitBoard(ulong friends = 0, ulong enemies = 0, ulong enemyAttacks = 0)
        {
            ulong tmp = base.PosibleMovesBitBoard(friends, enemies, enemyAttacks);
            return tmp - (tmp & enemyAttacks);
        }

        public static int CountOnes(ulong number)
        {
            ulong tmp = number;
            int counter = 0;
            while (tmp != 0)
            {
                if ((tmp & 1) == 1)
                    counter++;
                tmp = tmp >> 1;
            }
            return counter;
        }

        public override bool Move(ulong newPosition, ulong friends, ulong enemies, ulong enemyAttacks)
        {
            ulong i = newPosition & enemyAttacks;
            if ((newPosition & enemyAttacks) == 0)
                return base.Move(newPosition, friends, enemies, enemyAttacks);
            else return false;
        }

        #endregion
    }
}
