using UnityEditor;

namespace ShooterGame.App
{
    public static class ExitAppUseCase
    {
        public static void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit(0);
#endif
        }
    }
}