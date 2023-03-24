using Trooper.Player.States;
using Unity.VisualScripting;
using Utils;

namespace Trooper.Player
{
    public class Hero : Warrior
    {
        public bool isLeader;
        public Boid boid { get; private set; }

        public override void Start()
        {
            base.Start();
            boid = GetComponent<Boid>();
        }

        protected override void OnCreateStates()
        {
            new PlayerMoveState(this, "move");
        }
    }
}