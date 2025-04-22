using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Iscrtavanje
{
    class Polje
    {
        #region Atributes

        private int _rankActual;//rank
        private int _fileActual;//file
        private int _size;
        private bool _oznaceno;
        private bool _boja;

        private Panel _platnoSlike;

        private Brush _cetkica;
        private Pen _olovka;
        private TextureBrush _slicica = null;

        #endregion

        #region Property
        public bool Boja
        {
            get { return _boja; }
        }

        public Brush Cetkica
        {
            get { return _cetkica; }
            set { _cetkica = value; }
        }

        public bool Oznaceno
        {
            get { return _oznaceno; }
            set { _oznaceno = value; }
        }

        public TextureBrush Slicica
        {
            get { return _slicica; }
            set { _slicica = value; }
        }

        public Panel PlatnoSlike
        {
            get { return _platnoSlike; }
            set { _platnoSlike = value; }
        }

        #endregion

        #region Constructors

        public Polje(Point p, bool b, int size)
        {
            _platnoSlike = new Panel();
            _rankActual = p.X;
            _fileActual = p.Y;

            _platnoSlike.Location = p;
            _platnoSlike.Visible = true;
            _platnoSlike.Size = new Size(size, size);
            _platnoSlike.BorderStyle = BorderStyle.FixedSingle;
            _platnoSlike.Paint += new PaintEventHandler(onPaintPolje);
            _platnoSlike.MouseClick += new MouseEventHandler(onClick);

            if (b == false)
            {
                _cetkica = new SolidBrush(Globalne.SvetlaPolja);
            }
            else
            {
                _cetkica = new SolidBrush(Globalne.TamnaPolja);
            }
            _boja = b;
            _olovka = new Pen(Color.Black);
            _size = size;
        }

        #endregion

        #region Methods

        private ulong BitboardRepresentation()
        {
            int xActual = _rankActual / _size - 1;
            int yActual = _fileActual / _size - 1;
            ulong rez = 9223372036854775808;
            rez = rez >> (yActual * 8 + xActual);
            return rez;
        }

        #endregion

        #region Event Handlers

        public void onPaintPolje(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle r = new Rectangle(0, 0, _size, _size);
            g.DrawRectangle(_olovka, r);
            g.FillRectangle(_cetkica, r);
            if (_slicica != null)
            {
                Rectangle r1 = new Rectangle(0, 0, _size, _size);
                g.FillRectangle(_slicica, r1);
            }
        }

        private void onClick(object sender, MouseEventArgs e)
        {
            ulong clicked = this.BitboardRepresentation();
            this.onPoljeClicked(clicked);
        }
        #endregion

        #region Delegates

        public delegate void PoljeClick(ulong bitboard);

        #endregion

        #region Events

        public event PoljeClick PoljeClicked;

        public void onPoljeClicked(ulong bit)
        {
            PoljeClick handler = PoljeClicked;
            if (handler != null)
                handler(bit);
        }

        #endregion
    }
}
