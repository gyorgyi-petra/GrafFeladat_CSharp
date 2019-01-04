using System;
using System.Collections.Generic;

namespace GrafFeladat_CSharp
{
    /// <summary>
    /// Irányítatlan, egyszeres gráf.
    /// </summary>
    class Graf
    {
        int csucsokSzama;
        /// <summary>
        /// A gráf élei.
        /// Ha a lista tartalmaz egy(A, B) élt, akkor tartalmaznia kell
        /// a(B, A) vissza irányú élt is.
        /// </summary>
        readonly List<El> elek = new List<El>();
        /// <summary>
        /// A gráf csúcsai.
        /// A gráf létrehozása után új csúcsot nem lehet felvenni.
        /// </summary>
        readonly List<Csucs> csucsok = new List<Csucs>();

        /// <summary>
        /// Létehoz egy úgy, N pontú gráfot, élek nélkül.
        /// </summary>
        /// <param name="csucsok">A gráf csúcsainak száma</param>
        public Graf(int csucsok)
        {
            this.csucsokSzama = csucsok;

            // Minden csúcsnak hozzunk létre egy új objektumot
            for (int i = 0; i < csucsok; i++)
            {
                this.csucsok.Add(new Csucs(i));
            }
        }

        /// <summary>
        /// Hozzáad egy új élt a gráfhoz.
        /// Mindkét csúcsnak érvényesnek kell lennie:
        /// 0 &lt;= cs &lt; csúcsok száma.
        /// </summary>
        /// <param name="cs1">Az él egyik pontja</param>
        /// <param name="cs2">Az él másik pontja</param>
        public void Hozzaad(int cs1, int cs2)
        {
            if (cs1 < 0 || cs1 >= csucsokSzama ||
                cs2 < 0 || cs2 >= csucsokSzama)
            {
                throw new ArgumentOutOfRangeException("Hibas csucs index");
            }

            // Ha már szerepel az él, akkor nem kell felvenni
            foreach (var el in elek)
            {
                if (el.Csucs1 == cs1 && el.Csucs2 == cs2)
                {
                    return;
                }
            }

            elek.Add(new El(cs1, cs2));
            elek.Add(new El(cs2, cs1));
        }


		 public void szelessegiBejar(int kezdopont)
        {
            List<int> bejart = new List<int>();

            List<int> kovetkezok = new List<int>();

            kovetkezok.Add(kezdopont);
            bejart.Add(kezdopont);
            while (kovetkezok.Count != 0)
            {
                kezdopont = kovetkezok[0];
                kovetkezok.RemoveAt(0);

                Console.WriteLine(this.csucsok[kezdopont]);
                foreach (var el in this.elek)
                {
                    if (el.Csucs1 == kezdopont && !bejart.Contains(el.Csucs2))
                    {
                        kovetkezok.Add(el.Csucs2);
                        bejart.Add(el.Csucs2);
                    }
                }
            }
        }

     
        public void melysegiBejar(int kezdopont)
        {
            List<int> bejart = new List<int>();
            bejart.Add(kezdopont);
            this.melysegiBejarRek(kezdopont, bejart);



        }

        public void melysegiBejarRek(int kezdopont, List<int> bejart)
        {
            Console.WriteLine(this.csucsok[kezdopont]);
            foreach (var el in this.elek)
            {
                if (el.Csucs1==kezdopont && !bejart.Contains(el.Csucs2))
                {
                    bejart.Add(el.Csucs2);
                    this.melysegiBejarRek(el.Csucs2, bejart);
                }
            }
        }

        public bool osszeFuggo()
        {
            List<int> bejart = new List<int>();
            List<int> kovetkezok = new List<int>();

            kovetkezok.Add(0);
            bejart.Add(0);
            int k;
            while (kovetkezok.Count!=0)
            {
                k = kovetkezok[0];
                kovetkezok.RemoveAt(0);

                foreach (var el in this.elek)
                {
                    if (el.Csucs1==k && !bejart.Contains(el.Csucs2))
                    {
                        kovetkezok.Add(el.Csucs2);
                        bejart.Add(el.Csucs2);
                    }
                }

            }
            return (bejart.Count==this.csucsokSzama?true:false);
        }

        public Graf feszitoFa()
        {
            Graf fa = new Graf(this.csucsokSzama);
            List<int> bejart = new List<int>();
            List<int> kovetkezok= new List<int>();
            kovetkezok.Add(0);
            bejart.Add(0);

            int k;
            Console.WriteLine("Feszítőfa");
            while (kovetkezok.Count!=0)
            {
                k = kovetkezok[0];
                kovetkezok.RemoveAt(0);
                Console.WriteLine(k);
                foreach (var el in this.elek)
                {
                    if (el.Csucs1==k )
                    {
                        if (!bejart.Contains(el.Csucs2))
                        {
                            bejart.Add(el.Csucs2);
                            kovetkezok.Add(el.Csucs2);
                            fa.Hozzaad(el.Csucs1,el.Csucs2);
                        }
                    }
                }

            }
            return fa;

        }


        public override string ToString()
        {
            string str = "Csucsok:\n";
            foreach (var cs in csucsok)
            {
                str += cs + "\n";
            }
            str += "Elek:\n";
            foreach (var el in elek)
            {
                str += el + "\n";
            }
            return str;
        }
    }
}