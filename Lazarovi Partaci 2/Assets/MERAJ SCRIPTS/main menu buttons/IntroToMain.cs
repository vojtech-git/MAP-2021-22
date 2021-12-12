using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroToMain : MonoBehaviour
{
   public GameObject IntroMenu;
  public GameObject WholeMenu;

  public void Update(){
      if (WholeMenu.activeInHierarchy == false)
      {
          if (Input.GetKeyDown(KeyCode.Return))
          {
              IntroMenu.SetActive(false);
              WholeMenu.SetActive(true);
          }
      }
  }
}
