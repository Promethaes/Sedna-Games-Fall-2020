using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbilityQuest : Quest
{
    public ActivateAbiltiesZoneScript abilityZone;
    protected override void QuestUpdate()
    {
        completed = abilityZone._cutscene.cutsceneComplete;
    }

}
