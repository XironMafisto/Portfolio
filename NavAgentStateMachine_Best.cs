/*-------------------------------------------------------------------------------------------------------------------------------
 * Name: Shawn Kendall
 * Date: 4/21/18
 * Purpose: Make a functioning Nav Agent state machine.
 * 
 *--------------------------------------------------------------------------------------------------------------------------*/
//      Updated by
/*-------------------------------------------------------------------------------------------------------------------------------
* Name: Daniel Gilbert
* Date: 8/24/19
* Purpose: Added Pause, Angry, and Pursue states to extend functionality. Added a SetHighlight function to show 
*          selected guard. Updated PickNextNavPoint function to allow reversed ordering of the NavPoints.
*          Added the Angry function encapsulating that behavior. Added the Pause function encapsulating that behavior.
*          Added an if check to OnTriggerEnter to turn angry off after reaching the alarm.
*          Added Pursue function encapsulating that behavior, with an if check in update that determines the
*          player's distance from the guards then pursues if the player gets within range.
*          
*--------------------------------------------------------------------------------------------------------------------------*/

using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEditor;

public class NavAgentStateMachine_Best : FSGDN.StateMachine.MachineBehaviour
{
    [SerializeField] NavPoint[] myNavPoints = new NavPoint[4];
    int navIndex = 0;
    [SerializeField] private GameObject Highlight;
    [SerializeField] GameObject[] alarms = new GameObject[4];
    public int alarmsIndex = 0;
    [SerializeField] GameObject player;
    public float playerDistance = 50;

    // function to highlight selected index
    public void SetHighlight(bool highlightOn)
    {
        if (highlightOn)
        {
            Highlight.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else
        {
            Highlight.GetComponent<Renderer>().material.color = Color.grey;
        }
    }

    public override void AddStates()
    {
        AddState<PatrolState_Best>();
        AddState<IdleState_Best>();
        AddState<PauseState>();
        AddState<AngryState>();
        AddState<PursueState>();

        SetInitialState<PatrolState_Best>();
    }

    // A bool to reverse the order when picking next nav point
    public bool reverse;
    public void PickNextNavPoint()
    {
        // if the order is reverse decrement
        if (reverse)
        {
            SetMainColor(Color.blue);
            --navIndex;
            if (navIndex < 0)
                navIndex = myNavPoints.Length - 1;
        }
        // otherwise increment
        else
        {
            SetMainColor(Color.green);
            ++navIndex;
            if (navIndex >= myNavPoints.Length)
                navIndex = 0;
        }
    }

    public void FindDestination()
    { 
        GetComponent<NavMeshAgent>().SetDestination(myNavPoints[navIndex].transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<NavPoint>())
            ChangeState<IdleState_Best>();

        if (other.gameObject.name.Equals("Alarm"))
            Angry();
    }

    // Helper function for setting the object color
    public void SetMainColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }

    bool paused = false;
    FSGDN.StateMachine.State lastState = null;

    public void Pause()
    {
        // toggle paused value
        paused = !paused;

        if (paused)
        {
            // store current state for use when unpausing
            lastState = currentState;

            // change state to Pause
            ChangeState<PauseState>();
            GetComponent<NavMeshAgent>().isStopped = true;
        }

        else
        {
            // restore stored state when pausing earlier
            ChangeState(lastState.GetType());
            GetComponent<NavMeshAgent>().isStopped = false;
        }
    }

    bool angry = false;
    public void Angry()
    {
        // toggle angry value
        angry = !angry;

        if (angry)
        {
            // store current state for later use
            lastState = currentState;
            GetComponent<NavMeshAgent>().SetDestination(alarms[alarmsIndex].transform.position);
            // change state to Angry
            ChangeState<AngryState>();
        }

        else
        {
            // restore stored state from earlier
            ChangeState(lastState.GetType());
        }
    }

    bool pursue = false;
    public void Pursue()
    {
        // toggle value
        pursue = !pursue;

        if (pursue)
        {   // store current state for later use
            lastState = currentState;
            if(playerDistance < 12)
            {
                GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
            }
            
            else
            {
                //PickNextNavPoint();
                ChangeState(lastState.GetType());
            }
            // change state to Pursue
            ChangeState<PursueState>();
        }
        else
        {
            // restore stored state from before
            ChangeState(lastState.GetType());
        }
    }

    public override void Update()
    {
        // since this overrides the state machine’s Update()
        // it is very important to call parent class’ Update()
        // because that is where the state machine does it’s work for us
        base.Update();

        playerDistance = Vector3.Distance(player.transform.position, transform.position);
        if (playerDistance < 12)
        {
            Pursue();
        }
    }
}

// New base class for NavAgent states that gives us some utility
// functions to help clean things up even more
public class NavAgentState : FSGDN.StateMachine.State
{
    // Nice accessor for getting our state machine script reference
    protected NavAgentStateMachine_Best NavAgentStateMachine()
    {
        return ((NavAgentStateMachine_Best)machine);
    }
}

// NOTE: now inherits from NavAgentState
public class PatrolState_Best : NavAgentState
{
    public override void Enter()
    {
        base.Enter();
        NavAgentStateMachine().FindDestination();
    }
}

// NOTE: now inherits from NavAgentState
public class IdleState_Best : NavAgentState
{
    float timer = 0;

    public override void Enter()
    {
        base.Enter();
        timer = 0;
        NavAgentStateMachine().SetMainColor(new Color(0.0f, 0.5f, 0.0f));
    }

    public override void Execute()
    {
        timer += Time.deltaTime;
        if (timer >= 2.0f)
        {
            machine.ChangeState<PatrolState_Best>();
            NavAgentStateMachine().PickNextNavPoint();
        }
    }
}

public class PauseState : NavAgentState
{
    public override void Enter()
    {
        base.Enter();
        NavAgentStateMachine().SetMainColor(Color.grey);
    }
}

public class AngryState : NavAgentState
{
    public override void Enter()
    {
        base.Enter();
        NavAgentStateMachine().SetMainColor(Color.red);
    }
}

public class PursueState : NavAgentState
{
    public override void Enter()
    {
        base.Enter();
        NavAgentStateMachine().SetMainColor(new Color(1.0f, 0.0f, 1.0f));
    }
}