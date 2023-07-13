using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathzone : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        IPea colPea = collision.gameObject.GetComponent(typeof(IPea)) as IPea;
        if (colPea != null)
        {
            colPea.Die();
        }
    }

}
