using System.Collections.Generic;
using lljubici1_zadaca_3._Model.Composite;
using lljubici1_zadaca_3._Model.Podaci;

namespace lljubici1_zadaca_3._Model.Builder
{
    public interface IBuilderProgram
    {
        List<EmisijePrograma> DodajEmisijeSaDanimaIPocetkom(Program p,
            List<EmisijePrograma> emisijeKojeImajuDaneUTjednuIPocetak, List<EmisijePrograma> emisijeZaDodati);

        List<EmisijePrograma> DodajEmisijeSaDanimaBezPocetka(Program p, List<EmisijePrograma> emisijeSaDanimaBezPocetka,
            List<EmisijePrograma> emisijeZaDodati);

        List<EmisijePrograma> DodajEmisijeBezDanaIPocetka(Program p, List<EmisijePrograma> emisijeBezDanaIPocetka,
            List<EmisijePrograma> emisijeZaDodati);
    }
}
