using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour, IController ,IBehaviorAI
{
    //INPUT EVENT SYSTEMS
    public event InputEvent FireEvent;
    public event InputEventFloat ForwardEvent;
    public event InputEventVector TurnEvent;
   public Vector3 myTargetPosition = Vector3.zero;

   //behaviors
   public Selector rootAI;
   public Sequence CheckArrivalSequence;
   public Sequence MoveSequence;
   public Sequence DecideToAttack; //
   public Selector SelectTargetType;

    public GameObject Player;
    public Transform player;
   GameObject target = null;
   public string enemyFaction = "PlayerFaction";  //urcuje tym, zda jsou na strane nepratel nebo na strane playera
    void Start()         //CHEKCUJEME ZDA JSME V CILI NEBO NE  tohle ridi ai a jeho chovani
    {
        Player = GameObject.FindGameObjectWithTag("PlayerFaction");
        
        DecideToAttack = new Sequence(new List<BTNode>
        {
            new RandomChanceConditional(1,100,10),
            new FindNewTargetTask(this, enemyFaction),
          //  new TurnToTargetTask(this,TurnEvent), m
            
            
        });
        
        SelectTargetType = new Selector(new List<BTNode>
        {
            DecideToAttack,
            new FindWanderPointTask(this, 500f),

        });

        CheckArrivalSequence = new Sequence(new List<BTNode>
        {
            new CheckArrivalTask(this),
         /*    new FindWanderPointTask(this, 500f), */
         SelectTargetType

        });

          MoveSequence = new Sequence(new List<BTNode>
        {
            new TurnToTargetTask(this,TurnEvent),
            new MoveToTargetTask(this, 100f, ForwardEvent),
            new IsTargetVisible(this),
            new FireWeaponTask(this, FireEvent)
            
        });

        rootAI = new Selector(new List<BTNode>
        {
            CheckArrivalSequence,
            MoveSequence,
        });
        new FindWanderPointTask(this,500f);
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseMenu.GameIsPaused == false){
        rootAI.Evaluate();
        }
    }
    public Vector3 SetTargetPosition(Vector3 targetPosition) 
    {
       
        myTargetPosition = targetPosition;
        return myTargetPosition;
    }   
    public Transform GetAgentTransform()
    {
        return transform;
    }
    public Vector3 GetTargetPosition()
    {
         if(target != null) return target.transform.position;
        return myTargetPosition;
    }
    public GameObject SetTarget (GameObject newTarget)
    {
        target = newTarget;
        return target;
    }
    public GameObject GetTarget()
    {
       return target;
    }

    public Transform GetTransform()
    {
        return gameObject.transform;  //pozice enemy ai
    }
}
