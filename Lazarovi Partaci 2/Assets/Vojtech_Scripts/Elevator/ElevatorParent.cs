using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Kdy� poi��v� tenhle script tak se hod� na PARENT game object kterej podsebou mus� m�t game objecty kter� se jmenujou p�esn�: "elevator", "startMarker" a "endMarker"
/// Pomoc� toho �e zm�n� porm�nou moveElevator na true tak se za�ne elevator hejbat;
/// elevator by m�l b�t nastavenej p�esn� na pozici top markrku nebo bottom markru
/// </summary>
public class ElevatorParent : MonoBehaviour
{
    //switchi kterej flicknu mimos script
    public bool moveElevator;

    // priradit z unity
    Transform elevator;
    Transform bottomMarker;
    Transform topMarker;

    public GameObject player;

    // nastavim v editoru a ovlivnuje chovani scriptu
    public float speed = 1.0F;

    // vnitrni promenny scriptu
    private bool journeyAtStarted;
    private bool goingUp;
    private float startTime;
    private float journeyLength;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        bottomMarker = transform.Find("startMarker");
        topMarker = transform.Find("endMarker");
        elevator = transform.Find("elevator");

        journeyLength = Vector3.Distance(bottomMarker.position, topMarker.position);
    }

    void FixedUpdate()
    {
        if (moveElevator)
        {
            if (journeyAtStarted)
            {
                player.transform.parent = elevator;

                startTime = Time.time;

                if (this.transform.position == bottomMarker.position)
                    goingUp = true;
                else
                    goingUp = false;

                journeyAtStarted = false;
            }

            if (goingUp)
            {
                float distCovered = (Time.time - startTime) * speed;
                float fractionOfJourney = distCovered / journeyLength;
                this.transform.position = Vector3.Lerp(bottomMarker.position, topMarker.position, fractionOfJourney);

                if (this.transform.position == topMarker.position)
                {
                    moveElevator = false;
                    journeyAtStarted = true;

                    player.transform.parent = null;
                }
            }
            else if (!goingUp)
            {
                float distCovered = (Time.time - startTime) * speed;
                float fractionOfJourney = distCovered / journeyLength;
                elevator.position = Vector3.Lerp(topMarker.position, bottomMarker.position, fractionOfJourney);

                if (elevator.position == bottomMarker.position)
                {
                    moveElevator = false;
                    journeyAtStarted = true;

                    player.transform.parent = null;
                }
            } 
        }
    }
}
