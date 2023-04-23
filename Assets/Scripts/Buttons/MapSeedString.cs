using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSeedString : MonoBehaviour
{
    public InputField mapInput;
    private int convertedNumber;

    public void CustomSeed()
    {
        int.TryParse(mapInput.text, out int convertedNumber);
      
        if (GameManager.instance != null)
        {
            GameManager.instance.level.mapSeed = convertedNumber;
        }
     
    } 
}
