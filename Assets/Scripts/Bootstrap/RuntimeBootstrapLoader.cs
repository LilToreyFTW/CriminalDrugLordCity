using UnityEngine;

namespace CriminalDrugLordCity.Bootstrap
{
    public static class RuntimeBootstrapLoader
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void EnsureBootstrapExists()
        {
            GameBootstrap existing = Object.FindFirstObjectByType<GameBootstrap>();
            if (existing != null)
            {
                return;
            }

            GameObject bootstrap = new GameObject("GameBootstrap");
            bootstrap.AddComponent<GameBootstrap>();
        }
    }
}
