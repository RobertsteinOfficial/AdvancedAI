using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable AI/AI Decisions/Work Time?")]
public class WorkTimeDecision : AiDecision
{
    public override bool Decide(AiController controller)
    {
        switch (TimeManager.actualPhase)
        {
            default:
            case DayPhase.Night:
            case DayPhase.Dawn:
            case DayPhase.Evening:
            case DayPhase.Midnight:
                return false;
            case DayPhase.Morning:
            case DayPhase.Noon:
            case DayPhase.Afternoon:
            case DayPhase.Dusk:
                if(controller.myActualStamina < 100 || controller.myActualSatiety < 100)
                    return false;
                return true;
        }
    }

}
