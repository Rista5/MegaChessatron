using ChessEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Draw
{
    #region Enum

    enum FieldChess
    {
        A, B, C, D, E, F, G, H
    }

    #endregion

    public class Table
    {
        #region Atributes

        private Panel _panel;
        private int _sizeX;
        private int _sizeY;
        private int size = 8;
        private Field[] _fields;

        #endregion

        #region Constructors

        public Table(int x, int y)
        {
            _panel = new Panel();
            _sizeY = y;
            _sizeX = x;
            _panel.Paint += new PaintEventHandler(DrawTable);
            _panel.Size = new Size((size + 2 ) * _sizeX, (size + 2) * _sizeY);
            _panel.Location = new Point(0, 0);


            _fields = new Field[64];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Point p = new Point(_sizeX + j * _sizeX, (i + 1) * _sizeY);
                    if ((i + j) % 2 == 1)
                    {
                        _fields[i * 8 + j] = new Field(p, true, _sizeX);
                    }
                    else
                    {
                        _fields[i * 8 + j] = new Field(p, false, _sizeX);
                    }
                    _panel.Controls.Add(_fields[i * 8 + j].ImagePanel);
                    _fields[i * 8 + j].FieldClicked += this.onClick;
                }
            }
            _panel.Visible = true;
        }

        #endregion

        #region Methods

        private Point[] GetPossiblePositions(ulong Bitboard_code)
        {
            int i, j;
            List<Point> positions = new List<Point>();
            ulong temp = Bitboard_code;
            for (i = 0; i < 8; i++)
            {
                int rank = (char)(temp >> 7 * 8);
                temp = temp << 8;
                for (j = 0; j < 8; j++)
                {
                    if ((rank / 128) == 1)
                    {
                        positions.Add(new Point(i, j));
                    }
                    rank = rank << 1;
                    rank = rank & 0xff;
                }
            }
            return positions.ToArray();
        }

        private void DrawOnPoint(Point pozicija, Bitmap slika)
        {
            Bitmap b = new Bitmap(slika, new Size(_sizeX, _sizeY));
            _fields[pozicija.X * 8 + pozicija.Y].Picture = new TextureBrush(b);
            _fields[pozicija.X * 8 + pozicija.Y].ImagePanel.Invalidate();
        }

        private void onClick(ulong bit)
        {
            this.onClicked(bit);
        }

        #region Event Handrel

        private void DrawTable(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int i = 0; i < size + 1; i++)
            {
                #region Rank
                if (i < size)
                {
                    Label l = new Label();
                    l.Text = (7 - i + 1).ToString();
                    l.Size = new Size(_sizeX, _sizeY);
                    l.Location = new Point(0, (i + 1) * _sizeY);
                    l.Visible = true;
                    l.BorderStyle = BorderStyle.None;
                    l.TextAlign = ContentAlignment.MiddleCenter;
                    l.Font = new Font("Times New Roman", 20.0f, FontStyle.Bold);
                    _panel.Controls.Add(l);
                    l = null;
                }
                #endregion
                for (int j = 0; j < size; j++)
                {
                    #region File
                    if (i == size)
                    {

                        Label l1 = new Label();
                        l1.Text = ((FieldChess)(j)).ToString();
                        l1.Size = new Size(_sizeX, _sizeY);
                        l1.Location = new Point((j + 1) * _sizeX, (size + 1) * _sizeY);
                        l1.Visible = true;
                        l1.BorderStyle = BorderStyle.None;
                        l1.TextAlign = ContentAlignment.MiddleCenter;
                        l1.Font = new Font("Times New Roman", 20.0f, FontStyle.Bold);
                        _panel.Controls.Add(l1);
                    }
                    #endregion
                }
            }
        }

        #endregion

        #endregion

        #region Public API

        public void DrawFigure(ulong Bitboard_code, Bitmap SlikaFigure)
        {
            Point[] pozicije = GetPossiblePositions(Bitboard_code);
            foreach (Point p in pozicije)
            {
                DrawOnPoint(p, SlikaFigure);
            }
        }

        public void RemoveFigure(ulong BitBoard)
        {
            Point[] pozicije = GetPossiblePositions(BitBoard);
            foreach (Point p in pozicije)
            {
                _fields[p.X * size + p.Y].Picture = null;
                _fields[p.X * size + p.Y].ImagePanel.Refresh();
            }
        }

        public void Attach(Control form)
        {
            form.Controls.Add(_panel);
            form.Size = _panel.Size;
            form.Invalidate();

        }

        public void Refresh()
        {
            _panel.Refresh();
        }

        public void Clear()
        {
            foreach (Field p in _fields)
            {
                p.Picture = null;
                p.Marked = false;
                if (p.Color)
                    p.Brush = new SolidBrush(GlobalVariables.DarkFieldColor);
                else
                    p.Brush = new SolidBrush(GlobalVariables.LightFieldColor);

            }
        }

        #region Event Handlers

        public void onMove(ulong former, ulong next, Figure pointless = null)
        {
            Point[] f = this.GetPossiblePositions(former);
            TextureBrush b = _fields[f[0].X * size + f[0].Y].Picture;
            _fields[f[0].X * size + f[0].Y].Picture = null;
            _fields[f[0].X * size + f[0].Y].ImagePanel.Invalidate();
            f = this.GetPossiblePositions(next);
            _fields[f[0].X * size + f[0].Y].Picture = b;
            _fields[f[0].X * size + f[0].Y].ImagePanel.Invalidate();
            _panel.Update();
            onCheckPlayer(this, new EventArgs());
        }

        public void onDeselect(object sender, EventArgs args)
        {
            foreach (Field p in _fields)
            {
                if (p.Marked)
                {
                    p.Marked = false;
                    if (p.Color)
                        p.Brush = new SolidBrush(GlobalVariables.DarkFieldColor);
                    else
                        p.Brush = new SolidBrush(GlobalVariables.LightFieldColor);
                    p.ImagePanel.Invalidate();
                }
            }
        }

        public void OnHighlight(ulong polja)
        {
            SolidBrush black = new SolidBrush(Color.FromArgb(GlobalVariables.DarkFieldColor.R + 50,
                GlobalVariables.DarkFieldColor.G + 50, GlobalVariables.DarkFieldColor.B + 50));
            SolidBrush white = new SolidBrush(Color.FromArgb(205, 205, 205));
            Point[] h = this.GetPossiblePositions(polja);
            foreach (Point p in h)
            {
                if (_fields[p.X * size + p.Y].Color)
                    _fields[p.X * size + p.Y].Brush = black;
                else
                    _fields[p.X * size + p.Y].Brush = white;
                _fields[p.X * size + p.Y].Marked = true;
                _fields[p.X * size + p.Y].ImagePanel.Invalidate();
            }
        }

        public void onKingChecked(object sender, EventArgs e)
        {
            //MessageBox.Show("You can not escape.", "CHECK", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void onCheckMate(object sender, EventArgs e)
        {
            MessageBox.Show("Error 404: Moves not found", "CHECKMATE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void onPat(object sender, EventArgs e)
        {
            MessageBox.Show("If you see this message please inform us.", "Stalemate", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #endregion

        #region Delegates

        public delegate void Click(ulong bitboardRep);

        #endregion

        #region Events

        public event Click Clicked;

        private void onClicked(ulong bit)
        {
            Click handler = Clicked;
            if (handler != null)
            {
                handler(bit);
            }
        }

        public event EventHandler checkPlayer;

        private void onCheckPlayer(object sender, EventArgs e)
        {
            EventHandler handler = checkPlayer;
            if (handler != null)
                handler(sender, e);
        }

        #endregion
    }
}
