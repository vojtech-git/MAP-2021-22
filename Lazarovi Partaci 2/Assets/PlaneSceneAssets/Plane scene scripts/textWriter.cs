using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textWriter : MonoBehaviour
{

    private static textWriter instance;
    private List<TextWriterSingle> textWriterSingleList;  //pomoci listu muzem mit vice textWriteru


    private void Awake(){
        instance = this;
        textWriterSingleList = new List<TextWriterSingle>();
    }

    public static void addWriter_Static(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, Action onComplete){ //STATICKA TRIDA ABYCHOM NEMUSELI ODKAZOIVAT V JINYCH SKRIPTECH
        instance.addWriter(uiText,  textToWrite,  timePerCharacter,  invisibleCharacters, onComplete);

    }
    private void addWriter(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, Action onComplete){
     textWriterSingleList.Add(new TextWriterSingle( uiText,  textToWrite,  timePerCharacter,  invisibleCharacters, onComplete));
    }

    private void Update(){
        //Debug.Log(textWriterSingleList.Count);
        for(int i = 0; i< textWriterSingleList.Count; i++){
            bool destroyInstance =  textWriterSingleList[i].Update();
            if(destroyInstance){
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
       
    }
   


    public class TextWriterSingle  //1 textwriter instance 
    {

     private Text uiText;
    private string textToWrite;
    private int characterIndex;

    private float timePerCharacter;
    private  bool invisibleCharacters;
    private float timer;

    private Action onComplete;
        public TextWriterSingle(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, Action onComplete)
        {
        this.uiText = uiText;
        this.textToWrite = textToWrite;
        this.timePerCharacter=timePerCharacter;
        this.invisibleCharacters = invisibleCharacters;
        this.onComplete = onComplete;
        characterIndex = 0;
        }
        public bool Update(){ //vraci true kdyz je splnen
       
            timer -= Time.unscaledDeltaTime; //unscaleddeltatime neni ovlivneno pausovanim
            while(timer <=0f){
                timer+=timePerCharacter;
                characterIndex++;
                string text = textToWrite.Substring(0,characterIndex);
                if (invisibleCharacters){
                    text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                }
                uiText.text = text;

                if (characterIndex >= textToWrite.Length){
                    if(onComplete !=null){
                        onComplete();
                    }
                    return true;
                }
            
        }
        return false;
    }
    }
}
