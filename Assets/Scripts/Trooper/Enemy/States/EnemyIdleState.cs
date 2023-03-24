using FSM;
using Trooper.Player.States;
using Trooper.States;
using UnityEngine;
using Utils;

namespace Trooper.Enemy.States
{
    public class EnemyIdleState:BaseState
    {
        public EnemyIdleState(Entity entity, string animBoolName) : base(entity, animBoolName)
        {
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (Time.time > startTime + self.waitTime)
            {
                if (self.target && Utils2D.Distance2(self.position, self.target.position) > 0.01)
                    stateMachine.ChangeState<PlayerMoveState>();
            }
        }
    }
}