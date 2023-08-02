using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirection : MonoBehaviour
{
    Pea thisPea = null;
    //Wall logic
    public Vector3 wallBoxSize;
    public float maxWallDistance;
    public LayerMask groundLayerMask;
    public LayerMask peasLayerMask;
    public bool changesWithPeas = false;
    public bool doQueue = true;

    private void Start()
    {
        thisPea = transform.parent.GetComponent(typeof(IPea)) as Pea;
    }
    private void FixedUpdate()
    {
        WallCheck();
    }
    public bool WallCheck()
    {
        if (thisPea.isStuck || thisPea.GetCollisionType() != ScenarioObjectType.NONE)
        {
            //Debug.Log("CANT CHECK WALL COLLISION");
            return false;
        }
        var ground = Physics2D.BoxCast(transform.position, wallBoxSize, 0, transform.right, maxWallDistance, groundLayerMask);
        if (ground) {
            thisPea.RotateMovement();
            return true;
        }        
        var pea = Physics2D.BoxCast(transform.position, wallBoxSize, 0, transform.right, maxWallDistance, peasLayerMask);
        //si colisiona con un guisante, este guisante rebotará si:
        //-tiene que rebotar siempre que choque con un guisante
        //el guisante con el que rebote esta generando un bloqueo (en caso de que no se salte las colas)
        if (pea)
        {
            if(changesWithPeas ||
                doQueue && pea.transform.gameObject.GetComponent<Pea>().GetState() == PeaState.STOP)
            {
                thisPea.RotateMovement();
                return true;
            }
        }
        
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(transform.position + transform.right * maxWallDistance, wallBoxSize);
    }
}
