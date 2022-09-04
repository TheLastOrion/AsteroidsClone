using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    [SerializeField]private BorderType m_borderType;

    public void OnTriggerExit(Collider other)
    {
        GameEvents.FireBorderExit(m_borderType, other);
        
    }
}
