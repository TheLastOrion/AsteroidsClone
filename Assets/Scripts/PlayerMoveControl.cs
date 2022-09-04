
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveControl : MonoBehaviour
{
    private Rigidbody m_rigidbody;
    private Collider m_collider;
    [SerializeField] [Range(5f, 15f)] private float m_accelerationFactor = 10f;
    [SerializeField] [Range(5f, 15f)] private float m_turnSpeed = 5f;
    public void Start()
    {
        GameEvents.BorderExit += GameEventsOnBorderExit;
        m_rigidbody = GetComponent<Rigidbody>();
        m_collider = GetComponent<Collider>();
    }

    private void GameEventsOnBorderExit(BorderType borderType, Collider teleportCollider)
    {
        if (teleportCollider == m_collider)
        {
            transform.position = GameUtils.FindTeleportPlace(transform, borderType);
        }
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.W)) 
        {
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
