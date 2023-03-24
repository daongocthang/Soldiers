using FSM;
using Trooper.States;
using Utils;

namespace Trooper.Player.States
{
    public class PlayerMoveState : BaseState
    {
        private bool hasEnemy;

        public PlayerMoveState(Entity entity, string animBoolName) : base(entity, animBoolName)
        {
        }

        public override void DoChecks()
        {
            hasEnemy = self.target != null;
        }

        public override void LogicUpdate()
        {
            if (self.target)
            {
                var pos = self.target.position;
                if (Utils2D.Distance2(self.position, pos) < 0.01f)
                {
                    stateMachine.ChangeState<PlayerIdleState>();
                }
                else
                {
                    self.LookAt(pos);
                    self.MoveTo(pos);
                }
            }
            else
            {
                stateMachine.ChangeState<PlayerIdleState>();
            }
        }
    }
}