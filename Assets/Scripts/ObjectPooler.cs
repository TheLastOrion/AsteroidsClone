using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;


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
            poolableObject.SetParentTransform(containerObject.transform);
            containerObject.transform.position = Vector3.zero;
            containerObject.transform.parent = this.transform;
            if (poolableObject.getGameObject().GetComponent<PooledObjectType>() == null)
            {
                Debug.LogErrorFormat("The object {0} seems not to be set as poolable, " +
                                     "not instantiating!\nPlease attach a PooledObjectType and set it properly beforehand!"
                    , poolableObject.getGameObject().name);
                continue;
            }
            
            for (int i = 0; i < poolableObject.getAmount(); i++)
            {
                
                GameObject objectToBeCreated = Instantiate(poolableObject.getGameObject(), Vector3.zero,
                    Quaternion.identity, containerObject.transform);
                objectToBeCreated.SetActive(false);
                objectList.Add(objectToBeCreated);
            }
        }
    }

    public void AddGameObjectToPool(GameObject go)
    {
        if (go.GetComponent<PooledObjectType>() == null)
        {
            Debug.LogErrorFormat("Object is not set to be poolable, returning null");
        }
        PoolableType type = go.GetComponent<PooledObjectType>().PoolableType;
        
        foreach (var poolableObject in PoolableObjects)
        {

            if (poolableObject.getPoolableType() == type)
            {
                go.transform.parent = poolableObject.GetParentTransform();
                go.SetActive(false);
                go.transform.position = Vector3.zero;
                poolableObject.GetList().Add(go);
                Debug.LogFormat("Adding extra gameobject {0} to its pool!", go.name);
            }
        }

    }

    public GameObject GetPooledObject(GameObject go)
    {
    
        if (go.GetComponent<PooledObjectType>() == null)
        {
            Debug.LogErrorFormat("Object is not set to be poolable, returning null");
            return null;
        }
    
        PoolableType type = go.GetComponent<PooledObjectType>().PoolableType;
        foreach (var poolableObject in PoolableObjects)
        {
            if (poolableObject.getPoolableType() == type)
            {
                foreach (var returnObject in poolableObject.GetList())
                {
                    if (!returnObject.activeInHierarchy)
                        return returnObject;
                }
                AddGameObjectToPool(go);
                return go;
            }
        }
        Debug.LogErrorFormat("Object is not set to be poolable, returning null");

        return null;
    }


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

    public void SetParentTransform(Transform transform)
    {
        m_containerTransform = transform;
    }

    public Transform GetParentTransform()
    {
        return m_containerTransform;
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
