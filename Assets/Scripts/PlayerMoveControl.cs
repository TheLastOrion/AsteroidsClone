using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveControl : MonoBehaviour
{
    private Rigidbody m_rigidbody;
    private Collider m_collider;
    [SerializeField][Range(5f, 15f)] private float m_accelerationFactor = 10f;
    [SerializeField] [Range(5f, 15f)] private float m_turnSpeed = 5f;
    
    public void Start()
    {
        GameEvents.BorderExit += GameEventsOnBorderExit;
        m_rigidbody = GetComponent<Rigidbody>();
        m_collider = GetComponent<Collider>();
    }

    private void GameEventsOnBorderExit(BorderType borderType, Collider teleportCollider)
    {
        float distanceBetweenTopAndBottom = Mathf.Abs(GameObject.Find("BottomBorder").transform.position.y - GameObject.Find("TopBorder").transform.position.y);
        float distanceBetweenLeftAndRight = Mathf.Abs(GameObject.Find("LeftBorder").transform.position.x - GameObject.Find("RightBorder").transform.position.x);;
        Debug.LogFormat("Distance Between Top & Bottom: {0}   Distance Between Left & Right: {1} ", distanceBetweenTopAndBottom, distanceBetweenLeftAndRight);
        if (teleportCollider == m_collider)
        {
            switch (borderType)
            {
                case BorderType.Bottom:
                    
                    transform.position = new Vector3(transform.position.x, transform.position.y + distanceBetweenTopAndBottom, 0);
                    break;
                case BorderType.Left:
                    transform.position = new Vector3(transform.position.x + distanceBetweenLeftAndRight, transform.position.y, 0);

                    break;
                case BorderType.Right:
                    transform.position = new Vector3(transform.position.x -1f * + distanceBetweenLeftAndRight, transform.position.y , 0);

                    break;
                case BorderType.Top:
                    transform.position = new Vector3(transform.position.x, transform.position.y + (-1f * distanceBetweenTopAndBottom), 0);

                    break;
                default:
                    break;
                
            }
        }
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
