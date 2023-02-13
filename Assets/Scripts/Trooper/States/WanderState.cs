using FSM;
using UnityEngine;
using Utils;

namespace Trooper.States
{
    public class WanderState : BaseState
    {
        private Vector3 _destination;
        public WanderState(Entity entity, string animBoolName) : base(entity, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _destination = Random.insideUnitCircle * self.radiusMoving;
            self.LookAt(_destination);
        }

        public override void LogicUpdate()
        {
            self.MoveTo(_destination);
            if (Utils2D.Distance2(self.position, _destination) < 0.01f)
            {
                stateMachine.ChangeState<IdleState>();
            }
        }
    }
}