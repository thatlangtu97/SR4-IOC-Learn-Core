using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace CoreBT
{
    
    [TaskCategory("Extension")]
    public class CheckTarget : Conditional
    {
        public SharedTransform Owner;
        public SharedTransform Target;

        public Collider2D[] cols;
        public Vector2 sizeFindTarget;
        public LayerMask layerTarget;
        public override void OnAwake()
        {
            base.OnAwake();
            Owner.SetValue(transform); 
        }

        public override TaskStatus OnUpdate()
        {
            cols = null;
            cols = Physics2D.OverlapBoxAll(Owner.Value.transform.position , sizeFindTarget , 0, layerTarget);
            if (cols == null)
            {
                Target.SetValue(null);
                return TaskStatus.Failure;
            }
            else
            {
                foreach (var col in cols)
                {
                    if (col)
                    {
                        Target.SetValue(col.transform); 
                        break;
                    }
                }
                return TaskStatus.Success;
            }
        }
    }
}

