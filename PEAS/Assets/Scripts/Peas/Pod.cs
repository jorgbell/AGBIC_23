using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pod : MonoBehaviour
{
    List<IPea> peasInsidePot = new List<IPea>();
    [SerializeField]
    int peasToFill = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPea colPea = collision.gameObject.GetComponent(typeof(IPea)) as IPea;
        if (colPea != null)
        {
            colPea.Die();
            peasInsidePot.Add(colPea);
        }
    }

    private void Update()
    {
        if (peasInsidePot.Count == peasToFill) ClearPod();
    }

    void ClearPod()
    {
        //detectar multiplicadores
        int i = 0; int multiplier = 0; int pointsToAdd = 0; 
        IPea previousPea = null; IPea actualPea;
        while (i < peasInsidePot.Count)
        {
            actualPea = peasInsidePot[i];
            //si es la primera de la racha o si existe una racha (guisante igual al anterior)
            if (previousPea == null || actualPea.GetPeaType() == previousPea.GetPeaType())
            {
                multiplier += 1;
                previousPea = actualPea;
            }
            else
            {
                //Cada una de las que sean iguales suma su puntuacion x el multiplicador acumulado
                pointsToAdd = (previousPea.GetPoints() * multiplier) * multiplier;
                EventsManager._instance.addPoints.Invoke(pointsToAdd);
                previousPea = null;
                multiplier = 0;
            }
            i++;
        }
        if(pointsToAdd == 0)
        {
            Debug.Log("Congrats!! Todos son iguales");
            pointsToAdd = (peasInsidePot[peasToFill-1].GetPoints() * multiplier) * multiplier;
            EventsManager._instance.addPoints.Invoke(pointsToAdd);
        }
        peasInsidePot.Clear();

    }
}
