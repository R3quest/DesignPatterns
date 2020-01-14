using System;
using System.Collections.Generic;
using lljubici1_zadaca_3._Model.Podaci;

namespace lljubici1_zadaca_3._Model.FactoryMethod
{
    class VrstaEmisijeConcreateCreator : PodaciCreator
    {
        public VrstaEmisijeConcreateCreator(string putanja, char separator = ';') : base(putanja, separator)
        {
        }

        protected override List<Entitet> PripremiPodatke(string[] redovi)
        {
            string[] polja;
            //Emisija emisija;
            VrstaEmisije vrstaEmisije;
            List<Entitet> listaPodataka = new List<Entitet>();
            foreach (var red in redovi)
            {
                try
                {
                    polja = Array.ConvertAll(red.Split(base._separator), p => p.Trim());

                    vrstaEmisije = new VrstaEmisije(int.Parse(polja[0]), polja[1].ToLower(), int.Parse(polja[2]),
                        int.Parse(polja[3]));
                    listaPodataka.Add(vrstaEmisije);
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
    }
}
