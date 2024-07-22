using UnityEngine;

namespace SampleArcade.Movement
{
    public class PlayerSprintingController : MonoBehaviour, ISprintingSource
    {

        public bool IsSprinting { get; private set; }

        protected void Update()
        {
            IsSprinting = Input.GetKey(KeyCode.Space);
        }
    }
}