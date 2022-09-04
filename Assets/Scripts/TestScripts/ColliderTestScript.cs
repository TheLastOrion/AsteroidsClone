using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ColliderTestScript : MonoBehaviour
{
    private BoxCollider m_boxCollider; 
    // Start is called before the first frame update
    void Start()
    {
        m_boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     Debug.LogFormat("Trigger Enter! Collider: {0} Collision: {1}", this.gameObject.name, other.gameObject.name);
    // }
    //
    // private void OnTriggerExit(Collider other)
    // {
    //     Debug.LogFormat("Trigger Exit! Collider: {0} Collision: {1}", this.gameObject.name, other.gameObject.name);
    // }
}
