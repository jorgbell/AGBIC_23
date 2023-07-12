using UnityEngine;
/// <summary>
/// Interfaz creada para seguir unas reglas a la hora de crear un objeto del escenario.
/// Todos los objetos del escenario deben hacer minimo:
/// - Un método de activación que haga algo en el guisante que le entre
/// - Un método para colocarse en el escenario
/// - Tiene que tener un getter del tipo (enum)
/// - etc
/// </summary>
public interface IScenarioObject
{
    public ScenarioObjectType GetScenarioType();
    public abstract void Activate(Pea p);
    public void AddPea(IPea pea);
    public void DeletePea(IPea pea);
    public void SpawnScenarioObject(Transform t);
    public void DeSpawnScenarioObject();
    public void FixChildrenScale();
}
