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
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
    /// <summary>
    ///     Events for on highlighted and on selected
    /// </summary>
    public class SampleOnButtonEvents : MonoBehaviour, IPointerEnterHandler, ISelectHandler
    {
        public UnityEvent onHighlighted;
        public UnityEvent onSelected;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(onHighlighted != null)
            {
                onHighlighted.Invoke();
            }
        }

        public void OnSelect(BaseEventData eventData)
        {
            if(onSelected != null)
            {
                onSelected.Invoke();
            }
        }
    }
}