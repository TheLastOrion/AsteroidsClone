using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    //TODO Create a cleaner pool.
    [Tooltip(
        "When creating a poolable object, make sure to add a new enum type in Poolables and attach a PooledObjectType script to the object with proper type set")]
    [SerializeField]
    private List<ObjectPool> PoolableObjects = new List<ObjectPool>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        foreach (var poolableObject in PoolableObjects)
        {
            List<GameObject> objectList = poolableObject.GetList();
            if (objectList == null)
                objectList = new List<GameObject>();
            GameObject containerObject = new GameObject(poolableObject.getPoolableType().ToString() + "Container");
            containerObject.transform.position = Vector3.zero;
            containerObject.transform.parent = this.transform;
            
            
            for (int i = 0; i < poolableObject.getAmount(); i++)
            {
                GameObject objectToBeCreated = Instantiate(poolableObject.getGameObject(), Vector3.zero,
                    Quaternion.identity, containerObject.transform);
                objectToBeCreated.SetActive(false);
                objectList.Add(objectToBeCreated);
            }
        }
    }

    // public GameObject GetPooledObject(GameObject go)
    // {
    //
    //     if (go.GetComponent<PooledObjectType>() == null)
    //     {
    //         Debug.LogErrorFormat("Object is not set to be poolable, returning null");
    //         return null;
    //     }
    //
    //     PoolableType type = go.GetComponent<PooledObjectType>().PoolableType;
    //     foreach (var poolableObject in PoolableObjects)
    //     {
    //         if (poolableObject.getPoolableType() == type)
    //         {
    //             
    //         }
    //     }
    // }


}
[Serializable]
public class ObjectPool
{
    private List<GameObject> m_objectList = new List<GameObject>();
    private Transform m_containerTransform;
    [SerializeField] private GameObject m_pooledObject;
    [SerializeField] private int m_amountPreCreated;
    [SerializeField] private PoolableType m_poolableType;

    public ObjectPool(GameObject go, int amount, PoolableType poolableType)
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

    public void SetTransform(Transform transform)
    {
        m_containerTransform = transform;
    }

    public int getAmount()
    {
        return m_amountPreCreated;
    }

    public List<GameObject> GetList()
    {
        return m_objectList;
    }

}
