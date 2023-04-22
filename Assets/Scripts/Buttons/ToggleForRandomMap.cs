using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleForRandomMap : MonoBehaviour
{
    public void RandomMapBool(bool value)
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.level.isMapRandom = value;
        }
    }
}
