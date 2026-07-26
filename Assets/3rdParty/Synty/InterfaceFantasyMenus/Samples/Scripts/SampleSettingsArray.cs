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
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Synty.Interface.FantasyMenus.Samples
{
    /// <summary>
    ///     Allow the selection of a single option from an array of possible options
    /// </summary>

    public class SampleSettingsArray : MonoBehaviour
    {
         [Header("References")]
        public TMP_Text valueLabel;
        public Button left;
        public Button right;
        public Selectable selectable;

        [Space]
        public UnityEvent<int, string> onValueChanged;

        [Header("Parameters")]
        public string[] options;
        public int defaultOption = 0;
        public bool loop = false;

        public int OptionIndex => optionIndex;
        public string Option => options.Length > 0 ? options[optionIndex] : "";

        private int optionIndex;

        private void OnValidate()
        {
            SetDefaultOption();
        }

        private void Start()
        {
            SetDefaultOption();

            if (left != null)
            {
                left.onClick.AddListener(OnLeftClick);
            }
            if (right != null)
            {
                right.onClick.AddListener(OnRightClick);
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

        private void UpdateUI()
        {
            if (options.Length == 0)
            {
                valueLabel.SetText("");
            }
            else
            {
                valueLabel.SetText(options[optionIndex]);
            }
        }

        public void SetOption(int newOption)
        {
            if (newOption < 0)
            {
                if (loop)
                {
                    newOption = options.Length-1;
                }
                else
                {
                    newOption = 0;
                }
            }
            if (newOption >= options.Length)
            {
                if (loop)
                {
                    newOption = 0;
                }
                else
                {
                    newOption = options.Length - 1;
                }
            }

            if (left != null)
            {
                left.interactable = loop || newOption > 0;
            }
            if (right != null)
            {
                right.interactable = loop || newOption < options.Length-1;
            }

            if (optionIndex != newOption)
            {
                optionIndex = newOption;
                onValueChanged?.Invoke(optionIndex, options[optionIndex]);
            }
            UpdateUI();
        }

        public void OnLeftClick()
        {
            SetOption( optionIndex-1 );
        }

        public void OnRightClick()
        {
            SetOption( optionIndex+1 );
        }

        public void SetDefaultOption()
        {
            SetOption( defaultOption );
        }
    }
}