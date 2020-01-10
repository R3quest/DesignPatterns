using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lljubici1_zadaca_3.FactoryMethod
{
    public abstract class PodaciCreator
    {
        protected char _separator;
        protected string _putanja;
        public List<Entitet> entiteti { get; private set; } = new List<Entitet>();
        protected PodaciCreator(string putanja, char separator)
        {
            _separator = separator;
            _putanja = putanja;
            if (_putanja != "")
            {
                PopuniPodatke(putanja);
            }
        }
        protected virtual void PopuniPodatke(string putanjaDoDatoteke)
        {
            if (!File.Exists(putanjaDoDatoteke))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine($"Provjerite ispravnost putanje datoteke: '{putanjaDoDatoteke}'");
                Console.ResetColor();
                return;
            }

            string[] redovi = File.ReadAllLines(putanjaDoDatoteke).Skip(1).ToArray(); //.Distinct()

            entiteti = PripremiPodatke(redovi);
        }
        protected abstract List<Entitet> PripremiPodatke(string[] redovi);
    }
}
