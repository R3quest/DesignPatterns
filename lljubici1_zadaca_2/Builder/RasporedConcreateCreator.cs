using lljubici1_zadaca_2.Composite;
using lljubici1_zadaca_2.Podaci;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lljubici1_zadaca_2.Builder
{
    public class RasporedConcreateCreator : IBuilderProgram
    {
        public List<EmisijePrograma> DodajEmisijeSaDanimaIPocetkom(Program p, List<EmisijePrograma> emisijeKojeImajuDaneUTjednuIPocetak, List<EmisijePrograma> emisijeZaDodati)
        {
            foreach (var emisijaPrograma in emisijeKojeImajuDaneUTjednuIPocetak) //dodavanje prvo u emisije koje imamo (sve)
            {
                if (emisijaPrograma.ImaPočetak)
                {
                    //todo: zakomentirati ovo i guess
                    if (emisijaPrograma.Pocetak < p.Pocetak ||
                        (emisijaPrograma.Pocetak + emisijaPrograma.Emisija.Trajanje) > p.Kraj)
                    {
                        Console.WriteLine("Ne mogu dodati! Program tada ne radi! " + emisijaPrograma);
                        continue;
                    }

                    bool preklapanje = false;

                    foreach (var dodanaEmisija in emisijeZaDodati.ToList())
                    {
                        int pocetakDodane = dodanaEmisija.Pocetak;
                        int krajDodane = dodanaEmisija.Pocetak + dodanaEmisija.Emisija.Trajanje;

                        int pocetakOneZaDodat = emisijaPrograma.Pocetak;
                        int krajOneZaDodat = emisijaPrograma.Pocetak + emisijaPrograma.Emisija.Trajanje;

                        if (pocetakOneZaDodat > pocetakDodane && pocetakOneZaDodat < krajDodane ||
                            krajOneZaDodat > pocetakDodane && krajOneZaDodat < krajDodane)
                        {
                            Console.WriteLine("Pogreška, preklapanje>> " + emisijaPrograma);
                            preklapanje = true;
                            break;
                        }
                    }
                    if (!preklapanje)
                    {
                        //EmisijePrograma ep = new EmisijePrograma();
                        //Emisija em = new Emisija();
                        //em = emisijaPrograma.Emisija;
                        //ep.Emisija = em;
                        //ep.DaniUTjednu = emisijaPrograma.DaniUTjednu;
                        //ep.OsobeUloge = emisijaPrograma.OsobeUloge;
                        emisijeZaDodati.Add(VratiNovuEmisijuPrograma(emisijaPrograma));
                        ////emisijeZaDodati.Add(emisijaPrograma);
                    }
                }
            }
            emisijeZaDodati.Sort((e1, e2) => e1.Pocetak.CompareTo(e2.Pocetak));
            return emisijeZaDodati;
        }

        private EmisijePrograma VratiNovuEmisijuPrograma(EmisijePrograma emisijaPrograma)
        {
            EmisijePrograma ep = new EmisijePrograma();
            ep.Pocetak = emisijaPrograma.Pocetak;
            ep.ImaPočetak = emisijaPrograma.ImaPočetak;
            ep.OsobeUloge = emisijaPrograma.OsobeUloge;

            Emisija em = new Emisija();
            em = emisijaPrograma.Emisija;

            ep.Emisija = em;
            ep.DaniUTjednu = emisijaPrograma.DaniUTjednu;
            ep.OsobeUloge = emisijaPrograma.OsobeUloge;

            return ep;
        }

        public List<EmisijePrograma> DodajEmisijeSaDanimaBezPocetka(Program p, List<EmisijePrograma> emisijeSaDanimaBezPocetka, List<EmisijePrograma> emisijeZaDodati)
        {
            //if (!emisijeSaDanimaBezPocetka.Any())  //    return new List<EmisijePrograma>();
            emisijeSaDanimaBezPocetka.Sort((e2, e1) => e1.DaniUTjednu.Count.CompareTo(e2.DaniUTjednu.Count));
            foreach (var emisija in emisijeSaDanimaBezPocetka)
            {
                bool dodano = false;
                int početakSlobodnogVremena = Math.Max(p.Pocetak, (emisija.DaniUTjednu.Min() - 1) * 3600 * 24);
                foreach (var dodanaEmisija in emisijeZaDodati.ToList())
                {
                    int krajSlobodnogVremena = dodanaEmisija.Pocetak;
                    if (krajSlobodnogVremena > Math.Min(p.Kraj, emisija.DaniUTjednu.Max() * 3600 * 24))
                    {
                        break;
                    }
                    if (emisija.Emisija.Trajanje < (krajSlobodnogVremena - početakSlobodnogVremena))
                    {
                        //emisija.ImaPočetak = true;
                        emisija.Pocetak = početakSlobodnogVremena;

                        emisijeZaDodati.Add(VratiNovuEmisijuPrograma(emisija));
                        ////emisijeZaDodati.Add(emisija);
                        emisijeZaDodati.Sort((e1, e2) => e1.Pocetak.CompareTo(e2.Pocetak));
                        dodano = true;
                        break;
                    }

                    početakSlobodnogVremena =
                        dodanaEmisija.Pocetak + dodanaEmisija.Emisija.Trajanje;
                }

                if (!dodano)
                {
                    int krajSlobodnogVremena = Math.Min(p.Kraj, emisija.DaniUTjednu.Max() * 3600 * 24);
                    if (emisija.Emisija.Trajanje < (krajSlobodnogVremena - početakSlobodnogVremena))
                    {
                        //emisija.ImaPočetak = true;
                        emisija.Pocetak = početakSlobodnogVremena;

                        emisijeZaDodati.Add(VratiNovuEmisijuPrograma(emisija));
                        ////emisijeZaDodati.Add(emisija);
                        emisijeZaDodati.Sort((e1, e2) => e1.Pocetak.CompareTo(e2.Pocetak));
                    }
                }
            }
            emisijeZaDodati.Sort((e1, e2) => e1.Pocetak.CompareTo(e2.Pocetak));
            return emisijeZaDodati;
        }


        public List<EmisijePrograma> DodajEmisijeBezDanaIPocetka(Program p, List<EmisijePrograma> emisijeBezDanaIPocetka, List<EmisijePrograma> emisijeZaDodati)
        {
            foreach (var emisija in emisijeBezDanaIPocetka)
            {
                bool dodano = false;
                int početakSlobodnogVremena = p.Pocetak;
                foreach (var dodanaEmisija in emisijeZaDodati.ToList())
                {
                    int krajSlobodnogVremena = dodanaEmisija.Pocetak;
                    if (emisija.Emisija.Trajanje < (krajSlobodnogVremena - početakSlobodnogVremena))
                    {
                        //emisija.ImaPočetak = true;
                        emisija.Pocetak = početakSlobodnogVremena;

                        //emisija.DaniUTjednu.AddRange(Enumerable.Range(1, 7));

                        emisijeZaDodati.Add(VratiNovuEmisijuPrograma(emisija));
                        ////emisijeZaDodati.Add(emisija);
                        emisijeZaDodati.Sort((e1, e2) => e1.Pocetak.CompareTo(e2.Pocetak));
                        dodano = true;
                        break;
                    }

                    početakSlobodnogVremena =
                        dodanaEmisija.Pocetak + dodanaEmisija.Emisija.Trajanje;
                }

                if (!dodano)
                {
                    int krajSlobodnogVremena = p.Kraj;
                    if (emisija.Emisija.Trajanje < (krajSlobodnogVremena - početakSlobodnogVremena))
                    {
                        //TODO:????
                        //emisija.ImaPočetak = true;
                        emisija.Pocetak = početakSlobodnogVremena;

                        //emisija.DaniUTjednu.AddRange(Enumerable.Range(1, 7));

                        emisijeZaDodati.Add(VratiNovuEmisijuPrograma(emisija));
                        ////emisijeZaDodati.Add(emisija);
                        emisijeZaDodati.Sort((e1, e2) => e1.Pocetak.CompareTo(e2.Pocetak));
                    }
                }
            }
            return emisijeZaDodati;
        }
    }
}