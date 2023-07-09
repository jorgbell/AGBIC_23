/// <summary>
/// Interfaz que contiene todos los elementos comunes de las cosas que deben hacer los guisantes, sean del tipo que sean:
/// - Método para caminar
/// - Método para cambiar de dirección
/// - Método para cambiar de estado
/// - Getter del tipo de guisante
/// - Método para conseguir los puntos que da el guisante
/// - GroundCheck
/// - Interaccion con los objetos del escenario
/// - etc
/// </summary>

public interface IPea
{
    public PeaType GetPeaType();
    public ScenarioObjectType GetCollisionType();
    public void ChangeDirection();
    public void ChangeState(PeaState s);
    public bool GroundCheck();
    public abstract float GetPoints();
    public abstract void Walk();
    public void Die();
    public abstract void EntersScenarioObject(ScenarioObjectType st);
    public abstract void ExitsScenarioObject(ScenarioObjectType st);
    //Movement types: metodos por ahora vacios pero en esencia cada guisante deberá implementar su manera
    //de interactuar con el escenario
    public abstract void LadderMovement();
    public abstract void ElevatorMovement();
    public abstract void TrampolineMovement();
    public abstract void IceMovement();
    public abstract void CintaMovement();
    //Movimiento propio
    public abstract void AutoMovement();

}
