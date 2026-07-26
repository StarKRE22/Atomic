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

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
    /// <summary>
    ///     A settings slider that sets a value between a range
    /// </summary>

    public class SampleSettingsSlider : MonoBehaviour
    {
        [Header("References")]
        public TMP_Text valueLabel;
        public Slider valueSlider;
        public Selectable selectable;
        public Button left;
        public Button right;

        [Header("Parameters")]
        public float sliderStep = 1;
        public string valueFormat = "##0";

        private void Start()
        {
            if (left != null)
            {
                left.onClick.AddListener(OnLeftClick);
            }
            if (right != null)
            {
                right.onClick.AddListener(OnRightClick);
            }
            if(valueSlider != null)
            {
                valueSlider.onValueChanged.AddListener(OnValueChanged);
                OnValueChanged(valueSlider.value);
            }
        }

        private void Update()
        {
            if (selectable != null && EventSystem.current.currentSelectedGameObject == selectable.gameObject)
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    OnLeftClick();
                }
                else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    OnRightClick();
                }
            }
        }

        public void OnValueChanged(float value)
        {
            valueLabel.SetText( value.ToString(valueFormat) );
        }

        public void SetValue(float newValue)
        {
            valueSlider.value = newValue;
        }

        public void OnLeftClick()
        {
            valueSlider.value = valueSlider.value - sliderStep;
        }

        public void OnRightClick()
        {
            valueSlider.value = valueSlider.value + sliderStep;
        }
    }
}