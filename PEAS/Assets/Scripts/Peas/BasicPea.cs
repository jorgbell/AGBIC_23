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
                rb.velocity = new Vector2(0, 0);
                hasJumped = false;
                movesInUpdate = false;
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
                hasJumped = false;
                movesInUpdate = true;
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
        return true;
    }

    public override bool LadderMovement()
    {
        float step = ladderSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, ladderTarget, step);

        // Comprueba si ha llegado al punto final
        if (Vector2.Distance(transform.position, ladderTarget) < 0.001f)
        {
            // Detiene el movimiento
            return false;
        }
        return true;

    }

    public override bool TrampolineMovement()
    {
        if (!hasJumped) {
            rb.AddForce(new Vector3(trampolineForceX * myrrorForces, trampolineForceY, 0), ForceMode2D.Impulse); 
            hasJumped = true; }
        return base.TrampolineMovement();
    }


    public override bool AutoMovement()
    {
        if (base.AutoMovement())
        {
            transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
            return true;
        }
        return false;
    }

    public override bool CintaMovement()
    {
        return true;
    }

    public override bool ElevatorMovement()
    {
        return true;
    }
}
