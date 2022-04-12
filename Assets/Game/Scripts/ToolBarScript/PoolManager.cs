using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class ObjectPool
    {
        public int size;
        public GameObject prefab;
    }
    public enum TypeSpawn
    {
        Default,
        NotDeactive,
        
    }

    #region POOL DEFAULT
    Dictionary<GameObject, List<GameObject>> pooledObjects = new Dictionary<GameObject, List<GameObject>>();
    Dictionary<GameObject, GameObject> spawnedObjects = new Dictionary<GameObject, GameObject>();
    public ObjectPool[] PoolDefault;
    #endregion
    
    #region POOL NOT DEACTIVE
    Dictionary<GameObject, List<GameObject>> pooledObjectsNotDeactive = new Dictionary<GameObject, List<GameObject>>();
    Dictionary<GameObject, GameObject> spawnedObjectsNotDeactive = new Dictionary<GameObject, GameObject>();
    public ObjectPool[] PoolNotDeactive;
    #endregion
    
    #region POOL ENTITY
    private static List<GameEntity> entites= new List<GameEntity>();
    #endregion
    
    #region INSTANCE
    static CompositeDisposable _disposable;
    static PoolManager _instance;
    public static PoolManager instance
    {
        get
        {
            if (_disposable == null)
            {
                _disposable= new CompositeDisposable();
            }
            
            if (_instance != null)
                return _instance;

            _instance = Object.FindObjectOfType<PoolManager>();
            if (_instance != null)
                return _instance;

            var obj = new GameObject("PoolManager");
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
            _instance = obj.AddComponent<PoolManager>();
            return _instance;
        }
    }
    #endregion
    
    #region PUBLIC STATIC METHOD
    public static T Spawn<T>(T prefab, Transform parent, Vector3 position, Quaternion rotation , TypeSpawn typeSpawn) where T : Component
    {
        switch (typeSpawn)
        {
            case TypeSpawn.Default:
                return Spawn(prefab.gameObject, parent, position, rotation).GetComponent<T>();
                break;
            case TypeSpawn.NotDeactive:
                return SpawnNotDeactive(prefab.gameObject, parent, position, rotation).GetComponent<T>();
                break;
        }
        return Spawn(prefab.gameObject, parent, position, rotation).GetComponent<T>();
    }

    public static T Spawn<T>(T prefab, Vector3 position, Quaternion rotation, TypeSpawn typeSpawn) where T : Component
    {
        switch (typeSpawn)
        {
            case TypeSpawn.Default:
                return Spawn(prefab.gameObject, null, position, rotation).GetComponent<T>();
                break;
            case TypeSpawn.NotDeactive:
                return SpawnNotDeactive(prefab.gameObject, null, position, rotation).GetComponent<T>();
                break;
        }
        return Spawn(prefab.gameObject, null, position, rotation).GetComponent<T>();
    }

    public static T Spawn<T>(T prefab, Transform parent, Vector3 position, TypeSpawn typeSpawn) where T : Component
    {        
        switch (typeSpawn)
        {
            case TypeSpawn.Default:
                return Spawn(prefab.gameObject, parent, position, Quaternion.identity).GetComponent<T>();
                break;
            case TypeSpawn.NotDeactive:
                return SpawnNotDeactive(prefab.gameObject, parent, position, Quaternion.identity).GetComponent<T>();
                break;
        }
        return Spawn(prefab.gameObject, parent, position, Quaternion.identity).GetComponent<T>();
    }

    public static T Spawn<T>(T prefab, Vector3 position, TypeSpawn typeSpawn) where T : Component
    {        
        switch (typeSpawn)
        {
            case TypeSpawn.Default:
                return Spawn(prefab.gameObject, null, position, Quaternion.identity).GetComponent<T>();
                break;
            case TypeSpawn.NotDeactive:
                return SpawnNotDeactive(prefab.gameObject, null, position, Quaternion.identity).GetComponent<T>();
                break;
        }
        return Spawn(prefab.gameObject, null, position, Quaternion.identity).GetComponent<T>();
    }

    public static T Spawn<T>(T prefab, Transform parent, TypeSpawn typeSpawn) where T : Component
    {        
        switch (typeSpawn)
        {
            case TypeSpawn.Default:
                return Spawn(prefab.gameObject, parent, Vector3.zero, Quaternion.identity).GetComponent<T>();
                break;
            case TypeSpawn.NotDeactive:
                return SpawnNotDeactive(prefab.gameObject, parent, Vector3.zero, Quaternion.identity).GetComponent<T>();
                break;
        }
        return Spawn(prefab.gameObject, parent, Vector3.zero, Quaternion.identity).GetComponent<T>();
    }

    public static T Spawn<T>(T prefab, TypeSpawn typeSpawn) where T : Component
    {        
        switch (typeSpawn)
        {
            case TypeSpawn.Default:
                return Spawn(prefab.gameObject, null, Vector3.zero, Quaternion.identity).GetComponent<T>();
                break;
            case TypeSpawn.NotDeactive:
                return SpawnNotDeactive(prefab.gameObject, null, Vector3.zero, Quaternion.identity).GetComponent<T>();
                break;
        }
        return Spawn(prefab.gameObject, null, Vector3.zero, Quaternion.identity).GetComponent<T>();
    }

    public static GameObject Spawn(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
    {
        if (instance.pooledObjects.TryGetValue(prefab, out List<GameObject> list))
        {
            GameObject obj = null;
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
                    transform.parent = parent;
                    transform.localPosition = position;
                    transform.localRotation = rotation;
                    transform.localScale = prefab.transform.localScale;
                    obj.SetActive(true);
                    instance.spawnedObjects.Add(obj, prefab);
                    return obj;
                }
            }
            obj = (GameObject)Object.Instantiate(prefab);
            transform = obj.transform;
            transform.parent = parent;
            transform.localPosition = position;
            transform.localRotation = rotation;
            transform.localScale = prefab.transform.localScale;
            instance.spawnedObjects.Add(obj, prefab);
            return obj;
        }
        else
        {
            CreatePool(prefab, 1);
            return Spawn(prefab, parent, position, rotation);
        }
    }

    public static GameObject SpawnNotDeactive(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
    {
        if (instance.pooledObjectsNotDeactive.TryGetValue(prefab, out List<GameObject> list))
        {
            GameObject obj = null;
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
                    transform.parent = parent;
                    transform.localPosition = position;
                    transform.localRotation = rotation;
                    transform.localScale = prefab.transform.localScale;
                    obj.SetActive(true);
                    instance.spawnedObjectsNotDeactive.Add(obj, prefab);
                    return obj;
                }
            }
            obj = (GameObject)Object.Instantiate(prefab);
            transform = obj.transform;
            transform.parent = parent;
            transform.localPosition = position;
            transform.localRotation = rotation;
            transform.localScale = prefab.transform.localScale;
            instance.spawnedObjectsNotDeactive.Add(obj, prefab);
            return obj;
        }
        else
        {
            CreatePoolNotDeactive(prefab, 1);
            return SpawnNotDeactive(prefab, parent, position, rotation);
        }
    }
    
    public static void CreatePool(GameObject prefab, int initialPoolSize)
    {
        if (prefab != null && !instance.pooledObjects.ContainsKey(prefab))
        {
            var list = new List<GameObject>();
            instance.pooledObjects.Add(prefab, list);
            if (initialPoolSize > 0)
            {
                bool active = prefab.activeSelf;
                prefab.SetActive(false);
                //Transform parent = instance.transform;
                while (list.Count < initialPoolSize)
                {
                    var obj = (GameObject)Object.Instantiate(prefab, null, true);
                    //obj.transform.SetParent(parent);
                    list.Add(obj);
                }
                
                prefab.SetActive(active);
            }
        }
    }
    
    public static void CreatePoolNotDeactive(GameObject prefab, int initialPoolSize)
    {
        if (prefab != null && !instance.pooledObjectsNotDeactive.ContainsKey(prefab))
        {
            var list = new List<GameObject>();
            instance.pooledObjectsNotDeactive.Add(prefab, list);
            if (initialPoolSize > 0)
            {
                
                prefab.SetActive(true);
                while (list.Count < initialPoolSize)
                {
                    var obj = (GameObject)Object.Instantiate(prefab, null, true);
                    obj.transform.position = new Vector3(10000f,0,0f);
                    list.Add(obj);
                }
            }
        }
    }

    public static void Recycle(GameObject gameObject, TypeSpawn typeSpawn)
    {   
        switch (typeSpawn)
        {
            case TypeSpawn.Default:
                if (instance.spawnedObjects.TryGetValue(gameObject, out GameObject prefabSpawnedObjects))
                {
                    gameObject.SetActive(false);
                    instance.pooledObjects[prefabSpawnedObjects].Add(gameObject);
                    instance.spawnedObjects.Remove(gameObject);
                }
                break;
            case TypeSpawn.NotDeactive:
                if (instance.spawnedObjectsNotDeactive.TryGetValue(gameObject, out GameObject prefabNotDeactive))
                {
                    instance.pooledObjectsNotDeactive[prefabNotDeactive].Add(gameObject);
                    instance.spawnedObjectsNotDeactive.Remove(gameObject);
                    gameObject.transform.position= new Vector3(10000f,0f,0f);
                }
                break;
        }
    }

    public static void Recycle(GameObject gameObject,float Time, TypeSpawn typeSpawn)
    {
        Observable.Timer(TimeSpan.FromSeconds(Time)).Subscribe(l => {  Recycle(gameObject,typeSpawn); }).AddTo(_disposable);
    }
    
    public static void RecycleNotDeactive(GameObject gameObject)
    {   
        if (instance.spawnedObjectsNotDeactive.TryGetValue(gameObject, out GameObject prefab))
        {
            instance.pooledObjectsNotDeactive[prefab].Add(gameObject);
            instance.spawnedObjectsNotDeactive.Remove(gameObject);
            gameObject.transform.position= new Vector3(10000f,0f,0f);
        }
    }
   
    public static void RecycleNotDeactive(GameObject gameObject,float Time)
    {
        Observable.Timer(TimeSpan.FromSeconds(Time)).Subscribe(l => {  RecycleNotDeactive(gameObject); }).AddTo(_disposable);
    }
    #endregion

    private void Awake()
    {
        if(instance==null)
            _instance = this;
        _disposable = new CompositeDisposable();

    }

    private void Start()
    {
        CreateStartupPools();
        CreateStartupPoolsNotDeactive();
        CreatePoolEntity(Contexts.sharedInstance, 100);
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
    
    public static void CreateStartupPools()
    {
        var pools = instance.PoolDefault;
        if (pools != null && pools.Length > 0)
            for (int i = 0; i < pools.Length; ++i) 
                CreatePool(pools[i].prefab, pools[i].size);
    }
    public static GameEntity SpawnEntity()
    {
        if (entites.Count == 0)
        {
            CreatePoolEntity(Contexts.sharedInstance, 30);
        }
        int indexE = entites.Count - 1;
        GameEntity temp = entites[indexE];
        entites.RemoveAt(indexE);
        return temp;
    }
    public static void RecycleEntity(GameEntity entity)
    {
        entity.RemoveAllComponents();
        entites.Add(entity);
    }
    public static void CreateStartupPoolsNotDeactive()
    {
        var pools = instance.PoolNotDeactive;
        if (pools != null && pools.Length > 0)
            for (int i = 0; i < pools.Length; ++i)
                CreatePoolNotDeactive(pools[i].prefab, pools[i].size);

    }
}
