/// <summary>
/// Interfaz creada para seguir unas reglas a la hora de crear un objeto del escenario.
/// Todos los objetos del escenario deben hacer minimo:
/// - Un m�todo de activaci�n que haga algo en el guisante que le entre
/// - Un m�todo para colocarse en el escenario
/// - Tiene que tener un getter del tipo (enum)
/// - etc
/// </summary>
public interface IScenarioObject
{
    public ScenarioObjectType getType();
    public void Activate(IPea p);
    public void SpawnScenarioObject();
}
