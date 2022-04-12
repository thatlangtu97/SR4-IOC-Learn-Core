﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UniRx;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class ObjectPool
    {
        public int size;
        public PoolItem prefab;
    }
    
    #region POOL DEFAULT
    Dictionary<GameObject, List<PoolItem>> pooledObjects = new Dictionary<GameObject, List<PoolItem>>();
    Dictionary<PoolItem, GameObject> spawnedObjects = new Dictionary<PoolItem, GameObject>();
    public ObjectPool[] PoolDefault;
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
    public static T Spawn<T>(T prefab, Transform parent, Vector3 position, Quaternion rotation) where T : Component
    {
        return Spawn(prefab.gameObject, parent, position, rotation).GetComponent<T>();
    }

    public static T Spawn<T>(T prefab, Vector3 position, Quaternion rotation) where T : Component
    {
        return Spawn(prefab.gameObject, null, position, rotation).GetComponent<T>();
    }

    public static T Spawn<T>(T prefab, Transform parent, Vector3 position) where T : Component
    {
        return Spawn(prefab.gameObject, parent, position, Quaternion.identity).GetComponent<T>();
    }

    public static T Spawn<T>(T prefab, Vector3 position) where T : Component
    {
        return Spawn(prefab.gameObject, null, position, Quaternion.identity).GetComponent<T>();
    }

    public static T Spawn<T>(T prefab, Transform parent) where T : Component
    {
        return Spawn(prefab.gameObject, parent, Vector3.zero, Quaternion.identity).GetComponent<T>();
    }

    public static T Spawn<T>(T prefab) where T : Component
    {
        return Spawn(prefab.gameObject, null, Vector3.zero, Quaternion.identity).GetComponent<T>();
    }

    public static PoolItem Spawn(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
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
                    transform.parent = parent;
                    transform.localPosition = position;
                    transform.localRotation = rotation;
                    transform.localScale = prefab.transform.localScale;
                    obj.Spawn();
                    instance.spawnedObjects.Add(obj, prefab);
                    return obj;
                }
            }
            Debug.Log("khong co prefab");
            obj = Object.Instantiate(prefab).GetComponent<PoolItem>();
            transform = obj.transform;
            transform.parent = parent;
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
            return Spawn(prefab, parent, position, rotation);
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
        Observable.Timer(TimeSpan.FromSeconds(time)).Subscribe(l => {  Recycle(gameObject); }).AddTo(_disposable);
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
    #endregion

    #region UNITY METHOD
    private void Awake()
    {
        if(instance==null)
            _instance = this;
        _disposable = new CompositeDisposable();

    }

    private void Start()
    {
        CreateStartupPools();
        CreatePoolEntity(Contexts.sharedInstance, 100);
    }
    #endregion
   
}
