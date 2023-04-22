using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleForTwoPlayers : MonoBehaviour
{
    public void TwoPlayersBool(bool value)
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.twoPlayerGame = value;
        }
    }
}
