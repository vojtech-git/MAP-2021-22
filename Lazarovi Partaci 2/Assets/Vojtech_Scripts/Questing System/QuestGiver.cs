using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public List<Questline> questLines;

    List<Questline> availableQuestlines;
    List<Questline> acceptedQuestlines;
    List<Questline> completedQuestLines;

    private void Start()
    {
        //availableQuestlines.Clear();
        //acceptedQuestlines.Clear();
        //completedQuestLines.Clear();

        //availableQuestlines.Add( Instantiate(questLines[0], transform));

        //foreach (Questline questline in questLines)
        //{
        //    if (questline.completed)
        //    {
        //        completedQuestLines.Add(questline);
        //    }
        //    else if (questline.accepted)
        //    {
        //        acceptedQuestlines.Add(questline);
        //    }
        //    else if (questline.available)
        //    {
        //        availableQuestlines.Add(questline);
        //    }
        //}
    }
}
