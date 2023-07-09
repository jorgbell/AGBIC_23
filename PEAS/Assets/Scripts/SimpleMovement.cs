using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class SimpleMovement : MonoBehaviour
{
    Rigidbody2D rb; Collider2D col;
    public float speed = 5.0f;
    public float ladderSpeed = 5.0f;
    public Vector2 movementDirection= new Vector2(1f, 0f);

    //falling logic
    public Vector3 boxSize;
    public float maxGroundDistance;
    public LayerMask groundLayerMask;

    //ladder behaviour
    [HideInInspector]
    public bool isInLadder = false;
    Vector2 ladderTarget;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); col = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (GroundCheck() && !isInLadder)
        {
            movementDirection = movementDirection.normalized;
            Vector2 v = movementDirection * speed;
            rb.velocity = v;
        }
        else if (!isInLadder)
        {
            Debug.Log("quieto parao");
        }
    }
    void Update()
    {
        if (isInLadder)
        {
            float step = ladderSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, ladderTarget, step);

            // Comprueba si ha llegado al punto final
            if (Vector2.Distance(transform.position, ladderTarget) < 0.001f)
            {
                // Detiene el movimiento
                ExitsLadder();
            }
        }

    }

    public void EntersLadder(Vector3 to) {
        isInLadder = true; ladderTarget = to;
        rb.isKinematic = true; rb.velocity = new Vector2(0, 0); }
    public void ExitsLadder() { isInLadder = false; col.enabled = true;  rb.isKinematic = false; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up * maxGroundDistance, boxSize);
    }

    private bool GroundCheck()
    {
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxGroundDistance, groundLayerMask))
        {
            Debug.Log("IN GROUND");
            return true;
        }
        Debug.Log("NOT IN GROUND");
        return false;


    }

    public void InvertRotation()
    {
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        currentRotation.y += 180f;
        transform.localRotation = Quaternion.Euler(currentRotation);
        movementDirection *= -1;
    }
}
