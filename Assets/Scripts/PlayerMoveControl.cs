using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveControl : MonoBehaviour
{
    private Rigidbody m_rigidbody;
     
    public void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }
    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            m_rigidbody.AddForce(Vector3.up * 10f);
        }
    }
}
