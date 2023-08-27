using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUpdater : MonoBehaviour
{
    public void UpdateText(string newText) {
        this.gameObject.GetComponent<TextMeshProUGUI>().text = newText;
    }

    public void UpdateSeedCount() {
        UpdateText(GetShortenedNumber(PlayerData.Instance.seedCount));
    }

    public void UpdateWeedCount() {
        UpdateText(GetShortenedNumber(PlayerData.Instance.weedCount));
    }

    private string GetShortenedNumber(int count) {
        if (count < 1000){
            return count.ToString();
        }

        // Get first 3 sig figs of weed count with large number identifier (kilo, million, billion...)
        decimal newCount = count;
        int unit = 0;
        string[] unitStr = {"K", "M", "B", "T"};

        while (newCount >= 1000m){
            newCount /= 1000;
            unit += 1;
        }

        if (unit > 4){
            return count.ToString("E2"); // scientific exponential format
        }

        print(newCount + ", " + unit);
        return count.ToString("N2").Substring(0,3) + unitStr[unit-1]; // 2 decimal places

    }
}
