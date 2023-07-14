using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pod : MonoBehaviour
{
    List<PeaType> peasInsidePot = new List<PeaType>();
    [SerializeField]
    int peasToFill = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPea colPea = collision.gameObject.GetComponent(typeof(IPea)) as IPea;
        if (colPea != null)
        {
            colPea.Die();
            peasInsidePot.Add(colPea.GetPeaType());
        }
    }

    private void Update()
    {
        if (peasInsidePot.Count == peasToFill) ClearPod();
    }

    void ClearPod()
    {
        int pointsToAdd = 0;
        PeaType previousPea = PeaType.LASTPEA; //inicia sin racha
        int multiplier = 0; int maxIguales = 0;
        for (int i = 0; i < peasInsidePot.Count; i++)
        {
            //pillas el tipo actual y comparas con el anterior (ya haya una racha o no)
            PeaType actualPea = peasInsidePot[i];
            bool same = (actualPea == previousPea || previousPea == PeaType.LASTPEA);
            if (same)
            {
                multiplier++;
                previousPea = actualPea;
            }
            //suma puntos en caso de no ser una racha (else del anterior if)
            //o siempre que sea la ultima de todas
            if(!same || i == peasInsidePot.Count - 1)
            {
                pointsToAdd += DataManager._instance.GetPoints(previousPea) * multiplier * multiplier;
                maxIguales = multiplier > maxIguales ? multiplier : maxIguales;
                multiplier = 1;
                previousPea = PeaType.LASTPEA;
            }
        }
        Debug.Log("Mayor racha: " + maxIguales);
        EventsManager._instance.addPoints.Invoke(pointsToAdd);
        peasInsidePot.Clear();
    }
}
