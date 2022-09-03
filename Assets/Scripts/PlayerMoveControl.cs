using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveControl : MonoBehaviour
{
    private Rigidbody m_rigidbody;
    [SerializeField][Range(5f, 15f)] private float m_accelerationFactor = 10f;
    [SerializeField] [Range(5f, 15f)] private float m_turnSpeed = 5f;
    public void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }
    public void Update()
    {
        
        if (Input.GetKey(KeyCode.W)) 
        {
            // m_rigidbody.AddForce(Vector3.up * (transform.rotation.z * m_accelerationFactor) + 
            //                      Vector3.right * (transform.rotation.z * m_accelerationFactor));
            m_rigidbody.AddRelativeForce(Vector3.up * m_accelerationFactor);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward, m_turnSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward, -1f * m_turnSpeed);
        }
    }
}
