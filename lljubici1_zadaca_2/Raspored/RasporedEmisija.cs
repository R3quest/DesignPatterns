using lljubici1_zadaca_2.Builder;
using lljubici1_zadaca_2.Composite;
using lljubici1_zadaca_2.FactoryMethod;
using lljubici1_zadaca_2.Podaci;
using System.Collections.Generic;
using System.Linq;

namespace lljubici1_zadaca_2.Raspored
{
    public class RasporedEmisija
    {
        public static void DodajProgrameSDodanimRasporedomEmisija(List<Program> listaPrograma, PodaciCreator programEmisija,
            List<Emisija> listaEmisija, IBuilderProgram programBuilder)
        {
            foreach (var p in listaPrograma)
            {
                p.EmisijePrograma = VratiEmisijePrograma(programEmisija, p, listaEmisija, programBuilder);
                SingletonTvKuca.Instanca.DodajProgram(p);
            }
        }

        private static List<EmisijePrograma> VratiEmisijePrograma(PodaciCreator programEmisija, Program p, List<Emisija> listaEmisija, IBuilderProgram programBuilder)
        {
            List<EmisijePrograma> emisijeZaDodati = new List<EmisijePrograma>();
            PromjeniPutanjuDatotekePrograma(programEmisija, p);
            PopuniEmisijeProgramaPodacimaEmisije(p, listaEmisija);
            SortirajEmisijaProgramaPoDanimaIPocetku(p);
            var emisijeKojeImajuDaneUTjednuIPocetak = DohvatiEmisijeSDanimaIPocektom(p);
            //3. DODAVANJE ONIH KOJE SE NE PREKLAPAJU, I U VREMENU PROGRAMA SU, IMAJU TRAJANJE I DAN I IMAJU POCETAK
            emisijeZaDodati = programBuilder.DodajEmisijeSaDanimaIPocetkom(p, emisijeKojeImajuDaneUTjednuIPocetak, emisijeZaDodati);
            emisijeZaDodati = emisijeZaDodati.OrderByDescending(a => a.DaniUTjednu.Count).ThenBy(c => c.Emisija.Id)
                .ThenByDescending(b => b.Pocetak).ToList();

            //4. DODAVANJE EMISIJE BEZ POCETKA ILI DANA, ALI BEZ PREKLAPANJA
            emisijeZaDodati.Sort((e1, e2) => e1.Pocetak.CompareTo(e2.Pocetak));

            List<EmisijePrograma> emisijeSaDanimaBezPocetka = p.EmisijePrograma.Where(a => a.ImaPočetak == false && a.DaniUTjednu.Count != 0).ToList();
            emisijeZaDodati = programBuilder.DodajEmisijeSaDanimaBezPocetka(p, emisijeSaDanimaBezPocetka, emisijeZaDodati);
            emisijeZaDodati.Sort((e1, e2) => e1.Pocetak.CompareTo(e2.Pocetak));

            List<EmisijePrograma> emisijeBezDanaIPocetka = p.EmisijePrograma.Where(a => a.ImaPočetak == false && a.DaniUTjednu.Count == 0).ToList();
            emisijeZaDodati = programBuilder.DodajEmisijeBezDanaIPocetka(p, emisijeBezDanaIPocetka, emisijeZaDodati);
            emisijeZaDodati.Sort((e1, e2) => e1.Pocetak.CompareTo(e2.Pocetak));
            return emisijeZaDodati;
        }

        private static List<EmisijePrograma> DohvatiEmisijeSDanimaIPocektom(Program p)
        {
            List<EmisijePrograma> emisijeKojeImajuDaneUTjednuIPocetak =
                p.EmisijePrograma.Where(e => e.DaniUTjednu.Any() && e.ImaPočetak).ToList();
            return emisijeKojeImajuDaneUTjednuIPocetak;
        }

        private static void SortirajEmisijaProgramaPoDanimaIPocetku(Program p)
        {
            p.EmisijePrograma = p.EmisijePrograma.OrderByDescending(a => a.DaniUTjednu.Count).ThenByDescending(b => b.Pocetak)
                .ThenBy(c => c.Emisija.Id).ToList();
        }

        private static void PopuniEmisijeProgramaPodacimaEmisije(Program p, List<Emisija> listaEmisija)
        {
            foreach (var emisijaPrograma in p.EmisijePrograma)
            {
                var podaciEmisije = listaEmisija.Single(s => s.Id == emisijaPrograma.Emisija.Id);
                emisijaPrograma.Emisija.NazivEmisije = podaciEmisije.NazivEmisije;
                emisijaPrograma.Emisija.OsobeUloge = podaciEmisije.OsobeUloge;
                emisijaPrograma.Emisija.Trajanje = podaciEmisije.Trajanje;
            }
        }

        private static void PromjeniPutanjuDatotekePrograma(PodaciCreator programEmisija, Program p)
        {
            //@"C:\Users\root\Desktop\DZ_1\lljubici1_zadaca_1\podaci\" + 
            ((EmisijeProgramaConcreateCreator)programEmisija).PromjeniPutanjuZaProgram(
                p.NazivDatoteke);
            p.EmisijePrograma = programEmisija.entiteti.Cast<EmisijePrograma>().ToList();
        }
    }
}