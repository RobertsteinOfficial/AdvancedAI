using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventTest : MonoBehaviour
{
    public UnityEvent MyEvent;


    void Start()
    {
    }

    private void OnEnable()
    {
        EventManager.onExampleEvent += MyMethod;
        //EventManager.onExampleEvent += MyMethod;
    }

    int MyMethod()
    {
        Debug.Log(this.name);
        return 0;
    }

    private void OnDisable()
    {
        EventManager.onExampleEvent -= MyMethod;
    }
}
