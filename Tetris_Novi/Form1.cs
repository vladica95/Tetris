using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris_Novi.Klase;
using Tetris_Novi.User_control;

namespace Tetris_Novi
{
    public partial class Form1 : Form
    {
        private Figura _trenutnaFigura;
        private Figura _sledecaFigura;
        private int _brojPoena;
        private int _velicinaKockice;
        private int _kolikoKockicaSuFigure;
        private int _vrm;
        private bool _pause = false;
        

        private  DialogResult ZatvoriFormu()
        {
            return MessageBox.Show("Da li ste sigurni da zelite da zatvorite program?", "Obavestenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private void Pauza()
        {
            if (!_pause)
            {
                timer1.Stop();
                _pause = true;
                lblPauzirano.Visible = true;
                pauzaToolStripMenuItem.Text = "Nastavi";
                this.Refresh();
            }
            else
            {
                timer1.Start();
                _pause = false;
                lblPauzirano.Visible = false;
                this.Refresh();
                
            }
        }

        public Form1()
        {
            InitializeComponent();
            postaviVrednosti();
            ListaFigura.Instance.dodajOblike(_kolikoKockicaSuFigure);
            postavi();
            pauzaToolStripMenuItem.Enabled = false;
        }

        public void postaviVrednosti()
        {
            _velicinaKockice = 31;
            _kolikoKockicaSuFigure = 3;
        }

        public void postavi()
        {
            GlavnoPolje.ObjekatKlaseGlavnoPolje = new GlavnoPolje(17, 10, new Size(_velicinaKockice, _velicinaKockice), new SolidBrush(Color.Gray));
            randomSledeca();
            _brojPoena = 0;
            lblTrenutniRezultat.Text = _brojPoena.ToString();
        }

        public void randomSledeca()
        {
            Random k = new Random();
            int r = (int)(k.NextDouble() * ListaFigura.Instance.vratiBrojFiguraUlisti());
            _sledecaFigura = new Figura(ListaFigura.Instance.vratiFiguru(r));
        }

        public void staviSledecuFiguru()
        {
            _trenutnaFigura = _sledecaFigura;
            randomSledeca();
        }

        private void moveDown()
        {
            //obrisemo je sa tebele
            GlavnoPolje.ObjekatKlaseGlavnoPolje.obrisiFiguru(_trenutnaFigura);
            //pomerimo
            _trenutnaFigura.PodesiLokaciju(new Point(_trenutnaFigura.GlavnaKoordinata.X + 1, _trenutnaFigura.GlavnaKoordinata.Y));
            //pitamo da li ima mesta za nju gde sam je pomerio
            if(GlavnoPolje.ObjekatKlaseGlavnoPolje.ZauzetoJe(_trenutnaFigura))
            {
                //ako nema vratim joj trenutnu poziciju
                _trenutnaFigura.PodesiLokaciju(new Point(_trenutnaFigura.GlavnaKoordinata.X - 1, _trenutnaFigura.GlavnaKoordinata.Y ));
                //dodamo je na staru poziciju I proveravamo redove da se nije spojio ceo
                GlavnoPolje.ObjekatKlaseGlavnoPolje.dodajFiguru(_trenutnaFigura);
                _brojPoena += ((int)Math.Pow(GlavnoPolje.ObjekatKlaseGlavnoPolje.obrisiRedove(), 2) * 100);
                lblTrenutniRezultat.Text = _brojPoena.ToString();
                staviSledecuFiguru();
                if(GlavnoPolje.ObjekatKlaseGlavnoPolje.ZauzetoJe(_trenutnaFigura))
                {
                    //izgubljena igra 
                    IzgubljenaIgra();
                    return;
                }
                GlavnoPolje.ObjekatKlaseGlavnoPolje.dodajFiguru(_trenutnaFigura); //ako ne dodajemo je u tablu

            }
            else
            {
                GlavnoPolje.ObjekatKlaseGlavnoPolje.dodajFiguru(_trenutnaFigura);

            }
            this.Refresh();
        }

        public void IzgubljenaIgra()
        {
            timer1.Stop();
            if(int.Parse(lblTrenutniRezultat.Text) == 0)
            {
                DialogResult s = MessageBox.Show("Kraj igre. Da li zelite novu partiju?", "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (s == DialogResult.Yes)
                {
                    novaIgraToolStripMenuItem.PerformClick();
                }
                else
                    this.Close();
            }
            else
            {
                var frm = new FormIgrac(lblTrenutniRezultat.Text);
                DialogResult x = frm.ShowDialog();
                if (x == DialogResult.Yes)
                {
                    ListaIgraca l = new ListaIgraca();
                    l = l.DeSerialize("Top10.xml");
                    l.AddIgrac(frm.trenutniIgrac);
                    l.Serialize("Top10.xml");
                }
                DialogResult s = MessageBox.Show("Kraj igre. Da li zelite novu partiju?", "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (s == DialogResult.Yes)
                {
                    novaIgraToolStripMenuItem.PerformClick();
                }
                else
                    this.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void novaIgraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            postavi();
            _pause = false;
            _vrm = 0;
            staviSledecuFiguru();
            timer1.Start();
            GlavnoPolje.ObjekatKlaseGlavnoPolje.dodajFiguru(_trenutnaFigura);
            this.Refresh();
            pauzaToolStripMenuItem.Enabled = true;
        }

        //funkcija koja nam dozvoljava da koristimo strelice nasao na steckoverflow
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //ako nije ukljucen tajmer to znaci da je igrica na puzi
            if(timer1.Enabled)
            {
                if(keyData==Keys.Left || keyData==Keys.Right)
                {
                    //ovo je slicno kao pomeranje na dole
                    int move;
                    if (keyData == Keys.Left)
                    {
                        move = -1;
                    }
                    else
                        move = 1;
                    GlavnoPolje.ObjekatKlaseGlavnoPolje.obrisiFiguru(_trenutnaFigura); //obrisemo ga
                    _trenutnaFigura.PodesiLokaciju(new Point(_trenutnaFigura.GlavnaKoordinata.X, _trenutnaFigura.GlavnaKoordinata.Y + move));
                    if(GlavnoPolje.ObjekatKlaseGlavnoPolje.ZauzetoJe(_trenutnaFigura))
                    {
                        _trenutnaFigura.PodesiLokaciju(new Point(_trenutnaFigura.GlavnaKoordinata.X, _trenutnaFigura.GlavnaKoordinata.Y - move));
                        GlavnoPolje.ObjekatKlaseGlavnoPolje.dodajFiguru(_trenutnaFigura);
                    }
                    else
                    {
                        GlavnoPolje.ObjekatKlaseGlavnoPolje.dodajFiguru(_trenutnaFigura);
                        this.Refresh();
                    }
                    return true;
                }
                if(keyData==Keys.Space)
                {
                    GlavnoPolje.ObjekatKlaseGlavnoPolje.obrisiFiguru(_trenutnaFigura);
                    _trenutnaFigura.RotacijaLevo();
                    if(GlavnoPolje.ObjekatKlaseGlavnoPolje.ZauzetoJe(_trenutnaFigura))
                    {
                        _trenutnaFigura.RotacijaDesno();
                        GlavnoPolje.ObjekatKlaseGlavnoPolje.dodajFiguru(_trenutnaFigura);
                    }
                    else
                    {
                        GlavnoPolje.ObjekatKlaseGlavnoPolje.dodajFiguru(_trenutnaFigura);
                        this.Refresh();
                    }
                    return true;
                }
                if(keyData==Keys.S)
                {
                    GlavnoPolje.ObjekatKlaseGlavnoPolje.obrisiFiguru(_trenutnaFigura);
                    _trenutnaFigura.RotacijaDesno();
                    if(GlavnoPolje.ObjekatKlaseGlavnoPolje.ZauzetoJe(_trenutnaFigura))
                    {
                        _trenutnaFigura.RotacijaLevo();
                        GlavnoPolje.ObjekatKlaseGlavnoPolje.dodajFiguru(_trenutnaFigura);

                    }
                    else
                    {
                        GlavnoPolje.ObjekatKlaseGlavnoPolje.dodajFiguru(_trenutnaFigura);
                        this.Refresh();
                    }
                    return true;
                }
                if(keyData==Keys.Down)
                {
                    moveDown();
                    return true;
                }
                if (keyData == Keys.D)
                {
                    if (_brojPoena > 299)
                    {
                        randomSledeca();
                        _brojPoena = _brojPoena - 300;
                        lblTrenutniRezultat.Text = _brojPoena.ToString();
                        this.Refresh();
                    }
                    return true;
                }
                if(keyData == Keys.P)
                {
                    pauzaToolStripMenuItem.PerformClick();
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);            
        }

        private void pauzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pauza();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle zaSledeca;
            for(int i=0;i<_sledecaFigura.N;i++)
            {
                for(int j=0;j<_sledecaFigura.N;j++)
                {
                    Color chosen = Color.White;
                    if(_sledecaFigura.Matrica[i][j])
                    {
                        chosen = _sledecaFigura.Boja;
                    }
                    zaSledeca = new Rectangle(new Point(j * _velicinaKockice, i * _velicinaKockice), new Size(_velicinaKockice, _velicinaKockice));
                    e.Graphics.FillRectangle(new SolidBrush(chosen), zaSledeca);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            _vrm++;

            int min = _vrm / 60;
            int sec = _vrm % 60;
            lblVreme.Text = min.ToString() + ":" + sec.ToString();
            moveDown();
        }

        private void izlazToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            DialogResult dlg;
            dlg = MessageBox.Show("Da li ste sigurni da zelite da izadjete?", "Infromacija", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dlg == DialogResult.Yes)
            {
                this.Close();
            }
            else
                timer1.Start();
        }

        private void najboljiRezultatiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new Top10();
            DialogResult x = frm.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Pauza();
            if (ZatvoriFormu() == DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }
    }
}
