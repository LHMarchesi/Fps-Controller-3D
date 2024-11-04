using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.AI;

public class AiEnemy : MonoBehaviour
{
    public Transform objective;

    public float speed;

    public NavMeshAgent iA;

    //public Animation Anim; (Esto se deber�a activar cuando el Vig�a tenga las animaciones)
    //public string NombreAnimacionCaminar; (para cuando est� la animaci�n de caminar. Tipo, se pone el nombre) 
    //public string NombreAnimacionAtacar;

    void Update()
    {
        iA.speed = speed;
        iA.SetDestination(objective.position);

        /*if(iA.velocity == Vector3.zero)
         {
            Anim.CrossFade(NombreAnimacionAtacar);
         }
        else
        {
            Anim.CrossFade(NombreAnimacionCaminar);
        }
        */
    }
}
