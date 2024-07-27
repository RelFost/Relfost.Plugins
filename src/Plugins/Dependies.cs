using System.Collections.Generic;
using Oxide.Core;
using System.Threading.Tasks;

namespace Relfost.Plugins
{
    public class Dependies
    {
        private Dictionary<string, bool> _dependies = new Dictionary<string, bool>();
        private string _loaded_hook_name = "OnAllDependiesLoaded";
        public Dependies(List<string> dependies, string loaded_hook_name = null)
        {
            if (loaded_hook_name != null) _loaded_hook_name = loaded_hook_name;
            Initialize(dependies);
        }

        private async void Initialize(List<string> dependies)
        {
            await Task.Delay(500); 
            FillDependies(dependies);
        }
        private void FillDependies(List<string> dependies)
        {
            foreach (string dependence in dependies)
            {
                _dependies.Add(dependence, false);
            }
            CheckDependiesLoaded();
        }
        private async void CheckDependiesLoaded()
        {
            while (!AllDependiesLoaded())
            {
                var keys = new List<string>(_dependies.Keys);
                foreach (var key in keys)
                {
                    if (_dependies[key]) continue;

                    var plugin = Interface.Oxide.RootPluginManager.GetPlugin(key);
                    if (plugin != null && plugin.IsLoaded)
                    {
                        _dependies[key] = true;
                    }
                }

                await Task.Delay(500);
            }

            CallAllDependiesLoadedHook();
        }
        private bool AllDependiesLoaded()
        {
            if (_dependies.Count <= 0) return true;
            foreach (KeyValuePair<string, bool> dependence in _dependies)
                if (dependence.Value == false) return false;
            return true;
        }
        private void CallAllDependiesLoadedHook() => Interface.CallHook(_loaded_hook_name);
    }
}