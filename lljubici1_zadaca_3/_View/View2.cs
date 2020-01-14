using lljubici1_zadaca_3._Model.Composite;
using lljubici1_zadaca_3._Model.Podaci;
using System;
using System.Collections.Generic;

namespace lljubici1_zadaca_3._View
{
    public class View2 : IView
    {
        private int redniBrojLinije = -1;
        private string VratiPrefix()
        {
            redniBrojLinije++;


            return null;
        }

        public void _IspisVremenskogPlana()
        {
            Console.WriteLine("Ispis vremenskog plana");
        }

        public void _UnesiProgram()
        {
            Console.WriteLine("Unesi program> ");
        }

        public void _UnesiDanUTjednu()
        {
            Console.WriteLine("Dan u tjednu: od 1 (pon) do 7 (ned)\nUnesi dan u tjednu> ");
        }

        public void _UnesiOsobu()
        {
            Console.WriteLine("Unesi osobu> ");
        }

        public void _OsobaNemaNiJednuUlogu()
        {
            Console.WriteLine("Osoba nema ni jednu ulogu!");
        }

        public void _UnesiNovuUloguZaZamjenuPostojece()
        {
            Console.WriteLine("Unesi novu ulogu za zamjenu postojece> ");
        }

        public void _NePostojiEmisijaSJednoznacnimBrojem()
        {
            Console.WriteLine("Ne postoji emisija s tim jednoznacnim brojem");
        }

        public void _UnesiZeljenuBoju()
        {
            Console.WriteLine("Unesite zeljenu boju (plava, zelena, crvena)> ");
        }

        public void _UnesiZeljenoStanje()
        {
            Console.WriteLine("Unesi zeljeno stanje> ");
        }

        public void _NemaSpremljenihStanja()
        {
            Console.WriteLine("Nema spremljenih stanja!");
        }

        public void _ObrisiEmisijuRasporeda()
        {
            Console.WriteLine("Unesi jednoznacni redni broj emisije za brisanje> ");
        }

        public void _IspisPrihoda()
        {
            Console.WriteLine("Ispis prihoda");
        }

        public void _UnesiBrojVrsteEmisije()
        {
            Console.Write("Unesi broj vrste emisije> ");
        }

        public void _UnesiPostojecuUloguOsobe()
        {
            Console.WriteLine("Unesi postojecu ulogu osobe> ");
        }


        public void IspisGlavniIzbornik()
        {
            Console.WriteLine("1. Unesi program i dan u tjednu - Ispis vremenskog plana");
            Console.WriteLine("2. Unesi program i dan u tjednu za ispis potencijalnih prihoda od reklama (u min)");
            Console.WriteLine("3. Unesi vrstu emisije za ispis tjednog plana po svim programima, danima i emisijama");
            Console.WriteLine("4. Unesi osobu, postojecu ulogu osobe i novu ulogu kojom zamjenjuje postojecu u svojim emisijama");
            Console.WriteLine("5. Brisanje emisije");
            Console.WriteLine("6. Prikaz povjesti pohrane");
            Console.WriteLine("7. Vrati raspored na zeljeno stanje");
            Console.WriteLine("8. Dodatna funkcionalnost: promjena boje konzole");
            Console.Write("Odabir> ");
        }

        public void Ispisi(string ispis)
        {
            Console.WriteLine(ispis);
        }

        public void IspisiVrsteEmisija(List<VrstaEmisije> vrsteEmisije)
        {
            foreach (var vrstaEmisije in vrsteEmisije)
            {
                Console.WriteLine(vrstaEmisije);
            }
        }

        public void IspisiProgrameTvKuce(List<IRasporedProgramaComponent> RasporedPrograma)
        {
            foreach (Program rasporedProgramaComponent in RasporedPrograma)
            {
                Console.WriteLine(rasporedProgramaComponent.ToString());
            }
        }




        public void IspisiOsobe(List<Osoba> listaOsoba)
        {
            foreach (var osoba in listaOsoba)
            {
                Console.WriteLine(osoba);
            }
        }

        public void IspisiUloge(List<Uloga> uloge)
        {
            foreach (var uloga in uloge)
            {
                Console.WriteLine(uloga);
            }
        }
    }
}
