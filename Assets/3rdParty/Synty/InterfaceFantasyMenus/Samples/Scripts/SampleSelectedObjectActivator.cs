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
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
    /// <summary>
    ///     Toggles referenced game object on and off an object based on the toggle's value
    /// </summary>
    public class SampleSelectedObjectActivator : MonoBehaviour
    {
         [Header("References")]
        public Selectable selectable;

        public GameObject isOnObject;

        private void Start()
        {
            SetActiveObjects();
        }
        
        private void LateUpdate()
        {
            SetActiveObjects();
        }

        private void SetActiveObjects()
        {
            if (isOnObject != null) 
            {
                if (!isOnObject.activeSelf && selectable != null && EventSystem.current.currentSelectedGameObject == selectable.gameObject)
                {
                    isOnObject.SetActive(true);
                }
                if (isOnObject.activeSelf && selectable != null && EventSystem.current.currentSelectedGameObject != selectable.gameObject)
                {
                    isOnObject.SetActive(false);
                }
            }
        }
    }
}