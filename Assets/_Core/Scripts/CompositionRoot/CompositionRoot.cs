using SampleArcade.Timer;
using UnityEngine;

namespace SampleArcade.CompositionRoot
{
    public abstract class CompositionRoot<T>: MonoBehaviour where T : MonoBehaviour
    {
        public abstract T Compose(ITimer timer);
    }
}