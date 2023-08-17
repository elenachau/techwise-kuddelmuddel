using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public void PauseGame() { 
    
    }
    public void OpenItemShop()
    {
        SceneManager.LoadScene("ShopSystem");
    }


    public void OpenSettings()
    {

    }

}