using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace CriminalDrugLordCity.Game.Runtime;

internal static class GameBootstrap
{
    public static void Run()
    {
        var settings = GameWindowSettings.Default;
        settings.UpdateFrequency = 60;

        var native = new NativeWindowSettings
        {
            Title = "Criminal Drug Lord City",
            ClientSize = new Vector2i(1600, 900)
        };

        using var game = new CriminalDrugLordCityWindow(settings, native);
        game.Run();
    }
}
