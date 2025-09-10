using UnityEditor;

namespace ShooterGame.App
{
    public static class QuitUseCase
    {
        public static void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit(0);
#endif
        }
    }
}