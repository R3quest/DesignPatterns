using lljubici1_zadaca_2.Composite;
using lljubici1_zadaca_2.Decorator;
using lljubici1_zadaca_2.FactoryMethod;
using lljubici1_zadaca_2.Observer;
using lljubici1_zadaca_2.Pomagala;
using System.Collections.Generic;

namespace lljubici1_zadaca_2.Podaci
{
    public class EmisijePrograma : Entitet, IRasporedProgramaComponent, IObserver
    {
        public Emisija Emisija { get; set; } = new Emisija();
        public List<Osoba> OsobeUloge { get; set; } = new List<Osoba>();
        public List<int> DaniUTjednu { get; set; } = new List<int>();
        public int Pocetak { get; set; }

        public bool ImaPočetak { get; set; } = false;

        public EmisijePrograma()
        {

        }

        public EmisijePrograma(int idEmisije, List<int> daniUTjednu, List<Osoba> osobeUloge, string pocetak)
        {
            DaniUTjednu = daniUTjednu;
            if (osobeUloge != null)
            {
                OsobeUloge.AddRange(osobeUloge);
            }
            //StringPocetak = pocetak;
            if (pocetak != "")
            {
                Pocetak = Konverzija.PretvoriVrijemeUSekunde(pocetak);
                ImaPočetak = true;
            }
            Emisija.Id = idEmisije;
        }

        public EmisijePrograma(int idEmisije, List<int> daniUTjednu, Osoba osobeUloge, string pocetak)
        {
            DaniUTjednu = daniUTjednu;
            if (osobeUloge != null)
            {
                OsobeUloge.Add(osobeUloge);
            }
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

        public void Azuriraj(ISubject subject)
        {
            Osoba osoba = (Osoba)subject;
            var indexUloge = this.OsobeUloge.FindIndex(u => u.Id == osoba.Id);
            if (indexUloge != -1) OsobeUloge[indexUloge] = osoba;
        }

        public void DodajElementRasporeda(IRasporedProgramaComponent elementComposite)
        {
            Emisija = ((EmisijePrograma)elementComposite).Emisija;
            OsobeUloge = ((EmisijePrograma)elementComposite).OsobeUloge;
            DaniUTjednu = ((EmisijePrograma)elementComposite).DaniUTjednu;
            Pocetak = ((EmisijePrograma)elementComposite).Pocetak;
            ImaPočetak = ((EmisijePrograma)elementComposite).ImaPočetak;

            //observer prikvaci
            foreach (var o in OsobeUloge)
            {
                o.Prikaci(this);
            }

        }

        public List<IComponent> VratiRaspored()
        {
            List<IComponent> komponenta = new List<IComponent>();
            ConcreateComponentProgramDanEmisija concreateKomponenta =
                new ConcreateComponentProgramDanEmisija(this, null, null);
            komponenta.Add(concreateKomponenta);
            return komponenta;
        }

    }
}
