using System.Linq;
using FSM;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

namespace Trooper.States
{
    public class ChaseState : BaseState
    {
        private bool _hasEnemy;

        public ChaseState(Entity entity, string animBoolName) : base(entity, animBoolName)
        {
        }

        public override void DoChecks()
        {
            _hasEnemy = self.CheckEnemyExists();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (_hasEnemy)
            {
                self.LookAt(self.target.transform.position);

                if (self.seeEnemy)
                {
                    stateMachine.ChangeState<IdleState>();
                    Debug.Log("Has Collision");
                }
                else
                {
                    self.MoveTo(self.target.transform.position);    
                }
            }
            else
            {
                stateMachine.ChangeState<IdleState>();
            }
        }
    }
}