using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase base de la que heredarán el resto de guisantes
/// No está pensada para usarse directamente aunque tenga monobehaviour
/// Esta pensada para tener los valores basicos de todos los guisantes
/// Algunos metodos no estan implementados porque no será una clase que se use
/// </summary>
public abstract class Pea : MonoBehaviour, IPea
{
    //Pea information
    public PeaType type;
    public float points;
    private PeaState state;
    protected ScenarioObject objectCollision = null;

    //Movement
    public Vector2 movementDirection;
    public float movementSpeed;
    //Movement Logic
    [HideInInspector]
    public volatile bool isInGround = false;
    [HideInInspector]
    public volatile bool isStuck = false;
    [HideInInspector]
    public volatile bool checkIfIsStuck = false;

    //ScenarioObjects related data
    /// <summary>
    /// Según el tipo de movimiento requerido, puede ser que el guisante se mueva en el update o en el
    /// fixedupdate.
    /// </summary>
    protected bool movesInUpdate = true;
    //LADDER
    public float ladderSpeed;
    [HideInInspector]
    public Vector2 ladderTarget;

    protected Rigidbody2D rb; protected Collider2D col; protected SpriteRenderer sprrender;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); col = GetComponent<Collider2D>(); sprrender = GetComponent<SpriteRenderer>();

    }
    private void FixedUpdate()
    {
        if (!movesInUpdate)
        {
            Walk();
        }
    }
    private void Update()
    {
        if (isStuck) col.enabled = false;
        else col.enabled = true;
        if (movesInUpdate)
        {
            Walk();
        }
    }
    /// <summary>
    /// Changes movement direction and object rotation
    /// </summary>
    public void ChangeDirection()
    {
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        Quaternion q = Quaternion.identity;
        if (currentRotation.y == 0 || currentRotation.y == 180)
        {
            currentRotation.y += 180;
            q = Quaternion.Euler(currentRotation);
        }
        transform.localRotation = q;
    }
    public void ChangeState(PeaState s) { state = s; }
    public PeaType GetPeaType() { return type; }
    public ScenarioObjectType GetCollisionType()
    {
        if (objectCollision == null)
            return ScenarioObjectType.NONE;
        return objectCollision.type;
    }

    public float GetPoints() { return points; }
    public void Die()
    {
        throw new System.NotImplementedException();
    }

    //---------------------NOT IMPLEMENTED HERE-------------------------------
    public abstract void Walk();
    public abstract void EntersScenarioObject(ScenarioObject st);
    public abstract void ExitsScenarioObject(ScenarioObject st);
    public abstract void LadderMovement();
    public abstract void ElevatorMovement();
    public abstract void TrampolineMovement();
    public abstract void IceMovement();
    public abstract void CintaMovement();
    public abstract void AutoMovement();


}
