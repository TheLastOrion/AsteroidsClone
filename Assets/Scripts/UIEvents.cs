using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    public event Action MoveForwardPressed;
    public event Action MoveRightPressed;
    public event Action MoveLeftPressed;
    public event Action MoveBackPressed;
}
