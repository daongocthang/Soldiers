using System;
using System.Linq;
using FSM;
using SO;
using UnityEngine;
using Utils;

namespace Trooper
{
    public abstract class Warrior : Entity
    {
        private float rightFacing;
        private Rigidbody2D rb;

        [Header("Statistics")]
        public WarriorData data;
        public bool isEnemy = false;

        public float waitTime = 1f;
        public float radiusMoving = 5f;
        public float attackRange = 1f;
        public Transform pivot;
        public LayerMask whatIsTrooper;
        public Warrior target;


        public override void Start()
        {
            base.Start();
            rb = GetComponent<Rigidbody2D>();
            rightFacing = -1f;
        }

        public void MoveTo(Vector3 point)
        {
            position = Vector3.MoveTowards(position, point, Time.deltaTime * data.speed);
        }

        public void LookAt(Vector3 point)
        {
            var dir = Mathf.Lerp(-1f, 1f, point.x - position.x);

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
            var troopers = FindObjectsOfType<Warrior>();
            target = troopers.Where(other => other.isEnemy != isEnemy)
                .OrderBy(other => Utils2D.Distance2(other.position, position)).FirstOrDefault();
            return target != null;
        }

        public bool seeEnemy
        {
            get
            {
                var startPos = pivot.position;
                var endPos = startPos + (Vector3.right * rightFacing) * attackRange;
                var hit = Physics2D.Linecast(startPos, endPos, whatIsTrooper);

                Debug.DrawLine(startPos, endPos, Color.blue);

                if (hit.collider == null) return false;

                var other = hit.collider.gameObject.GetComponentInParent<Warrior>();

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