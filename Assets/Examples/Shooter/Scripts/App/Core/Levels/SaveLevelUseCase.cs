using UnityEngine;

namespace ShooterGame.App
{
    public static class SaveLevelUseCase
    {
        private const string PrefsKey = "CurrentLevel";

        public static int LoadLevel()
        {
            int level = PlayerPrefs.GetInt(PrefsKey, 0);
            Debug.Log($"Level Loaded: {level}");
            return level;
        }

        public static void SaveLevel(int level)
        {
            PlayerPrefs.SetInt(PrefsKey, level);
            Debug.Log($"Level Saved: {level}");
        }
    }
}