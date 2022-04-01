using System;
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
    public List<IAutoAdd<GameEntity>> AutoAdds = new List<IAutoAdd<GameEntity>>();
    
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
            entity = Contexts.sharedInstance.game.CreateEntity();
            link = gameObject.Link(entity);
            ComponentManagerUtils.AddComponent(this);
            var components = GetComponentsInChildren<IAutoAdd<GameEntity>>();
            foreach (var component in components)
            {
                if(AutoAdds.Contains(component)) continue;
                AutoAdds.Add(component);
            }
            foreach (var component in AutoAdds)
            {
                component.AddComponent(ref entity);
                ComponentManagerUtils.AddComponent(this);
            }
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
