using System;
using System.Linq;
using FSM;
using Trooper.States;
using UnityEngine;
using Utils;

namespace Trooper
{
    public class Trooper : Entity
    {
        public float speed = 1f;
        public float waitTime = 1f;
        public float radiusMoving = 5f;
        public float attackRange = 1f;
        public Transform pivot;
        public bool isEnemy = false;

        public Trooper target;

        public float rightFacing;

        public override void Awake()
        {
            base.Awake();

            new IdleState(this, "idle");
            new WanderState(this, "move");
            new ChaseState(this, "move");
        }

        public override void Start()
        {
            base.Start();
            rightFacing = -1f;

            stateMachine.Init<IdleState>();
        }

        public void MoveTo(Vector3 targetPos)
        {
            position = Vector3.MoveTowards(position, targetPos, speed * Time.deltaTime);
        }

        public void LookAt(Vector3 targetPos)
        {
            var dir = Mathf.Lerp(-1f, 1f, targetPos.x - position.x);

            if (Math.Abs(rightFacing - dir) > 0.0001f)
            {
                FlipHorizontal();
            }
        }

        public Vector3 position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public bool CheckEnemyExists()
        {
            var troopers = FindObjectsOfType<Trooper>();
            target = troopers.Where(other => other.isEnemy != isEnemy)
                .OrderBy(other => Utils2D.Distance2(other.position, position)).FirstOrDefault();
            return target != null;
        }

        public bool seeEnemy
        {
            get
            {
                var hit = Physics2D.Raycast(pivot.position, Vector2.right * rightFacing, attackRange);

                Debug.DrawRay(pivot.position, Vector3.right * rightFacing * attackRange, Color.blue);

                if (hit.collider == null) return false;

                var other = hit.collider.gameObject.GetComponentInParent<Trooper>();

                if (other == null) return false;

                return other.isEnemy != isEnemy;
            }
        }

        private void FlipHorizontal()
        {
            rightFacing *= -1;
            transform.Rotate(0f, 180f, 0f);
        }

        private void OnDrawGizmos()
        {
            // Gizmos.DrawWireSphere(position, radiusMoving);            
        }
    }
}