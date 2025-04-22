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


        public override ulong AttackedFields(ulong otherFigures = 0)
        {
            ulong available = BitBoard;
            ulong previous = BitBoard;
            ulong tmp = BitBoard;
            ulong result = BitBoard;
            
            do
            {
                previous = available;
                tmp = (previous << 9) & (ulong)Field.notHFile;
                if (tmp != 0 )
                    available = available | tmp;
                if ((tmp & otherFigures) != 0)
                    break;
            } while (previous != available);
            result = result | available;
            
            available = BitBoard;
            previous = available;
            do
            {
                previous = available;
                tmp = (previous << 7) & (ulong)Field.notAFile;
                if (tmp != 0)
                    available = available | tmp;
                if ((tmp & otherFigures) != 0)
                    break;
            } while (previous != available);

            result = result | available;
            available = BitBoard;
            previous = available;
            do
            {
                previous = available;
                tmp = (previous >> 9) & (ulong)Field.notAFile;
                if (tmp != 0)
                    available = available | tmp;
                if ((tmp & otherFigures) != 0)
                    break;
            } while (previous != available);
            result = result | available;
            available = BitBoard;
            previous = available;
            do
            {
                previous = available;
                tmp = (previous >> 7) & (ulong)Field.notHFile;
                if (tmp != 0)
                    available = available | tmp;
                if ((tmp & otherFigures) != 0)
                    break;
            } while (previous != available);
            result = result | available;
            return result ^ BitBoard;
        }
    }
}

