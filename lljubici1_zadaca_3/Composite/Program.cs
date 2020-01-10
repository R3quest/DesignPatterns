using lljubici1_zadaca_3.Decorator;
using lljubici1_zadaca_3.FactoryMethod;
using lljubici1_zadaca_3.Iterator;
using lljubici1_zadaca_3.Podaci;
using lljubici1_zadaca_3.Pomagala;
using System;
using System.Collections.Generic;

namespace lljubici1_zadaca_3.Composite
{
    public class Program : Entitet, IRasporedProgramaComponent, IAbstractCollectionSveEmisije
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



        public List<IComponent> VratiRaspored()
        {
            //TODO: druga zadaca
            List<IComponent> listaDana = new List<IComponent>();
            foreach (var r in RasporedDani)
            {
                listaDana.AddRange(r.VratiRaspored());
            }

            return listaDana;
        }

        public void IspisiRaspored() //za sve dane
        {
            //TODO: other
            IIterator iteratorEmisija = KreirajIterator();

            for (var item = iteratorEmisija.Prvi(); !iteratorEmisija.Gotovo; item = iteratorEmisija.Sljedeci())
            {
                item.VratiRaspored();
            }
        }

        public IIterator KreirajIterator()
        {
            return new ConcreateIteratorEmisijaTjednogPlana(RasporedDani);
        }


        public void IspisZaDan(int danIndex)
        {
            Console.WriteLine(((Dan)RasporedDani[danIndex - 1]).NazivDana);
            RasporedDani[danIndex - 1].VratiRaspored();
        }

    }
}
