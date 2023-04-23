using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleForCustomSeed : MonoBehaviour
{
    public void CustomSeedBool(bool value)
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.level.customMapSeed == true)
            {
                GameManager.instance.level.customMapSeed = false;
            }
            else
            {
                GameManager.instance.level.customMapSeed = true;

            }
        }
    }
}
