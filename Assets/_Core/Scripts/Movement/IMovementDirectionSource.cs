using UnityEngine;

namespace SampleArcade.Movement
{
    public interface IMovementDirectionSource
    {
        Vector3 MovementDirection { get; }
    }
}