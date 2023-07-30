
using UnityEngine;
/// <summary>
/// Interfaz que contiene todos los elementos comunes de las cosas que deben hacer los guisantes, sean del tipo que sean:
/// - M�todo para caminar
/// - M�todo para cambiar de direcci�n
/// - M�todo para cambiar de estado
/// - Getter del tipo de guisante
/// - M�todo para conseguir los puntos que da el guisante
/// - GroundCheck
/// - Interaccion con los objetos del escenario
/// - etc
/// </summary>
public interface IPea
{
    public PeaType GetPeaType();
    public ScenarioObjectType GetCollisionType();
    public void RotateMovement();
    public void SetMovementDirection(Vector3 initialRotation);
    public void ChangeState(PeaState s);
    public PeaState GetState();
    public abstract void Walk();
    public void Die();
    public abstract void EntersScenarioObject(ScenarioObject so);
    public abstract void ExitsScenarioObject(ScenarioObject so);
    //Movement types: metodos por ahora vacios pero en esencia cada guisante deber� implementar su manera
    //de interactuar con el escenario
    public abstract bool LadderMovement();
    public abstract bool ElevatorMovement();
    public abstract bool TrampolineMovement();
    public abstract bool IceMovement();
    public abstract bool CintaMovement();
    //Movimiento propio
    public abstract bool AutoMovement();

}
