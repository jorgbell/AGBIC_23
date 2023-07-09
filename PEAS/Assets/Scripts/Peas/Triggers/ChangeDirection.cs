using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirection : MonoBehaviour
{
    IPea thisPea = null;
    private void Start()
    {
        thisPea = transform.parent.GetComponent(typeof(IPea)) as IPea;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected");
        if(thisPea != null)
        {
            thisPea.ChangeDirection();
        }
        else { Debug.LogError("NOT A PEA!!"); }
    }
}
