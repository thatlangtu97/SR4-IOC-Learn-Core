using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

public class PoolManager : MonoBehaviour
{
    #region MODEL
    [System.Serializable]
    public class ObjectPool
    {
        public int size;
        public PoolItem prefab;

        public ObjectPool()
        {
            
        }
        public ObjectPool(ObjectPool clone)
        {
            size = clone.size;
            prefab = clone.prefab;
        }
    }
    [System.Serializable]
    public class PoolList
    {
        [FoldoutGroup("$name", expanded: true)]
        public string name;
        [FoldoutGroup("$name", expanded: true)]
        public ObjectPool[] ListPrefab;

        public PoolList()
        {
            
        }

        public PoolList(PoolList clone)
        {
            name = clone.name;
            ListPrefab = new ObjectPool[clone.ListPrefab.Length];
            int count = 0;
            foreach (var poolListItem in clone.ListPrefab)
            {
                ListPrefab[count] = new ObjectPool(poolListItem);
                count += 1;
            }
        }
    }
    #endregion
    
    #region POOL DEFAULT
    
    Dictionary<GameObject, List<PoolItem>> pooledObjects = new Dictionary<GameObject, List<PoolItem>>();
    Dictionary<PoolItem, GameObject> spawnedObjects = new Dictionary<PoolItem, GameObject>();
    public ObjectPool[] PoolDefault;
    public PoolList[] poolLists;
    //public ZoneData zodeData; 
    #endregion

    #region POOL ENTITY
    [ShowInInspector]
    private static List<GameEntity> entites= new List<GameEntity>();
    #endregion
    
    #region INSTANCE
    //static CompositeDisposable _disposable;
    static PoolManager _instance;
    public static PoolManager instance
    {
        get
        {
//            if (_disposable == null)
//            {
//                _disposable= new CompositeDisposable();
//            }
            return _instance;
//            if (_instance != null)
//                return _instance;
//
//            _instance = Object.FindObjectOfType<PoolManager>();
//            if (_instance != null)
//                return _instance;
//
//            var obj = new GameObject("PoolManager");
//            obj.transform.localPosition = Vector3.zero;
//            obj.transform.localRotation = Quaternion.identity;
//            obj.transform.localScale = Vector3.one;
//            _instance = obj.AddComponent<PoolManager>();
//            Debug.Log("create instance");
//            return _instance;
        }
    }
    #endregion
    
    #region PUBLIC STATIC METHOD
    public static T Spawn<T>(T prefab, Transform parent, Vector3 position, Vector3 rotation,Vector3 localScale) where T : Component
    {
        return SpawnPoolItem(prefab.gameObject, parent, position, rotation, localScale).GetComponent<T>();
    }
    public static T Spawn<T>(T prefab, Transform parent, Vector3 position, Quaternion rotation,Vector3 localScale) where T : Component
    {
        return SpawnPoolItem(prefab.gameObject, parent, position, rotation, localScale).GetComponent<T>();
    }
    
    public static T Spawn<T>(T prefab, Transform parent, Vector3 position, Quaternion rotation) where T : Component
    {
        return SpawnPoolItem(prefab.gameObject, parent, position, rotation).GetComponent<T>();
    }

    public static T Spawn<T>(T prefab, Vector3 position, Quaternion rotation) where T : Component
    {
        return SpawnPoolItem(prefab.gameObject, null, position, rotation).GetComponent<T>();
    }

    public static T Spawn<T>(T prefab, Transform parent, Vector3 position) where T : Component
    {
        return SpawnPoolItem(prefab.gameObject, parent, position, Quaternion.identity).GetComponent<T>();
    }

    public static T Spawn<T>(T prefab, Vector3 position) where T : Component
    {
        return SpawnPoolItem(prefab.gameObject, null, position, Quaternion.identity).GetComponent<T>();
    }

    public static T Spawn<T>(T prefab, Transform parent) where T : Component
    {
        return SpawnPoolItem(prefab.gameObject, parent, Vector3.zero, Quaternion.identity).GetComponent<T>();
    }

    public static T Spawn<T>(T prefab) where T : Component
    {
        return SpawnPoolItem(prefab.gameObject, null, Vector3.zero, Quaternion.identity).GetComponent<T>();
    }

    static PoolItem SpawnPoolItem(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
    {
        if (instance.pooledObjects.TryGetValue(prefab, out List<PoolItem> list))
        {
            PoolItem obj = null;
            Transform transform;
            if (list.Count > 0)
            {
                while (obj == null && list.Count > 0)
                {
                    obj = list[0];
                    list.RemoveAt(0);
                    break;
                }
                if (obj != null)
                {
                    transform = obj.transform;
                    if(parent)
                    
                    
                    
                    if(transform.parent != null)
                        transform.parent = parent;
                    if (parent != null)
                    {
                        transform.parent = parent;
                    }
                    transform.localPosition = position;
                    transform.localRotation = rotation;
                    transform.localScale = prefab.transform.localScale;
                    obj.Spawn();
                    instance.spawnedObjects.Add(obj, prefab);
                    return obj;
                }
            }
            
            obj = Object.Instantiate(prefab).GetComponent<PoolItem>();
            obj.Create();
            transform = obj.transform;
             if(transform.parent != null)
                transform.parent = parent;
             if (parent != null)
             {
                 transform.parent = parent;
             }
            transform.localPosition = position;
            transform.localRotation = rotation;
            transform.localScale = prefab.transform.localScale;
            obj.Spawn();
            instance.spawnedObjects.Add(obj, prefab);
            return obj;
        }
        else
        {
            CreatePool(prefab, 1);
            return SpawnPoolItem(prefab, parent, position, rotation);
        }
    }
    static PoolItem SpawnPoolItem(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation, Vector3 localScale)
    {
        if (instance.pooledObjects.TryGetValue(prefab, out List<PoolItem> list))
        {
            PoolItem obj = null;
            Transform transform;
            if (list.Count > 0)
            {
                while (obj == null && list.Count > 0)
                {
                    obj = list[0];
                    list.RemoveAt(0);
                    break;
                }
                if (obj != null)
                {
                    transform = obj.transform;
                    if(transform.parent != null)
                        transform.parent = parent;
                    if (parent != null)
                    {
                        transform.parent = parent;
                    }
                    transform.localPosition = position;
                    transform.localRotation = rotation;
                    transform.localScale = localScale;
                    obj.Spawn();
                    instance.spawnedObjects.Add(obj, prefab);
                    return obj;
                }
            }
            
            obj = Object.Instantiate(prefab).GetComponent<PoolItem>();
            obj.Create();
            transform = obj.transform;
            if(transform.parent != null)
                transform.parent = parent;
            if (parent != null)
            {
                transform.parent = parent;
            }
            transform.localPosition = position;
            transform.localRotation = rotation;
            transform.localScale = localScale;
            obj.Spawn();
            instance.spawnedObjects.Add(obj, prefab);
            return obj;
        }
        else
        {
            CreatePool(prefab, 1);
            return SpawnPoolItem(prefab, parent, position, rotation, localScale);
        }
    }
    
    static PoolItem SpawnPoolItem(GameObject prefab, Transform parent, Vector3 localPosition, Vector3 rightTransform , Vector3 localScale)
    {
        if (instance.pooledObjects.TryGetValue(prefab, out List<PoolItem> list))
        {
            PoolItem obj = null;
            Transform transform;
            if (list.Count > 0)
            {
                while (obj == null && list.Count > 0)
                {
                    obj = list[0];
                    list.RemoveAt(0);
                    break;
                }
                if (obj != null)
                {
                    transform = obj.transform;
                    //transform.parent = parent;
                    if(transform.parent != null)
                        transform.parent = parent;
                    if (parent != null)
                    {
                        transform.parent = parent;
                    }
                    transform.localPosition = localPosition;
                    transform.right = rightTransform;
                    transform.localScale = localScale;
                    obj.Spawn();
                    instance.spawnedObjects.Add(obj, prefab);
                    return obj;
                }
            }
            obj = Object.Instantiate(prefab).GetComponent<PoolItem>();;
            obj.Create();
            transform = obj.transform;
            //transform.parent = parent;
            if(transform.parent != null)
                transform.parent = parent;
            if (parent != null)
            {
                transform.parent = parent;
            }
            transform.localPosition = localPosition;
            transform.right = rightTransform;
            transform.localScale = localScale;
            obj.Spawn();
            instance.spawnedObjects.Add(obj, prefab);
            return obj;
        }
        else
        {
            CreatePool(prefab, 1);
            return SpawnPoolItem(prefab, parent, localPosition, rightTransform, localScale);
        }
    }
    public static void CreatePool(GameObject prefab, int initialPoolSize)
    {
        if (prefab != null && !instance.pooledObjects.ContainsKey(prefab))
        {
            var list = new List<PoolItem>();
            instance.pooledObjects.Add(prefab, list);
            if (initialPoolSize > 0)
            {
                //Transform parent = instance.transform;
                while (list.Count < initialPoolSize)
                {
                    var obj = Object.Instantiate(prefab).GetComponent<PoolItem>();
                    obj.Create();
                    //obj.transform.SetParent(instance.transform);
                    list.Add(obj);
                }
            }
        }
    }

    public static void Recycle(PoolItem gameObject)
    {   

        if (instance.spawnedObjects.TryGetValue(gameObject, out GameObject prefabSpawnedObjects))
        {
            gameObject.Recycle();
            //gameObject.transform.parent = instance.transform;
            instance.pooledObjects[prefabSpawnedObjects].Add(gameObject);
            instance.spawnedObjects.Remove(gameObject);
        }

    }

    public static void Recycle(PoolItem gameObject,float time)
    {
        //Observable.Timer(TimeSpan.FromSeconds(time)).Subscribe(l => {  Recycle(gameObject); }).AddTo(_disposable);
        Action tempAction = delegate { Recycle(gameObject);};
        
        ActionBufferManager.Instance.ActionDelayTime(tempAction,time);
    }

    
    public static void CreatePoolsList()
    {
        var pools = instance.poolLists;
        //var zoneData = instance.zodeData;
        var zoneData = LevelMapConfigManager.Instance.zoneData;
        List<PoolList> tempListPooled = new List<PoolList>();
        if (pools != null && pools.Length > 0)
        {
            foreach (var VARIABLE in pools)
            {
                for (int i = 0; i < VARIABLE.ListPrefab.Length; ++i)
                {
                    if (zoneData == null)
                        CreatePool(VARIABLE.ListPrefab[i].prefab.gameObject, VARIABLE.ListPrefab[i].size);
                    else
                    {

                        foreach (var zone in zoneData.poolSizeData)
                        {
                            if (VARIABLE.ListPrefab[i].prefab.name == zone.name)
                            {
                                tempListPooled.Add(VARIABLE);
                                CreatePool(VARIABLE.ListPrefab[i].prefab.gameObject,
                                    VARIABLE.ListPrefab[i].size * zone.size);
                            }
                        }

                    }
                }
            }
            foreach (var VARIABLE in pools)
            {
                if(!tempListPooled.Contains(VARIABLE))
                {
                    for (int i = 0; i < VARIABLE.ListPrefab.Length; ++i) 
                    {
                        CreatePool(VARIABLE.ListPrefab[i].prefab.gameObject, VARIABLE.ListPrefab[i].size);
                    }
                }
            }
        }
    }
    
    public static void CreateStartupPools()
    {
        var pools = instance.PoolDefault;
        if (pools != null && pools.Length > 0)
            for (int i = 0; i < pools.Length; ++i) 
                CreatePool(pools[i].prefab.gameObject, pools[i].size);
    }
    
    public static void CreatePoolEntity(Contexts context, int size)
    {
        int count = 0;
        while (count<size)
        {
            GameEntity temp = context.game.CreateEntity(); 
            entites.Add(temp);
            count += 1;
        }
    }

    public static GameEntity SpawnEntity()
    {
        if (entites.Count == 0)
        {
            CreatePoolEntity(Contexts.sharedInstance, 30);
        }
        GameEntity temp = entites[0];
        entites.RemoveAt(0);
        return temp;
    }
    
    public static void RecycleEntity(GameEntity entity)
    {
        entity.RemoveAllComponents();
        entites.Add(entity);
    }
    public static void DestroyAllEntity()
    {
        while (entites.Count > 0)
        {
            GameEntity temp = entites[0];
            try
            {
                if(temp!=null)
                    temp.Destroy();
            }
            catch (Exception e)
            {
            }

            entites.RemoveAt(0);
        }
    }
    public static void RecycleAllEntity()
    {
        while (entites.Count > 0)
        {
            GameEntity temp = entites[0];
            temp.Destroy();
            entites.RemoveAt(0);
        }
    }
    #endregion

    #region UNITY METHOD
    private void Awake()
    {
        if(instance==null)
            _instance = this;
        //zodeData = Resources.Load<ZoneData>("ZoneData/ZoneData1");
        //_disposable = new CompositeDisposable();

    }

    private void Start()
    {
        CreatePoolsList();
        CreateStartupPools();
        CreatePoolEntity(Contexts.sharedInstance, 100);
    }

    #endregion
   
}
