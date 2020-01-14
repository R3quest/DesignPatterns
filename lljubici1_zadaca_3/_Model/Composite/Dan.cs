using System.Collections.Generic;
using lljubici1_zadaca_3._Model.Podaci;
using lljubici1_zadaca_3._Model.Prototype;

namespace lljubici1_zadaca_3._Model.Composite
{
    public class Dan : IRasporedProgramaComponent, Kloniraj
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



        public List<IRasporedProgramaComponent> VratiRasporedEmisija()
        {
            //Console.WriteLine(NazivDana);
            //IIterator iteratorEmisija = KreirajIterator();

            //for (var item = iteratorEmisija.Prvi(); !iteratorEmisija.Gotovo; item = iteratorEmisija.Sljedeci())
            //{
            //    ((EmisijePrograma)item).VratiRasporedEmisija();
            //}
            List<IRasporedProgramaComponent> listaEmisija = new List<IRasporedProgramaComponent>();
            foreach (var r in RasporedEmisijaDana)
            {
                listaEmisija.AddRange(r.VratiRasporedEmisija());
            }

            return listaEmisija;
        }

        public List<IRasporedProgramaComponent> VratiRaspored()
        {
            return this.RasporedEmisijaDana;
        }

        public Kloniraj Kloniraj()
        {
            Dan d = new Dan(this.NazivDana);

            foreach (EmisijePrograma emisijeDana in RasporedEmisijaDana)
            {
                d.RasporedEmisijaDana.Add((EmisijePrograma)emisijeDana.Kloniraj());
            }

            return d;
        }
    }
}
