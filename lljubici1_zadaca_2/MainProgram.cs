﻿using lljubici1_zadaca_2.Builder;
using lljubici1_zadaca_2.Composite;
using lljubici1_zadaca_2.FactoryMethod;
using lljubici1_zadaca_2.KorisnikovaInterakcija;
using lljubici1_zadaca_2.Podaci;
using lljubici1_zadaca_2.Pomagala;
using lljubici1_zadaca_2.Raspored;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lljubici1_zadaca_2
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


            PodaciCreator program = new ProgramiConcreateCreator(datoteke["-t"]);
            PodaciCreator emisije = new EmisijeConcreteCreator(datoteke["-e"]);
            PodaciCreator osobe = new OsobeConcreateCreator(datoteke["-o"]);
            PodaciCreator uloge = new UlogeConcreateCreator(datoteke["-u"]);
            PodaciCreator programEmisija = new EmisijeProgramaConcreateCreator("");
            PodaciCreator vrstaEmisije = new VrstaEmisijeConcreateCreator(datoteke["-v"]);

            IBuilderProgram programBuilder = new RasporedConcreateCreator();

            var listaEmisija = emisije.entiteti.Cast<Emisija>().ToList();
            var listaPrograma = program.entiteti.Cast<Program>().ToList();
            RasporedEmisija.DodajProgrameSDodanimRasporedomEmisija(listaPrograma, programEmisija, listaEmisija, programBuilder);
            SingletonTvKuca.Instanca.RasporedPrograma = SingletonTvKuca.Instanca.Programi.Cast<IRasporedProgramaComponent>().ToList();
            //KreirajRaspored();
            IspisiRaspored();
            Konzola.KorisnikovOdabir(osobe, uloge);


            Console.ReadLine();
        }


        //private static void KreirajRaspored()
        //{
        //    foreach (var program in SingletonTvKuca.Instanca.RasporedPrograma)
        //    {
        //        for (int i = 1; i <= 7; i++)
        //        {
        //            Dan dan = new Dan(Enum.GetName(typeof(Dani), i));
        //            var emisijePrograma = ((Program)program).EmisijePrograma;
        //            foreach (var emisijaPrograma in emisijePrograma)
        //            {
        //                if (emisijaPrograma.DaniUTjednu.Contains(i))
        //                {
        //                    dan.DodajElementRasporeda(emisijaPrograma);
        //                }
        //            }
        //            program.DodajElementRasporeda(dan);
        //        }
        //    }
        //}

        private static void IspisiRaspored()
        {
            foreach (var program in SingletonTvKuca.Instanca.RasporedPrograma)
            {
                program.IspisiRaspored();
                Console.WriteLine();
            }
        }

    }
}