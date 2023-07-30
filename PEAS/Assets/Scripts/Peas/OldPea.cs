using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class OldPea : Pea
{
    //the old pea has to rest sometimes because it is tired
    [SerializeField]
    float restTimeSeconds = 3.0f;
    [SerializeField]
    float restWalkingProbability = 10;
    bool tryingToRest = false;
    private void Start()
    {
        restWalkingProbability /= 100;
    }
    void Rest()
    {
        ChangeState(PeaState.STOP);
        Invoke("StopRest", restTimeSeconds);
    }
    void TryRest()
    {
        float rand = UnityEngine.Random.Range(0, 100) / 100.0f;
        Debug.Log(rand);
        if(rand <= restWalkingProbability)
        {
            Rest();
            Debug.Log("old pea is tired, resting");
        }
        else
        {
            Debug.Log("old pea still on fire!");
        }
        tryingToRest = false;
    }
    void StopRest()
    {
        ChangeState(PeaState.WALK);
    }

    public override void Walk()
    {
        if(!tryingToRest && GetState() != PeaState.STOP && GetCollisionType() == ScenarioObjectType.NONE)
        {
            //every second try to rest
            tryingToRest = true;
            Invoke("TryRest", 1);
        }
        base.Walk();
    }


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
        Rest();
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
        if (!hasJumped)
        {
            rb.AddForce(new Vector3(trampolineForceX * myrrorForces, trampolineForceY, 0), ForceMode2D.Impulse);
            hasJumped = true;
        }
        return base.TrampolineMovement();
    }


    public override bool AutoMovement()
    {
        if (base.AutoMovement())
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
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