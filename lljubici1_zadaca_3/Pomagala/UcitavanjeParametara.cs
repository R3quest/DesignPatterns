using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lljubici1_zadaca_3.Pomagala
{
    public class UcitavanjeParametara
    {
        public static bool ProvjeriUlazneArgumente(string[] korisnikoviArgumenti, string[] osnovniArgumenti, short brojParametara)
        {
            if (!ProvjeriBrojParametara(korisnikoviArgumenti, brojParametara)) return false;
            //string korisnikovaKomanda = VratiKorisnikovuKomandu(korisnikoviArgumenti);
            if (!SadrziOsnovneParametre(osnovniArgumenti, korisnikoviArgumenti)) return false;
            if (!ProvjeriPostojanjeDatoteka(korisnikoviArgumenti)) return false;
            return true;
        }

        public static Dictionary<string, string> DohvatiPutanjeDatoteka(string[] korisnikoviArgumenti)
        {
            if (korisnikoviArgumenti.Length % 2 != 0)
            {
                Console.WriteLine("Neispravan broj argumenata!");
                Environment.Exit(0);
            }
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            for (int i = 0; i < korisnikoviArgumenti.Length; i += 2)
            {
                dictionary.Add(korisnikoviArgumenti[i], korisnikoviArgumenti[i + 1]);
            }
            return dictionary;
        }

        private static bool ProvjeriPostojanjeDatoteka(string[] korisnikoviArgumenti)
        {
            string ekstenzija = null;
            for (int i = 1; i < korisnikoviArgumenti.Length; i += 2)
            {
                if (!File.Exists(korisnikoviArgumenti[i]))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine($"Provjerite parametre! [{korisnikoviArgumenti[i]}]");
                    Console.ResetColor();
                    return false;
                }
            }
            return true;
        }
        private static bool ProvjeriBrojParametara(string[] argumenti, short brojParametara)
        {
            if (argumenti.Length != brojParametara)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine($"Provjerite broj ulaznih parametara. Potreban broj parametara je {brojParametara}! Uneseno: {argumenti.Length}.");
                Console.ResetColor();
                return false;
            }
            return true;
        }
        private static bool SadrziOsnovneParametre(string[] osnovniArgumenti, string[] korisnikoviArgumenti)
        {
            bool sadrzi = true;
            foreach (var argument in osnovniArgumenti)
            {
                if (korisnikoviArgumenti.Contains(argument))
                {
                    continue;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine($"Argument '{argument}' nije unesen!");
                Console.ResetColor();
                sadrzi = false;
            }
            return sadrzi;
        }
        //private static string VratiKorisnikovuKomandu(string[] argumenti)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    foreach (var argument in argumenti)
        //    {
        //        sb.Append(" " + argument);
        //    }
        //    return sb.ToString();
        //}


    }
}
