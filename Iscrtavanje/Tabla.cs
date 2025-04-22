using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using ChessEngine;

namespace Iscrtavanje
{
    #region Enum

    enum FieldChess
    {
        A, B, C, D, E, F, G, H
    }

    #endregion

    public class Tabla
    {
        #region Atributes

        private Panel _platno;
        private int _sizeX;
        private int _sizeY;
        private int size = 8;
        private Polje[] _nizPolja;

        #endregion

        #region Constructors

        public Tabla(int x, int y)
        {
            _platno = new Panel();
            _sizeY = y;
            _sizeX = x;
            _platno.Paint += new PaintEventHandler(IscrtajTablu);
            //_platno.MouseClick += new MouseEventHandler(onClick);
            _platno.Size = new Size((size + 2 /*2*/) * _sizeX, (size + 2) * _sizeY);        //mrckam velicinu
            _platno.Location = new Point(0, 0);
            //_platno


            _nizPolja = new Polje[64];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Point p = new Point(_sizeX + j * _sizeX, (i + 1) * _sizeY);
                    if ((i + j) % 2 == 1)
                    {
                        _nizPolja[i * 8 + j] = new Polje(p, true, _sizeX);
                    }
                    else
                    {
                        _nizPolja[i * 8 + j] = new Polje(p, false, _sizeX);
                    }
                    _platno.Controls.Add(_nizPolja[i * 8 + j].PlatnoSlike);
                    _nizPolja[i * 8 + j].PoljeClicked += this.onClick;
                }
            }
            _platno.Visible = true;
        }

        #endregion

        #region Methods

            private Point[] izvlacenjePozicije(ulong Bitboard_code)
        {
            int i, j;
            List<Point> pozicije = new List<Point>();
            ulong temp = Bitboard_code;
            //ostaje za promenu zbog smera kodiranja BitBoard-a
            for (i = 0; i < 8; i++)
            {
                int rank = (char)(temp >> 7 * 8);
                temp = temp << 8;
                for (j = 0; j < 8; j++)
                {
                    if ((rank / 128) == 1)
                    {
                        pozicije.Add(new Point(i, j));
                    }
                    rank = rank << 1;
                    rank = rank & 0xff;
                }
            }
            return pozicije.ToArray();
        }
    
            private void ucrtajNaTacku(Point pozicija, Bitmap slika)
        {
            Bitmap b = new Bitmap(slika, new Size(_sizeX, _sizeY));
            _nizPolja[pozicija.X * 8 + pozicija.Y].Slicica = new TextureBrush(b);
            _nizPolja[pozicija.X * 8 + pozicija.Y].PlatnoSlike.Invalidate();
        }

            private void onClick(ulong bit)
        {
            this.onClicked(bit);
        }

            #region Event Handrel

                private void IscrtajTablu(object sender, PaintEventArgs e)
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
                            _platno.Controls.Add(l);
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
                                _platno.Controls.Add(l1);
                            }
                            #endregion
                        }
                    }
                }

        #endregion

        #endregion
 
        #region Public API

            public void IscrtajFiguru(ulong Bitboard_code, Bitmap SlikaFigure)
            {
                Point[] pozicije = this.izvlacenjePozicije(Bitboard_code);
                foreach (Point p in pozicije)
                {
                    ucrtajNaTacku(p, SlikaFigure);
                }
            }

            public void IzbrisiFiguru(ulong BitBoard)
            {
                Point[] pozicije = this.izvlacenjePozicije(BitBoard);
                foreach(Point p in pozicije)
                {
                    _nizPolja[p.X * size + p.Y].Slicica = null;
                    _nizPolja[p.X * size + p.Y].PlatnoSlike.Refresh();
                }
            }

            public void Prikaci(Control forma)
            {
                forma.Controls.Add(_platno);
                forma.Size = _platno.Size;
                forma.Invalidate();

            }

            public void Refresh()
            {
                _platno.Refresh();
            }

            public void Clear()
            {
                foreach (Polje p in _nizPolja)
                {
                    p.Slicica = null;
                    p.Oznaceno = false;
                    if (p.Boja)
                        p.Cetkica = new SolidBrush(Globalne.TamnaPolja);
                    else
                        p.Cetkica = new SolidBrush(Globalne.SvetlaPolja);

                }
            }

            #region Event Handlers

                public void onMove(ulong former, ulong next,Figure pointless=null)
                {
                    Point[] f = this.izvlacenjePozicije(former);
                    TextureBrush b = _nizPolja[f[0].X * size + f[0].Y].Slicica;
                    _nizPolja[f[0].X * size + f[0].Y].Slicica = null;
                    _nizPolja[f[0].X * size + f[0].Y].PlatnoSlike.Invalidate();
                    f = this.izvlacenjePozicije(next);
                    _nizPolja[f[0].X * size + f[0].Y].Slicica = b;
                    _nizPolja[f[0].X * size + f[0].Y].PlatnoSlike.Invalidate();
                     _platno.Update();
                    onCheckPlayer(this, new EventArgs());
                }

                public void onDeselect(object sender, EventArgs args)
                {
                    foreach (Polje p in _nizPolja)
                    {
                        if (p.Oznaceno)
                        {
                            p.Oznaceno = false;
                            if (p.Boja)
                                p.Cetkica = new SolidBrush(Globalne.TamnaPolja);
                            else
                                p.Cetkica = new SolidBrush(Globalne.SvetlaPolja);
                            p.PlatnoSlike.Invalidate();
                        }
                    }
                }

                public void OnHighlight(ulong polja)
                {
                    SolidBrush black = new SolidBrush(Color.FromArgb(Globalne.TamnaPolja.R + 50,
                        Globalne.TamnaPolja.G + 50, Globalne.TamnaPolja.B + 50));
                    SolidBrush white = new SolidBrush(Color.FromArgb(205, 205, 205));
                    Point[] h = this.izvlacenjePozicije(polja);
                    foreach (Point p in h)
                    {
                        if (_nizPolja[p.X * size + p.Y].Boja)
                            _nizPolja[p.X * size + p.Y].Cetkica = black;
                        else
                            _nizPolja[p.X * size + p.Y].Cetkica = white;
                        _nizPolja[p.X * size + p.Y].Oznaceno = true;
                        _nizPolja[p.X * size + p.Y].PlatnoSlike.Invalidate();
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
                    MessageBox.Show("If you see this message please inform us. +381 60 369-58-58", "Stalemate", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
