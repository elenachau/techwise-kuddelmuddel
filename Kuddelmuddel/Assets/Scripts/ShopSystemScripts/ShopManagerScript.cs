using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[5, 5];
    [SerializeField] public List<PowerupEffect> powerups;
    public Text CoinsTXT;
    public static ShopManagerScript Instance;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        CoinsTXT.text = "Seeds:" + PlayerData.Instance.seedCount.ToString();

        //ID's
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;

        //Price
        shopItems[2, 1] = powerups[0].getCost();
        shopItems[2, 2] = powerups[1].getCost();
        shopItems[2, 3] = powerups[2].getCost();
        shopItems[2, 4] = powerups[3].getCost();
    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        int id = ButtonRef.GetComponent<ButtonInfo>().ItemID;

        if (PlayerData.Instance.seedCount >= shopItems[2, id])
        {
            PlayerData.Instance.AddSeeds(-shopItems[2, id]);
            CoinsTXT.text = "Seeds:" + PlayerData.Instance.seedCount.ToString();
            
            // Apply powerup automatically and close shop
            Powerup.Instance.powerupEffect = powerups[id - 1];
            Powerup.Instance.PowerUpAllWeeds();
            SceneManager.UnloadSceneAsync("ShopSystem");
        }

    }
}