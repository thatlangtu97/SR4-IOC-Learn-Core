﻿using System;
using System.Collections;
using System.Collections.Generic;
using Entitas.Unity;
using Sirenix.OdinInspector;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
    public GameEntity entity;
    public EntityLink link;
    public ProjectileMovement projectileMovement;
    public ProjectileCollider projectileCollider;
    [ShowInInspector]
    public List<AutoAddComponent> AutoAdds = new List<AutoAddComponent>();
    [Button("FIND AUTO ADD COMPONENT", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    void FindComponentEntitas()
    { 
        var components = GetComponentsInChildren<AutoAddComponent>();
        foreach (var component in components)
        {
            if(AutoAdds.Contains(component)) continue;
            AutoAdds.Add(component);
        }
//        foreach (var component in components)
//        {
//            component.CoppyData();
//        }
    }
    private void Awake()
    {
        projectileMovement = GetComponent<ProjectileMovement>();
        projectileCollider = GetComponent<ProjectileCollider>();
        projectileCollider.component = this;
    }
    public void OnEnable()
    {
        if (entity == null)
        {
            //entity = ObjectPool.instance.SpawnEntity();
            entity = PoolManager.SpawnEntity();
            link = gameObject.Link(entity);
            foreach (var component in AutoAdds)
            {
                component.AddComponent(entity);
            }
            //ComponentManagerUtils.AddComponent(this);
        }
    }

    public void UpdateProjectile()
    {
        
    }
    public void OnDisable()
    {
        if (entity != null)
        {
            gameObject.Unlink();
            entity.RemoveAllComponents();
            entity.Destroy();
            entity = null;
            link = null;
        }
    }
    private void OnDestroy()
    {
        OnDisable();
    }
    
}
