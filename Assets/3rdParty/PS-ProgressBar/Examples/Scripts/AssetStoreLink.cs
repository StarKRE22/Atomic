using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetStoreLink : MonoBehaviour {

    public string assetStoreID = "74356";

    public void VisitStore() {
        Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/content/" + assetStoreID);
    }
}