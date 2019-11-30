using lljubici1_zadaca_2.Builder;
using lljubici1_zadaca_2.Composite;
using lljubici1_zadaca_2.FactoryMethod;
using lljubici1_zadaca_2.Podaci;
using lljubici1_zadaca_2.Pomagala;
using lljubici1_zadaca_2.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lljubici1_zadaca_2.Raspored
{
    public class RasporedEmisija
    {
        public static void KreirajRasporedPoDanima(List<Program> listaPrograma, PodaciCreator programEmisija,
            List<Emisija> listaEmisija, IBuilderProgram programBuilder, List<Osoba> listaOsoba, List<Uloga> listaUloga, List<VrstaEmisije> listaVrsteEmisije)
        {
            DodajSveEmisijeIzDatotekeZaProgram(listaPrograma, programEmisija, listaEmisija, listaOsoba, listaUloga, listaVrsteEmisije);
            foreach (var program in listaPrograma)
            {
                IzbaciEmisijeKojeSuIzvanProgramskogVremena(program);
                DodajDaneSEmisijamaProgramu(programBuilder, program);
                SingletonTvKuca.Instanca.DodajElementRasporeda(program);
            }
        }

        private static void DodajDaneSEmisijamaProgramu(IBuilderProgram programBuilder, Program program)
        {
            for (int i = 1; i <= 7; i++)
            {
                Dan dan = new Dan(Enum.GetName(typeof(Enumeracije.Dani), i));
                List<EmisijePrograma> listaProgramaOdredenogDana = new List<EmisijePrograma>();

                List<EmisijePrograma> emisijeSPocetkomIDanom = program.EmisijePrograma
                    .Where(e => e.DaniUTjednu.Contains(i) && e.ImaPočetak).OrderBy(e => e.Pocetak).ToList();
                listaProgramaOdredenogDana =
                    programBuilder.DodajEmisijeSaDanimaIPocetkom(program, emisijeSPocetkomIDanom, listaProgramaOdredenogDana);

                List<EmisijePrograma> emisijeSDanomBezPocetka = program.EmisijePrograma
                    .Where(e => e.DaniUTjednu.Contains(i) && !e.ImaPočetak).OrderBy(e => e.Pocetak).ToList();
                listaProgramaOdredenogDana =
                    programBuilder.DodajEmisijeSaDanimaBezPocetka(program, emisijeSDanomBezPocetka, listaProgramaOdredenogDana);

                List<EmisijePrograma> emisijeBezDanaIBezPocetka = program.EmisijePrograma
                    .Where(e => e.DaniUTjednu.Count == 0 && !e.ImaPočetak).ToList();
                listaProgramaOdredenogDana =
                    programBuilder.DodajEmisijeBezDanaIPocetka(program, emisijeBezDanaIBezPocetka, listaProgramaOdredenogDana);

                foreach (var emisijaPrograma in listaProgramaOdredenogDana)
                {
                    dan.DodajElementRasporeda(emisijaPrograma);
                }
                program.DodajElementRasporeda(dan);
            }
        }

        private static void IzbaciEmisijeKojeSuIzvanProgramskogVremena(Program program)
        {
            var emisijeIzvanVremena = program.EmisijePrograma.Where(e =>
                (e.ImaPočetak && e.Pocetak < program.Pocetak) ||
                (e.ImaPočetak && ((e.Pocetak + e.Emisija.Trajanje) > program.Kraj))).ToList();
            emisijeIzvanVremena.ForEach(i => Console.WriteLine("Ne mogu dodati>> " + i.ToString() + " program tada ne radi!"));
            //2.0 izbaci one koje nisu u rangeu programa
            program.EmisijePrograma.RemoveAll(l => emisijeIzvanVremena.Contains(l));
        }

        private static void DodajSveEmisijeIzDatotekeZaProgram(List<Program> listaPrograma, PodaciCreator programEmisija,
            List<Emisija> listaEmisija, List<Osoba> listaOsoba, List<Uloga> listaUloga, List<VrstaEmisije> listaVrsteEmisija)
        {
            foreach (var p in listaPrograma)
            {
                //p.EmisijePrograma = VratiEmisijePrograma(programEmisija, p, listaEmisija, programBuilder);
                //SingletonTvKuca.Instanca.DodajProgram(p);
                PromjeniPutanjuDatotekePrograma(programEmisija, p);
                PopuniEmisijeProgramaPodacimaEmisije(p, listaEmisija);
                PopuniEmisijeProgramaPodacimaOsobaUloga(p, listaOsoba, listaUloga);
                PopuniEmisijeProgramaPodacimaVrsteEmisije(p, listaVrsteEmisija);
            }
        }

        private static void PopuniEmisijeProgramaPodacimaVrsteEmisije(Program program, List<VrstaEmisije> listaVrsteEmisija)
        {
            foreach (var emisijaPrograma in program.EmisijePrograma)
            {

                //try
                //{
                int idVrstaEmisije = emisijaPrograma.Emisija.VrstaEmisije.Id;
                var vrstaEmisije = listaVrsteEmisija.FirstOrDefault(ve => ve.Id == idVrstaEmisije);
                emisijaPrograma.Emisija.VrstaEmisije = vrstaEmisije;
                //}
                //catch { }
            }
        }

        private static void PopuniEmisijeProgramaPodacimaOsobaUloga(Program program, List<Osoba> listaOsoba, List<Uloga> listaUloga)
        {
            foreach (var emisijaPrograma in program.EmisijePrograma)
            {
                List<OsobaUloga> osobeUlogePrograma = emisijaPrograma.OsobeUloge;
                PopuniOsobuUloguEmisiji(listaOsoba, listaUloga, osobeUlogePrograma);
                List<OsobaUloga> osobeUlogeEmisije = emisijaPrograma.Emisija.OsobeUloge;
                PopuniOsobuUloguEmisiji(listaOsoba, listaUloga, osobeUlogeEmisije);
            }
        }

        private static void PopuniOsobuUloguEmisiji(List<Osoba> listaOsoba, List<Uloga> listaUloga, List<OsobaUloga> osobeUlogePrograma)
        {
            foreach (var osobaUloga in osobeUlogePrograma)
            {
                var osoba = listaOsoba.FirstOrDefault(o => o.Id == osobaUloga.Osoba.Id);
                var uloga = listaUloga.FirstOrDefault(u => u.Id == osobaUloga.Uloga.Id);

                osobaUloga.Osoba = osoba;
                osobaUloga.Uloga = uloga;
            }
        }

        private static void PopuniEmisijeProgramaPodacimaEmisije(Program p, List<Emisija> listaEmisija)
        {
            foreach (var emisijaPrograma in p.EmisijePrograma)
            {
                var podaciEmisije = listaEmisija.Single(s => s.Id == emisijaPrograma.Emisija.Id);
                emisijaPrograma.Emisija.NazivEmisije = podaciEmisije.NazivEmisije;
                emisijaPrograma.Emisija.OsobeUloge = podaciEmisije.OsobeUloge;
                emisijaPrograma.Emisija.Trajanje = podaciEmisije.Trajanje;
                emisijaPrograma.Emisija.VrstaEmisije = podaciEmisije.VrstaEmisije;
            }
        }

        private static void PromjeniPutanjuDatotekePrograma(PodaciCreator programEmisija, Program p)
        {
            //@"C:\Users\root\Desktop\DZ_1\lljubici1_zadaca_1\podaci\" + 
            ((EmisijeProgramaConcreateCreator)programEmisija).PromjeniPutanjuZaProgram(
                p.NazivDatoteke);
            p.EmisijePrograma = programEmisija.entiteti.Cast<EmisijePrograma>().ToList();
        }


        //private static List<EmisijePrograma> VratiEmisijePrograma(PodaciCreator programEmisija, Program p, List<Emisija> listaEmisija, IBuilderProgram programBuilder)
        //{
        //    List<EmisijePrograma> emisijeZaDodati = new List<EmisijePrograma>();
        //    PromjeniPutanjuDatotekePrograma(programEmisija, p);
        //    PopuniEmisijeProgramaPodacimaEmisije(p, listaEmisija);
        //    //SortirajEmisijaProgramaPoDanimaIPocetku(p);
        //    //var emisijeKojeImajuDaneUTjednuIPocetak = DohvatiEmisijeSDanimaIPocektom(p);
        //    ////3. DODAVANJE ONIH KOJE SE NE PREKLAPAJU, I U VREMENU PROGRAMA SU, IMAJU TRAJANJE I DAN I IMAJU POCETAK
        //    //emisijeZaDodati = programBuilder.DodajEmisijeSaDanimaIPocetkom(p, emisijeKojeImajuDaneUTjednuIPocetak, emisijeZaDodati);
        //    //emisijeZaDodati = emisijeZaDodati.OrderByDescending(a => a.DaniUTjednu.Count).ThenBy(c => c.Emisija.Id)
        //    //    .ThenByDescending(b => b.Pocetak).ToList();

        //    ////4. DODAVANJE EMISIJE BEZ POCETKA ILI DANA, ALI BEZ PREKLAPANJA
        //    //emisijeZaDodati.Sort((e1, e2) => e1.Pocetak.CompareTo(e2.Pocetak));

        //    //List<EmisijePrograma> emisijeSaDanimaBezPocetka = p.EmisijePrograma.Where(a => a.ImaPočetak == false && a.DaniUTjednu.Count != 0).ToList();
        //    //emisijeZaDodati = programBuilder.DodajEmisijeSaDanimaBezPocetka(p, emisijeSaDanimaBezPocetka, emisijeZaDodati);
        //    //emisijeZaDodati.Sort((e1, e2) => e1.Pocetak.CompareTo(e2.Pocetak));

        //    //List<EmisijePrograma> emisijeBezDanaIPocetka = p.EmisijePrograma.Where(a => a.ImaPočetak == false && a.DaniUTjednu.Count == 0).ToList();
        //    //emisijeZaDodati = programBuilder.DodajEmisijeBezDanaIPocetka(p, emisijeBezDanaIPocetka, emisijeZaDodati);
        //    //emisijeZaDodati.Sort((e1, e2) => e1.Pocetak.CompareTo(e2.Pocetak));
        //    //return emisijeZaDodati;
        //    return null;
        //}

        //private static List<EmisijePrograma> DohvatiEmisijeSDanimaIPocektom(Program p)
        //{
        //    List<EmisijePrograma> emisijeKojeImajuDaneUTjednuIPocetak =
        //        p.EmisijePrograma.Where(e => e.DaniUTjednu.Any() && e.ImaPočetak).ToList();
        //    return emisijeKojeImajuDaneUTjednuIPocetak;
        //}

        //private static void SortirajEmisijaProgramaPoDanimaIPocetku(Program p)
        //{
        //    p.EmisijePrograma = p.EmisijePrograma.OrderByDescending(a => a.DaniUTjednu.Count).ThenByDescending(b => b.Pocetak)
        //        .ThenBy(c => c.Emisija.Id).ToList();
        //}
    }
}