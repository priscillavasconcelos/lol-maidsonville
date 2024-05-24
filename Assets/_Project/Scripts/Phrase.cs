using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Phrase
{
    public CharacterSO Character;
    public Guid Id = Guid.NewGuid();
    [TextArea]
    public string Text;

    public List<ActionSO> TriggerAction;
    public bool WaitActionsToComplete;
}
