using lljubici1_zadaca_2.Podaci;
using System;
using System.Collections.Generic;

namespace lljubici1_zadaca_2.FactoryMethod
{
    class UlogeConcreateCreator : PodaciCreator
    {
        public UlogeConcreateCreator(string putanja, char separator = ';') : base(putanja, separator)
        {
        }

        protected override List<Entitet> PripremiPodatke(string[] redovi)
        {
            string[] polja;
            Uloga uloga;
            List<Entitet> listaPodataka = new List<Entitet>();
            foreach (var red in redovi)
            {
                try
                {
                    polja = Array.ConvertAll(red.Split(base._separator), p => p.Trim());
                    uloga = new Uloga(int.Parse(polja[0]), polja[1]);
                    listaPodataka.Add(uloga);
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine("Pogrešan podatak! " + red);
                    Console.ResetColor();
                }

            }
            return listaPodataka;
        }
    }
}
