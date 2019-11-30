using lljubici1_zadaca_2.Podaci;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace lljubici1_zadaca_2.FactoryMethod
{
    class EmisijeProgramaConcreateCreator : PodaciCreator
    {
        public EmisijeProgramaConcreateCreator(string putanja, char separator = ';') : base(putanja, separator)
        {
        }
        public void PromjeniPutanjuZaProgram(string putanjaPrograma)
        {
            _putanja = Path.GetFullPath(putanjaPrograma);
            PopuniPodatke(_putanja);
        }

        protected override List<Entitet> PripremiPodatke(string[] redovi)
        {
            string[] polja;
            char separator;
            EmisijePrograma programEmisija;
            List<Entitet> listaPodataka = new List<Entitet>();
            foreach (var red in redovi)
            {
                //TODO: provjera polja
                try
                {
                    polja = Array.ConvertAll(red.Split(base._separator), p => p.Trim());
                    separator = polja[1].Contains("-") ? '-' : ',';
                    polja = PromjeniVelicinuPolja(ref polja);
                    if (!ProvjeriVrijeme(ref polja[2]))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine("Pogrešan podatak! " + red);
                        Console.ResetColor();
                        continue;
                    }

                    programEmisija = new EmisijePrograma(int.Parse(polja[0]), vratiDaneUTjednu(polja[1], separator),
                        vratiOsobaUloga(polja[3], ','), polja[2]);
                    listaPodataka.Add(programEmisija);
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine("Pogrešan podatak! " + red);
                    Console.ResetColor();
                }
            }
            var sortiranaLista = listaPodataka.Cast<EmisijePrograma>().OrderByDescending(a => a.DaniUTjednu.Count).ThenByDescending(b => b.Pocetak)
                .ThenBy(c => c.Emisija.Id)
                .ToList().Cast<Entitet>().ToList();
            return sortiranaLista;
        }

        private string[] PromjeniVelicinuPolja(ref string[] polja)
        {
            if (polja.Length == 3)
            {
                Array.Resize(ref polja, 4);
                polja[3] = "";
            }

            return polja;
        }

        private bool ProvjeriVrijeme(ref string vrijeme)
        {
            if (vrijeme == "") return true;
            if (vrijeme.Length == 5)
            {
                vrijeme = vrijeme + ":00";
            }
            else if (vrijeme.Length == 4)
            {
                vrijeme = "0" + vrijeme + ":00";
            }
            Regex regex = new Regex(@"^(([0-1][0-9])|([0-2][0-3]))(:[0-5][0-9]){2}$");
            if (!regex.Match(vrijeme).Success)
            {
                return false;
            }
            //ok

            return true;
        }


        private List<Osoba> vratiOsobaUloga(string osobeUloge, char separator = ',')
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


                    o.Uloge.Add(u);
                    listaOsobeUloge.Add(o);
                }
            }
            return listaOsobeUloge;
        }
        private List<int> vratiDaneUTjednu(string daniUTjednu, char separator = ',')
        {
            List<int> dani = new List<int>();
            if (daniUTjednu.Equals(""))
            {
                return dani;
            }
            string[] _daniUTjednu = daniUTjednu.Trim().Split(separator);
            Regex regex = new Regex(@"(^[1-7]\-[2-7]$)|(^([1-7](\,[2-7])*)$)");
            if (regex.Match(daniUTjednu).Success)
            {
                if (_daniUTjednu.Length == 1)
                {
                    dani.Add(int.Parse(daniUTjednu));
                }
                else if (separator == '-')
                {
                    short _od = byte.Parse(_daniUTjednu[0]);
                    byte _do = (byte)((byte.Parse(_daniUTjednu[1]) - _od) + 1);
                    dani.AddRange(Enumerable.Range(_od, _do).ToList());
                }
                else if (separator == ',')
                {
                    List<int> _dani = daniUTjednu.Split(separator).Select(int.Parse).ToList();
                    dani.AddRange(_dani);
                }
            }
            return dani;
        }

    }
}
