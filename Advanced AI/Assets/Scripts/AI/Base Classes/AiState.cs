using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable AI/AI State")]
public class AiState : ScriptableObject
{
    public AiAction[] actions;
    public AiTransition[] transitions;

    public void UpdateState(AiController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    void DoActions(AiController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    void CheckTransitions(AiController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool transitionSuccess = transitions[i].decision.Decide(controller);
            if(transitionSuccess)
            {
                controller.ChangeState(transitions[i].trueState);
                return;
            }
            else
            {
                controller.ChangeState(transitions[i].falseState);
            }
        }
    }
}
