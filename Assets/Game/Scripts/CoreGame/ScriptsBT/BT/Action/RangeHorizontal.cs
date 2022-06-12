using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
namespace CoreBT
{
    [TaskCategory("Extension")]
    public class RangeHorizontal : Action
    {
        public SharedTransform Target;
        public SharedTransform Owner;
        public SharedFloat rangeToEnemy;
        public override void OnStart()
        {
            base.OnStart();
            float space = Target.Value.position.x - Owner.Value.position.x;
            rangeToEnemy.Value = Mathf.Abs(space);
        }
    }
}
