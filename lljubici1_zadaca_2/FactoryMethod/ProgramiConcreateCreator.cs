using lljubici1_zadaca_2.Podaci;
using lljubici1_zadaca_2.Pomagala;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace lljubici1_zadaca_2.FactoryMethod
{
    public class ProgramiConcreateCreator : PodaciCreator
    {
        public ProgramiConcreateCreator(string putanja, char separator = ';') : base(putanja, separator)
        {
        }
        protected override List<Entitet> PripremiPodatke(string[] redovi)
        {
            string[] polja;
            Program program = null;
            List<Entitet> listaPodataka = new List<Entitet>();
            foreach (var red in redovi)
            {
                try
                {
                    polja = Array.ConvertAll(red.Split(base._separator), p => p.Trim());
                    if (!ProvjeriVrijeme(ref polja[2]) || !ProvjeriVrijeme(ref polja[3]))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine("Pogrešan podatak! " + red);
                        Console.ResetColor();
                    }
                    PostaviInicijalnoVrijeme(polja);
                    program = new Program(int.Parse(polja[0]), polja[1], Konverzija.PretvoriVrijemeUSekunde(polja[2]), Konverzija.PretvoriVrijemeUSekunde(polja[3]), polja[4]); //TODO: obrada
                    listaPodataka.Add(program);
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
        private static void PostaviInicijalnoVrijeme(string[] polja)
        {
            if (polja[2].Length == 0)
            {
                polja[2] = "06:00:00";
            }
            if (polja[3].Length == 0)
            {
                polja[3] = "23:59:59";
            }
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
            return true;
        }

    }
}
