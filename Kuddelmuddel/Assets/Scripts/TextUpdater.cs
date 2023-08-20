using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUpdater : MonoBehaviour
{
    private TextMeshProUGUI textRef;
    private PlayerData pd;

    void Start() {
        textRef = this.gameObject.GetComponent<TextMeshProUGUI>();
        pd = GameObject.Find("Player").GetComponent<PlayerData>();
    } 

    public void UpdateText(string str) {
        textRef.text = str;
    }

    public void UpdateSeedCount() {
        textRef.text = pd.seedCount.ToString();
    }

    public void UpdateWeedCount() {
        textRef.text = pd.weedCount.ToString();
    }
}
