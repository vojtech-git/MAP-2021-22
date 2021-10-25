using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float rychlostVpred = 25f;
    public float rychlostDoBoku = 7.5f;
    public float rychlostVznesu = 5f;
    
    private float rychlostVpredLerp;
    private float rychlostDoBokuLerp;
    private float rychlostVznesuLerp;
    
    private float akceleraceDopredu = 2.5f;
    private float akceleraceDoBoku = 2f;
    private float akceleraceVznaseni = 2f;
    
    public float lookRateSpeed = 90f;
    private Vector2 lookInput;
    private Vector2 screenCenter;
    private Vector2 mouseDistance; 

    private float rollInput;
    public float rollSpeed = 90f;
    public float rollAcceleration=3.5f;

    public AudioSource spaceshipSound;
    public AudioSource spaceShipHorn;

//    public GameObject Camera;
//    public float cameraFollowSpeed;
//    private Vector3 CamOffset;

    
    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * .5f; 
        screenCenter.y = Screen.height * .5f;  //ukládání souřadnic středu obrazovky
        // CamOffset = Camera.transform.position - transform.position;

        Cursor.lockState = CursorLockMode.Confined; //omezení kurzoru na velikost naší hry
        //Cursor.visible=false;
      

    }

    // Update is called once per frame
    void Update()
    {

    // Vector3 moveToCam = transform.position - transform.forward * 20.0f + Vector3.up * 15;
    // Camera.transform.position = moveToCam;
    // Camera.transform.LookAt(transform.position + transform.forward * 30.0f);
    // //--------------------------
    //  Camera.transform.position = Vector3.Lerp(Camera.transform.position, transform.position + CamOffset, Time.smoothDeltaTime * cameraFollowSpeed );
    

    if(Input.GetKeyDown(KeyCode.W)){
        spaceshipSound.Play();
    }
    if(Input.GetKeyDown(KeyCode.S)){
        spaceshipSound.Pause();
    }
     if(Input.GetKeyDown(KeyCode.T)){
        spaceShipHorn.Play();
    }
    
    lookInput.x = Input.mousePosition.x; 
    lookInput.y = Input.mousePosition.y; //ukládání souřadnic naší AKTUÁLNÍ POZICE myši do proměnné LOOKINPUT

    mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y; //výpočet vzdálenosti myši od středu obrazovky
    mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;
   
    mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f); //určení hranice otáčení na obrazovce, abychom se nemohli otacet moc rychle

    rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime); //rolování ukládání do rollInputu. využití lerp funkce kvuli akceleraci(smoothness)

    transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self); //zajisteni rotace letadla pomocí myše
    //Debug.Log(mouseDistance.x);
    rychlostVpredLerp = Mathf.Lerp(rychlostVpredLerp, Input.GetAxisRaw("Vertical1") * rychlostVpred, akceleraceDopredu * Time.deltaTime); 
    rychlostDoBokuLerp = Mathf.Lerp(rychlostDoBokuLerp, Input.GetAxisRaw("Horizontal") * rychlostDoBoku, akceleraceDoBoku * Time.deltaTime);
    rychlostVznesuLerp = Mathf.Lerp( rychlostVznesuLerp,Input.GetAxisRaw("Hover") * rychlostVznesu, akceleraceVznaseni * Time.deltaTime);
   //Debug.Log(rychlostVpredAktiv);
  // Debug.Log("souradnice" + transform.position);
    transform.position += transform.forward * rychlostVpredLerp * Time.deltaTime;
    transform.position += transform.right * rychlostDoBokuLerp * Time.deltaTime;
    transform.position += transform.up * rychlostVznesuLerp * Time.deltaTime;
    /*ukládani do promenny, mathf.lerp používáme, abyhcom zajistili smooth pohyb a akceleraci. 
    Lerp funkce dělá to, že z jedné hodnoty A přejde na hodnotu B v nějakém určitém intervale T.
    Input.getaxisraw využíváme pro získání os v unity. používáme poté na náš movement letadla.
    nakonec máme movement hotov pomocí transform.forward * rychlostVpredAktiv, který právě reprezentuje tu Lerp funkci.
    */

    /*----------------vysvetleni transform.Rotate()
    transform.Rotate nam slouží pro rotaci letadla na svych lokalnich osach (mame to definovany pomocí SPACE.SELF). 
    bere hodnoty x, y, z a na zaklade techle hodnot se nam rotuje objekt. využívame mouseposition.y a .x protože na zaklade pozice mysi chceme nas objekt rotovat.
    první hodnota tj. mouseposition.y je zaporná, protože když myší koukáme dolů, tak je mouseposition.y zapornou hodnotu, ale v atributu rotace naseho objektu(letadla) pri koukani dolu je kladna.
    proto to v podstate vynasobujeme -1.
    */
    }

}
