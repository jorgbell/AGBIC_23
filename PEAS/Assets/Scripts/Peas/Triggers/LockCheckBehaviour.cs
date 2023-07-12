using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCheckBehaviour : MonoBehaviour
{
    Pea thisPea = null;
    //Lock logic
    public Vector3 stuckBoxSize;
    public float maxStuckDistance;
    public LayerMask groundLayerMask;

    private void Start()
    {
        thisPea = transform.parent.GetComponent(typeof(IPea)) as Pea;
    }

    /// <summary>
    /// Solo comprobara si esta dentro de otro gameobject en el momento en el que pueda haber comportamientos inesperados
    /// esto es cuando quitemos del escenario un objeto y el guisante este siendo movido por el, porque puede acabar dentro de otro gameobject
    /// </summary>
    /// <returns></returns>
    public bool LockCheck()
    {
        if (thisPea.checkIfIsStuck)
        {
            //needs to check
            if (Physics2D.BoxCast(transform.position, stuckBoxSize, 0, -transform.up, maxStuckDistance, groundLayerMask))
            {
                //Debug.Log("IS STUCK");
                thisPea.isStuck = true;
                return true;
            }
            //stops checking because object is ok
            //Debug.Log("NOT STUCK");
            thisPea.isStuck = false;
            thisPea.checkIfIsStuck = false;
            return false;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position - transform.up * maxStuckDistance, stuckBoxSize);
    }
    private void FixedUpdate()
    {
        LockCheck();
    }
}
