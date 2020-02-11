using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris_Novi.Klase;

namespace Tetris_Novi.User_control
{
    public partial class Polje : UserControl
    {
        public Polje()
        {
            InitializeComponent();
        }

       public void paint(PaintEventArgs e)
        {
            for (int i = 0; i < GlavnoPolje.ObjekatKlaseGlavnoPolje.N; i++)
            {
                for (int j = 0; j < GlavnoPolje.ObjekatKlaseGlavnoPolje.M; j++)
                {
                    if (GlavnoPolje.ObjekatKlaseGlavnoPolje.MatricaProvera[i][j])
                    {
                        e.Graphics.FillRectangle(GlavnoPolje.ObjekatKlaseGlavnoPolje.MatricaBoja[i][j], GlavnoPolje.ObjekatKlaseGlavnoPolje.Matrica[i][j]);
                    }
                }
            }

            for (int i = 0; i < GlavnoPolje.ObjekatKlaseGlavnoPolje.N; i++)
            {
                for (int j = 0; j < GlavnoPolje.ObjekatKlaseGlavnoPolje.M; j++)
                {
                    if (!GlavnoPolje.ObjekatKlaseGlavnoPolje.MatricaProvera[i][j])
                    {
                        e.Graphics.FillRectangle(GlavnoPolje.ObjekatKlaseGlavnoPolje.MatricaBoja[i][j], GlavnoPolje.ObjekatKlaseGlavnoPolje.Matrica[i][j]);
                        e.Graphics.DrawRectangle(new Pen(Color.RoyalBlue), GlavnoPolje.ObjekatKlaseGlavnoPolje.Matrica[i][j]);
                    }
                }
            }
        }

        private void pbTetris_Paint_1(object sender, PaintEventArgs e)
        {
            paint(e);
        }
    }
}
