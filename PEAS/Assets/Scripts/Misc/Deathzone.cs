using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathzone : MonoBehaviour
{
    public bool onEnter = false;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (onEnter)
            return;
        Die(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!onEnter)
            return;
        Die(collision);
    }

    void Die(Collider2D collision)
    {
        IPea colPea = collision.gameObject.GetComponent(typeof(IPea)) as IPea;
        if (colPea != null)
        {
            colPea.Die();
        }
    }
}
