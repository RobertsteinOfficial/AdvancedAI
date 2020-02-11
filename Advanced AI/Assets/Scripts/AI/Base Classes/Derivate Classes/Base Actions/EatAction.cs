using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable AI/AI Actions/Eat")]

public class EatAction : AiAction
{
    public int eatSpeed = 10;

    public override void Act(AiController controller)
    {
        if (Vector3.Distance(controller.transform.position, controller.eatPosition.transform.position) > 1)
        {
            controller.transform.position = Vector3.MoveTowards(controller.transform.position, controller.eatPosition.transform.position, controller.myStats.walkSpeed * Time.deltaTime);
            controller.transform.LookAt(controller.eatPosition.transform.position);
            return;
        }

        controller.myActualSatiety += Time.deltaTime * eatSpeed;
        controller.myActualSatiety = Mathf.Clamp(controller.myActualSatiety, 0, 100);
    }
}
