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

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
    /// <summary>
    ///     Allows the selection of a single option from an array of possible options
    /// </summary>
    public class SampleNavInputHelper : MonoBehaviour
    {
        public Toggle previous;
        public Toggle next;

        Toggle selectionToggle;
        bool seelctPrevious = false;
        bool selectNext = false;

        public void Awake()
        {
            selectionToggle = GetComponent<Toggle>();
        }

        public void Update()
        {
            if (selectionToggle != null && selectionToggle.isOn)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    seelctPrevious = true;
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    selectNext = true;
                }
            }
        }

        public void LateUpdate()
        {
            if (seelctPrevious)
            {
                SelectPrevious();
            }
            else if (selectNext)
            {
                SelectNext();
            }

            seelctPrevious = false;
            selectNext = false;
        }

        private void SelectPrevious()
        {
            if (previous != null )
            {
                previous.isOn = true;
            } 
        }

        private void SelectNext()
        {
            if (next != null )
            {
                next.isOn = true;
            } 
        }
    }
}