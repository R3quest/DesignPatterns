using lljubici1_zadaca_2.Podaci;
using System;
using System.Collections.Generic;

namespace lljubici1_zadaca_2.FactoryMethod
{
    class OsobeConcreateCreator : PodaciCreator
    {
        public OsobeConcreateCreator(string putanja, char separator = ';') : base(putanja, separator)
        {
        }
        protected override List<Entitet> PripremiPodatke(string[] redovi)
        {
            string[] polja;
            List<Entitet> listaPodataka = new List<Entitet>();
            foreach (var red in redovi)
            {
                try
                {
                    polja = Array.ConvertAll(red.Split(base._separator), p => p.Trim());
                    var osoba = new Osoba(int.Parse(polja[0]), polja[1]);
                    listaPodataka.Add(osoba);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ne mogu dodati " + red);
                }

            }
            return listaPodataka;
        }
    }
}
