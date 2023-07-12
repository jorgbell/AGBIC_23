using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneLadder : MonoBehaviour
{
    public Transform other;
    public Ladder ladder;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pea p = collision.GetComponent<Pea>();
        if (p!=null)
        {
            ladder.other = other;
            ladder.Activate(p);
        }
    }
}
