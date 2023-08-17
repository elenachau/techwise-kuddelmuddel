using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopBack : MonoBehaviour
{
    public void GoToGame()
    {
        SceneManager.LoadScene("Map1");
    }
}