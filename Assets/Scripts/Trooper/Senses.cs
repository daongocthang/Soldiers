using System;
using UnityEngine;

namespace Trooper
{
    public class Senses : MonoBehaviour
    {
        [SerializeField] private Transform pivot;

        public bool SeeEnemy(Warrior self)
        {
            var startPos = pivot.position;
            var endPos = startPos + Vector3.right * self.attackRange;
            var hit = Physics2D.Linecast(startPos, endPos);

            if (hit.collider == null) return false;

            if (!hit.collider.gameObject.TryGetComponent<Warrior>(out var other)) return false;

            return other.isEnemy != self.isEnemy;
        }
    }
}