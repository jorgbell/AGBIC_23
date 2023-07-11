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
    //GroundCheck logic
    public Vector3 groundBoxSize;
    public float maxGroundDistance;
    public LayerMask groundLayerMask;
    protected bool isInGround = false;
    //Stuck logic
    public Vector3 stuckBoxSize;
    public float maxStuckDistance;
    [HideInInspector]
    public volatile bool isStuck = false;
    [HideInInspector]
    public volatile bool checkIfIsStuck = false;

    protected Rigidbody2D rb; protected Collider2D col; protected SpriteRenderer sprrender;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); col = GetComponent<Collider2D>(); sprrender = GetComponent<SpriteRenderer>();

    }
    private void FixedUpdate()
    {
        StuckCheck();
        GroundCheck();
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

    public bool GroundCheck()
    {
        if (isStuck)
        {
            Debug.Log("CANT CHECK UNTIL IS OUT OF BOX");
            return false;
        }
        if (Physics2D.BoxCast(transform.position, groundBoxSize, 0, -transform.up, maxGroundDistance, groundLayerMask))
        {
            Debug.Log("IN GROUND");
            isInGround = true;
            return true;
        }
        Debug.Log("NOT IN GROUND");
        isInGround = false;
        return false;
    }

    /// <summary>
    /// Solo comprobara si esta dentro de otro gameobject en el momento en el que pueda haber comportamientos inesperados
    /// esto es cuando quitemos del escenario un objeto y el guisante este siendo movido por el, porque puede acabar dentro de otro gameobject
    /// </summary>
    /// <returns></returns>
    public bool StuckCheck()
    {
        if (checkIfIsStuck)
        {
            //needs to check
            if (Physics2D.BoxCast(transform.position, stuckBoxSize, 0, -transform.up, maxStuckDistance, groundLayerMask))
            {
                Debug.Log("IS STUCK");
                isStuck = true;
                return true;
            }
            //stops checking because object is ok
            Debug.Log("NOT STUCK");
            isStuck = false;
            checkIfIsStuck = false;
            return false;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up * maxGroundDistance, groundBoxSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position - transform.up * maxStuckDistance, stuckBoxSize);
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
