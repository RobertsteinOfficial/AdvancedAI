using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable AI/AI Actions/Work")]
public class WorkAction : AiAction
{
    int destinationIndex = 0;
    bool jobDone;
    float jobTimer = 5;
    float jobTime;


    public override void Act(AiController controller)
    {
        controller.myActualSatiety -= Time.deltaTime;

        if (Vector3.Distance(controller.transform.position, controller.workingPositions[destinationIndex].transform.position) < 3 && !jobDone)
        {
            if (!AreaManager.GetArea(controller.transform.position, 1))
            {
                jobTime += Time.deltaTime;
                controller.myActualStamina -= Time.deltaTime;
                


                if (jobTime < jobTimer)
                {
                    return;
                }
            }

            jobTime = 0;
            destinationIndex++;
            if (destinationIndex >= controller.workingPositions.Length)
            {
                destinationIndex = 0;
                //destinationReached = false;
            }
        }

        controller.transform.position = Vector3.MoveTowards(controller.transform.position, controller.workingPositions[destinationIndex].transform.position, controller.myStats.walkSpeed * Time.deltaTime);
        controller.transform.LookAt(controller.workingPositions[destinationIndex].transform.position);
    }
}
