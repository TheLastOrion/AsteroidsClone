using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    
    [Tooltip("When creating a poolable object, make sure to add a new enum type in Poolables")]

    [SerializeField] private List<PooledObject> PoolableObjects = new List<PooledObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    public GameObject GetPooledObject(GameObject go)
    {
        foreach (var poolableObject in PoolableObjects)
        {
            if(poolableObject.getGameObject())
        }
    }

    private void Start()
    {
        
    }


}
[Serializable]
public class PooledObject
{
    [SerializeField] private GameObject m_pooledObject;
    [SerializeField] private int m_amountPreCreated;
    [SerializeField] private PoolableType m_poolableType;

    public PooledObject(GameObject go, int amount, PoolableType poolableType)
    {
        m_pooledObject = go;
        m_amountPreCreated = amount;
        m_poolableType = poolableType;
    }

    public GameObject getGameObject()
    {
        return m_pooledObject;
    }

    public PoolableType getPoolableType()
    {
        return m_poolableType;
    }
    
}
