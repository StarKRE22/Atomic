using ShooterGame.App;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace ShooterGame
{
    public static class GameLauncher
    {
        private const string BootstrapScenePath = "Assets/Examples/Shooter/Scenes/ShooterGame (Bootstrap).unity";

        [MenuItem("ShooterGame/Play")]
        public static void PlayLevel()
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                Scene activeScene = EditorSceneManager.GetActiveScene();

                EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(BootstrapScenePath);
                EditorApplication.isPlaying = true;

                LoadGameUseCase.StartGame(AppContext.Instance, int.Parse(activeScene.name));
            }
        }
    }
}