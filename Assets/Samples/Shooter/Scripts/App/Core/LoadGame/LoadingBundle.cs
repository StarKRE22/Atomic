using System.Collections.Generic;

namespace ShooterGame.App
{
    public class LoadingBundle : Dictionary<string, object>
    {
        public T Get<T>(string key) => (T) this[key];
    }
}