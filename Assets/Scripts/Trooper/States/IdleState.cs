using FSM;
using UnityEngine;

namespace Trooper.States
{
    public class IdleState : BaseState
    {
        public IdleState(Entity entity, string animBoolName) : base(entity, animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (Time.time > startTime + self.waitTime)
            {
                stateMachine.ChangeState<WanderState>();
            }
        }
    }
}