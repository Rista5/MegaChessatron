using System.Drawing;
using System.Windows.Forms;

namespace Draw
{
    class Field
    {
        #region Atributes

        private int _rankActual;//rank
        private int _fileActual;//file
        private int _size;
        private bool _marked;
        private bool _color;

        private Panel _imagePanel;
        private Brush _brush;
        private Pen _pen;
        private TextureBrush _picture = null;

        #endregion

        #region Property
        public bool Color
        {
            get { return _color; }
        }

        public Brush Brush
        {
            get { return _brush; }
            set { _brush = value; }
        }

        public bool Marked
        {
            get { return _marked; }
            set { _marked = value; }
        }

        public TextureBrush Picture
        {
            get { return _picture; }
            set { _picture = value; }
        }

        public Panel ImagePanel
        {
            get { return _imagePanel; }
            set { _imagePanel = value; }
        }

        #endregion

        #region Constructors

        public Field(Point p, bool b, int size)
        {
            _imagePanel = new Panel();
            _rankActual = p.X;
            _fileActual = p.Y;

            _imagePanel.Location = p;
            _imagePanel.Visible = true;
            _imagePanel.Size = new Size(size, size);
            _imagePanel.BorderStyle = BorderStyle.FixedSingle;
            _imagePanel.Paint += new PaintEventHandler(onPaint);
            _imagePanel.MouseClick += new MouseEventHandler(onClick);

            if (b == false)
            {
                _brush = new SolidBrush(GlobalVariables.LightFieldColor);
            }
            else
            {
                _brush = new SolidBrush(GlobalVariables.DarkFieldColor);
            }
            _color = b;
            _pen = new Pen(System.Drawing.Color.Black);
            _size = size;
        }

        #endregion

        #region Methods

        private ulong BitboardRepresentation()
        {
            int xActual = _rankActual / _size - 1;
            int yActual = _fileActual / _size - 1;
            ulong result = 9223372036854775808;
            result = result >> (yActual * 8 + xActual);
            return result;
        }

        #endregion

        #region Event Handlers

        public void onPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle r = new Rectangle(0, 0, _size, _size);
            g.DrawRectangle(_pen, r);
            g.FillRectangle(_brush, r);
            if (_picture != null)
            {
                Rectangle r1 = new Rectangle(0, 0, _size, _size);
                g.FillRectangle(_picture, r1);
            }
        }

        private void onClick(object sender, MouseEventArgs e)
        {
            ulong clicked = this.BitboardRepresentation();
            this.onClicked(clicked);
        }
        #endregion

        #region Delegates

        public delegate void FieldClick(ulong bitboard);

        #endregion

        #region Events

        public event FieldClick FieldClicked;

        public void onClicked(ulong bit)
        {
            FieldClick handler = FieldClicked;
            if (handler != null)
                handler(bit);
        }

        #endregion
    }
}
