using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirection : MonoBehaviour
{
    Pea thisPea = null;
    private void Start()
    {
        thisPea = transform.parent.GetComponent(typeof(IPea)) as Pea;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected");
        if(!thisPea.isStuck && thisPea.GetCollisionType() == ScenarioObjectType.NONE) thisPea.ChangeDirection();
    }
}
