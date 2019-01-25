using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Examples
{   
    public class ExampleUnityEvent : MonoBehaviour
    {
        //Example like a normal event.
        private UnityEvent testEvent;

        //Example using the inspector.
        [SerializeField] private UnityEvent testEvent2;

        //Example using an custom event.
        [SerializeField] private CustomEvent customEvent;

        private void Awake() 
        {
            if (testEvent == null)
                testEvent = new UnityEvent();

            if (testEvent2 == null)
                testEvent2 = new UnityEvent();

            if(customEvent == null) 
                customEvent = new CustomEvent();
        }

        private void OnEnable()
        {
            testEvent.AddListener(MyEvent);
        }

        private void OnDisable() 
        {
            testEvent.RemoveListener(MyEvent);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                testEvent.Invoke();
                testEvent2.Invoke();
                customEvent.Invoke("DeltaTime: " + Time.deltaTime.ToString());
            }
        }

        private void MyEvent()
        {
            Debug.Log("MyEvent");
        }

        //Note that this function is public inorder to select it in the inspector.
        public void MyEvent2()
        {
            Debug.Log("MyEvent2");
        }

        public void MyCustomEvent(string value)
        {
            Debug.Log(value);
        }   
    }

    [System.Serializable]
    public class CustomEvent : UnityEvent<string>
    {
        
    }
}
