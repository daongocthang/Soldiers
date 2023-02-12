using System;

namespace FSM
{
    public class EntityState
    {
        protected readonly FiniteStateMachine stateMachine;
        protected readonly Entity entity;

        protected readonly string animBoolName;

        public EntityState(Entity entity, string animBoolName)
        {
            this.entity = entity;
            this.animBoolName = animBoolName;

            this.stateMachine = entity.stateMachine;
            stateMachine.AddState(this);
        }

        public virtual void Enter()
        {
            entity.anim?.SetBool(animBoolName, true);
            DoChecks();
        }

        public virtual void Exit()
        {
            entity.anim?.SetBool(animBoolName, false);
        }

        public virtual void LogicUpdate()
        {
        }

        public virtual void PhysicsUpdate()
        {
            DoChecks();
        }

        public virtual void DoChecks()
        {
        }

        public virtual void AnimTrigger(int resultCode)
        {
        }
    }
}