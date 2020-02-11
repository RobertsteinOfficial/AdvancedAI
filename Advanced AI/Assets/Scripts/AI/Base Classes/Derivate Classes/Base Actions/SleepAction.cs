using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable AI/AI Actions/Sleep")]
public class SleepAction : AiAction
{
    public override void Act(AiController controller)
    {
        controller.myActualStamina += Time.deltaTime;
    }
}
