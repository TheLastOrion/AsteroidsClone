using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : ButtonComponent
{
    protected override void ButtonClick()
    {
        UIEvents.FirePlayGameButtonPressed();
    }
}
