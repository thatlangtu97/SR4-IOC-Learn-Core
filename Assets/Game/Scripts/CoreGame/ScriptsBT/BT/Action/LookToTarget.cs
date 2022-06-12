using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
namespace CoreBT
{
    [TaskCategory("Extension")]
    public class LookToTarget : Action
    {
        private SharedComponentManager componentManager;
        public SharedTransform Target;
        private ComponentManager Manager;

        public override void OnAwake()
        {
            base.OnAwake();
            Manager = GetComponent<ComponentManager>();
        }
//        public override void OnStart()
//        {
//            base.OnStart();
//        }
        public override TaskStatus OnUpdate()
        {
            Manager.OnInputChangeFacing(Target.Value);
            //componentManager.Value.OnInputChangeFacing();
            return TaskStatus.Success;
        }
    }
}