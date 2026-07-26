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

namespace Synty.Interface.FantasyMenus.Samples
{
    /// <summary>
    ///     Sample script for a simple modal UI Popup
    /// </summary>

    public class SamplePopup : MonoBehaviour
    {
        [Header("References")]
        public SamplePopup acceptObject;
        public SamplePopup cancelObject;
        public Selectable inputBlocker;
        public Animator animator;

        [Header("Parameters")]
        public bool selfDismiss;
        public float dismissTime;
        bool dismissed = true;

        // Update is called once per frame
        private void Update()
        {
            if (dismissed)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if( acceptObject != null ) 
                {
                    acceptObject.ShowMe();
                }
                
                DismissMe();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (cancelObject != null) 
                {
                    cancelObject.ShowMe();
                }

                DismissMe();
            }
        }

        public void ShowMe()
        {
            CancelInvoke();
            dismissed = false;
            if (inputBlocker != null ) 
            {
                inputBlocker.gameObject.SetActive(true);
                inputBlocker.Select();
            }
            gameObject.SetActive(false);
            gameObject.SetActive(true);
            animator.SetBool("Active", true);
            if (selfDismiss)
            {
                Invoke("DisableMe", dismissTime);
            }
        }

        public void DismissMe()
        {
            animator.SetBool("Active", false);
            Invoke("DisableMe", dismissTime);
            if (inputBlocker != null ) 
            {
                inputBlocker.gameObject.SetActive(false);
            }
            dismissed = true;
        }

        public void DisableMe()
        {
            gameObject.SetActive(false);
        }
    }
}