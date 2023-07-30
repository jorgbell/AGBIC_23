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
        if (Physics2D.BoxCast(transform.position, wallBoxSize, 0, transform.right, maxWallDistance, groundLayerMask) ||
            (changesWithPeas && Physics2D.BoxCast(transform.position, wallBoxSize, 0, transform.right, maxWallDistance, peasLayerMask)))
        {
            //Debug.Log("COLLIDES WITH WALL(TER)");
            thisPea.RotateMovement();
            return true;
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(transform.position + transform.right * maxWallDistance, wallBoxSize);
    }
}
