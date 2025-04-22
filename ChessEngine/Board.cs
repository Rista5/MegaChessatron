using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Threading;
namespace ChessEngine
{
    public enum BishopPositionEval : ulong
    {

    }
    public class Board
    {
        private List<Figure> _whitePieces;
        private List<Figure> _blackPieces;
        private ulong _whiteAttackBoard;
        private ulong _blackAttackBoard;
        private ulong _whitePositions;
        private ulong _blackPositions;
        private bool _whiteOnMove;
        private static List<Board> _previousBoards;
        private Dictionary<ulong, HashNode> _transpositionTable;
        private static int _searchDepth;

        private static int[] PushToEdges ={
    100, 90, 80, 70, 70, 80, 90, 100,
     90, 70, 60, 50, 50, 60, 70,  90,
     80, 60, 40, 30, 30, 40, 60,  80,
     70, 50, 30, 20, 20, 30, 50,  70,
     70, 50, 30, 20, 20, 30, 50,  70,
     80, 60, 40, 30, 30, 40, 60,  80,
     90, 70, 60, 50, 50, 60, 70,  90,
    100, 90, 80, 70, 70, 80, 90, 100
  };
        private static int[] BishopsCentralised ={
    0, 1, 2, 3, 3, 2, 1, 0,
    1, 3, 4, 5, 5, 4, 3, 1,
    2, 4, 6, 7, 7, 6, 4, 2,
    3, 5, 7, 8, 8, 7, 5, 3,
    3, 5, 7, 8, 8, 7, 5, 3,
    2, 4, 6, 7, 7, 6, 4, 2,
    1, 3, 4, 5, 5, 4, 3, 1,
    0, 1, 2, 3, 3, 2, 1, 0
  };
        private static int[] KingCentralised ={
    0, 0, 0, 0, 0, 0, 0, 0,
    0, 1, 2, 1, 1, 2, 1, 0,
    0, 2, 2, 2, 2, 2, 2, 0,
    0, 1, 2, 3, 3, 2, 1, 0,
    0, 1, 2, 3, 3, 2, 1, 0,
    0, 2, 2, 2, 2, 2, 2, 0,
    0, 1, 2, 1, 1, 2, 1, 0,
    0, 0, 0, 0, 0, 0, 0, 0
  };


        #region Properties

        public ulong WhiteAttackBoard
        {
            get
            {
                return _whiteAttackBoard;
            }

            private set
            {
                _whiteAttackBoard = value;
            }
        }

        public ulong BlackAttackBoard
        {
            get
            {
                return _blackAttackBoard;
            }

            private set
            {
                _blackAttackBoard = value;
            }
        }

        //mozda da se preimenuje u initialize kao za attackboard
        public ulong PositionOfWhitePieces
        {
            get
            {
                ulong tmp = 0;
                foreach (Figure f in WhitePieces)
                    tmp = tmp | f.BitBoard;
                WhitePositions = tmp;
                return tmp;
            }
        }
        public ulong PositionOfBlackPieces
        {
            get
            {
                ulong tmp = 0;
                foreach (Figure f in BlackPieces)
                    tmp = tmp | f.BitBoard;
                BlackPositions = tmp;
                return tmp;
            }
        }

        public List<Figure> WhitePieces
        {
            get
            {
                return _whitePieces;
            }

            set
            {
                _whitePieces = value;
                if (_whitePieces != null)
                {
                    InitialiseWhiteAttack();

                }
            }
        }

        public List<Figure> BlackPieces
        {
            get
            {
                return _blackPieces;
            }

            set
            {
                _blackPieces = value;
                if (_blackPieces != null)
                {
                    InitialiseBlackAttack();

                }
            }
        }

        public ulong WhitePositions { get { return _whitePositions; } set { _whitePositions = value; } }
        public ulong BlackPositions { get { return _blackPositions; } set { _blackPositions = value; } }

        public bool WhiteOnMove
        {
            get
            {
                return _whiteOnMove;
            }

            set
            {
                _whiteOnMove = value;
            }
        }

        public static List<Board> PreviousBoards
        {
            get
            {
                return _previousBoards;
            }

            set
            {
                _previousBoards = value;
            }
        }

        public Dictionary<ulong, HashNode> TranspositionTable
        {
            get
            {
                return _transpositionTable;
            }

            set
            {
                _transpositionTable = value;
            }
        }

        public static int SearchDepth
        {
            get
            {
                return _searchDepth;
            }

            set
            {
                _searchDepth = value;
            }
        }

        public void InitialiseWhiteAttack()
        {
            ulong tmp = 0;
            foreach (Figure f in WhitePieces)
            {
                //Figure.PrintDiagnosticBitBoard(tmp);
                //Figure.PrintDiagnosticBitBoard(f.AttackedFields(WhitePositions));
                //Figure.PrintDiagnosticBitBoard(WhitePositions);
                tmp = tmp | f.AttackedFields(WhitePositions); //CODE 123

            }
            WhiteAttackBoard = tmp;
        }
        public void InitialiseBlackAttack()
        {
            ulong tmp = 0;
            foreach (Figure f in BlackPieces)
            {
                tmp = tmp | f.AttackedFields(BlackPositions);//CODE 123
            }
            BlackAttackBoard = tmp;
        }

        #endregion

        public Board() { }
        public Board(List<Figure> white, List<Figure> black, Dictionary<ulong, HashNode> dic = null)//ispravljeno white on move na false
        {
            WhitePieces = white;
            BlackPieces = black;
            FigureMoved += FigureMovedHandler;
            WhiteOnMove = false;
            foreach (Figure f in white)
                f.PosibleCapture += PosibleCaptureHandler;
            foreach (Figure f in black)
                f.PosibleCapture += PosibleCaptureHandler;
            WhitePositions = PositionOfWhitePieces;
            BlackPositions = PositionOfBlackPieces;
            InitialiseWhiteAttack();
            InitialiseBlackAttack();
            if (dic != null)
                TranspositionTable = dic;
            else TranspositionTable = new Dictionary<ulong, HashNode>(65536);
            SearchDepth = 8;
        }

        public Board(Board b)
        {
            WhitePieces = new List<Figure>();
            foreach (Figure f in b.WhitePieces)
            {
                if (f is King)
                    AddWhitePiece(new King((King)f));
                else if (f is Bishop)
                    AddWhitePiece(new Bishop((Bishop)f));
            }
            WhitePositions = PositionOfWhitePieces;
            InitialiseWhiteAttack();
            BlackPieces = new List<Figure>();
            foreach (Figure f in b.BlackPieces)
            {
                if (f is King)
                    AddBlackPiece(new King((King)f));
                else if (f is Bishop)
                    AddBlackPiece(new Bishop((Bishop)f));
            }
            BlackPositions = PositionOfBlackPieces;
            InitialiseBlackAttack();
            WhiteOnMove = b.WhiteOnMove;
        }
        #region Add/Remove Figure
        public void AddWhitePiece(Figure f)
        {
            WhitePieces.Add(f);
            f.PosibleCapture += PosibleCaptureHandler;
        }
        public void AddBlackPiece(Figure f)
        {
            BlackPieces.Add(f);
            f.PosibleCapture += PosibleCaptureHandler;
        }
        public void RemoveWhitePiece(Figure f)
        {

            if (WhitePieces.Find(f.Equals) != null)
                WhitePieces.Remove(f);
        }
        public void RemoveBlackPiece(Figure f)
        {
            if (WhitePieces.Find(f.Equals) != null)
                WhitePieces.Remove(f);
        }

        #endregion

        public static ulong FieldOnBoard(char number, char file)
        {
            ulong tmp;
            switch (file)
            {
                case 'A':
                case 'a':
                    tmp = 128;
                    break;
                case 'B':
                case 'b':
                    tmp = 64;
                    break;
                case 'C':
                case 'c':
                    tmp = 32;
                    break;
                case 'D':
                case 'd':
                    tmp = 16;
                    break;
                case 'E':
                case 'e':
                    tmp = 8;
                    break;
                case 'F':
                case 'f':
                    tmp = 4;
                    break;
                case 'G':
                case 'g':
                    tmp = 2;
                    break;
                case 'H':
                case 'h':
                    tmp = 1;
                    break;
                default:
                    tmp = 1;
                    break;
            }
            int num = number - '0';
            tmp = tmp << ((num - 1) * 8);
            return tmp;
        }
        public bool isEqual(Board b)
        {

            if ((WhiteAttackBoard == b.WhiteAttackBoard) && (WhitePositions == b.WhitePositions)
                && (BlackAttackBoard == b.BlackAttackBoard) && (BlackPositions == b.BlackPositions) &&
                (WhiteOnMove == b.WhiteOnMove))
                return true;
            else return false;
        }
        public void SaveTableXML(FileStream f)
        {
            DataContractSerializer sr = new DataContractSerializer(typeof(Dictionary<ulong, HashNode>));
            sr.WriteObject(f, TranspositionTable);
        }
        public void LoadTableXML(FileStream f)
        {
            DataContractSerializer sr = new DataContractSerializer(typeof(Dictionary<ulong, HashNode>));
            TranspositionTable = (Dictionary<ulong, HashNode>)sr.ReadObject(f);
        }
        #region Events
        public event EventHandler KingChecked;
        private void KingCheckedActivate()
        {
            if (KingChecked != null)
                KingChecked(this, new EventArgs());
        }
        public event EventHandler CheckMate;
        private void CheckMateActivate()
        {
            if (CheckMate != null)
                CheckMate(this, new EventArgs());
        }
        public delegate void HighlightDelegate(ulong highlight);
        public delegate void MoveDelegate(ulong previous, ulong next, Figure fig = null);

        public event MoveDelegate FigureMoved;
        protected void FigureMovedActivate(ulong previousPosition, ulong nextPosition, Figure f = null)
        {
            if (FigureMoved != null)
                FigureMoved(previousPosition, nextPosition, f);
        }
        public event HighlightDelegate Highlight;
        protected void HighLightActivate(ulong attackedFields)
        {
            if (Highlight != null)
                Highlight(attackedFields);
        }
        public event EventHandler Deselect;
        private void DeselectActivate()
        {
            if (Deselect != null)
                Deselect(this, new EventArgs());
        }
        public event EventHandler Draw;
        private void DrawActivate()
        {
            Draw?.Invoke(this, new EventArgs());
        }
        #endregion

        #region EventHandlers

        public void OnClickEventHandler(ulong position)
        {
            DeselectActivate();
            bool figureWasClicked = false;
            if (WhiteOnMove)
            {
                foreach (Figure f in WhitePieces)
                {
                    if (f.BitBoard == position)
                    {
                        Figure.InFocus = f;
                        figureWasClicked = true;
                        ulong otherFigures = Board.PositionsOfFigures(WhitePieces);
                        //    f.PrintDiagnosticBitBoard(f.BitBoard);
                        ulong fields = f.PosibleMovesBitBoard(WhitePositions, BlackPositions, BlackAttackBoard);
                        //    f.PrintDiagnosticBitBoard(fields);

                        HighLightActivate(fields);
                        return;
                    }
                }
            }
            else
            {
                foreach (Figure f in BlackPieces)
                {
                    if (f.BitBoard == position)
                    {
                        Figure.InFocus = f;
                        figureWasClicked = true;
                        ulong otherFigures = Board.PositionsOfFigures(BlackPieces);
                        ulong fields = f.PosibleMovesBitBoard(BlackPositions, WhitePositions, WhiteAttackBoard);

                        HighLightActivate(fields);
                        return;
                    }
                }
            }
            if (!figureWasClicked)
            {
                List<Figure> list;
                ulong enemies;
                ulong friends;
                ulong enemyAttacks;
                if (Figure.InFocus == null)
                    return;
                if (Figure.InFocus.White)
                {
                    list = WhitePieces;
                    enemies = BlackPositions;
                    enemyAttacks = BlackAttackBoard;
                    friends = WhitePositions;
                }
                else
                {
                    list = BlackPieces;
                    enemies = WhitePositions;
                    enemyAttacks = WhiteAttackBoard;
                    friends = BlackPositions;
                }


                ulong tmp = Figure.InFocus.BitBoard;
                if (Figure.InFocus.Move(position, friends, enemies, enemyAttacks))
                {
                    FigureMovedActivate(tmp, position);
                    if (WhiteOnMove)
                        WhiteOnMove = false;
                    else WhiteOnMove = true;
                    Figure.InFocus = null;
                }

            }
        }
        public static ulong PositionsOfFigures(List<Figure> list)
        {
            ulong others = 0;
            foreach (Figure f in list)
                others = others | f.BitBoard;
            return others;
        }
        private void FigureMovedHandler(ulong previous, ulong next, Figure inputFigure = null)//napisati ga kompaktnije
        {
            Figure figure;
            if (inputFigure != null)
                figure = inputFigure;
            else figure = Figure.InFocus;
            if (figure.White || (figure == null))//mozda ne treba null
            {
                WhitePositions = WhitePositions - (WhitePositions & previous);
                WhitePositions = WhitePositions | next;

                InitialiseWhiteAttack();
                foreach (Figure f in BlackPieces)
                {
                    if (f is King)
                    {
                        King k = (King)f;
                        int availableFields = k.NumberOfAvailableFileds(WhiteAttackBoard);//ne radi kako treba
                        if (availableFields == 0)
                            CheckMateActivate();
                        else if (availableFields == 1 && k.PosibleMovesBitBoard(BlackPositions, WhitePositions, WhiteAttackBoard) == 0)
                        {
                            DrawActivate();
                            //mozda da postoji property za available fields, i da se postavlja prilikom pomeranja figure,samo za kralja naravno, da se ne bi svaki put racunalo
                        }
                        else if (k.IsAttacked(WhiteAttackBoard))
                            KingCheckedActivate();
                    }
                }
            }
            else
            {
                BlackPositions = BlackPositions - (BlackPositions & previous);
                BlackPositions = BlackPositions | next;

                InitialiseBlackAttack();
                foreach (Figure f in WhitePieces)
                {
                    if (f is King)
                    {
                        King k = (King)f;
                        int availableFields = k.NumberOfAvailableFileds(BlackAttackBoard);
                        if (availableFields == 0)
                            CheckMateActivate();
                        else if (availableFields == 1 && k.PosibleMovesBitBoard(WhitePositions, BlackPositions, BlackAttackBoard) == 0)
                        {
                            DrawActivate();
                            //mozda da postoji property za available fields, i da se postavlja prilikom pomeranja figure,samo za kralja naravno, da se ne bi svaki put racunalo
                        }
                        else if (k.IsAttacked(BlackAttackBoard))
                            KingCheckedActivate();
                    }
                }
            }

            for (int i = (PreviousBoards.Count - 1); i > 0; i--)
                PreviousBoards[i] = PreviousBoards[i - 1];
            PreviousBoards[0] = new Board(this);
            //if (PreviousBoards[0].WhiteOnMove)//izgleda da je ovako,jer bez toga ne cita lepo iz tablice
            //    PreviousBoards[0].WhiteOnMove = false;
            //else PreviousBoards[0].WhiteOnMove = true;

        }

        private void PosibleCaptureHandler(object sender, EventArgs e)
        {
            Figure send;
            if (sender is Figure)
                send = (Figure)sender;
            else return;
            List<Figure> list;
            if (send.White)
                list = BlackPieces;
            else list = WhitePieces;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].BitBoard == send.BitBoard)
                {
                    list.RemoveAt(i);
                    break;
                }
            }
        }
        private void GenerateWhitePositions()//ne koristi se
        {
            ulong newWhite = 0;
            foreach (Figure f in WhitePieces)
            {
                newWhite = newWhite | f.AttackedFields(WhitePositions);
            }
            WhiteAttackBoard = newWhite;
        }
        #endregion


        public void OnClickEventHandlerAI(ulong position)
        {
            DeselectActivate();
            bool figureWasClicked = false;

            foreach (Figure f in BlackPieces)
            {
                if (f.BitBoard == position)
                {
                    Figure.InFocus = f;
                    figureWasClicked = true;
                    ulong otherFigures = Board.PositionsOfFigures(BlackPieces);

                    ulong fields = f.PosibleMovesBitBoard(BlackPositions, WhitePositions, WhiteAttackBoard);

                    HighLightActivate(fields);
                    return;
                }
            }

            if (!figureWasClicked)
            {
                List<Figure> list;
                ulong enemies;
                ulong friends;
                ulong enemyAttacks;
                if (Figure.InFocus == null)
                    return;

                list = BlackPieces;
                enemies = WhitePositions;
                enemyAttacks = WhiteAttackBoard;
                friends = BlackPositions;



                ulong tmp = Figure.InFocus.BitBoard;
                if (Figure.InFocus.Move(position, friends, enemies, enemyAttacks))
                {
                    FigureMovedActivate(tmp, position);
                    if (WhiteOnMove)
                        WhiteOnMove = false;
                    else
                    {
                        WhiteOnMove = true;
                        Thread NitAI = new Thread(GenerateMoveAI);
                        NitAI.Start();
                    }
                    Figure.InFocus = null;
                }

            }
        }



        private void GenerateMoveAI()
        {

            Figure bestFigure = null;
            ulong bestMove = 0;
            int evalPrev = EvaluateBoard(true);

            AlfaBeta(true, ref bestFigure, ref bestMove, -1000, 1000, SearchDepth);
            ulong tmp = bestFigure.BitBoard;

            BlackPositions = PositionOfBlackPieces;
            WhitePositions = PositionOfWhitePieces;
            InitialiseBlackAttack();
            InitialiseWhiteAttack();

            bestFigure.Move(bestMove, WhitePositions, BlackPositions, BlackAttackBoard);
            FigureMovedActivate(tmp, bestMove, bestFigure);
            WhitePositions = PositionOfWhitePieces;
            InitialiseWhiteAttack();

            WhiteOnMove = false;
        }

        public int EvaluateBoard(bool white)
        {
            foreach (Figure f in BlackPieces)
                if (f is King)
                    return 64 - ((King)f).NumberOfAvailableFileds(WhiteAttackBoard);
            return 0;

        }
        public int EvaluateBoard2(bool white)
        {
            ulong bishops = 0;
            int bishopVal = 0;
            King whiteKing = null;
            foreach (Figure f in WhitePieces)
                if (!(f is King))
                {
                    bishops |= f.BitBoard;
                    bishopVal += BishopDistanceFromCenter(f.BitBoard) << 3;
                }
                else
                {
                    whiteKing = (King)f;
                }

            ulong bishopInDanger = bishops & BlackAttackBoard;
            if (bishopInDanger != 0)
                bishopInDanger = bishopInDanger - (bishopInDanger & WhiteAttackBoard);
            if (!white && (bishopInDanger != 0))
            {
                return -1000;
            }
            else
            {

                King blackKing = null;

                foreach (Figure f in BlackPieces)
                    if (f is King)
                        blackKing = (King)f;
                int eval = 0;
                int NAF = 64 - (blackKing).NumberOfAvailableFileds(WhiteAttackBoard);
                if (NAF == 64)
                    return 1000;
                if (NAF == 63 && !blackKing.IsAttacked(WhiteAttackBoard) && !white)
                    return -1000;


                if (NAF < 62)
                    eval += BishopDistanceFromCenter(whiteKing.BitBoard);
                else eval += 8 - BishopDistanceFromCenter(whiteKing.BitBoard);
                if (NAF >= 62 && blackKing.IsAttacked(WhiteAttackBoard))
                {
                    NAF += 100;
                }

                eval += BlackKingInCenter(blackKing.BitBoard);
                return NAF + eval + ((4 - BlackKingDistanceFromCenter(blackKing.BitBoard)) << 5) + bishopVal;
            }


        }

        public int EvaluateBoard3(bool white)
        {
            ulong bishops = 0;
            int bishopVal = 0;
            King whiteKing = null;
            foreach (Figure f in WhitePieces)
                if (!(f is King))
                {
                    bishops |= f.BitBoard;
                    bishopVal += BishopsCentralised[CalaculateIndex(f)];
                }
                else
                {
                    bishopVal += KingCentralised[CalaculateIndex(f)];
                    whiteKing = (King)f;
                }

            ulong bishopInDanger = bishops & BlackAttackBoard;
            if (bishopInDanger != 0)
                bishopInDanger = bishopInDanger - (bishopInDanger & WhiteAttackBoard);
            if (!white && (bishopInDanger != 0))
            {
                return -1000;
            }
            else
            {

                King blackKing = null;

                foreach (Figure f in BlackPieces)
                    if (f is King)
                        blackKing = (King)f;
                int eval = 0;
                int NAF = 64 - (blackKing).NumberOfAvailableFileds(WhiteAttackBoard);
                if (NAF == 64)
                    return 1000;
                if (NAF == 63 && !blackKing.IsAttacked(WhiteAttackBoard) && !white)
                    return -1000;
                if (NAF > 62 && blackKing.IsAttacked(WhiteAttackBoard))
                    eval += 50;
                eval += PushToEdges[CalaculateIndex(blackKing)];
                return NAF + (eval) + bishopVal - (KingDistance2(whiteKing, blackKing));
            }
        }

        public int ArrayIndex(int x, int y)
        {
            return ((y - 1) << 3) + x - 1;
        }

        public int CalaculateIndex(Figure f)
        {
            int x = 0, y = 0;
            FigureFiles(f.BitBoard, ref x, ref y);
            return ArrayIndex(x, y);
        }

        /// <summary>
        /// Returns true if bishops are attacked or if it's draw or if the sequence of moves repeats
        /// </summary>
        /// <param name="white"></param>
        /// <returns></returns>
        public bool EvaluateNode(bool white)
        {
            foreach (Board b in PreviousBoards)
            {
                if (this.isEqual(b))
                    return true;
            }
            ulong bishops = 0;
            foreach (Figure f in WhitePieces)
                if (!(f is King))
                {
                    bishops |= f.BitBoard;
                }
            King blackKing = null;
            foreach (Figure f in BlackPieces)
                if (f is King)
                    blackKing = (King)f;

            ulong bishopInDanger = bishops & BlackAttackBoard;
            if (bishopInDanger != 0)
                bishopInDanger = bishopInDanger - (bishopInDanger & WhiteAttackBoard);
            if (!white && (bishopInDanger != 0))
            {
                return true;
            }
            else if (!white && blackKing.AvailableFields(WhiteAttackBoard) == blackKing.BitBoard) // prevents draw
                return true;
            return false;
        }

        /// <summary>
        /// Finds places where figure is on the board
        /// </summary>
        /// <param name="bitBoard"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void FigureFiles(ulong bitBoard, ref int x, ref int y)
        {
            y = 1;
            ulong compare = 0xff;
            while ((bitBoard & compare) == 0)
            {
                y++;
                compare = compare << 8;
            }
            x = 1;
            compare = 0x8080808080808080;
            while ((bitBoard & compare) == 0)
            {
                x++;
                compare = compare >> 1;
            }

        }

        /// <summary>
        /// Distance of black king from center
        /// </summary>
        /// <param name="blackKing"></param>
        /// <returns></returns>
        public int BlackKingDistanceFromCenter(ulong blackKing)
        {
            int x = 0, y = 0;
            FigureFiles(blackKing, ref x, ref y);
            if (x > 4)
                x = 8 - x;
            else x = x - 1;
            if (y > 4)
                y = 8 - y;
            else y = y - 1;
            return x + y;
        }

        /// <summary>
        /// Distance of bishop from center
        /// </summary>
        /// <param name="bishop"></param>
        /// <returns></returns>
        public int BishopDistanceFromCenter(ulong bishop)
        {
            int x = 0, y = 0;
            FigureFiles(bishop, ref x, ref y);
            if (x > 4)
                x = 8 - x;
            else x = x - 1;
            if (y > 4)
                y = 8 - y;
            else y = y - 1;
            return x + y;
        }


        public int BlackKingInCenter(ulong blackKing)
        {
            int x = 0, y = 0;
            FigureFiles(blackKing, ref x, ref y);
            if (x > 4)
                x = 8 - x;
            else x = x - 1;
            if (y > 4)
                y = 8 - y;
            else y = y - 1;
            if (x >= 3 && y >= 3)
                return -50;//-100
            else return 0;
        }


        public int DistanceBetweenKings()
        {
            int x = 0, y = 0;
            int x2 = 0, y2 = 0;

            foreach (Figure k in WhitePieces)
            {
                if (k is King)
                    FigureFiles(k.BitBoard, ref x, ref y);
            }

            foreach (King k in BlackPieces)
            {
                if (k is King)
                    FigureFiles(k.BitBoard, ref x2, ref y2);
            }

            return Math.Max(Math.Abs(x - x2), Math.Abs(y - y2));
        }

        public int KingDistance2(Figure whiteKing = null, Figure blackKing = null)
        {
            int x = 0, y = 0;
            int x2 = 0, y2 = 0;

            if (whiteKing == null)
                foreach (Figure k in WhitePieces)
                {
                    if (k is King)
                        FigureFiles(k.BitBoard, ref x, ref y);
                }
            else FigureFiles(whiteKing.BitBoard, ref x, ref y);

            if (blackKing == null)
                foreach (King k in BlackPieces)
                {
                    if (k is King)
                        FigureFiles(k.BitBoard, ref x2, ref y2);
                }
            else FigureFiles(blackKing.BitBoard, ref x2, ref y2);

            return Math.Abs(x - x2) + Math.Abs(y - y2);
        }

        public int AlfaBeta(bool white, ref Figure bestFigure, ref ulong bestMove, int alpha, int beta, int depth)
        {
            if (white)
            {
                int v = -1000;
                BlackPositions = PositionOfBlackPieces;
                InitialiseBlackAttack();
                WhitePositions = PositionOfWhitePieces;
                InitialiseWhiteAttack();

                Figure curBest = null;
                ulong curBestMove = 0;
                HashNode node = null;
                ulong key = WhiteAttackBoard ^ BlackAttackBoard;

                if (TranspositionTable.TryGetValue(key, out node))
                {
                    while (node != null && node.WhiteAttackBoard != WhiteAttackBoard && node.BlackAttackBoard != BlackAttackBoard && node.WhiteOnMove == white)
                        node = node.NextNode;
                    if (node != null && node.Depth >= depth && node.WhiteOnMove == white)
                    {
                        foreach (Figure f in WhitePieces)
                        {
                            if ((node.Move & f.BitBoard) != 0)
                            {
                                if (IsValidMoveFromTable(f, node.Move))
                                {
                                    bestFigure = f;
                                    bestMove = node.Move - f.BitBoard;
                                    v = node.BoardEvaluation;
                                    return v - 1 - node.Depth;
                                }
                                else break;
                            }//because of the validmove function, it avoids coming to the same state on the board, because of that it predefine the already existing value for the best move
                        }

                    }
                }

                if (depth <= 0)
                    return EvaluateBoard3(true);

                bool badMove = EvaluateNode(white);
                if (depth < SearchDepth && badMove)
                    return -1000;

                foreach (Figure fig in WhitePieces)
                {
                    ulong position = fig.BitBoard;
                    List<ulong> moveList = Figure.PosibleMovesStatic(fig, WhitePositions, BlackPositions, BlackAttackBoard);


                    foreach (ulong move in moveList)
                    {


                        fig.BitBoard = move;
                        int tmp = AlfaBeta(false, ref bestFigure, ref bestMove, alpha, beta, depth - 1);
                        alpha = Math.Max(alpha, tmp);
                        if (tmp > v)
                        {
                            v = tmp;
                            curBest = fig;
                            curBestMove = move;
                        }
                        if (beta <= alpha)
                            break;
                        //return v - 1;
                    }
                    fig.BitBoard = position;
                    WhitePositions = PositionOfWhitePieces;
                    InitialiseWhiteAttack();

                }
                bestFigure = curBest;
                bestMove = curBestMove;

                if (bestFigure == null)
                    return v - 1;
                try
                {
                    if (node != null && node.WhiteOnMove == white)
                    {
                        node.Move = bestMove | bestFigure.BitBoard;
                        node.Depth = depth;
                        node.BoardEvaluation = v;
                        node.WhiteOnMove = white;
                    }
                    else
                    {
                        node = new HashNode(WhiteAttackBoard, BlackAttackBoard, bestFigure.BitBoard | bestMove, v, depth, true);
                        TranspositionTable.Add(key, node);
                    }
                }
                catch (ArgumentException)
                {
                    TranspositionTable[key] = node;
                }
                catch (NullReferenceException)
                {

                }


                return v - 1;

            }
            else
            {
                int v = 1000;
                WhitePositions = PositionOfWhitePieces;
                InitialiseWhiteAttack();

                BlackPositions = PositionOfBlackPieces;
                InitialiseBlackAttack();


                Figure curBest = null;
                ulong curBestMove = 0;
                HashNode node;
                ulong key = WhiteAttackBoard ^ BlackAttackBoard;
                if (TranspositionTable.TryGetValue(key, out node))
                {
                    while (node != null && node.WhiteAttackBoard != WhiteAttackBoard && node.BlackAttackBoard != BlackAttackBoard && node.WhiteOnMove == white)
                        node = node.NextNode;
                    if (node != null && node.Depth >= depth && node.WhiteOnMove == white)
                    {

                        foreach (Figure f in BlackPieces)
                        {
                            if ((node.Move & f.BitBoard) != 0)
                            {
                                if (IsValidMoveFromTable(f, node.Move))
                                {
                                    bestFigure = f;
                                    bestMove = node.Move - f.BitBoard;
                                    v = node.BoardEvaluation;
                                    return v - 1 - node.Depth;
                                }
                                else break;
                            }
                        }

                    }
                }

                if (depth <= 0)
                    return EvaluateBoard3(false);

                bool badMove = EvaluateNode(white);
                if (badMove)
                    return -1000;

                foreach (Figure fig in BlackPieces)
                {
                    ulong position = fig.BitBoard;

                    List<ulong> moveList = Figure.PosibleMovesStatic(fig, BlackPositions, WhitePositions, WhiteAttackBoard);

                    // check for draw
                    if (moveList.Count == 0 && fig is King)
                    {
                        King blackKing = (King)fig;
                        if (blackKing.IsAttacked(WhiteAttackBoard))
                        {
                            return 1000;
                        }
                        else
                        {
                            return -1000;
                        }
                    }

                    foreach (ulong move in moveList)
                    {

                        fig.BitBoard = move;
                        int tmp = AlfaBeta(true, ref bestFigure, ref bestMove, alpha, beta, depth - 1);
                        beta = Math.Min(beta, tmp);
                        if (tmp < v)
                        {
                            v = tmp;
                            bestFigure = fig;
                            bestMove = move;

                        }
                        if (beta <= alpha)
                            break;
                    }
                    fig.BitBoard = position;
                    BlackPositions = PositionOfBlackPieces;
                    InitialiseBlackAttack();
                }
                bestFigure = curBest;
                bestMove = curBestMove;

                if (bestFigure == null)
                    return v - 1;
                try
                {
                    if (node != null && node.WhiteOnMove == white)
                    {
                        node.Move = bestMove | bestFigure.BitBoard; // bestMove;
                        node.Depth = depth;
                        node.BoardEvaluation = v;
                        node.WhiteOnMove = white;
                    }
                    else
                    {
                        node = new HashNode(WhiteAttackBoard, BlackAttackBoard, bestFigure.BitBoard | bestMove, v, depth, false);
                        TranspositionTable.Add(key, node);
                    }
                }
                catch (ArgumentException)
                {
                    TranspositionTable[key] = node;
                }
                catch (NullReferenceException)
                {

                }

                return v - 1;

            }

        }


        public bool IsValidMoveFromTable(Figure f, ulong move)
        {
            ulong startingPosition = f.BitBoard;
            f.BitBoard = move ^ f.BitBoard;
            WhitePositions = PositionOfWhitePieces;
            InitialiseWhiteAttack();

            bool retVal = EvaluateNode(WhiteOnMove);
            f.BitBoard = startingPosition;
            WhitePositions = PositionOfWhitePieces;
            InitialiseWhiteAttack();
            return retVal;
        }


        public int MinMax(bool white, ref Figure bestFigure, ref ulong bestMove, Figure inputFigure, ulong inputMove, int depth)
        {

            if (white)
            {
                BlackPositions = PositionOfBlackPieces;
                InitialiseBlackAttack();


                WhitePositions = PositionOfWhitePieces;
                InitialiseWhiteAttack();
                if (depth <= 0)
                    return EvaluateBoard2(true);
                bool badMove = EvaluateNode(white);
                if (badMove)
                    return -1000;

                int max = -1000;
                Figure curBest = null;
                ulong curBestMove = 0;
                foreach (Figure fig in WhitePieces)
                {
                    ulong position = fig.BitBoard;
                    List<ulong> moveList = Figure.PosibleMovesStatic(fig, WhitePositions, BlackPositions, BlackAttackBoard);
                    foreach (ulong move in moveList)
                    {

                        fig.BitBoard = move;
                        int tmp = MinMax(false, ref bestFigure, ref bestMove, fig, move, depth - 1);
                        if (tmp > max)
                        {
                            max = tmp;
                            curBest = fig;
                            curBestMove = move;
                        }
                    }
                    fig.BitBoard = position;

                }
                bestFigure = curBest;
                bestMove = curBestMove;
                return max;

            }
            else
            {
                WhitePositions = PositionOfWhitePieces;
                InitialiseWhiteAttack();

                BlackPositions = PositionOfBlackPieces;
                InitialiseBlackAttack();


                if (depth <= 0)
                    return EvaluateBoard2(true); //it is evaluated for white because it is looking for the most vacant positions

                bool badMove = EvaluateNode(white);
                if (badMove)
                    return -1000;

                int min = 1000;
                Figure curBest = null;
                ulong curBestMove = 0;
                foreach (Figure fig in BlackPieces)
                {
                    ulong position = fig.BitBoard;
                    List<ulong> moveList = Figure.PosibleMovesStatic(fig, BlackPositions, WhitePositions, WhiteAttackBoard);

                    // check for draw
                    if (moveList.Count == 0 && fig is King)
                    {
                        King blackKing = (King)fig;
                        if (blackKing.IsAttacked(WhiteAttackBoard))
                            return 1000;
                        else return -1000;
                    }
                    foreach (ulong move in moveList)
                    {

                        fig.BitBoard = move;
                        int tmp = MinMax(true, ref bestFigure, ref bestMove, fig, move, depth - 1);
                        if (tmp < min)
                        {
                            min = tmp;
                            bestFigure = fig;
                            bestMove = move;
                        }
                    }
                    fig.BitBoard = position;
                }
                bestFigure = curBest;
                bestMove = curBestMove;

                return min;

            }


        }

        public int MinMax2(bool white, ref Figure bestFigure, ref ulong bestMove, int depth)
        {

            if (white)
            {
                BlackPositions = PositionOfBlackPieces;
                InitialiseBlackAttack();


                WhitePositions = PositionOfWhitePieces;
                InitialiseWhiteAttack();
                if (depth <= 0)
                    return EvaluateBoard2(true);

                bool badMove = EvaluateNode(white/*, ref blackKingChecked*/);
                if (badMove)
                    return -1000;// int.MinValue;

                int max = -1000;// int.MinValue;
                Figure curBest = null;
                ulong curBestMove = 0;
                foreach (Figure fig in WhitePieces)
                {
                    ulong position = fig.BitBoard;
                    List<ulong> moveList = Figure.PosibleMovesStatic(fig, WhitePositions, BlackPositions, BlackAttackBoard);


                    foreach (ulong move in moveList)
                    {
                        fig.BitBoard = move;
                        int tmp = MinMax2(false, ref bestFigure, ref bestMove, depth - 1);
                        if (tmp > max)
                        {
                            max = tmp;
                            curBest = fig;
                            curBestMove = move;
                        }

                    }
                    fig.BitBoard = position;
                    WhitePositions = PositionOfWhitePieces;

                }
                bestFigure = curBest;
                bestMove = curBestMove;

                return max - 1;

            }
            else
            {
                WhitePositions = PositionOfWhitePieces;
                InitialiseWhiteAttack();

                BlackPositions = PositionOfBlackPieces;
                InitialiseBlackAttack();


                if (depth <= 0)
                    return EvaluateBoard2(false);//evaluira se za belog jer trazi najvise slobodnih pozicija     

                //bool blackKingChecked = false;
                bool badMove = EvaluateNode(white/*, ref blackKingChecked*/);
                if (badMove)
                    return -1000; //int.MinValue;

                int min = 1000;// int.MaxValue;
                Figure curBest = null;
                ulong curBestMove = 0;
                foreach (Figure fig in BlackPieces)
                {
                    ulong position = fig.BitBoard;

                    List<ulong> moveList = Figure.PosibleMovesStatic(fig, BlackPositions, WhitePositions, WhiteAttackBoard);

                    //test za pat
                    if (moveList.Count == 0 && fig is King)
                    {
                        King blackKing = (King)fig;
                        if (blackKing.IsAttacked(WhiteAttackBoard))
                            return 1000;// int.MaxValue;
                        else return -1000;// int.MinValue;
                    }
                    foreach (ulong move in moveList)//da se doda if za list count=0 da proveri pat??
                    {

                        //fig.Move(move, WhitePositions, BlackPositions, BlackAttackBoard);//treba druga fja, neka koja ne proverava validnost poteza
                        fig.BitBoard = move;
                        int tmp = MinMax2(true, ref bestFigure, ref bestMove, depth - 1);
                        if (tmp < min)
                        {
                            min = tmp;
                            bestFigure = fig;
                            bestMove = move;

                        }
                    }
                    fig.BitBoard = position;
                    BlackPositions = PositionOfBlackPieces;
                }
                bestFigure = curBest;
                bestMove = curBestMove;

                return min - 1;

            }

        }
    }
}


