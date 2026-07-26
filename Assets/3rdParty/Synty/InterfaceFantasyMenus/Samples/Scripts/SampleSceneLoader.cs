// Copyright (c) 2024 Synty Studios Limited. All rights reserved.
//
// Use of this software is subject to the terms and conditions of the End User Licence Agreement (EULA) 
// of the store at which you purchased this asset. 
//
// Synty assets are available at:
// https://www.syntystore.com
// https://assetstore.unity.com/publishers/5217
// https://www.fab.com/sellers/Synty%20Studios
//
// Sample scripts are included only as examples and are not intended as production-ready.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace Synty.Interface.FantasyMenus.Samples
{
    /// <summary>
    ///     A simple scene loader that transitions between screens.
    /// </summary>
    public class SampleSceneLoader : MonoBehaviour
    {
        [Header("References")]
        public Animator animator;

        [Header("Parameters")]
        public bool showCursor;

        [SerializeField]
        private List<string> _sceneNames;

#if UNITY_EDITOR
        private void RefreshScenes()
        {
            MonoScript script = MonoScript.FromMonoBehaviour(this);
            string samplesPath = Path.GetDirectoryName(Path.GetDirectoryName(AssetDatabase.GetAssetPath(script)));
            string[] searchPaths = { Path.Combine(samplesPath, "Scenes") };
            _sceneNames = AssetDatabase.FindAssets($"t:scene", searchPaths)
                .Select(guid => Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath(guid)))
                .ToList();
            EditorUtility.SetDirty(this);
        }

        private void OnValidate()
        {
            RefreshScenes();
        }
#endif

        private void OnEnable()
        {
            if (animator)
            {
                animator.gameObject.SetActive(true);
                animator.SetBool("Active", false);
            }

            if (showCursor)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        public void QuitApplication()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void NextScene()
        {
            string currentScene = SceneManager.GetActiveScene().name;
            SwitchScene(_sceneNames[(_sceneNames.IndexOf(currentScene) + 1) % _sceneNames.Count]);
        }

        public void PreviousScene()
        {
            string currentScene = SceneManager.GetActiveScene().name;
            SwitchScene(_sceneNames[(_sceneNames.IndexOf(currentScene) - 1 + _sceneNames.Count) % _sceneNames.Count]);
        }

        public void SwitchScene(string sceneName)
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
            {
                return;
            }
#endif
            StartCoroutine(C_SwitchScene(sceneName));
        }

        private IEnumerator C_SwitchScene(string sceneName)
        {
            if (animator)
            {
                animator.gameObject.SetActive(true);
                animator.SetBool("Active", true);
                yield return new WaitForSeconds(0.5f);
            }

#if UNITY_EDITOR
            string path = sceneName;
            if (path.IndexOf('/') == -1)
            {
                string guid = AssetDatabase.FindAssets($"{sceneName} t:scene")[0];
                path = AssetDatabase.GUIDToAssetPath(guid);
            }

            yield return EditorSceneManager.LoadSceneAsyncInPlayMode(path, new LoadSceneParameters(LoadSceneMode.Single));
#else
            // make sure the scene is in the build settings for runtime version
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
#endif
        }
    }
}
