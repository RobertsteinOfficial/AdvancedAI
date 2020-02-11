using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable AI/AI Actions/Debug")]
public class DebugAction : AiAction
{
    public string debugText;
    

    public override void Act(AiController controller)
    {
        //Debug.Log(debugText);
        if(controller.StateTimer > 5)
        {
            controller.myActualSatiety = 100;
            controller.myActualStamina = 100;
        }
    }
}
