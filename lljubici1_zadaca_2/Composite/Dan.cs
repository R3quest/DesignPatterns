using System.Collections.Generic;

namespace lljubici1_zadaca_2.Composite
{
    public class Dan : IRasporedProgramaComponent
    {
        public string NazivDana { get; set; }
        public List<IRasporedProgramaComponent> RasporedEmisijaDana { get; set; } = new List<IRasporedProgramaComponent>();

        public Dan(string nazivDana)
        {
            NazivDana = nazivDana;
        }

        public void DodajElementRasporeda(IRasporedProgramaComponent elementComposite) //DODAJE SE EMISIJA
        {
            RasporedEmisijaDana.Add(elementComposite);
        }

        public void IspisiRaspored()
        {
            //Console.WriteLine(NazivDana);
            //IIterator iteratorEmisija = KreirajIterator();

            //for (var item = iteratorEmisija.Prvi(); !iteratorEmisija.Gotovo; item = iteratorEmisija.Sljedeci())
            //{
            //    ((EmisijePrograma)item).IspisiRaspored();
            //}

            foreach (var r in RasporedEmisijaDana)
            {
                r.IspisiRaspored();
            }
        }

        //public void IspisiRasporedOdredenogDana()
        //{

        //}

        //public IIterator KreirajIterator()
        //{
        //    return new ConcreateIterator(RasporedEmisijaDana);
        //}
    }
}
