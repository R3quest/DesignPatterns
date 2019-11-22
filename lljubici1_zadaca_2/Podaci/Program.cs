using lljubici1_zadaca_2.FactoryMethod;
using lljubici1_zadaca_2.Pomagala;
using System.Collections.Generic;

namespace lljubici1_zadaca_2.Podaci
{
    public class Program : Entitet
    {
        public List<EmisijePrograma> EmisijePrograma { get; set; } = new List<EmisijePrograma>();
        public int Id { get; set; }
        public string NazivPrograma { get; set; }
        public int Pocetak { get; set; }
        public int Kraj { get; set; }
        public string NazivDatoteke { get; set; }

        public Program(int id, string nazivPrograma, int pocetak, int kraj, string nazivDatoteke)
        {
            Id = id;
            NazivPrograma = nazivPrograma;
            Pocetak = pocetak;
            Kraj = kraj;
            NazivDatoteke = nazivDatoteke;
        }

        public override string ToString()
        {
            return
                $"{nameof(NazivPrograma)}: {NazivPrograma}, {nameof(Pocetak)}: {Konverzija.PretvoriSekundeUVrijeme(Pocetak)}, {nameof(NazivDatoteke)}: {NazivDatoteke}";
        }
    }
}
