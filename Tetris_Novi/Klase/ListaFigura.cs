using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Novi.Klase
{
    public class ListaFigura
    {
        private static ListaFigura _instance = null;
        private List<Figura> _listaF;


        public ListaFigura()
        {
            _listaF = new List<Figura>();
        }
        public static ListaFigura Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ListaFigura();
                return _instance;
            }
        }

        public Figura vratiFiguru(int i)
        {
            if (i < 0 || i >= _listaF.Count)
                return _listaF[0];
            return _listaF[i];
        }

        public void dodajFiguru(Figura obj)
        {
            if (obj != null)
                _listaF.Add(obj);
        }
        public void dodajOblike(int n1)
        {
            _listaF.Clear();

            //crtamo i dodajemo u listu plavi pravugaonik
            Figura pravugaonik = new Figura(4, System.Drawing.Color.MediumTurquoise);
            for (int i = 0; i < 4; i++)
            {
                pravugaonik.Matrica[i][pravugaonik.GlavnaKordinataInt] = true;
            }

            //crtamo kocku popunjenu celu,veliku
            Figura kocka = new Figura(n1, System.Drawing.Color.ForestGreen);
            for (int i = 0; i < n1; i++)
            {
                for (int j = 0; j < n1; j++)
                {
                    kocka.Matrica[i][j] = true;
                }
            }

            //crtamo malu kockicu
            Figura kockica = new Figura(n1, System.Drawing.Color.Yellow);
            kockica.Matrica[kockica.GlavnaKordinataInt][kockica.GlavnaKordinataInt] = true;

            //crtamo krstic mali
            Figura krstic = new Figura(n1, System.Drawing.Color.MediumPurple);
            for (int i = 0; i < n1; i++)
                krstic.Matrica[i][krstic.GlavnaKordinataInt] = true;
            for (int j = 0; j < n1; j++)
                krstic.Matrica[krstic.GlavnaKordinataInt][j] = true;

            //crtamo crvenu figuru
            Figura kapica = new Figura(n1, System.Drawing.Color.Red);
            for (int i = 0; i < n1; i++)
            {
                kapica.Matrica[kapica.GlavnaKordinataInt][i] = true;
            }
            for (int i = 0; i < kapica.GlavnaKordinataInt; i++)
            {
                kapica.Matrica[i][kapica.GlavnaKordinataInt] = true;
            }

            Figura kvad2x2 = new Figura(2, System.Drawing.Color.Aquamarine);
            for (int i = 0; i < 2; i++)
            {
                kvad2x2.Matrica[0][i] = true;
                kvad2x2.Matrica[1][i] = true;
            }

            Figura Z = new Figura(3, System.Drawing.Color.BurlyWood);

            Z.Matrica[0][0] = true;
            Z.Matrica[1][1] = true;
            Z.Matrica[1][0] = true;
            Z.Matrica[2][1] = true;

            Figura Z1 = new Figura(3, System.Drawing.Color.DarkSeaGreen);

            Z1.Matrica[0][2] = true;
            Z1.Matrica[1][1] = true;
            Z1.Matrica[1][2] = true;
            Z1.Matrica[2][1] = true;

            Figura g1 = new Figura(3, System.Drawing.Color.Crimson);
            for (int i = 0; i < 3; i++)
            {
                g1.Matrica[i][0] = true;
                g1.Matrica[0][1] = true;
            }

            Figura g2 = new Figura(3, System.Drawing.Color.DarkTurquoise);
            for (int i = 0; i < 3; i++)
            {
                g2.Matrica[i][1] = true;
                g2.Matrica[0][0] = true;
            }

            Figura l1 = new Figura(2, System.Drawing.Color.MediumVioletRed);
            for (int i = 0; i < 2; i++)
            {
                l1.Matrica[0][i] = true;
                l1.Matrica[1][1] = true;
            }

            Figura l2 = new Figura(2, System.Drawing.Color.Gold);
            for (int i = 0; i < 2; i++)
            {
                l2.Matrica[0][i] = true;
                l2.Matrica[1][0] = true;
            }

            Figura i1 = new Figura(2, System.Drawing.Color.GreenYellow);
            for (int i = 0; i < 2; i++)
            {
                i1.Matrica[0][i] = true;

            }

            Figura i2 = new Figura(3, System.Drawing.Color.Gold);
            for (int i = 0; i < 3; i++)
            {
                i2.Matrica[i][0] = true;
                i2.Matrica[1][1] = true;
            }

            //dodajemo u listu figura
            _listaF.Add(pravugaonik);
            _listaF.Add(kocka);
            _listaF.Add(kockica);
            _listaF.Add(kapica);
            _listaF.Add(kvad2x2);
            _listaF.Add(Z);
            _listaF.Add(Z1);
            _listaF.Add(g1);
            _listaF.Add(g2);
            _listaF.Add(l1);
            _listaF.Add(l2);
            _listaF.Add(i1);
            _listaF.Add(i2);


        }

        public double vratiBrojFiguraUlisti()
        {
            return _listaF.Count;
        }



    }
}
