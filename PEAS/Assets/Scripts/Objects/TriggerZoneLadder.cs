using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneLadder : MonoBehaviour
{
    CircleCollider2D circleCollider; 
    Vector3 previousParentScale; 
    Vector3 newSphereScale;
    public Transform other;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        FixSphereScale();
    }
    private void FixSphereScale()
    {
        //transformamos la escala del padre a la escala original
        previousParentScale = transform.parent.localScale;
        transform.parent.localScale = new Vector3(1, 1, 1);
        //pillamos la escala que queremos para la esfera. Reiniciamos solo la Y, porque la escalera podra cambiar de tamano en la Y, no en lo demas
        newSphereScale = new Vector3(transform.localScale.x, transform.localScale.y / previousParentScale.y, transform.localScale.z);
        //reiniciamos la escala del padre
        transform.parent.localScale = previousParentScale;
        //reescalamos la del hijo 
        transform.localScale = newSphereScale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Pea p = collision.GetComponent<Pea>();
        if (p!=null)
        {
            if(p.GetCollisionType() == ScenarioObjectType.NONE){
                p.ladderTarget = other.position;
                p.EntersScenarioObject(ScenarioObjectType.LADDER);
            }
        }
        else
        {
            if(collision.GetComponent<ChangeDirection>() == null )Debug.LogError("NOT A PEA");
        }

    }
}
