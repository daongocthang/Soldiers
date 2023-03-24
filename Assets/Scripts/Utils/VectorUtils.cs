using UnityEngine;

namespace Utils
{
    public static class VectorUtils
    {
        public static Vector3 Scale(Vector3 vector, float scalar)
        {
            return vector * scalar;
        }
    }
}