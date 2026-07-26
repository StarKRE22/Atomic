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
    ///     Adds animator boolean parameter that reflects the toggle's state
    /// </summary>
    public class SampleToggleAnimatorEvents : MonoBehaviour
    {
        private Toggle toggle;
        private Animator animator;

        private void Awake()
        {
            toggle = GetComponent<Toggle>();
            animator = GetComponent<Animator>();

            if (toggle != null)
            {
                toggle.onValueChanged.AddListener(OnValueChanged);
                OnValueChanged(toggle.isOn);
            }
        }

        private void OnEnable()
        {
            if (toggle !=  null)
            {
                OnValueChanged(toggle.isOn);
            }
        }

        private void OnDestroy()
        {
            if (toggle !=  null)
            {
                toggle.onValueChanged.RemoveListener(OnValueChanged);
            }
        }

        private void OnValueChanged(bool value)
        {
            if (animator != null)
            {
                animator.SetBool( "ToggleIsOn", value );
            }
        }
    }
}