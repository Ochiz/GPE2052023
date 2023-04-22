using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSeedString : MonoBehaviour
{
    private int convertedNumber;
    public void CustomSeed(string value)
    {
        bool success = int.TryParse(value, out convertedNumber);
        if (success)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.level.mapSeed = convertedNumber;
            }
        }       
    } 
}
