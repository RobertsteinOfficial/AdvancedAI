[System.Serializable]
public struct AiTransition
{
    public string transitionName;
    public AiDecision decision;

    public AiState trueState;
    public AiState falseState;
}
