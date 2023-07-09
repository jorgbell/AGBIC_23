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
    protected ScenarioObjectType objectCollision = ScenarioObjectType.NONE;
    //Movement
    public Vector2 movementDirection;
    public float movementSpeed;
    //ScenarioObjects related data
    /// <summary>
    /// Según el tipo de movimiento requerido, puede ser que el guisante se mueva en el update o en el
    /// fixedupdate.
    /// </summary>
    protected bool movesInUpdate = false;
    //LADDER
    public float ladderSpeed;
    [HideInInspector]
    public Vector2 ladderTarget;
    //GroundCheck logic
    public Vector3 boxSize;
    public float maxGroundDistance;
    public LayerMask groundLayerMask;
    
    
    protected Rigidbody2D rb; protected Collider2D col;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); col = GetComponent<Collider2D>();

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
        currentRotation.y += 180f;
        transform.localRotation = Quaternion.Euler(currentRotation);
        movementDirection *= -1;
    }
    public void ChangeState(PeaState s) { state = s; }
    public PeaType GetPeaType() { return type; }
    public ScenarioObjectType GetCollisionType() { return objectCollision; }

    public bool GroundCheck()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxGroundDistance, groundLayerMask))
        {
            Debug.Log("IN GROUND");
            return true;
        }
        Debug.Log("NOT IN GROUND");
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up * maxGroundDistance, boxSize);
    }
    public float GetPoints() { return points; }
    public void Die()
    {
        throw new System.NotImplementedException();
    }

    //---------------------NOT IMPLEMENTED HERE-------------------------------
    public abstract void Walk();
    public abstract void EntersScenarioObject(ScenarioObjectType st);
    public abstract void ExitsScenarioObject(ScenarioObjectType st);
    public abstract void LadderMovement();
    public abstract void ElevatorMovement();
    public abstract void TrampolineMovement();
    public abstract void IceMovement();
    public abstract void CintaMovement();
    public abstract void AutoMovement();

}
