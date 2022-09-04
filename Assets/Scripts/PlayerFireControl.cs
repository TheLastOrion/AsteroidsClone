using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireControl : MonoBehaviour
{
    [SerializeField] private GameObject BulletObject;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GameObject go = ObjectPooler.Instance.GetPooledObject(BulletObject);
            go.transform.position = transform.position;
            go.transform.parent = transform.parent;
            go.SetActive(true);

        }
    }
}
