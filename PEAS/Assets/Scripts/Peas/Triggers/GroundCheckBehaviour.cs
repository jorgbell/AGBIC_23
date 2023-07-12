using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckBehaviour : MonoBehaviour
{
    Pea thisPea = null;
    //GroundCheck logic
    public Vector3 groundBoxSize;
    public float maxGroundDistance;
    public LayerMask groundLayerMask;

    private void Start()
    {
        thisPea = transform.parent.GetComponent(typeof(IPea)) as Pea;
    }
    public bool GroundCheck()
    {
        if (thisPea.isStuck)
        {
            //Debug.Log("CANT CHECK UNTIL IS OUT OF BOX");
            return false;
        }
        if (Physics2D.BoxCast(transform.position, groundBoxSize, 0, -transform.up, maxGroundDistance, groundLayerMask))
        {
            //Debug.Log("IN GROUND");
            thisPea.isInGround = true;
            return true;
        }
        //Debug.Log("NOT IN GROUND");
        thisPea.isInGround = false;
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up * maxGroundDistance, groundBoxSize);
    }
    private void FixedUpdate()
    {
        GroundCheck();
    }
}
