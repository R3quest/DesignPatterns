using lljubici1_zadaca_2.Composite;
using lljubici1_zadaca_2.Decorator;
using lljubici1_zadaca_2.FactoryMethod;
using lljubici1_zadaca_2.Observer;
using lljubici1_zadaca_2.Pomagala;
using System.Collections.Generic;

namespace lljubici1_zadaca_2.Podaci
{
    public class EmisijePrograma : Entitet, IRasporedProgramaComponent, ISubject
    {
        public Emisija Emisija { get; set; } = new Emisija();
        public List<OsobaUloga> OsobeUloge { get; set; } = new List<OsobaUloga>();
        public List<int> DaniUTjednu { get; set; } = new List<int>();
        public int Pocetak { get; set; }

        public bool ImaPočetak { get; set; } = false;

        public EmisijePrograma()
        {

        }

        public EmisijePrograma(int idEmisije, List<int> daniUTjednu, List<OsobaUloga> osobeUloge, string pocetak)
        {
            DaniUTjednu = daniUTjednu;
            OsobeUloge = osobeUloge;
            //StringPocetak = pocetak;
            if (pocetak != "")
            {
                Pocetak = Konverzija.PretvoriVrijemeUSekunde(pocetak);
                ImaPočetak = true;
            }
            Emisija.Id = idEmisije;
        }

        public override string ToString()
        {

            return $"{Konverzija.PretvoriSekundeUVrijeme(Pocetak)} - {Konverzija.PretvoriSekundeUVrijeme(Pocetak + Emisija.Trajanje)} {Emisija} ";
        }

        public void DodajElementRasporeda(IRasporedProgramaComponent elementComposite)
        {
            Emisija = ((EmisijePrograma)elementComposite).Emisija;
            OsobeUloge = ((EmisijePrograma)elementComposite).OsobeUloge;
            DaniUTjednu = ((EmisijePrograma)elementComposite).DaniUTjednu;
            Pocetak = ((EmisijePrograma)elementComposite).Pocetak;
            ImaPočetak = ((EmisijePrograma)elementComposite).ImaPočetak;
        }

        public List<IComponent> VratiRaspored()
        {
            List<IComponent> komponenta = new List<IComponent>();
            ConcreateComponentProgramDanEmisija concreateKomponenta =
                new ConcreateComponentProgramDanEmisija(this, null, null);
            komponenta.Add(concreateKomponenta);
            return komponenta;
        }

        public void Prikaci(IObserver observer)
        {
            OsobeUloge.Add(observer as OsobaUloga);
        }

        public void Odvoji(IObserver observer)
        {
            OsobeUloge.Remove(observer as OsobaUloga);
        }

        public void Obavijesti()
        {
            foreach (var osobaUloga in OsobeUloge)
            {
                osobaUloga.Azuriraj(this);
            }
        }
    }
}
