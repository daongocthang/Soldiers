using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public List<Boid> boids { get; set; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Update()
        {
            var ls = GameObject.FindObjectsByType<Boid>(FindObjectsSortMode.None).ToList();
        }
    }
}