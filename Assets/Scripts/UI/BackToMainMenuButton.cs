using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMainMenuButton : ButtonComponent
{
    
    protected override void ButtonClick()
    {
        UIEvents.FireBackToMainMenuButtonPressed();
    }
}
