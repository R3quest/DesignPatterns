using lljubici1_zadaca_2.Composite;
using lljubici1_zadaca_2.FactoryMethod;
using lljubici1_zadaca_2.Pomagala;
using System;
using System.Collections.Generic;

namespace lljubici1_zadaca_2.Podaci
{
    public class EmisijePrograma : Entitet, IRasporedProgramaComponent
    {
        public Emisija Emisija { get; set; } = new Emisija();
        public List<OsobaUloga> OsobeUloge { get; set; } = new List<OsobaUloga>();
        public List<int> DaniUTjednu { get; set; } = new List<int>();
        public int Pocetak { get; set; }

        public bool ImaPočetak { get; set; } = false;

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

        public void IspisiRaspored()
        {
            Console.WriteLine(ToString()); //TODO!: ispis
        }
    }
}
