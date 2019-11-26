using lljubici1_zadaca_2.FactoryMethod;
using System.Collections.Generic;

namespace lljubici1_zadaca_2.Podaci
{
    public class Emisija : Entitet
    {
        public int Id { get; set; }
        public string NazivEmisije { get; set; }

        public VrstaEmisije VrstaEmisije { get; set; }
        //public int VrstaEmisije { get; set; }
        public int Trajanje { get; set; }
        public List<OsobaUloga> OsobeUloge { get; set; }

        public Emisija()
        {

        }

        public Emisija(int id, string nazivEmisije, VrstaEmisije vrstaEmisije, int trajanje, List<OsobaUloga> osobeUloge)
        {
            Id = id;
            NazivEmisije = nazivEmisije;
            VrstaEmisije = vrstaEmisije;
            Trajanje = trajanje;
            OsobeUloge = osobeUloge;
        }

        public override string ToString()
        {
            //return $"{nameof(Id)}: {Id}, {nameof(NazivEmisije)}: {NazivEmisije}, {nameof(Trajanje)}: {Konverzija.PretvoriSekundeUVrijeme(Trajanje)}";
            return $"{NazivEmisije}";
        }

    }
}