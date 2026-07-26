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
    ///     A sample script that simulates brightness settings.
    /// </summary>
    public class SampleBrightnessDisplay : MonoBehaviour
    {
        [Header("References")]
        public Image image;
        public Slider brightnessValueSlider;
        
        [Header("Paramters")]
        public float multiplier = 1;
        public float min = 0;
        public float max = 1;

        Color defaultColor;

        private void Awake()
        {
            defaultColor = image.color;
            brightnessValueSlider.onValueChanged.AddListener(OnValueChanged);
            OnValueChanged(brightnessValueSlider.value);
        }

        private void OnDestroy()
        {
            brightnessValueSlider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            image.color = defaultColor * Mathf.Lerp(min, max, value * multiplier);
        }
    }
}