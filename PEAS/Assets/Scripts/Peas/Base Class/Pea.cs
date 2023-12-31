using UnityEngine;

/// <summary>
/// Clase base de la que heredar�n el resto de guisantes
/// No est� pensada para usarse directamente aunque tenga monobehaviour
/// Esta pensada para tener los valores basicos de todos los guisantes
/// Algunos metodos no estan implementados porque no ser� una clase que se use
/// </summary>
public abstract class Pea : MonoBehaviour, IPea
{
    //Pea information
    public PeaType type;
    protected PeaState state;
    protected ScenarioObject objectCollision = null;

    //Movement
    protected float myrrorForces = 1;
    public float movementSpeed;
    //Movement Logic
    [HideInInspector]
    public volatile bool isInGround = false;
    [HideInInspector]
    public volatile bool isStuck = false;
    [HideInInspector]
    public volatile bool checkIfIsStuck = false;
    [SerializeField]
    protected ChangeDirection directionTrigger;

    //ScenarioObjects related data
    /// <summary>
    /// Seg�n el tipo de movimiento requerido, puede ser que el guisante se mueva en el update o en el
    /// fixedupdate.
    /// </summary>
    protected bool movesInUpdate = true;
    //LADDER
    public float ladderSpeed;
    [HideInInspector]
    public Vector2 ladderTarget;
    //ELEVATOR
    public float elevatorSpeed;
    //TRAMPOLINE
    public float trampolineForceX;
    public float trampolineForceY;
    protected bool hasJumped = false;

    protected Rigidbody2D rb; protected Collider2D col; protected SpriteRenderer sprrender;

    private void Awake()
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
    public void RotateMovement()
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
    public void SetMovementDirection(Vector3 initialRotation)
    {
        transform.localRotation = Quaternion.Euler(initialRotation);
        if (initialRotation.y == 180) myrrorForces = -1;
        else myrrorForces = 1;
    }

    public void ChangeState(PeaState s) { state = s; }
    public PeaType GetPeaType() { return type; }
    public ScenarioObjectType GetCollisionType()
    {
        if (objectCollision == null)
            return ScenarioObjectType.NONE;
        return objectCollision.type;
    }

    public void Die()
    {
        state = PeaState.DEAD;
        gameObject.SetActive(false);
    }
    /// <summary>
    /// M�todo que indicar� como debe caminar este guisante seg�n
    /// donde se encuentre.
    /// Si no est� siendo afectado por ning�n objeto del escenario, se mover� en la branch NONE con su movimiento aut�nomo.
    /// Por el contrario, si est� siendo afectado por el escenario, se mover� seg�n c�mo le afecte el escenario a este tipo de guisante (no tiene por que ser igual para todos)
    /// </summary>
    public virtual void Walk()
    {
        //casos base
        if (isStuck)
        {
            return;
        }
        if (state == PeaState.STOP)
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

    public PeaState GetState()
    {
        return state;
    }
}
