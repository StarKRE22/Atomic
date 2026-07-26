// Copyright (c) 2024 Synty Studios Limited. All rights reserved.
//
// Use of this software is subject to the terms and conditions of the End User Licence Agreement (EULA) 
// of the store at which you purchased this asset. 
//
// Synty assets are available at:
// http://www.syntystore.com
// http://www.unityassetstore.com
// http://www.fab.com
//
// Sample scripts are included only as examples and are not intended as production-ready.

using UnityEngine;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
    /// <summary>
    ///     Sets the default selected object on the EventSystem
    /// </summary>
    public class SampleDefaultSelectable : MonoBehaviour
    {
        [Header("References")]
        public Selectable defaultSelectable;

        bool started = false;

        private void Start()
        {
            
            if (defaultSelectable != null)
            {
                defaultSelectable.Select();
            }

            started = true;
        }
        private void OnEnable()
        {
            if(!started)
            {
                return;
            }

            if (defaultSelectable != null)
            {
                defaultSelectable.Select();
            }
        }

        public void Select()
        {
            if (defaultSelectable != null)
            {
                defaultSelectable.Select();
            }
        }
    }
}