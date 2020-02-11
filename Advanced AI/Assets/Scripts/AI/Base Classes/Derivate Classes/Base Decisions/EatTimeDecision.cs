using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable AI/AI Decisions/Eat Time?")]
public class EatTimeDecision : AiDecision
{
    public override bool Decide(AiController controller)
    {
        switch (TimeManager.actualPhase)
        {
            default:
            case DayPhase.Morning:
            case DayPhase.Noon:
            case DayPhase.Evening:
            case DayPhase.Midnight:
                if (controller.myActualSatiety < controller.myStats.hungerThreshold)
                    return true;
                else
                    return false;
            case DayPhase.Afternoon:
            case DayPhase.Dawn:
            case DayPhase.Night:
            case DayPhase.Dusk:
                if (controller.myActualSatiety >= controller.myStats.hungerThreshold)
                    return false;
                else
                    return true;
        }
    }
}
