using ChessEngine;
using Draw;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MegaChessatron
{
    public partial class GameForm : Form
    {

        public GameForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        #region Atributes

        int widthForm, heightForm;
        Table tablica = new Table(50, 50);
        private Board _board;
        private bool _ai;

        #endregion

        #region Property

        public Board Board
        {
            get { return _board; }
            set
            {
                _board = value;
                List<Figure> list = _board.WhitePieces;
                foreach (Figure f in list)
                {
                    if (f is King)
                        tablica.DrawFigure(f.BitBoard, Global.WhiteKing);
                    else tablica.DrawFigure(f.BitBoard, Global.WhiteBishop1);
                }
                list = _board.BlackPieces;
                foreach (Figure f in list)
                {
                    if (f is King)
                        tablica.DrawFigure(f.BitBoard, Global.BlackKing);
                    else tablica.DrawFigure(f.BitBoard, Properties.Resources.Chess_bdt60);
                }
                //treba da se povezu API
                if (AI)
                    tablica.Clicked += _board.OnClickEventHandlerAI;
                else tablica.Clicked += _board.OnClickEventHandler;
                _board.Deselect += tablica.onDeselect;
                _board.FigureMoved += tablica.onMove;
                _board.Highlight += tablica.OnHighlight;
                _board.CheckMate += tablica.onCheckMate;
                _board.Draw += tablica.onPat;
                _board.KingChecked += tablica.onKingChecked;

            }
        }

        public bool AI
        {
            get
            {
                return _ai;
            }

            set
            {
                _ai = value;
            }
        }

        #endregion

        #region Event Handlers

        private void Form1_Load(object sender, EventArgs e)
        {
            widthForm = this.ClientSize.Width - panel1.Width;
            heightForm = this.ClientSize.Height - panel1.Height;
            tablica.Attach(this.panel1);
            tablica.checkPlayer += this.checkPlayerHandler;
            AI = true;
            PlayerIndication();
            List<Figure> listW = new List<Figure>();
            List<Figure> listB = new List<Figure>();
            listW.Add(new King(Board.FieldOnBoard('5', 'F'), true));
            listW.Add(new Bishop(Board.FieldOnBoard('5', 'E'), true));
            listB.Add(new King(Board.FieldOnBoard('5', 'H'), false));
            listW.Add(new Bishop(Board.FieldOnBoard('4', 'E'), true));
            Board = new Board(listW, listB);

            ClearPreviousBoards();

            this.LoadXMLLib("TranspositionTable.xml");
            this.lblSeachDeapth.Text = "SearchDepth: " + Board.SearchDepth.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SaveXMLLib("TranspositionTable.xml");
        }

        #region ToolStripManu

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult rez;
            rez = MessageBox.Show("Do you want to start new game?", "New game", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rez == DialogResult.No)
                return;
            if (AI)
                tablica.Clicked -= _board.OnClickEventHandlerAI;
            else tablica.Clicked -= _board.OnClickEventHandler;
            tablica.Clear();

            List<Figure> listW = new List<Figure>();
            List<Figure> listB = new List<Figure>();
            listW.Add(new King(Board.FieldOnBoard('8', 'H'), true));
            listW.Add(new Bishop(Board.FieldOnBoard('8', 'G'), true));
            listB.Add(new King(Board.FieldOnBoard('1', 'H'), false));
            listW.Add(new Bishop(Board.FieldOnBoard('8', 'F'), true));

            //Board = new Board(listW, listB);//ovo je pravilo gresku

            Dictionary<ulong, HashNode> tmp;
            if (Board != null)
            {
                tmp = Board.TranspositionTable;
                Board = new Board(listW, listB, tmp);
            }
            else Board = new Board(listW, listB);

            btnCrni.BackColor = Color.FromKnownColor(KnownColor.Control);
            btnBeli.BackColor = Color.Red;

            ClearPreviousBoards();
            PlayerIndication();
            tablica.Refresh();
        }

        private void currentDepthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Board.SearchDepth >= 10)
                Board.SearchDepth = 10;
            else
                Board.SearchDepth++;
            lblSeachDeapth.Text = "SearchDepth: " + Board.SearchDepth.ToString();
        }

        private void decreaseSeachDepthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Board.SearchDepth <= 4)
                Board.SearchDepth = 4;
            else
                Board.SearchDepth--;
            lblSeachDeapth.Text = "SearchDepth: " + Board.SearchDepth.ToString();
        }



        private void setupBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBordSetup frm = new FormBordSetup();
            DialogResult rez = frm.ShowDialog();
            if (rez == DialogResult.Cancel)
                return;
            if (AI)
                tablica.Clicked -= _board.OnClickEventHandlerAI;
            else tablica.Clicked -= _board.OnClickEventHandler;

            tablica.Clear();

            List<Figure> listW = new List<Figure>();
            List<Figure> listB = new List<Figure>();
            listW.Add(new King(frm.WhiteKing, true));
            listW.Add(new Bishop(frm.WhiteBishop1, true));
            listB.Add(new King(frm.BlackKing, false));
            listW.Add(new Bishop(frm.WhiteBishop2, true));

            Dictionary<ulong, HashNode> tmp;
            if (Board != null)
            {
                tmp = Board.TranspositionTable;
                Board = new Board(listW, listB, tmp);
            }
            else Board = new Board(listW, listB);

            ClearPreviousBoards();

            btnCrni.BackColor = Color.FromKnownColor(KnownColor.Control);
            btnBeli.BackColor = Color.Red;

            PlayerIndication();
            tablica.Refresh();
        }

        private void iconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormIcons frm = new FormIcons();
            DialogResult rez = frm.ShowDialog();
            if (rez == DialogResult.OK)
            {
                Global.WhiteBishop1 = frm.Bishop;
                Global.WhiteKing = frm.WhiteKing;
                Global.BlackKing = frm.BlackKing;
            }
        }

        #endregion

        private void panel1_ClientSizeChanged(object sender, EventArgs e)
        {
            Size s = new Size(panel1.ClientSize.Width + widthForm
                , panel1.ClientSize.Height + heightForm);

            this.ClientSize = s;
        }

        private void turnOffToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!AI)
                return;
            AI = false;
            tablica.Clicked -= _board.OnClickEventHandlerAI;
            tablica.Clicked += _board.OnClickEventHandler;
        }

        private void turnOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AI)
                return;
            AI = true;
            tablica.Clicked -= _board.OnClickEventHandler;
            tablica.Clicked += _board.OnClickEventHandlerAI;
        }

        private void loadTranspositionTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream f = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    Board.LoadTableXML(f);
                }
            }
        }

        private void saveTranspositionTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream f = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    Board.SaveTableXML(f);
                }
            }
        }

        private void checkPlayerHandler(object sender, EventArgs e)
        {
            if (!_board.WhiteOnMove)
            {
                btnCrni.BackColor = Color.FromKnownColor(KnownColor.Control);
                btnBeli.BackColor = Color.Red;
            }
            else
            {
                btnBeli.BackColor = Color.FromKnownColor(KnownColor.Control);
                btnCrni.BackColor = Color.Red;
            }
        }


        #endregion

        #region Methods

        private void LoadXMLLib(String path)
        {
            try
            {
                using (FileStream f = new FileStream(path, FileMode.Open))
                {
                    Board.LoadTableXML(f);
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Transposition table went on a smoke break. Left a message: \"Cu se vrnem!\"", "Table load",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SaveXMLLib(String path)
        {
            try
            {
                using (FileStream f = new FileStream(path, FileMode.Create))
                {
                    Board.SaveTableXML(f);
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Transposition table died on transportation to storage facility. RIP in peace", "Table load",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SaveShell()
        {
            this.SaveXMLLib("TranspositionTable.xml");
        }

        private void ClearPreviousBoards()
        {
            Board.PreviousBoards = new List<Board>();//puni se tabela prethodnih poteza
            Board.PreviousBoards.Add(new Board());
            Board.PreviousBoards.Add(new Board());
            Board.PreviousBoards.Add(new Board());
            Board.PreviousBoards.Add(new Board());
            Board.PreviousBoards.Add(new Board());
            Board.PreviousBoards.Add(new Board());
            Board.PreviousBoards.Add(new Board());
            Board.PreviousBoards.Add(new Board());
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Thread NitDialog = new Thread(SaveShell);
            NitDialog.Start();
            MessageBox.Show("Closing", null);
            NitDialog.Join();
        }

        private void PlayerIndication()
        {
            if (AI)
            {
                btnBeli.BackColor = Color.FromKnownColor(KnownColor.Control);
                btnCrni.BackColor = Color.Red;
            }
            else
            {
                btnBeli.BackColor = Color.Red;
                btnCrni.BackColor = Color.FromKnownColor(KnownColor.Control);
            }
        }
        #endregion
    }
}

