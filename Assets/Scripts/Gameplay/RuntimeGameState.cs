using System;
using CriminalDrugLordCity.Content;

namespace CriminalDrugLordCity.Gameplay
{
    public static class RuntimeGameState
    {
        public static MapDefinition ActiveMap { get; set; }
        public static DlcDefinition[] SeasonalDlcs { get; set; } = Array.Empty<DlcDefinition>();
        public static PlayerRuntimeState PlayerState { get; set; }
    }
}
