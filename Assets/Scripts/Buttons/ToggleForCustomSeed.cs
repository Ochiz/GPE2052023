using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleForCustomSeed : MonoBehaviour
{
    public void CustomSeedBool(bool value)
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.level.customMapSeed = value;
        }
    }
}
