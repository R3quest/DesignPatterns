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

        private void ChangeView()
        {
            if (view.GetType() == typeof(View1))
            {
                view = new View2();
            }
            else
            {
                view = new View1();
            }
        }

        public void KorisnikovUnos()
        {
            int izbor = 0, program = 0, dan = 0;
            while (true)
            {
                view.IspisGlavniIzbornik();
                izbor = OdabirProvjera(izbor, 1, 9);
                Console.Clear();
                if (izbor == 1)
                {
                    program = OpcijaIspisVremenskogPlana(program, ref dan);
                }
                else if (izbor == 2)
                {
                    program = OpcijaIspisPrihoda(program, ref dan);
                }
                else if (izbor == 3)
                {
                    izbor = OpcijaIspisVrste(izbor);
                }
                else if (izbor == 4)
                {
                    OpcijaZamjenaUloge();
                }
                else if (izbor == 5)
                {
                    OpcijaObrisiEmisijuRasporeda();
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
                    OpcijaDodatnaFunkcionalnost();
                }
                else if (izbor == 9)
                {
                    ChangeView();
                }
            }
        }

        private void OpcijaDodatnaFunkcionalnost()
        {
            view._UnesiZeljenuBoju();
            if (!model.PromjenaBojeKonzoleDodatnaFunkcionalnost(Console.ReadLine()))
            {
                Console.ResetColor();
            }
        }

        private void OpcijaObrisiEmisijuRasporeda()
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

        private void OpcijaZamjenaUloge()
        {
            int osobaId = -1, ulogaPostojece = -1, ulogaZeljene = -1;
            view.IspisiOsobe(model.VratiOsobe());
            view._UnesiOsobu();
            osobaId = OdabirOsobeProvjera(osobaId, model.VratiOsobe());
            if (model.VratiUlogePojedineOsobe(osobaId).Count == 0)
            {
                view._OsobaNemaNiJednuUlogu();
                return;
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

        private int OpcijaIspisVrste(int izbor)
        {
            view.IspisiVrsteEmisija(model.VratiVrsteEmisija());
            //TODO: FIX
            view._UnesiBrojVrsteEmisije();
            izbor = OdabirProvjera(izbor, 1, model.VratiBrojVrstaEmisija());
            view.Ispisi(model.IspisiTjedniPlanVrsteEmisija(model.VratiVrsteEmisija()[izbor - 1].Vrsta));
            return izbor;
        }

        private int OpcijaIspisPrihoda(int program, ref int dan)
        {
            view._IspisPrihoda();
            view._UnesiProgram();
            program = OdabirProvjera(program, 1, model.VratiBrojPrograma());
            view._UnesiDanUTjednu();
            dan = OdabirProvjera(dan, 1, 7);
            view.Ispisi(model.IspisiPrihodeOdReklama(program, dan, model.DohvatiProgrameTvKuce()));
            return program;
        }

        private int OpcijaIspisVremenskogPlana(int program, ref int dan)
        {
            view._IspisVremenskogPlana();
            view.IspisiProgrameTvKuce(model.DohvatiProgrameTvKuce());
            view._UnesiProgram();
            program = OdabirProvjera(program, 1, model.VratiBrojPrograma());
            view._UnesiDanUTjednu();
            dan = OdabirProvjera(dan, 1, 7);

            view.Ispisi(model.VratiRasporedZaDan(program, dan, model.DohvatiProgrameTvKuce()));
            //view.IspisiRasporedZaDan(program, dan, model.DohvatiProgrameTvKuce());
            return program;
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
