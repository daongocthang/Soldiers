using FSM.Interfaces;
using UnityEngine;

namespace FSM
{
    public class Entity : MonoBehaviour, ITriggerable
    {
        public FiniteStateMachine stateMachine { get; private set; }

        public Animator anim { get; private set; }

        

        public virtual void Awake()
        {
            stateMachine = new FiniteStateMachine();

            //TODO: Initialize entity states
        }

        public virtual void Start()
        {
            anim = GetComponentInChildren<Animator>();

            //TODO: Start State Machine
        }

        public virtual void Update()
        {
            stateMachine.currentState.LogicUpdate();            
        }

        public virtual void FixedUpdate()
        {
            stateMachine.currentState.PhysicsUpdate();
        }
        public virtual void OnTriggered(int resultCode)
        {
            stateMachine.currentState.AnimTrigger(resultCode);
        }
        
    }
}