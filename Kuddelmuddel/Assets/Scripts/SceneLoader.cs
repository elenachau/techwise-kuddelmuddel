using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void PauseGame()
    { 
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }
    
    public void OpenItemShop()
    {
        SceneManager.LoadScene("ShopSystem", LoadSceneMode.Additive);
    }

    public void CloseItemShop()
    {
        SceneManager.UnloadSceneAsync("ShopSystem");
    }

    public void PlayGame()
    {
        if (PlayerData.Instance == null){ // game has not started yet
            SceneManager.LoadScene("Map1");
        }
        else {
            SceneManager.UnloadSceneAsync("Menu");
        }
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

}