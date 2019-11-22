using lljubici1_zadaca_2.FactoryMethod;
using lljubici1_zadaca_2.Pomagala;
using System.Collections.Generic;

namespace lljubici1_zadaca_2.Podaci
{
    public class EmisijePrograma : Entitet
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
            return $"{Emisija} {nameof(Pocetak)}EmisijePrograma: {Konverzija.PretvoriSekundeUVrijeme(Pocetak)}";
        }

        //public override bool Equals(object obj)
        //{
        //    if (obj == null)
        //        return false;
        //    if (obj.GetType() != typeof(EmisijePrograma))
        //        return false;

        //    EmisijePrograma drugi = (EmisijePrograma) obj;
        //    return this.Emisija.Id == drugi.Emisija.Id;
        //}
    }
}
