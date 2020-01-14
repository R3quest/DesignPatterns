using lljubici1_zadaca_3._Model;
using lljubici1_zadaca_3._Model.Podaci;
using lljubici1_zadaca_3._View;
using System;
using System.Collections.Generic;

namespace lljubici1_zadaca_3._Controller
{
    public class Controller
    {
        private Model model;
        private IView view;

        public Controller(Model model, IView view)
        {
            this.model = model;
            this.view = view;
        }

        public void SetView(IView view)
        {
            this.view = view;
        }

        public void KorisnikovUnos()
        {
            int izbor = 0, program = 0, dan = 0;
            while (true)
            {
                view.IspisGlavniIzbornik();
                izbor = OdabirProvjera(izbor, 1, 8);
                Console.Clear();
                if (izbor == 1)
                {

                    view._IspisVremenskogPlana();
                    view.IspisiProgrameTvKuce(model.DohvatiProgrameTvKuce());
                    view._UnesiProgram();
                    program = OdabirProvjera(program, 1, model.VratiBrojPrograma());
                    view._UnesiDanUTjednu();
                    dan = OdabirProvjera(dan, 1, 7);

                    view.Ispisi(model.VratiRasporedZaDan(program, dan, model.DohvatiProgrameTvKuce()));
                    //view.IspisiRasporedZaDan(program, dan, model.DohvatiProgrameTvKuce());
                }
                else if (izbor == 2)
                {
                    view._IspisPrihoda();
                    view._UnesiProgram();
                    program = OdabirProvjera(program, 1, model.VratiBrojPrograma());
                    view._UnesiDanUTjednu();
                    dan = OdabirProvjera(dan, 1, 7);
                    view.Ispisi(model.IspisiPrihodeOdReklama(program, dan, model.DohvatiProgrameTvKuce()));
                }
                else if (izbor == 3)
                {
                    view.IspisiVrsteEmisija(model.VratiVrsteEmisija());
                    //TODO: FIX
                    view._UnesiBrojVrsteEmisije();
                    izbor = OdabirProvjera(izbor, 1, model.VratiBrojVrstaEmisija());
                    view.Ispisi(model.IspisiTjedniPlanVrsteEmisija(model.VratiVrsteEmisija()[izbor - 1].Vrsta));
                }
                else if (izbor == 4)
                {
                    int osobaId = -1, ulogaPostojece = -1, ulogaZeljene = -1;
                    view.IspisiOsobe(model.VratiOsobe());
                    view._UnesiOsobu();
                    osobaId = OdabirOsobeProvjera(osobaId, model.VratiOsobe());
                    if (model.VratiUlogePojedineOsobe(osobaId).Count == 0)
                    {
                        view._OsobaNemaNiJednuUlogu();
                        continue;
                    }
                    view.IspisiUloge(model.VratiUlogePojedineOsobe(osobaId));

                    view._UnesiPostojecuUloguOsobe();
                    ulogaPostojece = OdabirUlogeProvjera(ulogaPostojece, model.VratiUlogePojedineOsobe(osobaId));
                    view.IspisiUloge(model.VratiUloge());
                    //ispisaneSve
                    view._UnesiNovuUloguZaZamjenuPostojece();
                    ulogaZeljene = OdabirUlogeProvjera(ulogaPostojece, model.VratiUloge());
                    model.ZamjenaPostojeceUlogeNovom(model.VratiUlogePojedineOsobe(osobaId), ulogaPostojece, ulogaZeljene, osobaId);
                }
                else if (izbor == 5)
                {
                    int jednoznacniBroj = -1;
                    view._ObrisiEmisijuRasporeda();
                    if (model.OdabirEmisijeZaBrisanjeProvjera(ref jednoznacniBroj))
                    {
                        model.SpremiIObrisiStanje(jednoznacniBroj);
                    }
                    else
                    {
                        view._NePostojiEmisijaSJednoznacnimBrojem();
                    }
                }
                else if (izbor == 6)
                {
                    DohvatiPovjestRasporedaPrijasnjihStanja();
                }
                else if (izbor == 7)
                {
                    VratiRasporedNaPrijasnjeStanje();
                }
                else if (izbor == 8)
                {
                    view._UnesiZeljenuBoju();
                    if (!model.PromjenaBojeKonzoleDodatnaFunkcionalnost(Console.ReadLine()))
                    {
                        Console.ResetColor();
                    }
                }
            }
        }



        public void DohvatiPovjestRasporedaPrijasnjihStanja()
        {
            if (model.caretaker.GetListCount() != 0)
            {
                model.caretaker.ShowHistory();
            }
            else
            {
                view._NemaSpremljenihStanja();
            }
        }

        private void VratiRasporedNaPrijasnjeStanje()
        {
            model.caretaker.ShowHistoryDates();
            if (model.caretaker.GetListCount() != 0)
            {
                view._UnesiZeljenoStanje();
                model.caretaker.Restore(int.Parse(Console.ReadLine()));
            }
            else
            {
                view._NemaSpremljenihStanja();
            }
        }






        private int OdabirProvjera(int izbor, int najmanjiBroj, int najveciBroj)
        {
            while (true)
            {
                izbor = int.TryParse(Console.ReadLine(), out izbor) ? izbor : 0;
                if (izbor >= najmanjiBroj && izbor <= najveciBroj)
                {
                    break;
                }
                Console.Write($"Neispravan odabir! Unesite brojeve od {najmanjiBroj} - {najveciBroj}.\nOdabir> ");
            }
            return izbor;
        }

        //private static void ObserverZamjeniUlogu(List<Osoba> listaOsoba, List<Uloga> listaUloga)
        //{
        //    int osobaId = -1, ulogaPostojece = -1, ulogaZeljene = -1;
        //    //IspisiOsobe(listaOsoba);
        //    //Console.Write("Unesi osobu> ");
        //    //osobaId = OdabirOsobeProvjera(osobaId, listaOsoba);
        //    //List<Uloga> uloge = SingletonTvKuca.Instanca.VratiUlogePojedineOsobe(osobaId);
        //    if (uloge.Count == 0)
        //    {
        //        Console.WriteLine("Osoba nema ni jednu ulogu!");
        //        return;
        //    }

        //    ZamjenaPostojeceUlogeNovom(listaUloga, uloge, ulogaPostojece, ulogaZeljene, osobaId);
        //}

        //uloge to su uloge osobe
        //lista uloga sve uloge

        //private static void ZamjenaPostojeceUlogeNovom(List<Uloga> listaUloga, List<Uloga> uloge, int ulogaPostojece, int ulogaZeljene, int idOsobe)
        //{
        //    //IspisiUloge(uloge);
        //    //Console.Write("Unesi postojecu ulogu osobe> ");
        //    //ulogaPostojece = OdabirUlogeProvjera(ulogaPostojece, uloge);
        //    //IspisiUloge(listaUloga);
        //    //Console.Write("Unesi novu ulogu za zamjenu postojece> ");
        //    //ulogaZeljene = OdabirUlogeProvjera(ulogaZeljene, listaUloga);
        //    //stara
        //    try
        //    {
        //        string opisStare = listaUloga.FirstOrDefault(o => o.Id == ulogaPostojece).Opis;
        //        Uloga staraUloga = new Uloga(ulogaPostojece, opisStare);
        //        //nova
        //        string opisNove = listaUloga.FirstOrDefault(o => o.Id == ulogaZeljene).Opis;
        //        Uloga novaUloga = new Uloga(ulogaZeljene, opisNove);

        //        List<Osoba> listaOsoba = new List<Osoba>();
        //        listaOsoba = SingletonTvKuca.Instanca.VratiOsobe(idOsobe);

        //        foreach (var osoba in listaOsoba)
        //        {
        //            osoba.PostaviStanje(staraUloga, novaUloga);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //}



        private static int OdabirOsobeProvjera(int osobaId, List<Osoba> osobe)
        {
            while (true)
            {
                osobaId = int.TryParse(Console.ReadLine(), out osobaId) ? osobaId : -1;
                if (osobe.Exists(x => x.Id == osobaId))
                {
                    break;
                }
                Console.Write($"Neispravan odabir!\nUnesi postojecu osobu> ");
            }
            return osobaId;
        }

        private static int OdabirUlogeProvjera(int ulogaPostojece, List<Uloga> uloge)
        {
            while (true)
            {
                ulogaPostojece = int.TryParse(Console.ReadLine(), out ulogaPostojece) ? ulogaPostojece : -1;
                if (uloge.Exists(x => x.Id == ulogaPostojece))
                {
                    break;
                }
                Console.Write($"Neispravan odabir!\nUnesi postojecu ulogu osobe> ");
            }

            return ulogaPostojece;
        }

    }
}
