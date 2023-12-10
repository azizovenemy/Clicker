using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    public class Utils
    {
        [MenuItem("Utils/Clear Player Prefs")]
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
