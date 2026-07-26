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

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
    /// <summary>
    ///     Sample for radial menu input
    /// </summary>
    public class SampleRadialMenuInput : MonoBehaviour
    {
        [Header("References")]
        public Transform origin;

        public Selectable[] selectables;

        [Header("Paramters")]

        [Range(0,1)]
        public float currentSelectionBias = 0.1f;

        private bool IsRadialMenuElementSelected()
        {
            for (int i = 0; i < selectables.Length; ++i)
            {
                if (EventSystem.current.currentSelectedGameObject == selectables[i].gameObject)
                {
                    return true;
                }
            }

            return false;
        }

        private float GetInput(string[] inputAxies)
        {
            float result = 0;
            for (int i = 0; i < inputAxies.Length; ++i)
            {
                result += Input.GetAxisRaw(inputAxies[i]);
            }
            return result;
        }

        float GetHorizontal()
        {
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                return -1;
            }

            if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                return 1;
            }

            return 0;
        }

        float GetVerticle()
        {
            if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                return -1;
            }
            
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                return 1;
            }

            return 0;
        }

        private void Update()
        {
            if (IsRadialMenuElementSelected() && !EventSystem.current.alreadySelecting)
            {
                // we're in focus. let's process our input!
                Vector3 input = new Vector3( GetHorizontal(), GetVerticle(), 0);

                if (input.magnitude > 0.1f)
                {
                    Vector3 inputDirection = input.normalized;

                    Debug.DrawLine(origin.position, origin.position + inputDirection * 100, Color.red);
                    
                    int bestFit = 0;
                    float bestDot = 0;
                    for (int i = 0; i < selectables.Length; ++i)
                    {
                        Debug.DrawLine(origin.position, selectables[i].transform.position, Color.green);
                        bool currentSelection = EventSystem.current.currentSelectedGameObject == selectables[i].gameObject;
                        Vector3 elementDirection = (selectables[i].transform.position - origin.position).normalized;
                        float testDot = Vector3.Dot(inputDirection, elementDirection) + (currentSelection ? currentSelectionBias : 0);
                        if (i == 0)
                        {
                            bestFit = 0;
                            bestDot = testDot;
                            continue;
                        }

                        if (testDot > bestDot)
                        {
                            bestDot = testDot;
                            bestFit = i;
                        }
                    }

                    if (EventSystem.current.currentSelectedGameObject != selectables[bestFit].gameObject)
                    {
                        selectables[bestFit].Select();
                    }
                }
            }
        }
    }
}