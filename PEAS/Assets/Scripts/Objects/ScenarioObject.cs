using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScenarioObject : MonoBehaviour, IScenarioObject
{
    public ScenarioObjectType type;
    public bool fixChildrenScale = false;
    [HideInInspector]
    public List<IPea> peasAffectedByThis = new List<IPea>();
    public List<IPea> peasToRemove = new List<IPea>();

    public ScenarioObjectType GetScenarioType() { return type; }

    public void SpawnScenarioObject(Transform t)
    {
        transform.position = t.position;
        transform.localScale = t.localScale;
        if (fixChildrenScale) FixChildrenScale();
    }
    public void DeSpawnScenarioObject()
    {
        transform.position = new Vector3(800, 800, 800);
        transform.localScale = new Vector3(1, 1, 1);
        //en caso de que el guisante este siendo afectado por el objeto de escenario en el momento de eliminarse, se quitan.
        foreach (Pea p in peasAffectedByThis)
        {
            if (p.GetCollisionType() == type) { 
                p.ExitsScenarioObject(this); 
                p.checkIfIsStuck = true;
            }
        }
    }
    /// <summary>
    /// Al cambiar la escala del objeto, los hijos del objeto (que contienen el trigger) tambien cambian.
    /// En cambio, el trigger podemos querer que se quede en un tamaño original.
    /// Por lo tanto, lo reseteamos aqui, sin cambiar la escala nueva del padre
    /// </summary>

    public void FixChildrenScale()
    {
        //transformamos la escala del padre a la escala original
        Vector3 previousParentScale = transform.localScale;
        transform.localScale = new Vector3(1, 1, 1);
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childScale = transform.GetChild(i);
            //pillamos la escala que queremos para la esfera. 
            Vector3 newTriggerScale = new Vector3(childScale.localScale.x / previousParentScale.x,
                childScale.localScale.y / previousParentScale.y,
                childScale.localScale.z / previousParentScale.z);
            //reescalamos la del hijo 
            childScale.localScale = newTriggerScale;
        }
        //reiniciamos la escala del padre
        transform.localScale = previousParentScale;

    }
    public void AddPea(IPea pea)
    {
        peasAffectedByThis.Add(pea);
    }

    public void DeletePea(IPea pea)
    {
        peasToRemove.Add(pea);
    }
    private void Update()
    {
        if (peasToRemove.Count > 0)
        {
            foreach(IPea p in peasToRemove)
            {
                peasAffectedByThis.Remove(p);
            }
            peasToRemove.Clear();
        }
    }
    //------------------NOT IMPLEMENTED HERE
    public abstract void Activate(Pea p);


}
