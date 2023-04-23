using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleForTwoPlayers : MonoBehaviour
{
    public void TwoPlayersBool(bool value)
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.twoPlayerGame == true)
            {
                GameManager.instance.twoPlayerGame = false;
            }
            else
            {
                GameManager.instance.twoPlayerGame = true;

            }
        }
    }
}
