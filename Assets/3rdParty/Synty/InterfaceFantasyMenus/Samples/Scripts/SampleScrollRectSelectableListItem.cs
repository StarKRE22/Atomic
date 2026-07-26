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
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
    /// <summary>
    ///     Sample scroll window item. Snaps into view when selected
    /// </summary>
    public class SampleScrollRectSelectableListItem : MonoBehaviour
    {
        Selectable selectable;
        bool wasSelected = false;
        ScrollRect parentScrollRect;

        private void Start()
        {
            parentScrollRect = GetComponentInParent<ScrollRect>();
            selectable = GetComponentInChildren<Selectable>();
        }

        private void Update()
        {
            if (parentScrollRect != null && selectable != null)
            {
                bool isSelected = EventSystem.current.currentSelectedGameObject == selectable.gameObject;
                if (!wasSelected && isSelected)
                {
                    parentScrollRect.SnapChildIntoView(GetComponent<RectTransform>());
                }
                wasSelected = isSelected;
            }
        }
    }
}