using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BasicPea : Pea
{

    public override void EntersScenarioObject(ScenarioObjectType st)
    {
        objectCollision = st;
        switch (st)
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

    public override void ExitsScenarioObject(ScenarioObjectType st)
    {
        objectCollision = ScenarioObjectType.NONE;
        switch (st)
        {
            case ScenarioObjectType.LADDER:
                col.enabled = true; rb.isKinematic = false;
                movesInUpdate = false;
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

    public override void IceMovement()
    {
        throw new System.NotImplementedException();
    }

    public override void LadderMovement()
    {
        float step = ladderSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, ladderTarget, step);

        // Comprueba si ha llegado al punto final
        if (Vector2.Distance(transform.position, ladderTarget) < 0.001f)
        {
            // Detiene el movimiento
            ExitsScenarioObject(ScenarioObjectType.LADDER);
        }
    }

    public override void TrampolineMovement()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Método que indicará como debe caminar este guisante según
    /// donde se encuentre.
    /// Si no está siendo afectado por ningún objeto del escenario, se moverá en la branch NONE con su movimiento autónomo.
    /// Por el contrario, si está siendo afectado por el escenario, se moverá según cómo le afecte el escenario a este tipo de guisante (no tiene por que ser igual para todos)
    /// </summary>
    public override void Walk()
    {
        switch (objectCollision)
        {
            case ScenarioObjectType.LADDER:
                LadderMovement();
                break;
            case ScenarioObjectType.ELEVATOR:
                ElevatorMovement();
                break;
            case ScenarioObjectType.TRAMPOLINE:
                TrampolineMovement();
                break;
            case ScenarioObjectType.ICE:
                IceMovement();
                break;
            case ScenarioObjectType.CINTA:
                CintaMovement();
                break;
            //---------------------------------default movement
            default:
                AutoMovement();
                break;
        }


    }
    public override void AutoMovement()
    {
        if (GroundCheck())
        {
            movementDirection = movementDirection.normalized;
            Vector2 v = movementDirection * movementSpeed;
            rb.velocity = v;
        }

    }

    public override void CintaMovement()
    {
        throw new System.NotImplementedException();
    }

    public override void ElevatorMovement()
    {
        throw new System.NotImplementedException();
    }
}
