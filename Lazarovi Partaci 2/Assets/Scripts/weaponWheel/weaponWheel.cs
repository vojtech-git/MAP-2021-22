using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class weaponWheel : MonoBehaviour
{


    [SerializeField] private KeyCode wheelKey = KeyCode.Tab; //urcime na kterou klavesu chceme wheel
    [SerializeField] private GameObject wheelParent;
    [SerializeField] private Camera mojeKamera;  //kamera playera
    public bool WheelEnabled;
    [Header ("Pieces")]
    public GameObject GunTop;
    public GameObject GunRightTop;
    public GameObject GunRightBot;
    public GameObject GunLeftBot;
    public GameObject GunLeftTop;
    [System.Serializable] public class wheel
    {
        public Material highlightSprite; //zlomový bod 
        private Material m_NormalSprite;
        public Image wheela;
      

        public Material NormalSprite
        {
            get => m_NormalSprite;
            set => m_NormalSprite = value;

        }
    }
    [SerializeField] private wheel[] wheels = new wheel[5];
     
    [Header("Dots & Lines")]
    [SerializeField] private Transform[] dots = new Transform[6];
    private Vector2[] pos=new Vector2[6];
    private Vector2 start, end;
    private Vector2 mousePos;



     private void OnDrawGizmos()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            pos[i].x = dots[i].position.x;
            pos[i].y = dots[i].position.y;
        }

        start.x = pos[0].x;
        start.y = pos[0].y;

        for (int i = 0; i < pos.Length; i++)
        {
            end.x = pos[i].x;
            end.y = pos[i].y;
            Debug.DrawLine(start, end, Color.red);
        }
        for (int i = 0; i < pos.Length - 1; i++) //zkresleni trojuhelniku
        {
            start.x = pos[i].x;
            start.y = pos[i].y;
            end.x = pos[i + 1].x;
            end.y = pos[i + 1].y;
            Debug.DrawLine(start, end, Color.red);
        }
        start.x = pos[5].x; //posledni trojuhelnik
        start.y = pos[5].y;
        end.x = pos[1].x;
        end.y = pos[1].y;
        Debug.DrawLine(start, end, Color.red);  //ZKRESELENI TROJUHELNIKU PRO VYMEZENI HRANIC VYBERU V  WEAPON WHEELU

    }

    
    private float Area(Vector2 v1, Vector2 v2, Vector2 v3)
    {
        return Mathf.Abs(f:(v1.x *(v2.y - v3.y) + v2.x *(v3.y-v1.y)+ v3.x * (v1.y - v2.y))/2f);
    }
        
    private bool IsInside(Vector2 v1, Vector2 v2, Vector2 v3, Vector2 v)
    {
        float A = Area(v1, v2, v3);    //checkujeme zda mys se nachazi se v nasem trojuhelniku
        float A1 = Area(v1, v2, v);
        float A2 = Area(v1, v, v3);
        float A3 = Area(v, v2, v3);

        return (Mathf.Abs(f: A1 + A2 + A3 - A) < 1f);
    }
    // Start is called before the first frame update
    void Start()
    {
        disableWheel();
        for(int i =0; i < wheels.Length; i++){

                if(wheels[i].wheela !=null){

                    wheels[i].NormalSprite = wheels[i].wheela.material;
                }
            }
    }

     private void EnableAllHighlight(int index)
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            if (wheels[i].wheela != null && wheels[i].highlightSprite != null)
            {
                if (i == index)
                {
                    wheels[i].wheela.material = wheels[i].highlightSprite;
                   // zvuk.Play();

                }
                else
                {
                    wheels[i].wheela.material = wheels[i].NormalSprite;  //metoda na zoranzoveni wheelu
                }
            }  //honza je píča jsem v hlubokých depresích Meraji proč mi to děláš

        }
    }
    private void DisableAllHighlight()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            if (wheels[i].wheela != null)
            {
                wheels[i].wheela.material = wheels[i].NormalSprite;
            }
        }
    }
    private void enableWheel()
    {
        //if (PauseMenu.GameIsPaused == false && GameObject.Find("WeaponHolder") != null  && defeatscreen.jsiDead==false) {
            if (wheelParent != null)
                wheelParent.SetActive(true);
            WheelEnabled = true;
            Time.timeScale = 0.1f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
    }

    public void disableWheel()   //udelal jsem z metody public abych to mohl pouzivat na fixnuti bagu na load a na new game, drive to bylo PRIVATE
    {
       // if (PauseMenu.GameIsPaused == false) {
        if (wheelParent != null)
            wheelParent.SetActive(false);
        WheelEnabled = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }




    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(wheelKey)){
            enableWheel();
            CheckForCurrentWeapon();
        }
        else if(Input.GetKeyUp(wheelKey)){
            disableWheel();
        }
    }



     private void CheckForCurrentWeapon()
    {
        //if (PauseMenu.GameIsPaused == false && GameObject.Find("WeaponHolder") != null && defeatscreen.jsiDead==false) {  //podminka hlida aby jsme nemohli dat wheel kdyz mame pauslou hru
            if (mojeKamera == null)
                return;


            for (int i = 0; i < pos.Length; i++)
            {
                pos[i] = mojeKamera.WorldToScreenPoint(dots[i].position); //prevadime na screen
            }

            mousePos = Input.mousePosition;

            if (IsInside(v1: pos[0], v2: pos[1], v3: pos[2], v: mousePos))  //tyto podminky nam rikaji co se ma stat kdyz jsme v urcitem okruhu trojuhelniku
            {
                EnableAllHighlight(index: 0);   //vyber ktery se ma zvyrznit kdyz jsme v urcitem trojuhelniku
                                                //pepa.selectedWeapon=1;
                                                //TADY DOPLNIT UNLOCK ZBRANI
                /*WeaponSwitching pepa = GameObject.Find("WeaponHolder").transform.GetComponent<WeaponSwitching>(); //reference na wepaonswitch
                pepa.selectedWeapon = 0;
               */
               GunTop.SetActive(true);
               GunRightTop.SetActive(false);
               GunRightBot.SetActive(false);
               GunLeftBot.SetActive(false);
               GunLeftTop.SetActive(false);
            
       


            }

            if (IsInside(pos[0], pos[2], pos[3], mousePos)) {

                EnableAllHighlight(1);
              /*  WeaponSwitching pepa = GameObject.Find("WeaponHolder").transform.GetComponent<WeaponSwitching>();
                pepa.selectedWeapon = 1;
                */
               GunTop.SetActive(false);
               GunRightTop.SetActive(true);
               GunRightBot.SetActive(false);
               GunLeftBot.SetActive(false);
               GunLeftTop.SetActive(false);
    
            }
            if (IsInside(pos[0], pos[3], pos[4], mousePos)) {

                EnableAllHighlight(2);
              /*  WeaponSwitching pepa = GameObject.Find("WeaponHolder").transform.GetComponent<WeaponSwitching>();
                pepa.selectedWeapon = 2;
               */
               GunTop.SetActive(false);
               GunRightTop.SetActive(false);
               GunRightBot.SetActive(true);
               GunLeftBot.SetActive(false);
               GunLeftTop.SetActive(false);
  
            }
            if (IsInside(pos[0], pos[4], pos[5], mousePos)) {

                EnableAllHighlight(3);
               /* WeaponSwitching pepa = GameObject.Find("WeaponHolder").transform.GetComponent<WeaponSwitching>();
                pepa.selectedWeapon = 3;
              */
               GunTop.SetActive(false);
               GunRightTop.SetActive(false);
               GunRightBot.SetActive(false);
               GunLeftBot.SetActive(true);
               GunLeftTop.SetActive(false);
                 
            }
            if (IsInside(pos[0], pos[5], pos[1], mousePos)) {

                EnableAllHighlight(4);
                /*WeaponSwitching pepa = GameObject.Find("WeaponHolder").transform.GetComponent<WeaponSwitching>();
                //pepa.selectedWeapon = 0;
                pepa.selectedWeapon = 4;
             */
               GunTop.SetActive(false);
               GunRightTop.SetActive(false);
               GunRightBot.SetActive(false);
               GunLeftBot.SetActive(false);
               GunLeftTop.SetActive(true);
                   

           } 
        }
    }
//}
