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
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Synty.Interface.FantasyMenus.Samples
{
    /// <summary>
    ///     Sample script to allow controller and keyboard input for a toggle group
    /// </summary>

    public class SampleSettingsToggle : MonoBehaviour
    {
         [Header("References")]
        public Selectable selectable;
        public GameObject toggleGroupParent;

        [Header("Parameters")]
        public bool loop = false;

        Toggle[] toggles;
        int defaultToggle = 0;

        private void Start()
        {
            toggles = toggleGroupParent.GetComponentsInChildren<Toggle>();
            for (int i = 0; i < toggles.Length; ++i)
            {
                if (toggles[i].isOn)
                {
                    defaultToggle = i;
                    break;
                }
            }
        }

        private void Update()
        {
            if (selectable != null && EventSystem.current.currentSelectedGameObject == selectable.gameObject)
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    PreviousToggle();
                }
                else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    NextToggle();
                }
            }
        }

        private void NextToggle()
        {
            if (toggles.Length == 0 )
            {
                return;
            }

            for (int i = 0; i < toggles.Length; ++i)
            {
                if (toggles[i].isOn)
                {
                    int next = i+1;
                    if (next >= toggles.Length)
                    {
                        if(!loop) return;
                        next = 0;
                    }
                    toggles[next].isOn = true;
                    return;
                }
            }
        }

        private void PreviousToggle()
        {
            if (toggles.Length == 0 )
            {
                return;
            }

            for (int i = 0; i < toggles.Length; ++i)
            {
                if (toggles[i].isOn)
                {
                    int next = i-1;
                    if(next < 0)
                    {
                        if(!loop) return;
                        next = toggles.Length - 1;
                    } 
                    toggles[next].isOn = true;
                    return;
                }
            }

            SelectDefaultToggle();
        }

        private void SelectDefaultToggle()
        {
            if (toggles.Length > 0)
            {
                return;
            }

            toggles[defaultToggle].isOn = true;
        }
    }
}