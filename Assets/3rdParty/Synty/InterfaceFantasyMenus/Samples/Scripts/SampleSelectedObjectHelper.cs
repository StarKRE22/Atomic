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
using System.Collections.Generic;

namespace Synty.Interface.FantasyMenus.Samples
{
    /// <summary>
    ///     Sample script that helps the application naivgate with the keyboard and controller
    /// </summary>

    public class SampleSelectedObjectHelper : MonoBehaviour
    {
        [Header("References")]
        public StandaloneInputModule inputModule;
        List<Selectable> selectedObjectHistory = new List<Selectable>();
        int maxHistorySize = 1000;

        private Selectable GetLastSelectedObject()
        {
            for (int i = selectedObjectHistory.Count-1; i >= 0; --i)
            {
                if(selectedObjectHistory[i] != null && selectedObjectHistory[i].gameObject.activeInHierarchy)
                {
                    return selectedObjectHistory[i];
                }

                selectedObjectHistory.RemoveAt(i);
            }

            return null;
        }

        private void Update()
        {
            Selectable lastSelectedObject = GetLastSelectedObject();
            Selectable currentSelectedObject = EventSystem.current.currentSelectedGameObject != null ? EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>() : null;

            if (currentSelectedObject != null && currentSelectedObject.gameObject.activeInHierarchy && lastSelectedObject != EventSystem.current.currentSelectedGameObject)
            {
                if (selectedObjectHistory.Contains(currentSelectedObject))
                {
                    selectedObjectHistory.Remove(currentSelectedObject);
                }
                selectedObjectHistory.Add(currentSelectedObject);

                if ( selectedObjectHistory.Count > maxHistorySize )
                {
                    selectedObjectHistory.RemoveAt(0);
                }
            }
            else if (lastSelectedObject != null)
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    lastSelectedObject.Select();
                    return;
                }
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    lastSelectedObject.Select();
                    return;
                }
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    lastSelectedObject.Select();
                    return;
                }
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    lastSelectedObject.Select();
                    return;
                }

            }
        }
    }
}