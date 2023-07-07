using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 5.0f;
    public Vector2 direction = new Vector2(1f, 0f);
    bool isFalling = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!isFalling)
        {
            direction = direction.normalized;
            Vector2 v = direction * speed;
            rb.velocity = v;
        }
        else
        {
            Debug.Log("quieto parao");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("toco hierba");
        isFalling = false;

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("DREAM ON");
        isFalling = true;

    }


    public void InvertRotation()
    {
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        currentRotation.y += 180f;
        transform.localRotation = Quaternion.Euler(currentRotation);
        direction *= -1;
    }
}
