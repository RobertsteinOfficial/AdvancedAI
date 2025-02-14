﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AiDecision : ScriptableObject
{
    public abstract bool Decide(AiController controller);
}
