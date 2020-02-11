using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable AI/AI Actions/Sleep")]
public class SleepAction : AiAction
{
    public int sleepSpeed = 10;

    public override void Act(AiController controller)
    {
        if (Vector3.Distance(controller.transform.position, controller.eatPosition.transform.position) > 1)
        {
            controller.transform.position = Vector3.MoveTowards(controller.transform.position, controller.sleepPosition.transform.position, controller.myStats.walkSpeed * Time.deltaTime);
            controller.transform.LookAt(controller.sleepPosition.transform.position);
            return;
        }

        controller.myActualStamina += Time.deltaTime * sleepSpeed;
        controller.myActualStamina = Mathf.Clamp(controller.myActualStamina, 0, 100);
    }
}
