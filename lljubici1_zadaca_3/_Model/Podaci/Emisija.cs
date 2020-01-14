using System.Collections.Generic;
using lljubici1_zadaca_3._Model.FactoryMethod;
using lljubici1_zadaca_3._Model.Prototype;

namespace lljubici1_zadaca_3._Model.Podaci
{
    public class Emisija : Entitet, Kloniraj
    {
        public int Id { get; set; }
        public string NazivEmisije { get; set; }

        public VrstaEmisije VrstaEmisije { get; set; }
        //public int VrstaEmisije { get; set; }
        public int Trajanje { get; set; }
        public List<Osoba> OsobeUloge { get; set; } = new List<Osoba>();

        public Emisija()
        {

        }

        public Emisija(int id, string nazivEmisije, VrstaEmisije vrstaEmisije, int trajanje, List<Osoba> osobeUloge)
        {
            Id = id;
            NazivEmisije = nazivEmisije;
            VrstaEmisije = vrstaEmisije;
            Trajanje = trajanje;
            if (osobeUloge != null)
            {
                OsobeUloge.AddRange(osobeUloge);
            }
        }

        public Emisija(int id, string nazivEmisije, VrstaEmisije vrstaEmisije, int trajanje, Osoba osobeUloge)
        {
            Id = id;
            NazivEmisije = nazivEmisije;
            VrstaEmisije = vrstaEmisije;
            Trajanje = trajanje;
            if (osobeUloge != null)
            {
                OsobeUloge.Add(osobeUloge);
            }
        }

        public override string ToString()
        {
            //return $"{nameof(Id)}: {Id}, {nameof(NazivEmisije)}: {NazivEmisije}, {nameof(Trajanje)}: {Konverzija.PretvoriSekundeUVrijeme(Trajanje)}";
            return $"{NazivEmisije}";
        }

        public Kloniraj Kloniraj()
        {
            Emisija e = new Emisija();
            e.VrstaEmisije = (VrstaEmisije)VrstaEmisije.Kloniraj();
            e.Id = Id;
            e.NazivEmisije = NazivEmisije;
            foreach (var osoba in OsobeUloge)
            {
                e.OsobeUloge.Add((Osoba)osoba.Kloniraj());
            }

            e.Trajanje = Trajanje;
            return e;
        }
    }
}