using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    public Transform target; // LETADLO
    private Vector3 myPosition; // sračka nepotřebná
    public float speed = 14f;
    public Vector3 offset = new Vector3(0, 3, -15); //offset, určuje vzdálenost kamery od letadla


    //  void Awake(){ //a
    //   myPosition = transform.position;

    //  }

    void LateUpdate()

    {

        //KAMERA SKRIPT, UDELAN PROTO ABY KAMERA NEBYLA CHILD ELEMENTEM LETADLA. DODELAT DELAY U ROTACE, POMOCÍ FUNKCE LERP

        transform.position = target.TransformPoint(offset);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * 10f);
        //transform.rotation = target.rotation;



    }
}
