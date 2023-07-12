using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : ScenarioObject
{
    public override void Activate(Pea p)
    {
        p.EntersScenarioObject(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pea p = collision.gameObject.GetComponent<Pea>();
        if (p != null)
        {
            Activate(p);
        }
    }
}
