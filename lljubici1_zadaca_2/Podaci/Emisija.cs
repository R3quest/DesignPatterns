using lljubici1_zadaca_2.FactoryMethod;
using lljubici1_zadaca_2.Pomagala;
using System.Collections.Generic;

namespace lljubici1_zadaca_2.Podaci
{
    public class Emisija : Entitet
    {
        public int Id { get; set; }
        public string NazivEmisije { get; set; }
        public int VrstaEmisije { get; set; }
        public int Trajanje { get; set; }
        public List<OsobaUloga> OsobeUloge { get; set; }

        public Emisija()
        {

        }

        public Emisija(int id, string nazivEmisije, int vrstaEmisije, int trajanje, List<OsobaUloga> osobaUloge)
        {
            Id = id;
            NazivEmisije = nazivEmisije;
            VrstaEmisije = vrstaEmisije;
            Trajanje = trajanje;
            OsobeUloge = osobaUloge;
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(NazivEmisije)}: {NazivEmisije}, {nameof(Trajanje)}: {Konverzija.PretvoriSekundeUVrijeme(Trajanje)}";
        }

    }
}