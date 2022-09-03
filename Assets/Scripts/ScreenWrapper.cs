using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    [SerializeField]private BorderType m_borderType;

    public void OnTriggerExit(Collider other)
    {
        GameEvents.FireBorderEnter(m_borderType, other);
        // switch (m_borderType)
        // {
        //     case BorderType.Bottom:
        //         break;
        //     case BorderType.Left:
        //         break;
        //     case BorderType.Right:
        //         break;
        //     case BorderType.Top:
        //         break;
        //     default:
        //         break;
        // }
    }
}
