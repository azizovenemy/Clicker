using UnityEngine;

namespace CodeBase
{
    public static class VectorExtensions
    {
        public static Vector3 AddY(this Vector3 vector, float height)
        {
            vector.y += height;
            return vector;
        }
    }
}