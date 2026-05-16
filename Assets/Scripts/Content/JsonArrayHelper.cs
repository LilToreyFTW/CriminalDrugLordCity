using System;

namespace CriminalDrugLordCity.Content
{
    public static class JsonArrayHelper
    {
        [Serializable]
        private sealed class Wrapper<T>
        {
            public T[] items = Array.Empty<T>();
        }

        public static T[] FromJson<T>(string json)
        {
            string wrapped = "{\"items\":" + json + "}";
            Wrapper<T> result = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(wrapped);
            return result != null ? result.items : Array.Empty<T>();
        }
    }
}
