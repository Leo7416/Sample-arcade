using UnityEngine;

namespace SampleArcade.Timer
{
    public class UnityTimer: ITimer
    {
        public float DeltaTime => Time.deltaTime;
    }
}