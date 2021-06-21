using UnityEngine;
using System.Collections.Generic;

public class Pool : MonoBehaviour
{
    public List<GameObject> prefabsList;
    private List<GameObject> pool = new List<GameObject>();
    public string items_name;
    private static int itemsCeatedNum = 0;
    private Transform _tr;
    private GameObject container;

    public static Pool create(string name)
    {
        GameObject gameobj = new GameObject(name);
        Pool pool = gameobj.AddComponent<Pool>();
        pool.items_name = name;
        return pool;
    }

    /*
    public static Pool create(string colorID, List<GameObject> p)
    {
        GameObject gameobj = new GameObject(colorID);
        Pool pool = gameobj.AddComponent<Pool>();
        pool.items_name = colorID;
        pool.prefabsList = p;
        return pool;
    }
    */

    public static Pool create(string name, GameObject gameObjectPrefab, int n)
    {
        GameObject gameobj = new GameObject(name);
        Pool pool = gameobj.AddComponent<Pool>();
        pool.items_name = name;
        pool.prefabsList = new List<GameObject>() {gameObjectPrefab};
        pool.CreateRandomItems(n);
        return pool;
    }

    public void Awake()
    {
        container = new GameObject("container");
        container.transform.SetParent(this.transform);
        _tr = container.transform;
        container.SetActive(false);
    }

    void Start()
    {
        name = "Pool_of_(" + items_name + ")";
    }

    public class PrefabInfo : MonoBehaviour
    {
        public GameObject prefab;
        public Pool pool;
    }

    public int count => pool.Count;

    public void CreateRandomItemToPool()
    {
        GameObject item = CreateItem(prefabsList[Random.Range(0, prefabsList.Count)]);
        ReturnItem(item);
    }

    public void CreateRandomItems(int n)
    {
        for (int i = 0; i < n; i++)
        {
            ReturnItem(CreateItem(prefabsList[Random.Range(0, prefabsList.Count)]));
        }
    }


//    public void CreateItemToPool(GameObject prefab)
//    {
//        GameObject item = CreateItem(prefab);
//        ReturnItem(item);
//    }
    public GameObject CreateItem(GameObject prefab)
    {
        GameObject item = Object.Instantiate(prefab) as GameObject;
        item.name = items_name + "_" + itemsCeatedNum++;
        PrefabInfo pt = item.AddComponent<PrefabInfo>();
        pt.pool = this;
        pt.prefab = prefab;

        return item;
    }

    public GameObject GetPrefab(int index)
    {
        if (index > prefabsList.Count)
        {
            index = 0;
        }

        return prefabsList[index];
    }

    public T GetItem<T>()
    {
        return GetItem(0).GetComponent<T>();
    }

    public GameObject GetItem(int index)
    {
        GameObject item;
        GameObject itemType = prefabsList[index];
        int poolIndex = -1;
        for (int i = 0; i < pool.Count; i++)
        {
            GameObject poolItem = pool[i];
            PrefabInfo pt = poolItem.GetComponent<PrefabInfo>();
            if (pt != null)
            {
                //Debug.Log(pt + " - "+ (pt.prefab == itemType) +" - "+itemType);
                if (pt.prefab == itemType)
                {
                    poolIndex = i;
                    break;
                }
            }
        }

        if (poolIndex == -1)
        {
            item = CreateItem(itemType);
            pool.Add(item);
            item.transform.SetParent(_tr, false);
            poolIndex = pool.IndexOf(item);
        }

        item = pool[poolIndex];
        pool.RemoveAt(poolIndex);

        return item;
    }

    public GameObject GetItem(bool isRandom = false)
    {
        GameObject item;
        if (pool.Count == 0)
        {
            item = CreateItem(prefabsList[Random.Range(0, prefabsList.Count)]);
            pool.Add(item);
            item.transform.SetParent(_tr, false);
        }

        int index = 0;
        if (isRandom)
        {
            index = Random.Range(0, pool.Count);
        }

        item = pool[index];
        pool.RemoveAt(index);

        return item;
    }

    public void ReturnItem(GameObject item)
    {
        if (pool == null)
        {
            return;
        }

        // remove item's doubles before adding
        checkPoolItemExist(item);
        pool.Add(item);
        item.transform.SetParent(_tr, false);
    }

    private void checkPoolItemExist(GameObject item)
    {
        int index = pool.IndexOf(item);
        if (index >= 0)
        {
            pool.RemoveAt(index);
            checkPoolItemExist(item);
        }
    }
}