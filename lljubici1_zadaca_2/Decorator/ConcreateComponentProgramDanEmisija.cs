using lljubici1_zadaca_2.Podaci;
using lljubici1_zadaca_2.Pomagala;

namespace lljubici1_zadaca_2.Decorator
{
    class ConcreateComponentProgramDanEmisija : IComponent
    {
        private const int sirinaStupca = 20;
        private const int sirinaNazivaEmisije = 40;

        private const int sirinaTablice = 105;
        //svi propertiji
        public EmisijePrograma EmisijaPrograma { get; set; }
        public string NazivPrograma { get; set; }
        public string NazivDana { get; set; }

        public ConcreateComponentProgramDanEmisija(EmisijePrograma emisijaPrograma, string program, string dan)
        {
            EmisijaPrograma = emisijaPrograma;
            NazivPrograma = program;
            NazivDana = dan;
        }

        public string Operacija()
        {
            //provjeri
            //if (EmisijaPrograma == null)
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
                return VratiPodatkeEmisijeSaDanom();
            }
            if (string.IsNullOrEmpty(NazivDana) && string.IsNullOrEmpty(NazivPrograma))
            {
                return VratiPodatkeEmisijeBezDana();
            }

            return VratiKraj();
        }


        //

        public string VratiGlavnoZaglavlje()
        {
            string zaglavlje = new string('-', sirinaTablice) + '\n' +
                $"|{"Program",-sirinaStupca}|{"Dan",-sirinaStupca}|{"Emisija",-sirinaNazivaEmisije}|{"Pocetak - Kraj",-sirinaStupca}|\n" +
                new string('-', sirinaTablice) + '\n';
            return zaglavlje;
        }

        public string VratiPodatkeProgramDanEmisija()
        {
            //"Pocetak - Kraj"
            string pocetak = Konverzija.PretvoriSekundeUVrijeme(EmisijaPrograma.Pocetak);
            string kraj = Konverzija.PretvoriSekundeUVrijeme(EmisijaPrograma.Pocetak + EmisijaPrograma.Emisija.Trajanje);

            //new string('-', sirinaTablice) + '\n' +
            string programDanEmisija =
                               $"|{NazivPrograma,-sirinaStupca}|{NazivDana,-sirinaStupca}|{EmisijaPrograma.Emisija.NazivEmisije,-sirinaNazivaEmisije}|" +
                               $"{pocetak + " - " + kraj,-sirinaStupca}|\n" + new string('-', sirinaTablice) + '\n';
            return programDanEmisija;
        }

        public string VratiPodatkeEmisijeSaDanom()
        {
            string pocetak = Konverzija.PretvoriSekundeUVrijeme(EmisijaPrograma.Pocetak);
            string kraj = Konverzija.PretvoriSekundeUVrijeme(EmisijaPrograma.Pocetak + EmisijaPrograma.Emisija.Trajanje);
            string emisija =
                $"|{"",-sirinaStupca}|{NazivDana,-sirinaStupca}|{EmisijaPrograma.Emisija.NazivEmisije,-sirinaNazivaEmisije}|" +
                $"{pocetak + " - " + kraj,-sirinaStupca}|\n" + new string('-', sirinaTablice) + '\n';

            return emisija;
        }

        public string VratiPodatkeEmisijeBezDana()
        {
            string pocetak = Konverzija.PretvoriSekundeUVrijeme(EmisijaPrograma.Pocetak);
            string kraj = Konverzija.PretvoriSekundeUVrijeme(EmisijaPrograma.Pocetak + EmisijaPrograma.Emisija.Trajanje);

            string emisija =
                $"|{"",-sirinaStupca}|{"",-sirinaStupca}|{EmisijaPrograma.Emisija.NazivEmisije,-sirinaNazivaEmisije}|" +
                $"{pocetak + " - " + kraj,-sirinaStupca}|\n" + new string('-', sirinaTablice) + '\n';

            return emisija;
        }

        public string VratiKraj()
        {
            return new string('-', sirinaTablice);
        }
    }
}
