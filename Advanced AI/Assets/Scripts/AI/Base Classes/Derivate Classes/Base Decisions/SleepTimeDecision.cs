using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable AI/AI Decisions/Sleep Time?")]
public class SleepTimeDecision : AiDecision
{
    public override bool Decide(AiController controller)
    {
        switch (TimeManager.actualPhase)
        {
            default:
            case DayPhase.Night:
            case DayPhase.Evening:
            case DayPhase.Midnight:
            case DayPhase.Dusk:
                if (controller.myActualStamina < controller.myStats.tiredThreshold)
                    return true;
                else
                    return false;
            case DayPhase.Dawn:
            case DayPhase.Morning:
            case DayPhase.Noon:
            case DayPhase.Afternoon:
                if (controller.myActualStamina >= controller.myStats.tiredThreshold)
                    return false;
                else
                    return true;
        }
    }
}
