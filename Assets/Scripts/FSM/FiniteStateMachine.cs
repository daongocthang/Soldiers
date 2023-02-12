using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FSM
{
    public class FiniteStateMachine
    {
        public EntityState currentState { get; private set; }
        private readonly List<EntityState> _states = new List<EntityState>();

        public void Init<T>() where T : EntityState
        {
            currentState = GetState<T>();
            currentState.Enter();
        }

        public void AddState(EntityState state)
        {
            if (!_states.Contains(state))
            {
                _states.Add(state);
            }
        }

        public void ChangeState<T>() where T : EntityState
        {
            ChangeState(GetState<T>());
        }

        private void ChangeState(EntityState newState)
        {
            if (currentState == newState) return;
            currentState.Exit();
            currentState = newState;
            currentState.Enter();
        }

        private T GetState<T>() where T : EntityState
        {
            var state = _states.OfType<T>().FirstOrDefault();
            if (state == null)
            {
                Debug.LogWarning($"{typeof(T)} not found on {this.GetType().Name}");
            }

            return state;
        }
    }
}