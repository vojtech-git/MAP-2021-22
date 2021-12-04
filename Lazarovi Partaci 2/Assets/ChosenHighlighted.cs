using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChosenHighlighted : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject graphicsPanel;
   public GameObject volumePanel;
   public GameObject graphicsINGraphics ;
   public GameObject graphicsInVOlume;
   public GameObject volumeInGraphics;
   public GameObject volumeInVolume;

   public GameObject controls2;

    public void HighlightActiveMenu(){
        if(graphicsPanel.activeInHierarchy == true){
         //EventSystem.current.SetSelectedGameObject( graphicsINGraphics. );
        
        }
    }


}
