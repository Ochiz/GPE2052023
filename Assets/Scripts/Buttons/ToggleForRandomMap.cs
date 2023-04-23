using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleForRandomMap : MonoBehaviour
{
    public void RandomMapBool(bool value)
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.level.isMapRandom == true)
            {
                GameManager.instance.level.isMapRandom = false;
            }
            else
            {
                GameManager.instance.level.isMapRandom = true;

            }
        }
    }
}
