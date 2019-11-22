using System.Collections.Generic;

namespace lljubici1_zadaca_2.Composite
{
    public class Dan : IRasporedProgramaComponent
    {
        public string NazivDana { get; set; }
        public List<IRasporedProgramaComponent> Raspored { get; set; } = new List<IRasporedProgramaComponent>();

        public Dan(string nazivDana)
        {
            NazivDana = nazivDana;
        }

        public void DodajElementRasporeda(IRasporedProgramaComponent elementComposite) //DODAJE SE DAN!
        {
            Raspored.Add(elementComposite);
        }

        public void IspisiRaspored()
        {
            foreach (var r in Raspored)
            {
                r.IspisiRaspored();
            }
        }
    }
}
