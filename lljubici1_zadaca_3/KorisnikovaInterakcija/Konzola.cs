using lljubici1_zadaca_3._Model.Memento;
using lljubici1_zadaca_3._Model.Podaci;
using System.Collections.Generic;

namespace lljubici1_zadaca_3.KorisnikovaInterakcija
{
    public class Konzola
    {
        public static void KorisnikovOdabir(List<Osoba> listaOsoba, List<Uloga> listaUloga, List<VrstaEmisije> listaVrsti,
            Originator originator, Caretaker caretaker)
        {
            int izbor = 0, program = 0, dan = 0;
            //while (true)
            //{
            //    if (izbor == 1)
            //    {
            //        IspisVremenskogPlana(program, dan);
            //    }
            //    else if (izbor == 2)
            //    {
            //        IspisPrihoda(program, dan);
            //    }
            //    else if (izbor == 3)
            //    {
            //        IspisTjednogPlanaVrste(listaVrsti);
            //    }
            //    else if (izbor == 4)
            //    {
            //        ObserverZamjeniUlogu(listaOsoba, listaUloga);
            //    }
            //    else if (izbor == 5)
            //    {
            //        ObrisiEmisijuRasporeda(caretaker, originator);
            //    }
            //    else if (izbor == 6)
            //    {
            //        DohvatiPovjestRasporedaPrijasnjihStanja(caretaker);
            //    }
            //    else if (izbor == 7)
            //    {
            //        VratiRasporedNaPrijasnjeStanje(caretaker);
            //    }
            //    else if (izbor == 8)
            //    {
            //        if (!PromjenaBojeKonzoleDodatnaFunkcionalnost())
            //        {
            //            Console.ResetColor();
            //        }
            //    }
            //}
        }



        //private static void ObrisiEmisijuRasporeda(Caretaker caretaker, Originator originator)
        //{
        //    int jednoznacniBroj = -1;
        //    Console.Write("Unesi jednoznacni redni broj emisije za brisanje> ");
        //    if (OdabirEmisijeZaBrisanjeProvjera(ref jednoznacniBroj))
        //    {
        //        caretaker.Backup();
        //        SingletonTvKuca.Instanca.ObrisiEmisijuNaTemeljuJednoznacnogRednogBroja(jednoznacniBroj, originator);
        //    }
        //    else
        //    {
        //        Console.WriteLine("Ne postoji emisija s tim jednoznacnim brojem");
        //    }
        //}

        //private static bool OdabirEmisijeZaBrisanjeProvjera(ref int idEmisijeZaBrisanje)
        //{
        //    List<EmisijePrograma> listaEmisijaPrograma =
        //        SingletonTvKuca.Instanca.VratiRasporedEmisija().Cast<EmisijePrograma>().ToList();

        //    idEmisijeZaBrisanje = int.TryParse(Console.ReadLine(), out idEmisijeZaBrisanje) ? idEmisijeZaBrisanje : -1;
        //    int idEmisije = idEmisijeZaBrisanje;
        //    if (listaEmisijaPrograma.Exists(x => x.RedniBroj == idEmisije))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //private static void VratiRasporedNaPrijasnjeStanje(Caretaker caretaker)
        //{
        //    caretaker.ShowHistoryDates();
        //    if (caretaker.GetListCount() != 0)
        //    {
        //        Console.Write("Unesi zeljeno stanje> ");
        //        //TODO: provjeri jel postoji stanje
        //        caretaker.Restore(int.Parse(Console.ReadLine()));
        //    }
        //    else
        //    {
        //        Console.WriteLine("Nema spremljenih stanja!");
        //    }
        //}

        //private static void DohvatiPovjestRasporedaPrijasnjihStanja(Caretaker caretaker)
        //{
        //    if (caretaker.GetListCount() != 0)
        //    {
        //        caretaker.ShowHistory();
        //    }
        //    else
        //    {
        //        Console.WriteLine("Nema spremljenih stanja!");
        //    }
        //}
    }
}