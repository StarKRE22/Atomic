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

using UnityEngine;
using UnityEngine.Serialization;

namespace Synty.Interface.FantasyMenus.Samples
{
    /// <summary>
    ///     Toggles on and off game objects based on the array's value
    /// </summary>
    public class SampleSettingsArrayObject : MonoBehaviour
    {
        [Header("References")]
        public SampleSettingsArray arraySetting;
        [Tooltip("Populated from arraySettings")]
        public string[] options;
        [FormerlySerializedAs("gameObejects")] 
        public GameObject[] gameObjects;

        private void OnValidate()
        {
            if(arraySetting == null)
            {
                return;
            }
            options = arraySetting.options;
        }

        private void Awake()
        {
            if (arraySetting != null)
            {
                arraySetting.onValueChanged.AddListener(OnValueChanged);
                OnValueChanged(arraySetting.OptionIndex, arraySetting.Option);
            }
        }

        private void OnDestroy()
        {
            if (arraySetting != null)
            {
                arraySetting.onValueChanged.RemoveListener(OnValueChanged);
            }
        }

        private void OnValueChanged(int index, string option)
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (gameObjects[i] != null) gameObjects[i].SetActive(index == i);
            }
        }
    }
}