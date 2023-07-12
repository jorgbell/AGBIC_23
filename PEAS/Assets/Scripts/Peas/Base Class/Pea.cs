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
    protected float myrrorForces;
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
    //TRAMPOLINE
    public float trampolineForceX;
    public float trampolineForceY;
    protected bool hasJumped = false;

    protected Rigidbody2D rb; protected Collider2D col; protected SpriteRenderer sprrender;

    private void Start()
    {
        myrrorForces = movementDirection.x;
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
            myrrorForces *= -1;
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
    /// <summary>
    /// Método que indicará como debe caminar este guisante según
    /// donde se encuentre.
    /// Si no está siendo afectado por ningún objeto del escenario, se moverá en la branch NONE con su movimiento autónomo.
    /// Por el contrario, si está siendo afectado por el escenario, se moverá según cómo le afecte el escenario a este tipo de guisante (no tiene por que ser igual para todos)
    /// </summary>
    public void Walk()
    {
        //casos base
        if (isStuck)
        {
            return;
        }
        if (objectCollision == null)
        {
            AutoMovement();
            return;
        }
        //resto de casos
        bool result = true;
        switch (objectCollision.type)
        {
            case ScenarioObjectType.LADDER:
                result = LadderMovement();
                break;
            case ScenarioObjectType.ELEVATOR:
                result = ElevatorMovement();
                break;
            case ScenarioObjectType.TRAMPOLINE:
                result = TrampolineMovement();
                break;
            case ScenarioObjectType.ICE:
                result = IceMovement();
                break;
            case ScenarioObjectType.CINTA:
                result = CintaMovement();
                break;
            //---------------------------------default movement
            default:
                AutoMovement();
                break;
        }
        if (!result && objectCollision != null) ExitsScenarioObject(objectCollision);
    }

    public virtual void EntersScenarioObject(ScenarioObject st)
    {
        objectCollision = st;
        //switch statement here, implemented in inherited class
    }
    public virtual void ExitsScenarioObject(ScenarioObject st)
    {
        objectCollision = null;
        st.DeletePea(this);
        //switch statement here, implemented in inherited class

    }
    /// <summary>
    /// El movimiento default comparte (en principio) para todos los guisantes que solo se hace cuando esta en el suelo
    /// </summary>
    /// <returns></returns>
    public virtual bool AutoMovement()
    {
        if (isInGround) return true;
        return false;
    }
    public virtual bool TrampolineMovement()
    {
        if (isInGround)
            return false;
        return true;
    }

    //---------------------NOT IMPLEMENTED HERE-------------------------------
    public abstract bool LadderMovement();
    public abstract bool ElevatorMovement();
    public abstract bool IceMovement();
    public abstract bool CintaMovement();


}
