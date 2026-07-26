using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSwitchBar : MonoBehaviour {

    [SerializeField] ProgressBarPro[] progressBarPrefabs;
    [SerializeField] Transform prefabParent;
    [SerializeField] int currentPrefab;
    [SerializeField] Text prefabName;

    ProgressBarPro bar;
    float currentValue = 1f;

    void Start () {
        InstantiateBar(currentPrefab);
	}

    public void SetRandomBar() {
        InstantiateBar(Random.Range(0, progressBarPrefabs.Length));
    }

    public void ShiftBar(int shift) {
        int newValue = currentPrefab + shift;

        if (newValue >= progressBarPrefabs.Length)
            InstantiateBar(0);
        if (newValue < 0)
            InstantiateBar(progressBarPrefabs.Length - 1);
        else
            InstantiateBar(newValue);
    }

    void InstantiateBar(int num) {
        if (num < 0 || num >= progressBarPrefabs.Length)
            return;

        currentPrefab = num;

        if (bar != null)
            Destroy(bar.gameObject);

#if UNITY_5_2 || UNITY_5_3 || UNITY_5_4_OR_NEWER
        bar = Instantiate<ProgressBarPro>(progressBarPrefabs[num], prefabParent);
#else
        bar = Instantiate(progressBarPrefabs[num]) as ProgressBarPro;
        bar.transform.SetParent(prefabParent);
#endif
        bar.gameObject.SetActive(false);
        bar.transform.localScale = Vector3.one;
        bar.SetValue(currentValue);
        bar.gameObject.SetActive(true);

        prefabName.text = progressBarPrefabs[currentPrefab].gameObject.name;

        Invoke("EnableBar", 0.01f);
    }

    void EnableBar() {
        if (bar != null)
            bar.gameObject.SetActive(true);
    }


    public void SetValue(float value) {
        currentValue = value;

        if (bar != null)
            bar.SetValue(value);
    }

    public void SetBarColor(Color color) {
        if (bar != null)
            bar.SetBarColor(color);
    }

}
