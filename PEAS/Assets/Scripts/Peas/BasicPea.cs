using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BasicPea : Pea
{
    public override void EntersScenarioObject(ScenarioObject so)
    {
        base.EntersScenarioObject(so);
        switch (so.type)
        {
            case ScenarioObjectType.LADDER:
                rb.isKinematic = true; rb.velocity = new Vector2(0, 0);
                movesInUpdate = true;
                break;
            case ScenarioObjectType.ELEVATOR:
                break;
            case ScenarioObjectType.TRAMPOLINE:
                break;
            case ScenarioObjectType.DEATHZONE:
                break;
            case ScenarioObjectType.ICE:
                break;
            case ScenarioObjectType.CINTA:
                break;
            case ScenarioObjectType.TRAMPILLA:
                break;
            case ScenarioObjectType.NONE:
                break;
        }
    }

    public override void ExitsScenarioObject(ScenarioObject so)
    {
        base.ExitsScenarioObject(so);
        switch (so.type)
        {
            case ScenarioObjectType.LADDER:
                rb.isKinematic = false;
                movesInUpdate = true;
                break;
            case ScenarioObjectType.ELEVATOR:
                break;
            case ScenarioObjectType.TRAMPOLINE:
                break;
            case ScenarioObjectType.DEATHZONE:
                break;
            case ScenarioObjectType.ICE:
                break;
            case ScenarioObjectType.CINTA:
                break;
            case ScenarioObjectType.TRAMPILLA:
                break;
            case ScenarioObjectType.NONE:
                break;
        }
    }

    public override bool IceMovement()
    {
        return false;
    }

    public override bool LadderMovement()
    {
        float step = ladderSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, ladderTarget, step);

        // Comprueba si ha llegado al punto final
        if (Vector2.Distance(transform.position, ladderTarget) < 0.001f)
        {
            // Detiene el movimiento
            ExitsScenarioObject(objectCollision);
        }
        return false;

    }

    public override bool TrampolineMovement()
    {
        return false;
    }


    public override bool AutoMovement()
    {
        if (base.AutoMovement())
            transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
        return false;
    }

    public override bool CintaMovement()
    {
        return false;
    }

    public override bool ElevatorMovement()
    {
        return false;
    }
}
