using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Iscrtavanje;

namespace MegaChessatron
{
    public partial class FormBordSetup : Form
    {
        public FormBordSetup()
        {
            InitializeComponent();
        }

        int widthForm, heightForm;
        Tabla tabla = new Tabla(40,40);

        #region Property

        public ulong WhiteBishop1
        {
            get;
            set;
        }

        public ulong WhiteBishop2
        { get; set;}

        public ulong WhiteKing
        { get; set; }

        public ulong BlackKing
        { get; set; }

        #endregion

        #region Event Handlers

        private void FormBordSetup_Load(object sender, EventArgs e)
        {
            widthForm = this.ClientSize.Width - panel1.Width;
            heightForm = this.ClientSize.Height - panel1.Height;
            tabla.Prikaci(panel1);
            tabla.Clicked += this.mouseClick;
        }

        #region Button Clicks

        private void btnWhiteKing_Click(object sender, EventArgs e)
        {
            //highlight
            btnBlackKing.BackColor = Color.FromKnownColor(KnownColor.Control);
            btnWhiteBishop1.BackColor = Color.FromKnownColor(KnownColor.Control);
            btnWhiteBishop2.BackColor = Color.FromKnownColor(KnownColor.Control);
            btnWhiteKing.BackColor = Color.DeepSkyBlue;

            rbtnWhiteKing.Checked = true;
        }

        private void btnWhiteBishop1_Click(object sender, EventArgs e)
        {
            btnBlackKing.BackColor = Color.FromKnownColor(KnownColor.Control);
            btnWhiteKing.BackColor = Color.FromKnownColor(KnownColor.Control);
            btnWhiteBishop2.BackColor = Color.FromKnownColor(KnownColor.Control);
            btnWhiteBishop1.BackColor = Color.DeepSkyBlue;

            rbtnWhiteBishop1.Checked = true;
        }

        private void btnWhiteBishop2_Click(object sender, EventArgs e)
        {
            btnBlackKing.BackColor = Color.FromKnownColor(KnownColor.Control);
            btnWhiteKing.BackColor = Color.FromKnownColor(KnownColor.Control);
            btnWhiteBishop1.BackColor = Color.FromKnownColor(KnownColor.Control);
            btnWhiteBishop2.BackColor = Color.DeepSkyBlue;

            rbtnWhiteBishop2.Checked = true;
        }

        private void btnBlackKing_Click(object sender, EventArgs e)
        {
            btnWhiteKing.BackColor = Color.FromKnownColor(KnownColor.Control);
            btnWhiteBishop2.BackColor = Color.FromKnownColor(KnownColor.Control);
            btnWhiteBishop1.BackColor = Color.FromKnownColor(KnownColor.Control);
            btnBlackKing.BackColor = Color.DeepSkyBlue;

            rbtBlackKing.Checked = true;
        }

        #endregion

        private void panel1_ClientSizeChanged(object sender, EventArgs e)
        {
            Size s = new Size(panel1.ClientSize.Width + widthForm
                , panel1.ClientSize.Height + heightForm);

            this.ClientSize = s;
        }

        private void mouseClick(ulong bit)
        {
            if (rbtBlackKing.Checked)
            {
                ulong b = BlackKing;
                BlackKing = bit;
                tabla.IzbrisiFiguru(b);
                tabla.IscrtajFiguru(bit, Global.BlackKing);
                //tabla.Refresh();
            }
            if (rbtnWhiteBishop1.Checked)
            {
                ulong b = WhiteBishop1;
                WhiteBishop1 = bit;
                tabla.IzbrisiFiguru(b);
                tabla.IscrtajFiguru(bit, Global.WhiteBishop1);
                //tabla.Refresh();
            }
            if (rbtnWhiteBishop2.Checked)
            {
                ulong b = WhiteBishop2;
                WhiteBishop2 = bit;
                tabla.IzbrisiFiguru(b);
                tabla.IscrtajFiguru(bit, Global.WhiteBishop1);
                //tabla.Refresh();
            }
            if (rbtnWhiteKing.Checked)
            {
                ulong b = WhiteKing;
                WhiteKing = bit;
                tabla.IzbrisiFiguru(b);
                tabla.IscrtajFiguru(bit, Global.WhiteKing);
                //tabla.Refresh();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!Validacija())
            {
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        #endregion

        #region Methods

        private bool Validacija()
        {
            if (BlackKing == 0)
            {
                MessageBox.Show("Crni kralj nije postavljen", "Nepotpuna tabla",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (WhiteBishop1 == 0 || WhiteBishop2 == 0)
            {
                MessageBox.Show("Beli lovac nije postavljen", "Nepotpuna tabla",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (WhiteKing == 0)
            {
                MessageBox.Show("Beli kralj nije postavljen", "Nepotpuna tabla",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        #endregion
    }
}
