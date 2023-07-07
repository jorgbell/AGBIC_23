using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirection : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected");
        Transform parentTransform = transform.parent;

        if (parentTransform != null)
        {
            SimpleMovement parentScript = parentTransform.GetComponent<SimpleMovement>();
            if (parentScript != null)
            {
                parentScript.InvertRotation();
            }
        }
    }
}
