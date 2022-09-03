using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotator : MonoBehaviour
{
    [SerializeField][Range(0.1f, 5f)]
    private float m_rotateSpeed = 0.5f;
    private void FixedUpdate()
    {
        transform.Rotate(m_rotateSpeed,m_rotateSpeed,m_rotateSpeed);
    }
}
