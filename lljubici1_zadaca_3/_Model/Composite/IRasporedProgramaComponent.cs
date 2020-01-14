using System.Collections.Generic;

namespace lljubici1_zadaca_3._Model.Composite
{
    public interface IRasporedProgramaComponent
    {
        void DodajElementRasporeda(IRasporedProgramaComponent elementComposite);
        List<IRasporedProgramaComponent> VratiRasporedEmisija();

        List<IRasporedProgramaComponent> VratiRaspored();
    }
}
