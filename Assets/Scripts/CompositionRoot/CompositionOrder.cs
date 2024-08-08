//using SampleArcade.Timer;
//using System.Collections.Generic;
//using UnityEngine;

//namespace SampleArcade.CompositionRoot
//{
//    public class CompositionOrder : MonoBehaviour
//    {
//        [SerializeField]
//        private List<CharacterCompositionRoot> _order;

//        protected void Awake()
//        {
//            ITimer timer = new UnityTimer();

//            foreach (var compositionRoot in _order)
//            {
//                compositionRoot.Compose(timer);
//            }
//        }
//    }
//}
