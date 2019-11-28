using lljubici1_zadaca_2.Composite;
using lljubici1_zadaca_2.Podaci;
using System.Collections.Generic;

namespace lljubici1_zadaca_2.Iterator
{
    public class ConcreateIteratorEmisijaZeljeneVrste : IIterator
    {
        //https://www.dofactory.com/net/iterator-design-pattern

        private List<IRasporedProgramaComponent> _kolekcija = new List<IRasporedProgramaComponent>();
        private int _trenutni = 0;
        private int _korak = 1;



        // Constructor
        public ConcreateIteratorEmisijaZeljeneVrste(List<IRasporedProgramaComponent> kolekcija, string vrsta)
        {
            foreach (Program program in kolekcija)
            {
                foreach (Dan dan in program.RasporedDani)
                {
                    foreach (EmisijePrograma emisija in dan.RasporedEmisijaDana)
                    {
                        if (emisija.Emisija.VrstaEmisije.Vrsta.Equals(vrsta))
                        {
                            _kolekcija.Add(emisija);
                        }
                    }
                }
            }
        }

        // Gets first item
        public IRasporedProgramaComponent Prvi()
        {
            _trenutni = 0;
            return _kolekcija[_trenutni];
        }

        // Gets next item

        public IRasporedProgramaComponent Sljedeci()
        {
            _trenutni += _korak;
            if (!Gotovo)
                return _kolekcija[_trenutni];
            return null;
        }

        // Gets or sets stepsize

        public int Korak
        {
            get { return _korak; }
            set { _korak = value; }
        }

        // Gets current iterator item

        public IRasporedProgramaComponent Trenutni
        {
            get { return _kolekcija[_trenutni] as IRasporedProgramaComponent; }
        }

        // Gets whether iteration is complete

        public bool Gotovo
        {
            get { return _trenutni >= _kolekcija.Count; }
        }
    }
}
