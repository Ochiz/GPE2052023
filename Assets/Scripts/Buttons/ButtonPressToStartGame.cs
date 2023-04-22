using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressToStartGame : MonoBehaviour
{
    public void StartGame()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.DoGamePlayState();
        }
    }
}
