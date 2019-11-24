using lljubici1_zadaca_2.FactoryMethod;
using lljubici1_zadaca_2.Podaci;
using lljubici1_zadaca_2.Pomagala;
using System;
using System.Collections.Generic;

namespace lljubici1_zadaca_2.Composite
{
    public class Program : Entitet, IRasporedProgramaComponent
    {
        public List<IRasporedProgramaComponent> RasporedDani { get; set; } = new List<IRasporedProgramaComponent>();

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

        public void DodajElementRasporeda(IRasporedProgramaComponent elementComposite) //dodaj dan
        {
            RasporedDani.Add(elementComposite);
        }

        public void IspisiRaspored() //za sve dane
        {
            foreach (var dan in RasporedDani)
            {
                Console.WriteLine(((Dan)dan).NazivDana);
                dan.IspisiRaspored();
            }
        }

        public void IspisZaDan(int danIndex)
        {
            Console.WriteLine(((Dan)RasporedDani[danIndex - 1]).NazivDana);
            RasporedDani[danIndex - 1].IspisiRaspored();
        }

    }
}
