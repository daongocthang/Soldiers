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
        
        public override void LogicUpdate()
        {
            
        }
    }
}