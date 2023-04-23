using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleForMapOfDay : MonoBehaviour
{
    public void MapOfDayBool(bool value)
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.level.isMapOfTheDay == true)
            {
                GameManager.instance.level.isMapOfTheDay = false;
            }
            else
            {
                GameManager.instance.level.isMapOfTheDay = true;

            }
        }
    }
}
