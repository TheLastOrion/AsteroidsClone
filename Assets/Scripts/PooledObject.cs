using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour, IPoolable
{
    public PoolableType PoolableType;
    public void DeSpawn()
    {
        ObjectPooler.Instance.GetPooledObject(this.gameObject);
    }
    
}
