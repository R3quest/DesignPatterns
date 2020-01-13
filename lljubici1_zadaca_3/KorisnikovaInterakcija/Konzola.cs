using lljubici1_zadaca_3.ChainOfResponsibility;
using lljubici1_zadaca_3.Memento;
using lljubici1_zadaca_3.Podaci;
using lljubici1_zadaca_3.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lljubici1_zadaca_3.KorisnikovaInterakcija
{
    public class Konzola
    {
        public static void KorisnikovOdabir(List<Osoba> listaOsoba, List<Uloga> listaUloga, List<VrstaEmisije> listaVrsti)
        {
            int izbor = 0, program = 0, dan = 0;
            Originator originator = new Originator(SingletonTvKuca.Instanca.GetRasporedPrograma());
            Caretaker caretaker = new Caretaker(originator);
            while (true)
            {
                IspisGlavniIzbornik();
                izbor = OdabirProvjera(izbor, 1, 8);
                Console.Clear();
                if (izbor == 1)
                {
                    IspisVremenskogPlana(program, dan);
                }
                else if (izbor == 2)
                {
                    IspisPrihoda(program, dan);
                }
                else if (izbor == 3)
                {
                    IspisTjednogPlanaVrste(listaVrsti);
                }
                else if (izbor == 4)
                {
                    int osobaId = -1, ulogaPostojece = -1, ulogaZeljene = -1;
                    //todo ispisi osobe
                    IspisiOsobe(listaOsoba);
                    Console.Write("Unesi osobu> ");
                    osobaId = OdabirOsobeProvjera(osobaId, listaOsoba);
                    List<Uloga> uloge = SingletonTvKuca.Instanca.VratiUlogePojedineOsobe(osobaId);
                    if (uloge.Count == 0)
                    {
                        Console.WriteLine("Osoba nema ni jednu ulogu!");
                        continue;
                    }
                    ZamjenaPostojeceUlogeNovom(listaUloga, uloge, ulogaPostojece, ulogaZeljene, osobaId);
                }
                else if (izbor == 5)
                {
                    ObrisiEmisijuRasporeda(caretaker, originator);
                }
                else if (izbor == 6)
                {
                    DohvatiPovjestRasporedaPrijasnjihStanja(caretaker);
                }
                else if (izbor == 7)
                {
                    VratiRasporedNaPrijasnjeStanje(caretaker);
                }
                else if (izbor == 8)
                {
                    if (!PromjenaBojeKonzoleDodatnaFunkcionalnost())
                    {
                        Console.ResetColor();
                    }
                }
            }
        }



        private static bool PromjenaBojeKonzoleDodatnaFunkcionalnost()
        {
            Console.Write("Unesite željenu boju (plava, zelena, crvena)> ");
            string boja = Console.ReadLine();

            var plava = new BlueHandler();
            var zelena = new GreenHandler();
            var crvena = new RedHandler();

            plava.SetNext(zelena).SetNext(crvena);
            return plava.Handle(boja);
        }

        private static void ObrisiEmisijuRasporeda(Caretaker caretaker, Originator originator)
        {
            int jednoznacniBroj = -1;
            Console.Write("Unesi jednoznacni redni broj emisije za brisanje> ");
            if (OdabirEmisijeZaBrisanjeProvjera(ref jednoznacniBroj))
            {
                caretaker.Backup();
                SingletonTvKuca.Instanca.ObrisiEmisijuNaTemeljuJednoznacnogRednogBroja(jednoznacniBroj, originator);
            }
            else
            {
                Console.WriteLine("Ne postoji emisija s tim jednoznacnim brojem");
            }
        }

        private static bool OdabirEmisijeZaBrisanjeProvjera(ref int idEmisijeZaBrisanje)
        {
            List<EmisijePrograma> listaEmisijaPrograma =
                SingletonTvKuca.Instanca.VratiRasporedEmisija().Cast<EmisijePrograma>().ToList();

            idEmisijeZaBrisanje = int.TryParse(Console.ReadLine(), out idEmisijeZaBrisanje) ? idEmisijeZaBrisanje : -1;
            int idEmisije = idEmisijeZaBrisanje;
            if (listaEmisijaPrograma.Exists(x => x.RedniBroj == idEmisije))
            {
                return true;
            }
            return false;
        }

        private static void VratiRasporedNaPrijasnjeStanje(Caretaker caretaker)
        {
            caretaker.ShowHistoryDates();
            if (caretaker.GetListCount() != 0)
            {
                Console.Write("Unesi zeljeno stanje> ");
                //TODO: provjeri jel postoji stanje
                caretaker.Restore(int.Parse(Console.ReadLine()));
            }
            else
            {
                Console.WriteLine("Nema spremljenih stanja!");
            }
        }

        private static void DohvatiPovjestRasporedaPrijasnjihStanja(Caretaker caretaker)
        {
            if (caretaker.GetListCount() != 0)
            {
                caretaker.ShowHistory();
            }
            else
            {
                Console.WriteLine("Nema spremljenih stanja!");
            }
        }


        private static void ZamjenaPostojeceUlogeNovom(List<Uloga> listaUloga, List<Uloga> uloge, int ulogaPostojece, int ulogaZeljene, int idOsobe)
        {
            IspisiUloge(uloge);
            Console.Write("Unesi postojecu ulogu osobe> ");
            ulogaPostojece = OdabirUlogeProvjera(ulogaPostojece, uloge);
            IspisiUloge(listaUloga);
            Console.Write("Unesi novu ulogu za zamjenu postojece> ");
            ulogaZeljene = OdabirUlogeProvjera(ulogaZeljene, listaUloga);
            //stara
            try
            {
                string opisStare = listaUloga.FirstOrDefault(o => o.Id == ulogaPostojece).Opis;
                Uloga staraUloga = new Uloga(ulogaPostojece, opisStare);
                //nova
                string opisNove = listaUloga.FirstOrDefault(o => o.Id == ulogaZeljene).Opis;
                Uloga novaUloga = new Uloga(ulogaZeljene, opisNove);

                List<Osoba> listaOsoba = new List<Osoba>();
                listaOsoba = SingletonTvKuca.Instanca.VratiOsobu(idOsobe);

                foreach (var osoba in listaOsoba)
                {
                    osoba.PostaviStanje(staraUloga, novaUloga);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }



        private static void IspisiOsobe(List<Osoba> listaOsoba)
        {
            foreach (var osoba in listaOsoba)
            {
                Console.WriteLine(osoba);
            }
        }

        private static void IspisiUloge(List<Uloga> uloge)
        {
            foreach (var uloga in uloge)
            {
                Console.WriteLine(uloga);
            }
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

        private static void IspisTjednogPlanaVrste(List<VrstaEmisije> vrsteEmisije)
        {
            int izbor = 0;
            //IEnumerable<Enumeracije.VrsteEmisije> vrsteEmisija =
            //    Enum.GetValues(typeof(Enumeracije.VrsteEmisije)).Cast<Enumeracije.VrsteEmisije>();
            //List<string> vrijednostiVrstaEmisija = new List<string>(vrsteEmisija.Select(v => v.ToString().Replace("_", " ")));
            //foreach (string vrste in vrijednostiVrstaEmisija)
            //    Console.WriteLine(vrijednostiVrstaEmisija.IndexOf(vrste) + 1 + " - " + vrste);
            foreach (var vrstaEmisije in vrsteEmisije)
            {
                Console.WriteLine(vrstaEmisije);
            }

            Console.Write("Unesi broj vrste emisije> ");
            izbor = OdabirProvjera(izbor, 1, vrsteEmisije.Count);
            SingletonTvKuca.Instanca.IspisiTjedniPlanVrsteEmisija(vrsteEmisije[izbor - 1].Vrsta);
        }

        private static void IspisPrihoda(int program, int dan)
        {
            Console.WriteLine("Ispis prihoda");
            SingletonTvKuca.Instanca.IspisiProgrameTvKuce();
            Console.Write("Unesi program> ");
            program = OdabirProvjera(program, 1, SingletonTvKuca.Instanca.VratiBrojPrograma());
            Console.Write("Dan u tjednu: od 1 (pon) do 7 (ned)\nUnesi dan u tjednu> ");
            dan = OdabirProvjera(dan, 1, 7);
            SingletonTvKuca.Instanca.IspisiPrihodeOdReklama(program, dan);
        }

        private static void IspisVremenskogPlana(int program, int dan)
        {
            Console.WriteLine("Ispis vremenskog plana");
            SingletonTvKuca.Instanca.IspisiProgrameTvKuce();
            Console.Write("Unesi program> ");
            program = OdabirProvjera(program, 1, SingletonTvKuca.Instanca.VratiBrojPrograma());
            Console.Write("Dan u tjednu: od 1 (pon) do 7 (ned)\nUnesi dan u tjednu> ");
            dan = OdabirProvjera(dan, 1, 7);
            SingletonTvKuca.Instanca.IspisiRasporedZaDan(program, dan);
        }

        private static void IspisGlavniIzbornik()
        {
            Console.WriteLine("1. Unesi program i dan u tjednu - Ispis vremenskog plana");
            Console.WriteLine("2. Unesi program i dan u tjednu za ispis potencijalnih prihoda od reklama (u min)");
            Console.WriteLine("3. Unesi vrstu emisije za ispis tjednog plana po svim programima, danima i emisijama");
            Console.WriteLine("4. Unesi osobu, postojecu ulogu osobe i novu ulogu kojom zamjenjuje postojecu u svojim emisijama");
            Console.WriteLine("5. Brisanje emisije");
            Console.WriteLine("6. Prikaz povjesti pohrane");
            Console.WriteLine("7. Vrati raspored na zeljeno stanje");
            Console.WriteLine("8. Dodatna funkcionalnost: promjena boje konzole");
            Console.Write("Odabir> ");
        }

        private static int OdabirProvjera(int izbor, int najmanjiBroj, int najveciBroj)
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
    }
}