using System;
using FSM;
using Trooper.States;
using UnityEngine;

namespace Trooper
{
    public class Trooper : Entity
    {
        public float speed = 1f;
        public float waitTime = 1f;
        public float radiusMoving = 5f;
        public bool isEnemy = false;


        private float _rightFacing;
        public override void Awake()
        {
            base.Awake();

            new IdleState(this, "idle");
            new WanderState(this, "move");
        }

        public override void Start()
        {
            base.Start();
            _rightFacing = -1f;

            stateMachine.Init<IdleState>();
        }

        public void MoveTo(Vector3 targetPos)
        {
            position = Vector3.MoveTowards(position, targetPos, speed * Time.deltaTime);
        }


        public void LookAt(Vector3 targetPos)
        {
            var dir = Mathf.Lerp(-1f, 1f, targetPos.x - position.x);

            if (Math.Abs(_rightFacing - dir) > 0.0001f)
            {
                // Flip Horizontal
                _rightFacing *= -1;
                transform.Rotate(0f, 180f, 0f);
            }
        }

        public Vector3 position
        {
            get => transform.position;
            set => transform.position = value;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(position, radiusMoving);
        }
    }
}