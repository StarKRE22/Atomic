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
    ///     Helper functions for the scroll rect component
    /// </summary>
    public static class ScrollRectExtensions
    {
        public static void SnapChildIntoView(this ScrollRect instance, RectTransform child)
        {
            instance.content.localPosition = GetSnapToPositionToBringChildIntoView(instance, child);
        }

        public static Vector3 GetSnapToPositionToBringChildIntoView(this ScrollRect instance, RectTransform child)
        {
            Canvas.ForceUpdateCanvases();

            RectTransform rectTransform = instance.viewport != null ? instance.viewport : instance.GetComponent<RectTransform>();
            
            Bounds childBounds = RectTransformUtility.CalculateRelativeRectTransformBounds(rectTransform, child);

            float shiftDown = -Mathf.Max(0, childBounds.max.y - rectTransform.rect.yMax);
            float shiftUp = -Mathf.Min(0, childBounds.min.y - rectTransform.rect.yMin);

            float shiftLeft = -Mathf.Max(0, childBounds.max.x - rectTransform.rect.xMax);
            float shiftRight = -Mathf.Min(0, childBounds.min.x - rectTransform.rect.xMin);

            Vector3 shift = new Vector3( instance.horizontal ? shiftLeft + shiftRight : 0, instance.vertical ? shiftUp + shiftDown: 0, 0);
            return instance.content.localPosition + shift;
        }
    }
}