using System;
using System.Collections.Generic;
using lljubici1_zadaca_3._Model.Podaci;
using lljubici1_zadaca_3._Model.Pomagala;

namespace lljubici1_zadaca_3._Model.FactoryMethod
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

        private List<Osoba> vratiOsobaUloga(string osobeUloge, char separator)
        {
            List<Osoba> listaOsobeUloge = new List<Osoba>();
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
                    bool tmp = false;


                    foreach (var osoba in listaOsobeUloge)
                    {
                        if (osoba.Id == o.Id)
                        {
                            osoba.Uloge.Add(u);
                            tmp = true;
                            break;
                        }
                    }
                    if (tmp)
                    {
                        continue;
                    }
                    //OsobaUloga osobaUloga = new OsobaUloga(o, u);
                    o.Uloge.Add(u);
                    listaOsobeUloge.Add(o);

                }
            }
            return listaOsobeUloge;
        }
    }
}
