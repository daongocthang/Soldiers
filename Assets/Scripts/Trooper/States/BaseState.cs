using FSM;
using UnityEngine;

namespace Trooper.States
{
    public class BaseState : EntityState
    {

        protected readonly Trooper self;
        protected float startTime;
        public BaseState(Entity entity, string animBoolName) : base(entity, animBoolName)
        {
            self = entity as Trooper;
        }

        public override void Enter()
        {
            base.Enter();
            startTime = Time.time;
        }
    }
}