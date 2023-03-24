using FSM;
using UnityEngine;

namespace Trooper.States
{
    public class BaseState : EntityState
    {

        protected readonly Warrior self;
        protected float startTime;
        public BaseState(Entity entity, string animBoolName) : base(entity, animBoolName)
        {
            self = entity as Warrior;
        }

        public override void Enter()
        {
            base.Enter();
            startTime = Time.time;
        }
    }
}