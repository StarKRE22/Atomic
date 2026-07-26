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
    ///     'Hot key' binding for the referenced button.
    /// </summary>

    public class SampleUIButtonInput : MonoBehaviour
    {
        [Header("References")]
        public Button button;
        
        [Header("Parameters")]
        public KeyCode keyCode;
        public KeyCode secondaryKeyCode = KeyCode.None;

        private void Update()
        {
            if(Input.GetKeyDown(keyCode) || Input.GetKeyDown(secondaryKeyCode))
            {
                EventSystem eventSystem = GameObject.FindAnyObjectByType<EventSystem>();
                if(eventSystem != null && button != null )
                {
                    ExecuteEvents.Execute(button.gameObject, new BaseEventData(eventSystem), ExecuteEvents.submitHandler);
                }
            }
        }
    }

}