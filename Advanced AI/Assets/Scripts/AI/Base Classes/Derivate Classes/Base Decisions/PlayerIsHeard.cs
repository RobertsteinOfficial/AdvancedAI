using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable AI/AI Decisions/Player is Heard")]
public class PlayerIsHeard : AiDecision
{
    public override bool Decide(AiController controller)
    {
        float distanceToPlayer = Vector3.Distance(controller.transform.position,
                                PlayerController.PlayerTransform.position);

        if (distanceToPlayer < controller.myStats.hearRange)
            return true;
        else
            return false;
    }

    
}
