using BehaviorDesigner.Runtime.Tasks;
namespace CoreBT
{
    [TaskCategory("Extension")]
    public class ChangeState : Action
    {
        private StateMachineController Controller;
        public NameState nameState;
        public int idState;
        public bool forceChangeState;
        public override void OnAwake()
        {
            base.OnAwake();
            Controller = transform.GetComponent<StateMachineController>();
        }

        public override void OnStart()
        {
            base.OnStart();
            Controller.ChangeState(nameState, idState, forceChangeState);
        }
    }
}