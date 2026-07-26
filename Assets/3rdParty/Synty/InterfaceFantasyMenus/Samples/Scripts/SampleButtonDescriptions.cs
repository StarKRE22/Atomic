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

namespace Synty.Interface.FantasyMenus.Samples
{
    /// <summary>
    ///     Controls the activation state of button description objects
    /// </summary>

    public class SampleButtonDescriptions : MonoBehaviour
    {
        // Start is called before the first frame update
        public void SetActive(GameObject description)
        {
            for(int i = 0; i < transform.childCount; ++i)
            {
                Transform child = transform.GetChild(i);
                child.gameObject.SetActive( child.gameObject == description );
            }
        }
    }
}