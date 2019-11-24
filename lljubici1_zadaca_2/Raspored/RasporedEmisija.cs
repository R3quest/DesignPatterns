using lljubici1_zadaca_2.Builder;
using lljubici1_zadaca_2.Composite;
using lljubici1_zadaca_2.FactoryMethod;
using lljubici1_zadaca_2.Podaci;
using lljubici1_zadaca_2.Pomagala;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lljubici1_zadaca_2.Raspored
{
    public class RasporedEmisija
    {
        public static void KreirajRasporedPoDanima(List<Program> listaPrograma, PodaciCreator programEmisija,
            List<Emisija> listaEmisija, IBuilderProgram programBuilder)
        {
            DodajSveEmisijeIzDatotekeZaProgram(listaPrograma, programEmisija, listaEmisija);
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
                Dan dan = new Dan(Enum.GetName(typeof(Dani), i));
                List<EmisijePrograma> listaProgramaDana = new List<EmisijePrograma>();

                List<EmisijePrograma> emisijeSPocetkomIDanom = program.EmisijePrograma
                    .Where(e => e.DaniUTjednu.Contains(i) && e.ImaPočetak).OrderBy(e => e.Pocetak).ToList();
                listaProgramaDana =
                    programBuilder.DodajEmisijeSaDanimaIPocetkom(program, emisijeSPocetkomIDanom,
                        listaProgramaDana);

                List<EmisijePrograma> emisijeSDanomBezPocetka = program.EmisijePrograma
                    .Where(e => e.DaniUTjednu.Contains(i) && !e.ImaPočetak).OrderBy(e => e.Pocetak).ToList();
                listaProgramaDana =
                    programBuilder.DodajEmisijeSaDanimaBezPocetka(program, emisijeSDanomBezPocetka,
                        listaProgramaDana);

                List<EmisijePrograma> emisijeBezDanaIBezPocetka = program.EmisijePrograma
                    .Where(e => e.DaniUTjednu.Count == 0 && !e.ImaPočetak).ToList();
                listaProgramaDana =
                    programBuilder.DodajEmisijeBezDanaIPocetka(program, emisijeBezDanaIBezPocetka, listaProgramaDana);

                foreach (var emisijaPrograma in listaProgramaDana)
                {
                    dan.DodajElementRasporeda(emisijaPrograma);
                }

                program.RasporedDani.Add(dan);
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
            List<Emisija> listaEmisija)
        {
            foreach (var p in listaPrograma)
            {
                //p.EmisijePrograma = VratiEmisijePrograma(programEmisija, p, listaEmisija, programBuilder);
                //SingletonTvKuca.Instanca.DodajProgram(p);
                PromjeniPutanjuDatotekePrograma(programEmisija, p);
                PopuniEmisijeProgramaPodacimaEmisije(p, listaEmisija);
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