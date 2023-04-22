using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleForMapOfDay : MonoBehaviour
{
    public void MapOfDayBool(bool value)
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.level.isMapOfTheDay = value;
        }
    }
}
