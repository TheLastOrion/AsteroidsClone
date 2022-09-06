using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoresButton : ButtonComponent
{
    // Start is called before the first frame update
    protected override void ButtonClick()
    {
        UIEvents.FireHighScoresButtonPressed();
    }
}
