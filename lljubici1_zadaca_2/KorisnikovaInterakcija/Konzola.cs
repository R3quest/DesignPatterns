using lljubici1_zadaca_2.Composite;
using lljubici1_zadaca_2.FactoryMethod;
using lljubici1_zadaca_2.Podaci;
using lljubici1_zadaca_2.Pomagala;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lljubici1_zadaca_2.KorisnikovaInterakcija
{
    public class Konzola
    {
        public static void KorisnikovOdabir(PodaciCreator osobe, PodaciCreator uloge)
        {
            int izbor = 0;
            while (true)
            {
                IspisGlavniIzbornik();
                izbor = OdabirProvjera(izbor, 1, 2);
                Console.Clear();
                if (izbor == 1)
                {
                    int program = 0, dan = 0;
                    Console.WriteLine("Ispis vremenskog plana");
                    IspisiProgrameTvKuce();
                    Console.Write("Unesi program> ");
                    program = OdabirProvjera(program, 1, SingletonTvKuca.Instanca.VratiBrojPrograma());
                    Console.Write("Dan u tjednu: od 1 do 7\nUnesi dan u tjednu> ");
                    dan = OdabirProvjera(dan, 1, 7);
                    //Program p = SingletonTvKuca.Instanca.Programi[program - 1];
                    //IspisiEmisijaPrograma(p, dan, osobe, uloge); TODO:vrati
                    try
                    {
                        SingletonTvKuca.Instanca.IspisiRasporedZaDan(program, dan);

                        //((Program)SingletonTvKuca.Instanca.RasporedPrograma[program - 1]).IspisZaDan(dan);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                else if (izbor == 2)
                {
                    IspisPodatakaOBrojuEmisija();
                    izbor = OdabirProvjera(izbor, 1, 2);
                    if (izbor == 1)
                    {
                        //IspisBrojEmisijaUPojedinomProgramuPoPojedinomDanu(); TODO:vrati
                    }
                    else if (izbor == 2)
                    {
                        //IspisiStatistikuSvakogPrograma(); TODO:vrati
                    }
                }
            }
        }















        private static void IspisiStatistikuSvakogPrograma()
        {
            foreach (var p in SingletonTvKuca.Instanca.Programi)
            {
                EmisijePrograma zadnjaEmisijaTogDana = null;
                Console.WriteLine(p);
                int krajPrograma = p.Kraj;
                double emitiranjeSignala = 0;

                int trajanjePrograma = p.Kraj - p.Pocetak;
                int trajanjeEmisijaZaNekiDan = 0;

                double slobodnoVrijeme = 0;
                string okvir = string.Format("{0,-11}\t{1,-17}\t{2,-17}\t{3,-18}", "Dan", "Slobodno vrijeme", "Trajanje emisija", "Emitiranje signala");
                Console.WriteLine(okvir);
                for (int i = 1; i <= 7; i++)
                {
                    foreach (var emisija in p.EmisijePrograma)
                    {
                        if (emisija.DaniUTjednu.Contains(i))
                        {
                            trajanjeEmisijaZaNekiDan += emisija.Emisija.Trajanje;
                            zadnjaEmisijaTogDana = emisija;
                        }
                    }

                    slobodnoVrijeme = (double)(trajanjePrograma - trajanjeEmisijaZaNekiDan) / trajanjePrograma;
                    if (zadnjaEmisijaTogDana != null)
                    {
                        emitiranjeSignala =
                            krajPrograma -
                            (zadnjaEmisijaTogDana.Pocetak + zadnjaEmisijaTogDana.Emisija.Trajanje) + p.Pocetak;
                    }
                    string formatiraniIspis = string.Format("{0,-11}\t{1,-17}\t{2,-17}\t{3,-18}", $"{Enum.GetName(typeof(Dani), i)}",
                        slobodnoVrijeme * 100 + "%", (1 - slobodnoVrijeme) * 100 + "%", (emitiranjeSignala / ((krajPrograma - p.Pocetak)) * 100 + "%"));
                    Console.WriteLine(formatiraniIspis);

                    //Console.WriteLine($"{Enum.GetName(typeof(Dani), i)}");
                    //Console.WriteLine("Slobodno vrijeme: " + slobodnoVrijeme * 100 + "%");
                    //Console.WriteLine("Trajanje emisija: " + (1 - slobodnoVrijeme) * 100 + "%");
                    //Console.WriteLine("Emitiranje signala: " + (emitiranjeSignala / ((krajPrograma - p.Pocetak)) * 100 + "%"));
                    trajanjeEmisijaZaNekiDan = 0;
                    zadnjaEmisijaTogDana = null;
                }
                Console.WriteLine();
            }
        }

        private static void IspisBrojEmisijaUPojedinomProgramuPoPojedinomDanu()
        {
            int brojEmisija = 0;

            foreach (var p in SingletonTvKuca.Instanca.Programi)
            {
                Console.WriteLine(p);
                string okvir = string.Format("{0,-11}\t{1,-12}", "Dan", "Broj emisija");
                Console.WriteLine(okvir);
                for (int i = 1; i <= 7; i++)
                {
                    foreach (var emisija in p.EmisijePrograma)
                    {
                        if (emisija.DaniUTjednu.Contains(i))
                        {
                            brojEmisija++;
                        }
                    }
                    string formatiraniIspis = string.Format("{0,-11}\t{1,-12}", Enum.GetName(typeof(Dani), i), brojEmisija);
                    Console.WriteLine(formatiraniIspis);
                    //Console.WriteLine($"Broj emisija {Enum.GetName(typeof(Dani), i)}: {brojEmisija}");
                    brojEmisija = 0;
                }

                Console.WriteLine();
            }
        }

        private static void IspisiEmisijaPrograma(Program p, int dan, PodaciCreator osobe, PodaciCreator uloge)
        {
            List<Osoba> _osobe = osobe.entiteti.Cast<Osoba>().ToList();
            List<Uloga> _uloge = uloge.entiteti.Cast<Uloga>().ToList();

            string okvir = string.Format("{0,-10} {1,-10} {2,-25} {3,-50}", "Pocetak", "Kraj", "Naziv emisije", "Osobe-Uloge");
            Console.WriteLine(okvir);
            foreach (var emisija in p.EmisijePrograma)
            {
                if (emisija.DaniUTjednu.Contains(dan))
                {
                    IEnumerable<string> emisijaProgramaOU = emisija.Emisija.OsobeUloge.Select(ou => $"{_osobe.FirstOrDefault(o => o.Id == ou.IdOsoba)?.ImeIPrezime}-{_uloge.FirstOrDefault(o => o.Id == ou.IdUloga)?.Opis}");
                    string emisijaProgramaOUSvi = string.Join(", ", emisijaProgramaOU);

                    IEnumerable<string> emisijaOU = emisija.OsobeUloge.Select(ou => $"{_osobe.FirstOrDefault(o => o.Id == ou.IdOsoba)?.ImeIPrezime}-{_uloge.FirstOrDefault(o => o.Id == ou.IdUloga)?.Opis}");
                    string emisijaOUSvi = string.Join(", ", emisijaOU);


                    string formatiraniIspis = string.Format("{0,-10} {1,-10} {2,-25} {3,-50}",
                        Konverzija.PretvoriSekundeUVrijeme(emisija.Pocetak),
                        Konverzija.PretvoriSekundeUVrijeme(emisija.Pocetak + emisija.Emisija.Trajanje),
                        emisija.Emisija.NazivEmisije,
                        emisijaProgramaOUSvi + " " + emisijaOUSvi);

                    Console.WriteLine(formatiraniIspis);

                    //Console.WriteLine(Konverzija.PretvoriSekundeUVrijeme(emisija.Pocetak) +
                    //                  "-" + Konverzija.PretvoriSekundeUVrijeme(emisija.Pocetak + emisija.Emisija.Trajanje) +
                    //                  " " + emisija.Emisija.NazivEmisije + " " + emisijaProgramaOUSvi + "; " + emisijaOUSvi);
                }
            }

            Console.WriteLine();
        }

        private static void IspisiProgrameTvKuce()
        {
            for (int i = 0; i < SingletonTvKuca.Instanca.Programi.Count; i++)
            {
                Console.WriteLine(i + 1 + ". " + SingletonTvKuca.Instanca.Programi[i]);
            }
        }

        private static void IspisPodatakaOBrojuEmisija()
        {
            Console.WriteLine("Ispis podataka:");
            Console.WriteLine("\t1 -  po broju emisija po pojedinom danu u tjednu");
            Console.WriteLine("\t2 - % popunjenosti pojedinog programa po pojedinom danu u tjednu (emitiranje signala, emisije, slobodno vrijeme)");
            Console.Write("Odabir> ");
        }

        private static void IspisGlavniIzbornik()
        {
            Console.WriteLine("1. Unesi program i dan u tjednu - Ispis vremenskog plana");
            Console.WriteLine("2. Ispis podataka: \n\t- po broju emisija po pojedinom danu u tjednu");
            Console.WriteLine("\t- % popunjenosti pojedinog programa po pojedinom danu u tjednu");
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