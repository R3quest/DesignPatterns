using lljubici1_zadaca_2.Podaci;
using lljubici1_zadaca_2.Pomagala;

namespace lljubici1_zadaca_2.Decorator
{
    class ConcreateComponent : IComponent
    {
        private const int sirinaStupca = 20;

        private const int sirinaTablice = 85;
        //svi propertiji
        public EmisijePrograma EmisijaPrograma { get; set; }
        public string NazivPrograma { get; set; }
        public string NazivDana { get; set; }

        public ConcreateComponent(EmisijePrograma emisijaPrograma, string program, string dan)
        {
            EmisijaPrograma = emisijaPrograma;
            NazivPrograma = program;
            NazivDana = dan;
        }

        public string Operacija()
        {
            //provjeri
            if (EmisijaPrograma == null)
            {
                return VratiGlavnoZaglavlje();
            }
            if (!string.IsNullOrEmpty(NazivPrograma))
            {
                return VratiPodatkeProgramDanEmisija();
            }
            if (!string.IsNullOrEmpty(NazivDana))
            {
                return VratiPodatkeEmisije();
            }
            return VratiKraj();
        }


        //

        public string VratiGlavnoZaglavlje()
        {
            string zaglavlje = new string('-', sirinaTablice) + '\n' +
                $"|{"Program",-sirinaStupca}|{"Dan",-sirinaStupca}|{"Emisija",-sirinaStupca}|{"Pocetak - Kraj",-sirinaStupca}|\n" +
                new string('-', sirinaTablice) + '\n';
            return zaglavlje;
        }

        public string VratiPodatkeProgramDanEmisija()
        {
            //"Pocetak - Kraj"
            string pocetak = Konverzija.PretvoriSekundeUVrijeme(EmisijaPrograma.Pocetak);
            string kraj = Konverzija.PretvoriSekundeUVrijeme(EmisijaPrograma.Pocetak + EmisijaPrograma.Emisija.Trajanje);

            string programDanEmisija = new string('-', sirinaTablice) + '\n' +
                               $"|{NazivPrograma,-sirinaStupca}|{NazivDana,-sirinaStupca}|{EmisijaPrograma.Emisija.NazivEmisije,-sirinaStupca}|" +
                               $"{pocetak + " - " + kraj,-sirinaStupca}|\n" + new string('-', sirinaTablice) + '\n';
            return programDanEmisija;
        }

        public string VratiPodatkeEmisije()
        {
            string pocetak = Konverzija.PretvoriSekundeUVrijeme(EmisijaPrograma.Pocetak);
            string kraj = Konverzija.PretvoriSekundeUVrijeme(EmisijaPrograma.Pocetak + EmisijaPrograma.Emisija.Trajanje);

            string emisija = new string('-', sirinaTablice) + '\n' +
                             $"|{"",-sirinaStupca}|{"",-sirinaStupca}|{EmisijaPrograma.Emisija.NazivEmisije,-sirinaStupca}|" +
                             $"{pocetak + " - " + kraj,-sirinaStupca}|\n" + new string('-', sirinaTablice) + '\n';

            return emisija;
        }
        public string VratiKraj()
        {
            return new string('-', sirinaTablice);
        }
    }
}
