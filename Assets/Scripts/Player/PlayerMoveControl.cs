using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveControl : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Collider _collider;
    [FormerlySerializedAs("m_accelerationFactor")] [SerializeField] [Range(5f, 15f)] private float _accelerationFactor = 10f;
    [FormerlySerializedAs("m_turnSpeed")] [SerializeField] [Range(5f, 15f)] private float _turnSpeed = 5f;
    
    public void Start()
    {
        GameEvents.BorderExit += GameEventsOnBorderExit;
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }


    private void GameEventsOnBorderExit(BorderType borderType, Collider teleportCollider)
    {
        if (teleportCollider == _collider)
        {
            transform.position = GameUtils.FindTeleportPlace(transform, borderType);
        }
    }

    
    public void Update()
    {
        if (Input.GetKey(KeyCode.W)) 
        {
            _rigidbody.AddRelativeForce(Vector3.up * _accelerationFactor);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward, _turnSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward, -1f * _turnSpeed);
        }
    }
    
}
