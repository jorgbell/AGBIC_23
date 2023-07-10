using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : ScenarioObject
{
    [HideInInspector]
    public Transform other;
    public override void Activate(Pea p)
    {
        if (p.GetCollisionType() == ScenarioObjectType.NONE)
        {
            p.ladderTarget = other.position;
            p.EntersScenarioObject(this);
            AddPea(p);
        }
    }


}
