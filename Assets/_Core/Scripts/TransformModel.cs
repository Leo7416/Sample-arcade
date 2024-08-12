using UnityEngine;

namespace SampleArcade
{
    public class TransformModel
    {
        public Vector3 Position { get; set; }

        public Quaternion Rotation { get; set; }

        public TransformModel(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}