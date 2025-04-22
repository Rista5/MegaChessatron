namespace ChessEngine
{
    public class Program
    {
        static void Main(string[] args)
        {

            ulong posk = Board.FieldOnBoard('4', 'C');
            ulong posb = Board.FieldOnBoard('3', 'D');
            Bishop b = new Bishop(posb, true);
            b.PrintBitBoard(b.AttackedFields(posk));
        }
    }
}
