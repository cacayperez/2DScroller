using System.Collections;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace SCS.Mio
{
    public class LocalizationManager : SingletonBehaviour <LocalizationManager>
    {
        public static string GetLocalizedString(StringTable table, string entryName)
        {

            if (string.IsNullOrEmpty(entryName))
            {
                return Constants.MissingString;
            }
            else
            {
                var entry = table.GetEntry(entryName);
                return entry.GetLocalizedString();
            }
        }

        protected override void OnInitialize()
        {
            
        }
    }

}