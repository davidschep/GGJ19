using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Examples
{
    [RequireComponent(typeof(Collider))]
    public class OnTriggerEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent onTriggerEnterEvent;
        [SerializeField] private UnityEvent onTriggerStayEvent;
        [SerializeField] private UnityEvent onTriggerExitEvent;

        private void Awake() 
        {
            if(onTriggerEnterEvent == null)
                onTriggerEnterEvent = new UnityEvent();

            if(onTriggerStayEvent == null)
                onTriggerStayEvent = new UnityEvent();

            if(onTriggerExitEvent == null)
                onTriggerExitEvent = new UnityEvent();
        }

        private void OnTriggerEnter(Collider other) 
        {
            onTriggerEnterEvent.Invoke();
        }

        private void OnTriggerStay(Collider other) 
        {
            onTriggerStayEvent.Invoke();
        }

        private void OnTriggerExit(Collider other) 
        {
            onTriggerExitEvent.Invoke();
        }
    }
}