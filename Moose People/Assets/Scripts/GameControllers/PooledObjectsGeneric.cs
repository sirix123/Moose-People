using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool;
    public bool willGrow;
}

public class PooledObjectsGeneric : MonoBehaviour
{
    public static PooledObjectsGeneric SharedInstance;
    public List<ObjectPoolItem> itemsToPool;
    public List<GameObject> pooledObjects;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start ()
    {
        //goes through pooled objects and creates a clone, sets the clone to deactive
        pooledObjects = new List<GameObject>();
        foreach(ObjectPoolItem item in  itemsToPool)
        {
            for( int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        //if the gameobject is not active in hierarchy return a clone
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }

        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.willGrow)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
      return null;
    }	

	public void DestroyObjectPool(GameObject obj)
	{
		print ("destroy");
		pooledObjects.Add (obj);
		obj.SetActive (false);
	}
}
