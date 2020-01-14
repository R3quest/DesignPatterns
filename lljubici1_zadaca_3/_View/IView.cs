using lljubici1_zadaca_3._Model.Composite;
using lljubici1_zadaca_3._Model.Podaci;
using System.Collections.Generic;

namespace lljubici1_zadaca_3._View
{
    public interface IView
    {
        void _IspisVremenskogPlana();
        void _UnesiProgram();
        void _UnesiDanUTjednu();
        void _UnesiOsobu();
        void _OsobaNemaNiJednuUlogu();
        void _UnesiNovuUloguZaZamjenuPostojece();
        void _NePostojiEmisijaSJednoznacnimBrojem();
        void _UnesiZeljenuBoju();
        void _UnesiZeljenoStanje();

        void _NemaSpremljenihStanja();

        void _ObrisiEmisijuRasporeda();
        void _IspisPrihoda();
        void _UnesiBrojVrsteEmisije();
        void _UnesiPostojecuUloguOsobe();
        void IspisGlavniIzbornik();

        void Ispisi(string ispis);
        void IspisiOsobe(List<Osoba> listaOsoba);
        void IspisiUloge(List<Uloga> uloge);

        void IspisiVrsteEmisija(List<VrstaEmisije> vrsteEmisije);

        void IspisiProgrameTvKuce(List<IRasporedProgramaComponent> RasporedPrograma);

    }
}
