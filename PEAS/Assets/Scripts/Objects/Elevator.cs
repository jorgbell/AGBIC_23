using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : ScenarioObject
{
    public Transform goTo;
    public override void Activate(Pea p)
    {
        if (p.GetCollisionType() == ScenarioObjectType.NONE)
        {
            p.ladderTarget = goTo.position;
            p.EntersScenarioObject(this);
            AddPea(p);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pea p = collision.GetComponent<Pea>();
        if (p != null)
        {
            Activate(p);
        }
    }


}