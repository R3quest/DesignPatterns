using lljubici1_zadaca_3.Builder;
using lljubici1_zadaca_3.Composite;
using lljubici1_zadaca_3.FactoryMethod;
using lljubici1_zadaca_3.KorisnikovaInterakcija;
using lljubici1_zadaca_3.Podaci;
using lljubici1_zadaca_3.Pomagala;
using lljubici1_zadaca_3.Raspored;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lljubici1_zadaca_3
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            if (!UcitavanjeParametara.ProvjeriUlazneArgumente(args, new[] { "-t", "-e", "-o", "-u", "-v" }, 10))
            {
                Console.ReadLine();
                return;
            }
            Dictionary<string, string> datoteke = UcitavanjeParametara.DohvatiPutanjeDatoteka(args);
            PodaciCreator(datoteke, out var program, out var emisije, out var osobe, out var uloge, out var programEmisija, out var vrstaEmisije);

            IBuilderProgram programBuilder = new RasporedConcreateCreator();

            var listaEmisija = emisije.entiteti.Cast<Emisija>().ToList();
            var listaPrograma = program.entiteti.Cast<Program>().ToList();
            var listaOsoba = osobe.entiteti.Cast<Osoba>().ToList();
            var listaUloga = uloge.entiteti.Cast<Uloga>().ToList();
            var listaVrstaEmisije = vrstaEmisije.entiteti.Cast<VrstaEmisije>().ToList();

            RasporedEmisija.KreirajRasporedPoDanima(listaPrograma, programEmisija, listaEmisija, programBuilder,
                listaOsoba, listaUloga, listaVrstaEmisije);

            Konzola.KorisnikovOdabir(listaOsoba, listaUloga, listaVrstaEmisije);
            Console.ReadLine();
        }

        private static void PodaciCreator(Dictionary<string, string> datoteke, out PodaciCreator program, out PodaciCreator emisije,
            out PodaciCreator osobe, out PodaciCreator uloge, out PodaciCreator programEmisija, out PodaciCreator vrstaEmisije)
        {
            program = new ProgramiConcreateCreator(datoteke["-t"]);
            emisije = new EmisijeConcreteCreator(datoteke["-e"]);
            osobe = new OsobeConcreateCreator(datoteke["-o"]);
            uloge = new UlogeConcreateCreator(datoteke["-u"]);
            programEmisija = new EmisijeProgramaConcreateCreator("");
            vrstaEmisije = new VrstaEmisijeConcreateCreator(datoteke["-v"]);
        }
    }
}
