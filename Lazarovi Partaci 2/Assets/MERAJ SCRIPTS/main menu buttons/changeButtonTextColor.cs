using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class changeButtonTextColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text myText;
 
     void Start (){
         myText = GetComponentInChildren<Text>();
     
     }
 
     public void OnPointerEnter (PointerEventData eventData)  //mouse hover color change
     {
        myText.color=Color.black;
     }
 
     public void OnPointerExit (PointerEventData eventData)
     {
         Color color;
         ColorUtility.TryParseHtmlString("#C0C0C1", out color);
         myText.color = color;
     }
     public void changeColorBack(){
         Color color;
         ColorUtility.TryParseHtmlString("#C0C0C1", out color);
         myText.color = color;
     }

      public void OnSelect(BaseEventData eventData)
     {
         // Do something.
         Color color;
         ColorUtility.TryParseHtmlString("#C0C0C1", out color);
         myText.color = color;
        
     }
}
