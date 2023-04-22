using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressToMainMenu : MonoBehaviour
{
    public void ChangeToMainMenu()
    {
        GameManager.instance.DoMainMenuState();
    }
}
