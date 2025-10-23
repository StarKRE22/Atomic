using UnityEngine;

namespace ShooterGame.App
{
    public static partial class LevelsUseCase
    {
        private const string PrefsKey = "CurrentLevel";

        public static bool LoadLevel(out int level)
        {
            if (PlayerPrefs.HasKey(PrefsKey))
            {
                level = PlayerPrefs.GetInt(PrefsKey, 0);
                Debug.Log($"Level Loaded: {level}");
                return true;
            }

            level = -1;
            return false;
        }

        public static void SaveLevel(int level)
        {
            PlayerPrefs.SetInt(PrefsKey, level);
            Debug.Log($"Level Saved: {level}");
        }
    }
}