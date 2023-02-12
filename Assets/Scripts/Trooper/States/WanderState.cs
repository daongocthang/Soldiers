using FSM;
using UnityEngine;
using Utils;

namespace Trooper.States
{
    public class WanderState : BaseState
    {
        private Vector3 _target;
        public WanderState(Entity entity, string animBoolName) : base(entity, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _target = Random.insideUnitCircle * self.radiusMoving;
            self.LookAt(_target);
        }

        public override void LogicUpdate()
        {
            self.MoveTo(_target);
            if (Utils2D.Distance2(self.position, _target) < 0.01f)
            {
                stateMachine.ChangeState<IdleState>();
            }
        }
    }
}