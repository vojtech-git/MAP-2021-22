using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
   public Transform target; // LETADLO
  private Vector3 myPosition; // sračka nepotřebná
  public float speed = 14f;
    private Vector3 offset = new Vector3(0, 6, -15); //offset, určuje vzdálenost kamery od letadla


  //  void Awake(){ //a
  //   myPosition = transform.position;

  //  }

    void Update()

    {

       //KAMERA SKRIPT, UDELAN PROTO ABY KAMERA NEBYLA CHILD ELEMENTEM LETADLA. DODELAT DELAY U ROTACE, POMOCÍ FUNKCE LERP

        transform.position = target.TransformPoint(offset);
        //transform.rotation = Quaternion.Lerp(target.rotation, transform.rotation,Time.deltaTime /speed); //Funguje, ale nedela to zadny delay, je to stejny jako radek pod timhle. VRATIT SE K TOMU
         transform.rotation = target.rotation;



    }
}
