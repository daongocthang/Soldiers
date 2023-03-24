using Trooper.Player.States;

namespace Trooper.Player
{
    public class Hero : Warrior
    {
        protected override void OnCreateStates()
        {
            new PlayerIdleState(this, "idle");
            new PlayerMoveState(this, "move");
        }
    }
}