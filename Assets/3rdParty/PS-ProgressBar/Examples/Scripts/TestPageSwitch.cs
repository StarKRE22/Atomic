using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPageSwitch : MonoBehaviour {

    int currentPage = 0;

	void Start () {
        ShowPage();
    }

    public void ShiftPage(int offset) {
        currentPage += offset;

        if (currentPage >= transform.childCount)
            currentPage = 0;
        else if (currentPage < 0)
            currentPage = transform.childCount - 1;

        ShowPage();
    }

    void ShowPage() { 
        int i = 0;

        foreach (Transform child in transform) { 
            child.gameObject.SetActive(i == currentPage);
            i++;
        }
    }

}
