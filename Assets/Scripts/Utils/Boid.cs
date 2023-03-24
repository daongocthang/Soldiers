using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public class Boid : MonoBehaviour
    {
        public const float MAX_FORCE = 5.4f;
        public const float MAX_VELOCITY = 3f;

        // Leader sight evasion
        public const float LEADER_BEHIND_DIST = 50f;
        public const float LEADER_SIGHT_RADIUS = 30f;

        // Separation
        public const float MAX_SEPARATION = 2.0f;
        public const float SEPARATION_RADIUS = 50f;

        // Wander
        public const float CIRCLE_DISTANCE = 6f;
        public const float CIRCLE_RADIUS = 8f;
        public const float ANGLE_CHANGE = 1f;

        public bool isLeader;


        private Vector3 steering;
        private float mass;

        public Vector3 velocity { get; set; }
        public Vector3 position
        {
            get => transform.position;
            set => transform.position = value;
        }

        private void Start()
        {
            velocity = new Vector3(-1, -2);
            steering = new Vector3();
        }

        private void Update()
        {
            steering = Truncate(steering, MAX_FORCE);
            steering = VectorUtils.Scale(steering, 1 / mass);
            velocity += steering;
            velocity = Truncate(velocity, (isLeader ? 1f : 0.7f + Random.value * 0.3f));

            position += velocity;
        }

        public void MoveTo(Vector3 target)
        {
            steering += Arrive(target, 50);
        }

        public void FollowLeader(IEnumerable<Boid> boids)
        {
            var myTeam = boids as Boid[] ?? boids.ToArray();
            var leader = myTeam.FirstOrDefault(b => b.isLeader);
            if (leader == null) return;

            steering += Follow(leader, myTeam);
        }


        private Vector3 Seek(Vector3 target, float slowingRadius = 0)
        {
            var desired = target - position;

            var distance = Vector3.Magnitude(desired);
            var acceleration = (distance > slowingRadius) ? 1 : distance / slowingRadius;
            return VectorUtils.Scale(desired.normalized, MAX_VELOCITY * acceleration) - velocity;
        }

        private Vector3 Arrive(Vector3 target, float slowingRadius = 200)
        {
            return Seek(target, slowingRadius);
        }

        private Vector3 Flee(Vector3 target)
        {
            var desired = position - target;
            return VectorUtils.Scale(desired.normalized, MAX_VELOCITY) - velocity;
        }

        private Vector3 Evade(Boid target)
        {
            var dist = target.position - position;
            var updatesNeeded = dist.magnitude / MAX_VELOCITY;
            var targetFuturePosition = target.position + VectorUtils.Scale(target.velocity, updatesNeeded);
            return Flee(targetFuturePosition);
        }

        public Vector3 Wander()
        {
            var circleCenter = VectorUtils.Scale(velocity.normalized, CIRCLE_DISTANCE);
            var displacement = VectorUtils.Scale(new Vector3(0, -1), CIRCLE_RADIUS);

            return circleCenter + displacement;
        }

        private Vector3 Separation(IEnumerable<Boid> boids)
        {
            var force = new Vector3();
            var neighborCount = 0;
            foreach (var b in boids)
            {
                if (b == this || DistanceTo(b) > SEPARATION_RADIUS) continue;
                force.x += b.position.x - position.x;
                force.y += b.position.y - position.y;
                neighborCount++;
            }

            if (neighborCount != 0)
            {
                force.x /= neighborCount;
                force.y /= neighborCount;

                force = VectorUtils.Scale(force, -1);
            }

            return VectorUtils.Scale(force.normalized, MAX_SEPARATION);
        }

        public Vector3 Follow(Boid leader, IEnumerable<Boid> boids)
        {
            var force = new Vector3();
            var tv = VectorUtils.Scale(leader.velocity.normalized, LEADER_BEHIND_DIST);

            var ahead = leader.position + tv;
            tv = VectorUtils.Scale(tv, -1);
            var behind = leader.position + tv;


            if (IsOnLeaderSight(leader, ahead))
            {
                force += Evade(leader);
                force = VectorUtils.Scale(force, 1.8f);
            }

            force += Arrive(behind, 50);
            force += Separation(boids);

            return force;
        }

        private bool IsOnLeaderSight(Boid leader, Vector3 leaderAhead)
        {
            return Utils2D.Distance2(leaderAhead, position) <= LEADER_SIGHT_RADIUS ||
                   Utils2D.Distance2(leader.position, position) <= LEADER_SIGHT_RADIUS;
        }

        private static Vector3 Truncate(Vector3 vector, float maxMagnitude)
        {
            var i = maxMagnitude / vector.magnitude;
            i = i < 1f ? i : 1f;
            return VectorUtils.Scale(vector, i);
        }

        private float DistanceTo(Boid other)
        {
            return Utils2D.Distance2(position, other.position);
        }
    }
}