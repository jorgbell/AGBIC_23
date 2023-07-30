using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : ScenarioObject
{
    public Transform pointA, pointB;
    public override void Activate(Pea p)
    {
        if (p.GetCollisionType() == ScenarioObjectType.NONE)
        {
            float distanceToA = Vector3.Distance(p.gameObject.transform.position, pointA.position);
            float distanceToB = Vector3.Distance(p.gameObject.transform.position, pointB.position);
            p.ladderTarget = distanceToA < distanceToB ? pointB.position : pointA.position;
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
