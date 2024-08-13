#if UNITY_ANDROID
using SampleArcade.GameManagers;
using SampleArcade.Movement;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SampleArcade.Player
{
    public class ButtonPlayerSprintingController : MonoBehaviour, ISprintingSource, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsSprinting { get; private set; }

        private void Start()
        {
            var sprintButton = GameManager.Instance.SprintButton;

            EventTrigger eventTrigger = sprintButton.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry entryDown = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
            entryDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
            eventTrigger.triggers.Add(entryDown);

            EventTrigger.Entry entryUp = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
            entryUp.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
            eventTrigger.triggers.Add(entryUp);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsSprinting = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsSprinting = false;
        }
    }
}
#endif