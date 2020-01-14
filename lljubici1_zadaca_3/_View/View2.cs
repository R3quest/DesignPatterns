using lljubici1_zadaca_3._Model.Composite;
using lljubici1_zadaca_3._Model.Podaci;
using System;
using System.Collections.Generic;

namespace lljubici1_zadaca_3._View
{
    public class View2 : IView
    {
        private int redniBrojLinije = 0;
        private string VratiPrefix()
        {
            redniBrojLinije++;
            return String.Format("[{0:D5}] ", redniBrojLinije);
        }

        public void _IspisVremenskogPlana()
        {
            Console.WriteLine(VratiPrefix() + "Ispis vremenskog plana");
        }

        public void _UnesiProgram()
        {
            Console.WriteLine(VratiPrefix() + "Unesi program> ");
        }

        public void _UnesiDanUTjednu()
        {
            Console.WriteLine(VratiPrefix() + "Dan u tjednu: od 1 (pon) do 7 (ned)\nUnesi dan u tjednu> ");
        }

        public void _UnesiOsobu()
        {
            Console.WriteLine(VratiPrefix() + "Unesi osobu> ");
        }

        public void _OsobaNemaNiJednuUlogu()
        {
            Console.WriteLine(VratiPrefix() + "Osoba nema ni jednu ulogu!");
        }

        public void _UnesiNovuUloguZaZamjenuPostojece()
        {
            Console.WriteLine(VratiPrefix() + "Unesi novu ulogu za zamjenu postojece> ");
        }

        public void _NePostojiEmisijaSJednoznacnimBrojem()
        {
            Console.WriteLine(VratiPrefix() + "Ne postoji emisija s tim jednoznacnim brojem");
        }

        public void _UnesiZeljenuBoju()
        {
            Console.WriteLine(VratiPrefix() + "Unesite zeljenu boju (plava, zelena, crvena)> ");
        }

        public void _UnesiZeljenoStanje()
        {
            Console.WriteLine(VratiPrefix() + "Unesi zeljeno stanje> ");
        }

        public void _NemaSpremljenihStanja()
        {
            Console.WriteLine(VratiPrefix() + "Nema spremljenih stanja!");
        }

        public void _ObrisiEmisijuRasporeda()
        {
            Console.WriteLine(VratiPrefix() + "Unesi jednoznacni redni broj emisije za brisanje> ");
        }

        public void _IspisPrihoda()
        {
            Console.WriteLine(VratiPrefix() + "Ispis prihoda");
        }

        public void _UnesiBrojVrsteEmisije()
        {
            Console.Write(VratiPrefix() + "Unesi broj vrste emisije> ");
        }

        public void _UnesiPostojecuUloguOsobe()
        {
            Console.WriteLine(VratiPrefix() + "Unesi postojecu ulogu osobe> ");
        }


        public void IspisGlavniIzbornik()
        {
            Console.WriteLine(VratiPrefix() + "1. Unesi program i dan u tjednu - Ispis vremenskog plana");
            Console.WriteLine(VratiPrefix() + "2. Unesi program i dan u tjednu za ispis potencijalnih prihoda od reklama (u min)");
            Console.WriteLine(VratiPrefix() + "3. Unesi vrstu emisije za ispis tjednog plana po svim programima, danima i emisijama");
            Console.WriteLine(VratiPrefix() + "4. Unesi osobu, postojecu ulogu osobe i novu ulogu kojom zamjenjuje postojecu u svojim emisijama");
            Console.WriteLine(VratiPrefix() + "5. Brisanje emisije");
            Console.WriteLine(VratiPrefix() + "6. Prikaz povjesti pohrane");
            Console.WriteLine(VratiPrefix() + "7. Vrati raspored na zeljeno stanje");
            Console.WriteLine(VratiPrefix() + "8. Dodatna funkcionalnost: promjena boje konzole");
            Console.WriteLine(VratiPrefix() + "9. Promjeni pogled");
            Console.Write(VratiPrefix() + "Odabir> ");
        }

        public void Ispisi(string ispis)
        {
            Console.WriteLine(VratiPrefix() + ispis);
        }

        public void IspisiVrsteEmisija(List<VrstaEmisije> vrsteEmisije)
        {
            foreach (var vrstaEmisije in vrsteEmisije)
            {
                Console.WriteLine(VratiPrefix() + vrstaEmisije);
            }
        }

        public void IspisiProgrameTvKuce(List<IRasporedProgramaComponent> RasporedPrograma)
        {
            foreach (Program rasporedProgramaComponent in RasporedPrograma)
            {
                Console.WriteLine(VratiPrefix() + rasporedProgramaComponent.ToString());
            }
        }




        public void IspisiOsobe(List<Osoba> listaOsoba)
        {
            foreach (var osoba in listaOsoba)
            {
                Console.WriteLine(VratiPrefix() + osoba);
            }
        }

        public void IspisiUloge(List<Uloga> uloge)
        {
            foreach (var uloga in uloge)
            {
                Console.WriteLine(VratiPrefix() + uloga);
            }
        }
    }
}
