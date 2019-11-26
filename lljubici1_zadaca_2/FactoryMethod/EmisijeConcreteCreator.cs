using lljubici1_zadaca_2.Podaci;
using lljubici1_zadaca_2.Pomagala;
using System;
using System.Collections.Generic;

namespace lljubici1_zadaca_2.FactoryMethod
{
    public class EmisijeConcreteCreator : PodaciCreator
    {
        public EmisijeConcreteCreator(string putanja, char separator = ';') : base(putanja, separator)
        {
        }

        protected override List<Entitet> PripremiPodatke(string[] redovi)
        {
            string[] polja;
            Emisija emisija;
            List<Entitet> listaPodataka = new List<Entitet>();
            foreach (var red in redovi)
            {
                try
                {
                    polja = Array.ConvertAll(red.Split(base._separator), p => p.Trim());
                    //polja[2] su minute
                    var _vrijeme = TimeSpan.FromMinutes(double.Parse(polja[3]));
                    VrstaEmisije vrstaEmisije = new VrstaEmisije();
                    vrstaEmisije.Id = int.Parse(polja[2]);
                    emisija = new Emisija(int.Parse(polja[0]), polja[1], vrstaEmisije,
                        Konverzija.PretvoriVrijemeUSekunde(_vrijeme.ToString()), vratiOsobaUloga(polja[4], ','));
                    listaPodataka.Add(emisija);
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine("Pogrešan podatak! " + red);
                    Console.ResetColor();
                }

            }
            return listaPodataka;
        }

        private List<OsobaUloga> vratiOsobaUloga(string osobeUloge, char separator)
        {
            List<OsobaUloga> listaOsobeUloge = new List<OsobaUloga>();
            string[] _osobeUloge = Array.ConvertAll(osobeUloge.Split(separator), p => p.Trim());
            foreach (var ou in _osobeUloge)
            {
                if (!string.IsNullOrEmpty(ou))
                {
                    string[] osobaUlogaBroj = ou.Split('-');
                    Osoba o = new Osoba();
                    o.Id = int.Parse(osobaUlogaBroj[0]);
                    Uloga u = new Uloga();
                    u.Id = int.Parse(osobaUlogaBroj[1]);
                    OsobaUloga osobaUloga = new OsobaUloga(o, u);
                    listaOsobeUloge.Add(osobaUloga);
                }
            }
            return listaOsobeUloge;
        }
    }
}
