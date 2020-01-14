using lljubici1_zadaca_3.Podaci;
using lljubici1_zadaca_3.Pomagala;
using System.Linq;
using System.Text;

namespace lljubici1_zadaca_3.Decorator
{
    class ConcreateComponentProgramDanEmisija : IComponent
    {
        private const int sirinaStupca = 20;
        private const int sirinaNazivaEmisije = 42;

        private const int sirinaTablice = 171;
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

        private string VratiOsobeUloge()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var osoba in EmisijaPrograma.OsobeUloge)
            {
                foreach (var uloga in osoba.Uloge)
                {
                    if (EmisijaPrograma.OsobeUloge.First() == osoba)
                    {
                        sb.Append($"{osoba.ImeIPrezime + " - " + uloga,-sirinaNazivaEmisije}|\n");
                        continue;
                    }
                    sb.Append(
                        $"|{"",-sirinaStupca}|{"",-sirinaStupca}|{"",-sirinaStupca}|{"",-sirinaNazivaEmisije}|{"",-sirinaStupca}|" +
                        $"{osoba.ImeIPrezime + " - " + uloga,-sirinaNazivaEmisije}|\n");

                }
            }

            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            else
            {
                sb.Append($"{"|",sirinaNazivaEmisije + 1}");
            }
            return sb.ToString();
        }

        public string VratiGlavnoZaglavlje()
        {
            string zaglavlje = new string('-', sirinaTablice) + '\n' +
                               $"|{"Jedinstveni broj",-sirinaStupca}|{"Program",-sirinaStupca}|{"Dan",-sirinaStupca}|{"Emisija",-sirinaNazivaEmisije}|" +
                               $"{"Pocetak - Kraj",-sirinaStupca}|{"Osoba - Uloga",-sirinaNazivaEmisije}|\n" +
                               new string('-', sirinaTablice) + '\n';
            return zaglavlje;
        }

        public string VratiPodatkeProgramDanEmisija()
        {
            //"Pocetak - Kraj"
            string pocetak = Konverzija.PretvoriSekundeUVrijeme(EmisijaPrograma.Pocetak);
            string kraj = Konverzija.PretvoriSekundeUVrijeme(EmisijaPrograma.Pocetak + EmisijaPrograma.Emisija.Trajanje);

            //new string('-', sirinaTablice) + '\n' +
            string programDanEmisija = $"|{EmisijaPrograma.RedniBroj,sirinaStupca}" +
                $"|{NazivPrograma,-sirinaStupca}|{NazivDana,-sirinaStupca}|{EmisijaPrograma.Emisija.NazivEmisije,-sirinaNazivaEmisije}|" +
                $"{pocetak + " - " + kraj,-sirinaStupca}|" + VratiOsobeUloge() + '\n' +
                new string('-', sirinaTablice) + '\n';
            return programDanEmisija;
        }

        public string VratiPodatkeEmisijeSaDanom()
        {
            string pocetak = Konverzija.PretvoriSekundeUVrijeme(EmisijaPrograma.Pocetak);
            string kraj = Konverzija.PretvoriSekundeUVrijeme(EmisijaPrograma.Pocetak + EmisijaPrograma.Emisija.Trajanje);
            string emisija = $"|{EmisijaPrograma.RedniBroj,sirinaStupca}" +
                $"|{"",-sirinaStupca}|{NazivDana,-sirinaStupca}|{EmisijaPrograma.Emisija.NazivEmisije,-sirinaNazivaEmisije}|" +
                $"{pocetak + " - " + kraj,-sirinaStupca}|" + VratiOsobeUloge() + '\n' +
                new string('-', sirinaTablice) + '\n';

            return emisija;
        }

        public string VratiPodatkeEmisijeBezDana()
        {
            string pocetak = Konverzija.PretvoriSekundeUVrijeme(EmisijaPrograma.Pocetak);
            string kraj = Konverzija.PretvoriSekundeUVrijeme(EmisijaPrograma.Pocetak + EmisijaPrograma.Emisija.Trajanje);

            string emisija = $"|{EmisijaPrograma.RedniBroj,sirinaStupca}" +
                $"|{"",-sirinaStupca}|{"",-sirinaStupca}|{EmisijaPrograma.Emisija.NazivEmisije,-sirinaNazivaEmisije}|" +
                $"{pocetak + " - " + kraj,-sirinaStupca}|" + VratiOsobeUloge() + '\n' +
                new string('-', sirinaTablice) + '\n';

            return emisija;
        }

        public string VratiKraj()
        {
            return new string('-', sirinaTablice);
        }


    }
}
