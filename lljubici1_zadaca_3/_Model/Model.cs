using lljubici1_zadaca_3._Model.Builder;
using lljubici1_zadaca_3._Model.ChainOfResponsibility;
using lljubici1_zadaca_3._Model.Composite;
using lljubici1_zadaca_3._Model.Decorator;
using lljubici1_zadaca_3._Model.FactoryMethod;
using lljubici1_zadaca_3._Model.Iterator;
using lljubici1_zadaca_3._Model.Memento;
using lljubici1_zadaca_3._Model.Podaci;
using lljubici1_zadaca_3._Model.Pomagala;
using lljubici1_zadaca_3._Model.Singleton;
using lljubici1_zadaca_3._Model.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lljubici1_zadaca_3._Model
{
    public class Model
    {
        private List<VrstaEmisije> vrsteEmisija;
        private List<Osoba> osobe;
        private List<Uloga> uloge;
        public Originator originator { get; }
        public Caretaker caretaker { get; }

        public Model(string[] args)
        {
            Init(args);
            originator = new Originator(DohvatiProgrameTvKuce());
            caretaker = new Caretaker(originator);
        }

        private void Init(string[] args)
        {
            if (!UcitavanjeParametara.ProvjeriUlazneArgumente(args, new[] { "-t", "-e", "-o", "-u", "-v" }, 10))
            {
                Console.ReadLine();
                return;
            }
            Dictionary<string, string> datoteke = UcitavanjeParametara.DohvatiPutanjeDatoteka(args);
            PodaciCreator(datoteke, out var program, out var emisije, out var osobe, out var uloge, out var programEmisija, out var vrstaEmisije);

            IBuilderProgram programBuilder = new RasporedConcreateCreator();

            var listaEmisija = emisije.entiteti.Cast<Emisija>().ToList();
            var listaPrograma = program.entiteti.Cast<Program>().ToList();
            this.osobe = osobe.entiteti.Cast<Osoba>().ToList();
            this.uloge = uloge.entiteti.Cast<Uloga>().ToList();
            this.vrsteEmisija = vrstaEmisije.entiteti.Cast<VrstaEmisije>().ToList();

            KreirajRasporedPoDanima(listaPrograma, programEmisija, listaEmisija, programBuilder,
                this.osobe, this.uloge, this.vrsteEmisija);
        }

        private static void PodaciCreator(Dictionary<string, string> datoteke, out PodaciCreator program, out PodaciCreator emisije,
            out PodaciCreator osobe, out PodaciCreator uloge, out PodaciCreator programEmisija, out PodaciCreator vrstaEmisije)
        {
            program = new ProgramiConcreateCreator(datoteke["-t"]);
            emisije = new EmisijeConcreteCreator(datoteke["-e"]);
            osobe = new OsobeConcreateCreator(datoteke["-o"]);
            uloge = new UlogeConcreateCreator(datoteke["-u"]);
            programEmisija = new EmisijeProgramaConcreateCreator("");
            vrstaEmisije = new VrstaEmisijeConcreateCreator(datoteke["-v"]);
        }

        public static void KreirajRasporedPoDanima(List<Program> listaPrograma, PodaciCreator programEmisija,
           List<Emisija> listaEmisija, IBuilderProgram programBuilder, List<Osoba> listaOsoba, List<Uloga> listaUloga, List<VrstaEmisije> listaVrsteEmisije)
        {
            int redniBroj = 1;
            DodajSveEmisijeIzDatotekeZaProgram(listaPrograma, programEmisija, listaEmisija, listaOsoba, listaUloga, listaVrsteEmisije);
            foreach (var program in listaPrograma)
            {
                IzbaciEmisijeKojeSuIzvanProgramskogVremena(program);
                DodajDaneSEmisijamaProgramu(programBuilder, program, ref redniBroj);
                SingletonTvKuca.Instanca.DodajElementRasporeda(program);
            }
        }

        private static void DodajDaneSEmisijamaProgramu(IBuilderProgram programBuilder, Program program, ref int redniBroj)
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
                    emisijaPrograma.RedniBroj = redniBroj;
                    redniBroj++;
                    emisijaPrograma.DodajElementRasporeda(emisijaPrograma);
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
                List<Osoba> osobeUlogePrograma = emisijaPrograma.OsobeUloge;
                PopuniOsobuUloguEmisiji(listaOsoba, listaUloga, osobeUlogePrograma);
                //TODO: IZMJENI
                //List<OsobaUloga> osobeUlogeEmisije = emisijaPrograma.Emisija.OsobeUloge;
                //PopuniOsobuUloguEmisiji(listaOsoba, listaUloga, osobeUlogeEmisije);
            }
        }

        private static void PopuniOsobuUloguEmisiji(List<Osoba> listaOsoba, List<Uloga> listaUloga, List<Osoba> osobeUlogePrograma)
        {
            foreach (var osobaUloga in osobeUlogePrograma)
            {
                var osoba = listaOsoba.FirstOrDefault(o => o.Id == osobaUloga.Id);

                for (int i = 0; i < osobaUloga.Uloge.Count; i++)
                {
                    var _uloga = listaUloga.FirstOrDefault(u => osoba != null && u.Id == osobaUloga.Uloge[i].Id);
                    //if (osoba != null) osoba.Uloge[i] = _uloga;
                    osobaUloga.Uloge[i] = _uloga;
                }

                osobaUloga.ImeIPrezime = osoba?.ImeIPrezime;
                //osobaUloga.Uloge = osoba.Uloge;
                //foreach (var __osoba in osobeUlogePrograma)
                //{
                //    foreach (var __uloga in __osoba.Uloge)
                //    {
                //        var uloga = listaUloga.FirstOrDefault(u => u.Id == __uloga.Id);
                //        if (osoba != null) osoba.Uloge.Add(uloga);
                //    }
                //}

                //osobaUloga.ImeIPrezime = osoba.ImeIPrezime;
                //osobaUloga.Uloge = osoba.Uloge;
            }
        }

        private static void PopuniEmisijeProgramaPodacimaEmisije(Program p, List<Emisija> listaEmisija)
        {
            foreach (var emisijaPrograma in p.EmisijePrograma)
            {
                var podaciEmisije = listaEmisija.Single(s => s.Id == emisijaPrograma.Emisija.Id);
                emisijaPrograma.Emisija.NazivEmisije = podaciEmisije.NazivEmisije;
                emisijaPrograma.Emisija.OsobeUloge = podaciEmisije.OsobeUloge;

                emisijaPrograma.OsobeUloge.AddRange(podaciEmisije.OsobeUloge);

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




        public List<IRasporedProgramaComponent> DohvatiProgrameTvKuce()
        {
            return SingletonTvKuca.Instanca.GetRasporedPrograma();
        }

        public int VratiBrojPrograma()
        {
            return SingletonTvKuca.Instanca.GetRasporedPrograma().Count;
        }

        public List<Uloga> VratiUlogePojedineOsobe(int osobaId)
        {
            List<Uloga> ulogeOsobe = new List<Uloga>();
            var iterator = new ConcreateIteratorEmisijaTjednogPlana(DohvatiProgrameTvKuce());
            while (!iterator.Gotovo)
            {
                EmisijePrograma emisijaPrograma = (EmisijePrograma)iterator.Trenutni;
                Osoba osoba = emisijaPrograma.OsobeUloge.Find(ou => ou.Id == osobaId);
                if (osoba != null)
                {
                    List<Uloga> listaUlogaOsobe = osoba.Uloge;
                    ulogeOsobe.AddRange(listaUlogaOsobe);

                }
                iterator.Sljedeci();
            }

            return ulogeOsobe.Distinct().ToList();
        }

        public List<Uloga> VratiUloge()
        {
            return uloge;
        }

        public string IspisiPrihodeOdReklama(int program, int dan, List<IRasporedProgramaComponent> RasporedPrograma)
        {
            KalkulirajPrihodVisitor kalkulirajVisitor = new KalkulirajPrihodVisitor();
            //int prihod = 0;
            var _program = (Program)RasporedPrograma[program - 1];
            var _dan = (Dan)_program.RasporedDani[dan - 1];
            List<IComponent> sveKomponente = new List<IComponent>();
            ConcreateComponentPrihodiReklama komponenta = new ConcreateComponentPrihodiReklama(null, null, null);
            sveKomponente.Add(komponenta);
            for (int i = 0; i < _dan.RasporedEmisijaDana.Count; i++)
            {
                EmisijePrograma emisijaPrograma = (EmisijePrograma)_dan.RasporedEmisijaDana[i];

                kalkulirajVisitor.Visit(emisijaPrograma.Emisija.VrstaEmisije);
                if (i == 0)
                {
                    komponenta = new ConcreateComponentPrihodiReklama(emisijaPrograma, _program.NazivPrograma, _dan.NazivDana);
                    sveKomponente.Add(komponenta);
                    continue;
                }
                komponenta = new ConcreateComponentPrihodiReklama(emisijaPrograma, null, null);
                sveKomponente.Add(komponenta);
            }
            komponenta = new ConcreateComponentPrihodiReklama(null, _program.NazivPrograma, null, kalkulirajVisitor.UkupanPrihod);
            sveKomponente.Add(komponenta);
            Decorator.Decorator dekorator = new Decorator.Decorator(sveKomponente);
            return dekorator.Operacija();
        }

        public List<Osoba> VratiOsobe()
        {
            return osobe;
        }

        public List<VrstaEmisije> VratiVrsteEmisija()
        {
            return vrsteEmisija;
        }

        public int VratiBrojVrstaEmisija()
        {
            return vrsteEmisija.Count;
        }

        public string IspisiTjedniPlanVrsteEmisija(string vrstaEmisije)
        {
            ConcreateIteratorEmisijaZeljeneVrste iterator = KreirajIterator(vrstaEmisije) as ConcreateIteratorEmisijaZeljeneVrste;
            List<IComponent> sveKomponente = new List<IComponent>();
            ConcreateComponentProgramDanEmisija emisijeVrste = new ConcreateComponentProgramDanEmisija(null, null, null);
            sveKomponente.Add(emisijeVrste);
            while (!iterator.Gotovo)
            {
                var emisijaPrograma = ((EmisijePrograma)iterator.Trenutni);
                if (iterator.NoviProgram)
                {
                    emisijeVrste = new ConcreateComponentProgramDanEmisija(emisijaPrograma, iterator.TrenutniProgram(), iterator.TrenutniDan());
                }
                else if (iterator.NoviDan)
                {
                    emisijeVrste = new ConcreateComponentProgramDanEmisija(emisijaPrograma, null, iterator.TrenutniDan());
                }
                else
                {
                    emisijeVrste = new ConcreateComponentProgramDanEmisija(emisijaPrograma, null, null);
                }
                sveKomponente.Add(emisijeVrste);
                iterator.Sljedeci();
            }
            Decorator.Decorator dekorator = new Decorator.Decorator(sveKomponente);
            return dekorator.Operacija();
        }

        public IIterator KreirajIterator(string vrstaEmisije)
        {
            return new ConcreateIteratorEmisijaZeljeneVrste(DohvatiProgrameTvKuce(), vrstaEmisije);
        }

        public void ZamjenaPostojeceUlogeNovom(List<Uloga> listaUloga, int ulogaPostojece, int ulogaZeljene, int idOsobe)
        {
            try
            {
                string opisStare = listaUloga.FirstOrDefault(o => o.Id == ulogaPostojece).Opis;
                Uloga staraUloga = new Uloga(ulogaPostojece, opisStare);
                //nova
                string opisNove = this.uloge.FirstOrDefault(o => o.Id == ulogaZeljene).Opis;
                Uloga novaUloga = new Uloga(ulogaZeljene, opisNove);

                List<Osoba> listaOsoba = new List<Osoba>();
                listaOsoba = VratiOsobe(idOsobe);

                foreach (var osoba in listaOsoba)
                {
                    osoba.PostaviStanje(staraUloga, novaUloga);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private List<Osoba> VratiOsobe(int osobaId)
        {
            List<Osoba> osobe = new List<Osoba>();
            var iterator = new ConcreateIteratorEmisijaTjednogPlana(DohvatiProgrameTvKuce());
            while (!iterator.Gotovo)
            {
                EmisijePrograma emisijaPrograma = (EmisijePrograma)iterator.Trenutni;
                Osoba _osoba = emisijaPrograma.OsobeUloge.Find(ou => ou.Id == osobaId);
                if (_osoba != null)
                {
                    osobe.Add(_osoba);
                }
                iterator.Sljedeci();
            }
            return osobe;
        }

        public bool OdabirEmisijeZaBrisanjeProvjera(ref int idEmisijeZaBrisanje)
        {
            List<EmisijePrograma> listaEmisijaPrograma = VratiRasporedEmisija().Cast<EmisijePrograma>().ToList();

            idEmisijeZaBrisanje = int.TryParse(Console.ReadLine(), out idEmisijeZaBrisanje) ? idEmisijeZaBrisanje : -1;
            int idEmisije = idEmisijeZaBrisanje;
            if (listaEmisijaPrograma.Exists(x => x.RedniBroj == idEmisije))
            {
                return true;
            }
            return false;
        }

        public List<IRasporedProgramaComponent> VratiRasporedEmisija()
        {
            List<IRasporedProgramaComponent> listaEmisijaPrograma = new List<IRasporedProgramaComponent>();
            foreach (Program program in DohvatiProgrameTvKuce())
            {
                listaEmisijaPrograma.AddRange(program.VratiRasporedEmisija());
            }

            return listaEmisijaPrograma;
        }

        public void SpremiIObrisiStanje(int jednoznacniBroj)
        {
            caretaker.Backup();
            ObrisiEmisijuNaTemeljuJednoznacnogRednogBroja(jednoznacniBroj, originator);
        }

        private void ObrisiEmisijuNaTemeljuJednoznacnogRednogBroja(int obrisiID, Originator o)
        {
            o.ObrisiEmisiju(obrisiID);
        }

        public bool PromjenaBojeKonzoleDodatnaFunkcionalnost(string boja)
        {
            var plava = new BlueHandler();
            var zelena = new GreenHandler();
            var crvena = new RedHandler();

            plava.SetNext(zelena).SetNext(crvena);
            return plava.Handle(boja);
        }

        public string VratiRasporedZaDan(int program, int dan, List<IRasporedProgramaComponent> RasporedPrograma)
        {
            try
            {
                //((Program)RasporedPrograma[program - 1]).IspisZaDan(dan);
                var _program = (Program)RasporedPrograma[program - 1];
                var _dan = (Dan)_program.RasporedDani[dan - 1];
                List<IComponent> sveKomponente = new List<IComponent>();
                ConcreateComponentProgramDanEmisija komponenta = new ConcreateComponentProgramDanEmisija(null, null, null);
                sveKomponente.Add(komponenta);
                for (int i = 0; i < _dan.RasporedEmisijaDana.Count; i++)
                {
                    EmisijePrograma emisijaPrograma = (EmisijePrograma)_dan.RasporedEmisijaDana[i];
                    if (i == 0)
                    {
                        komponenta = new ConcreateComponentProgramDanEmisija(emisijaPrograma, _program.NazivPrograma, _dan.NazivDana);
                        sveKomponente.Add(komponenta);
                        continue;
                    }
                    komponenta = new ConcreateComponentProgramDanEmisija(emisijaPrograma, null, null);
                    sveKomponente.Add(komponenta);
                }
                _Model.Decorator.Decorator dekorator = new _Model.Decorator.Decorator(sveKomponente);
                return dekorator.Operacija();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}
