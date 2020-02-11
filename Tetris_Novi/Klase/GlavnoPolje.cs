using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris_Novi.Klase
{
    public class GlavnoPolje
    {
        private Rectangle[][] _matrica;
        private int _n;
        private int _m;
        private Size _velicinaKockica; 
        private bool[][] _matricaProvera; 
        private SolidBrush[][] _matricaBoja;
        private SolidBrush _primarnaCetka;

        private static GlavnoPolje _objekatKlaseGlavnoPolje;

        public Rectangle[][] Matrica
        {
            get
            {
                return _matrica;
            }

            set
            {
                _matrica = value;
            }
        }

        public int N
        {
            get
            {
                return _n;
            }

            set
            {
                _n = value;
            }
        }

        public int M
        {
            get
            {
                return _m;
            }

            set
            {
                _m = value;
            }
        }

        public Size VelicinaKockica
        {
            get
            {
                return _velicinaKockica;
            }

            set
            {
                _velicinaKockica = value;
            }
        }

        public bool[][] MatricaProvera
        {
            get
            {
                return _matricaProvera;
            }

            set
            {
                _matricaProvera = value;
            }
        }

        public SolidBrush[][] MatricaBoja
        {
            get
            {
                return _matricaBoja;
            }

            set
            {
                _matricaBoja = value;
            }
        }

        public SolidBrush PrimarnaCetka
        {
            get
            {
                return _primarnaCetka;
            }

            set
            {
                _primarnaCetka = value;
            }
        }

        public static GlavnoPolje ObjekatKlaseGlavnoPolje
        {
            get
            {
                return _objekatKlaseGlavnoPolje;
            }

            set
            {
                _objekatKlaseGlavnoPolje = value;
            }
        }

        public GlavnoPolje (int n,int m,Size size,SolidBrush cetka)
        {
            _velicinaKockica = size;
            PrimarnaCetka = cetka;
            N = n;
            M = m;
            Matrica = new Rectangle[n][];
            MatricaProvera = new bool[n][];
            MatricaBoja = new SolidBrush[n][];
            for(int i=0;i<n;i++)
            {
                Matrica[i] = new Rectangle[m];
                MatricaBoja[i] = new SolidBrush[M];
                MatricaProvera[i] = new bool[m];
            }
            for(int i=0;i<n;i++)
            {
                for(int j=0;j<M;j++)
                {
                    Matrica[i][j] = new Rectangle(new Point(j * size.Height, i * size.Width),size);
                    MatricaProvera[i][j] = false;
                    MatricaBoja[i][j] = PrimarnaCetka;
                }
            }
        }
        public void dodajFiguru(Figura obj)
        {
            int icordinata = obj.GlavnaKoordinata.X;
            int jcordinata = obj.GlavnaKoordinata.Y;
            for(int i=0;i<obj.N;i++)
            {
                for(int j=0;j<obj.N;j++)
                {
                    if(obj.Matrica[i][j])
                    {
                        MatricaProvera[i + icordinata][j + jcordinata] = true;
                        MatricaBoja[i + icordinata][j + jcordinata] = new SolidBrush(obj.Boja);
                    }
                }
            }
        }

        public void obrisiFiguru(Figura obj)
        {
            int icoordinata = obj.GlavnaKoordinata.X;
            int jcoordinata = obj.GlavnaKoordinata.Y;
            for(int i=0;i<obj.N;i++)
            {
                for(int j=0;j<obj.N;j++)
                {
                    if(obj.Matrica[i][j])
                    {
                        MatricaProvera[i + icoordinata][j + jcoordinata] = false;
                        MatricaBoja[i + icoordinata][j + jcoordinata] = PrimarnaCetka;
                    }
                }
            }
        }

        public bool ZauzetoJe(Figura obj)
        {
            int icoordinata = obj.GlavnaKoordinata.X;
            int jcoordinata = obj.GlavnaKoordinata.Y;
            int proveri1 = obj.N - 1 + icoordinata;
            int proveri2 = obj.N - 1 + jcoordinata;
            int provera3 = jcoordinata;
            bool provera = false;

            for(int j=obj.N-1;j>=0;j--)
            {
                for(int i=0;i<obj.N;i++)
                {
                    if (obj.Matrica[i][j])
                    {
                        provera = true;
                    }
                }
                if (provera)
                    break;
                proveri2--;
            }

            provera = false;
            for(int j=obj.N-1;j>=0;j--)
            {
                for(int i=0;i<obj.N;i++)
                {
                    if(obj.Matrica[j][i])
                    {
                        provera = true;
                    }
                }
                if (provera)
                    break;
                proveri1--;
            }


            provera = false;
            for(int j=0;j<obj.N;j++)
            {
                for(int i=0;i<obj.N;i++)
                {
                    if(obj.Matrica[i][j])
                    {
                        provera = true;
                    }
                }
                if (provera)
                    break;
                provera3++;
            }

            if (proveri1 >= N || proveri2 >= M || provera3 < 0)
                return true;
            for(int i=0;i<obj.N;i++)
            {
                for(int j=0;j<obj.N;j++)
                {
                    if(obj.Matrica[i][j])
                    {
                        if (MatricaProvera[i + icoordinata][j + jcoordinata] == true)
                            return true;
                    }
                }
                
            }
            return false;
        }

        public int obrisiRedove()
        {
            int red = 0;
            for(int i=N-1;i>=0;i--)
            {
                bool brisati = true;
                for (int j = M - 1; j >= 0; j--)
                    if (MatricaProvera[i][j] == false)
                        brisati = false;
                if(brisati)
                {
                    red++;
                    for(int k=i;k>0;k--)
                    {
                        for(int l=M-1;l>=0;l--)
                        {
                            MatricaProvera[k][l] = MatricaProvera[k - 1][l];
                            MatricaBoja[k][l] = MatricaBoja[k - 1][l];
                        }
                    }
                    for(int k=M-1;k>=0;k--)
                    {
                        MatricaProvera[0][k] = false;
                        MatricaBoja[0][k] = PrimarnaCetka;
                    }
                    i++;
                }
            }
            return red;
        }
    }
}
