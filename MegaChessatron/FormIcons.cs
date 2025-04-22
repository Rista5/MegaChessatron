using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MegaChessatron.Properties;

namespace MegaChessatron
{
 
    public partial class FormIcons : Form
    {
        public FormIcons()
        {
            InitializeComponent();
        }

        #region Property
        public Bitmap Bishop
        { get; set; }
        public Bitmap WhiteKing
        { get; set; }
        public Bitmap BlackKing
        { get; set; }
        #endregion

        private void FormIcons_Load(object sender, EventArgs e)
        {
            cbxWhiteBishop.Items.Add(new Slika(Resources.Chess_blt60, "Classic"));
            cbxWhiteBishop.Items.Add(new Slika(Resources.megapop, "Realistic"));
            cbxWhiteBishop.Items.Add(new Slika(Resources.swissGuard, "English Reform: Swiss Guard"));
            cbxWhiteBishop.SelectedIndex = 0;
            cbxWhiteKing.Items.Add(new Slika(Resources.Chess_klt60, "Classic"));
            cbxWhiteKing.Items.Add(new Slika(Resources.pope, "English Reform: Pope"));
            cbxWhiteKing.SelectedIndex = 0;
            cbxBlackKing.Items.Add(new Slika(Resources.Chess_kdt60, "Classic"));
            cbxBlackKing.Items.Add(new Slika(Resources.henry, "English Reform: Henry"));
            cbxBlackKing.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Slika s = (Slika)cbxWhiteBishop.SelectedItem;
            pictureBox1.Image=(Image)s._picture;
            Bishop = s._picture;
        }

        private void cbxWhiteKing_SelectedIndexChanged(object sender, EventArgs e)
        {
            Slika s = (Slika)cbxWhiteKing.SelectedItem;
            pbxWhiteKing.Image = (Image)s._picture;
            WhiteKing = s._picture;
        }

        private void cbxBlackKing_SelectedIndexChanged(object sender, EventArgs e)
        {
            Slika s = (Slika)cbxBlackKing.SelectedItem;
            pbxBlackKing.Image = (Image)s._picture;
            BlackKing = s._picture;
        }
    }

    class Slika
    {
        public Bitmap _picture;
        public string _naziv;

        public Slika(Bitmap s, string n)
        {
            _picture = new Bitmap(s,new Size(75,75));
            _naziv = n;
        }

        public override string ToString()
        {
            return _naziv;
        }
    }
}
