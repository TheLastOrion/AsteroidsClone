using System;
using System.Collections;
using System.Collections.Generic;
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
        // Prewarm Object Pool with objects
        foreach (var poolableObject in PoolableObjects)
        {
            List<GameObject> objectList = poolableObject.GetList();
            if (objectList == null)
                objectList = new List<GameObject>();
            GameObject containerObject = new GameObject(poolableObject.GetPoolableType().ToString() + "Container");
            poolableObject.SetParentTransform(containerObject.transform);
            containerObject.transform.position = Vector3.zero;
            containerObject.transform.parent = this.transform;
            if (poolableObject.GetGameObject().GetComponent<PooledObject>() == null)
            {
                Debug.LogErrorFormat("The object {0} seems not to be set as poolable, " +
                                     "not instantiating!\nPlease attach a PooledObjectType and set it properly beforehand!"
                    , poolableObject.GetGameObject().name);
                continue;
            }
            
            for (int i = 0; i < poolableObject.GetAmount(); i++)
            {
                
                GameObject objectToBeCreated = Instantiate(poolableObject.GetGameObject(), Vector3.zero,
                    Quaternion.identity, containerObject.transform);
                objectToBeCreated.SetActive(false);
                objectList.Add(objectToBeCreated);
            }
            poolableObject.SetList(objectList);
        }
    }

    public void AddGameObjectToPool(GameObject go)
    {
        if (go.GetComponent<PooledObject>() == null)
        {
            Debug.LogErrorFormat("Object is not set to be poolable, returning null");
        }
        PoolableType type = go.GetComponent<PooledObject>().PoolableType;
        
        foreach (var poolableObject in PoolableObjects)
        {

            if (poolableObject.GetPoolableType() == type)
            {
                go = Instantiate(go, poolableObject.GetParentTransform());
                go.transform.parent = poolableObject.GetParentTransform();
                go.SetActive(false);
                go.transform.position = Vector3.zero;
                List<GameObject> tempList = poolableObject.GetList();
                tempList.Add(go);
                poolableObject.SetList(tempList);
                
                Debug.LogFormat("Adding extra gameobject {0} to its pool!", go.name);
            }
        }

    }

    public GameObject GetPooledObject(GameObject go)
    {
    
        if (go.GetComponent<PooledObject>() == null)
        {
            Debug.LogErrorFormat("Object is not set to be poolable, returning null");
            return null;
        }
    
        PoolableType type = go.GetComponent<PooledObject>().PoolableType;
        foreach (var poolableObject in PoolableObjects)
        {
            if (poolableObject.GetPoolableType() == type)
            {
                foreach (var returnObject in poolableObject.GetList())
                {
                    if (!returnObject.activeSelf)
                        return returnObject;
                }
                AddGameObjectToPool(go);
                return go;
            }
        }
        Debug.LogErrorFormat("Object is not set to be poolable, returning null");

        return null;
    }

    public void RecyclePooledObject(GameObject recycledObject)
    {
        if (recycledObject.GetComponent<PooledObject>() == null)
        {
            Debug.LogErrorFormat("Object is not set to be poolable, skipping recycle");
            return;
        }

        PoolableType type = recycledObject.GetComponent<PooledObject>().PoolableType;
        foreach (var poolableObject in PoolableObjects)
        {
            if (poolableObject.GetPoolableType() == type)
            {
                Debug.LogFormat("Recycling Object {0}", recycledObject.name);
                recycledObject.SetActive(false);
                recycledObject.transform.SetParent(poolableObject.GetParentTransform());
                recycledObject.transform.position = Vector3.zero;
            }
        }
        
        
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

    public GameObject GetGameObject()
    {
        return m_pooledObject;
    }

    public PoolableType GetPoolableType()
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

    public int GetAmount()
    {
        return m_amountPreCreated;
    }

    public List<GameObject> GetList()
    {
        return m_objectList;
    }

    public void SetList(List<GameObject> objectList)
    {
        m_objectList = objectList;
    }

}
