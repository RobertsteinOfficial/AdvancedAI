using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate int OnExampleEvent();
    public static event OnExampleEvent onExampleEvent;

    public static Action onStart;


    void Start()
    {
        //onStart();


    }

    void TestMethod(Action toDo)
    {
        toDo.Invoke();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (onExampleEvent != null)
                onExampleEvent();

            TestMethod(delegate { });
            TestMethod(() => { });
        }
    }

    void MyMethod()
    {
        FindEnemy("enemy", () => { Debug.Log("EnemyFound"); });
        FindEnemy("player", FindEnemy("topo"));
        FindEnemy("enemy");
    }

    Action FindEnemy(string type, Action onEnd = null)
    {

        return null;
    }
}
