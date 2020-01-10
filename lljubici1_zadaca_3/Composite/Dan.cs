using lljubici1_zadaca_3.Decorator;
using System.Collections.Generic;

namespace lljubici1_zadaca_3.Composite
{
    public class Dan : IRasporedProgramaComponent
    {
        public string NazivDana { get; set; }
        public List<IRasporedProgramaComponent> RasporedEmisijaDana { get; set; } = new List<IRasporedProgramaComponent>();

        public Dan(string nazivDana)
        {
            NazivDana = nazivDana;
        }

        public void DodajElementRasporeda(IRasporedProgramaComponent elementComposite)
        {
            RasporedEmisijaDana.Add(elementComposite);
        }



        public List<IComponent> VratiRaspored()
        {
            //Console.WriteLine(NazivDana);
            //IIterator iteratorEmisija = KreirajIterator();

            //for (var item = iteratorEmisija.Prvi(); !iteratorEmisija.Gotovo; item = iteratorEmisija.Sljedeci())
            //{
            //    ((EmisijePrograma)item).VratiRaspored();
            //}
            List<IComponent> listaEmisija = new List<IComponent>();
            foreach (var r in RasporedEmisijaDana)
            {
                listaEmisija.AddRange(r.VratiRaspored());
            }

            return listaEmisija;
        }
    }
}
