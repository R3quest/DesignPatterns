using lljubici1_zadaca_2.Podaci;

namespace lljubici1_zadaca_2.Decorator
{
    public class ConcreateComponentPrihodiReklama : IComponent
    {
        private const int sirinaStupca = 20;
        private const int sirinaNazivaEmisije = 40;
        private const int sirinaTablice = 105;
        public int Prihod { get; set; }

        public EmisijePrograma EmisijaPrograma { get; set; }
        public string NazivPrograma { get; set; }
        public string NazivDana { get; set; }

        public ConcreateComponentPrihodiReklama(EmisijePrograma emisijaPrograma, string nazivPrograma, string nazivDana, int ukupniPrihod = -1)
        {
            this.EmisijaPrograma = emisijaPrograma;
            this.NazivPrograma = nazivPrograma;
            this.NazivDana = nazivDana;
            this.Prihod = ukupniPrihod;
        }

        public string Operacija()
        {
            if (Prihod != -1)
            {
                return VratiKraj();
            }
            if (EmisijaPrograma == null && string.IsNullOrEmpty(NazivDana) && string.IsNullOrEmpty(NazivPrograma))
            {
                return VratiGlavnoZaglavlje();
            }
            if (EmisijaPrograma != null && !string.IsNullOrEmpty(NazivDana) && !string.IsNullOrEmpty(NazivPrograma))
            {
                return VratiPodatkeProgramDanEmisija();
            }
            //if (EmisijaPrograma != null)
            //{
            return VratiPodatkeEmisijeBezDana();
            //}
        }

        public string VratiGlavnoZaglavlje()
        {
            string zaglavlje = new string('-', sirinaTablice) + '\n' +
                               $"|{"Program",-sirinaStupca}|{"Dan",sirinaStupca}|{"Emisija",-sirinaNazivaEmisije}|{"Prihod",-sirinaStupca}|\n" +
                               new string('-', sirinaTablice) + '\n';
            return zaglavlje;
        }

        public string VratiPodatkeEmisijeBezDana()
        {
            string emisija =
                $"|{"",-sirinaStupca}|{"",-sirinaStupca}|{EmisijaPrograma.Emisija.NazivEmisije,-sirinaNazivaEmisije}|" +
                $"{EmisijaPrograma.Emisija.VrstaEmisije.TrajanjeReklame,sirinaStupca}|\n" + new string('-', sirinaTablice) + '\n';

            return emisija;
        }

        public string VratiPodatkeProgramDanEmisija()
        {
            string programDanEmisija =
                $"|{NazivPrograma,-sirinaStupca}|{NazivDana,-sirinaStupca}|{EmisijaPrograma.Emisija.NazivEmisije,-sirinaNazivaEmisije}|" +
                $"{EmisijaPrograma.Emisija.VrstaEmisije.TrajanjeReklame,sirinaStupca}|\n" + new string('-', sirinaTablice) + '\n';
            return programDanEmisija;
        }

        public string VratiKraj()
        {
            return $"|{"Ukupno",-sirinaStupca}{"",(sirinaStupca + sirinaNazivaEmisije + 2)}|{Prihod,sirinaStupca}|\n" + new string('-', sirinaTablice);
        }
    }
}
