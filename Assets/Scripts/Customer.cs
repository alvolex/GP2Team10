using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Scriptables;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private ScriptableTableSeater tableSeater;
    //todo should we just change this to reside in a static class so we dont have to pull it into every script that needs it
    [SerializeField] private ScriptablePlayerCurrentAction currentAction;

    [Header("Highlight Alien")] 
    [SerializeField] private Material defaultMat;
    [SerializeField] private Material selectedMat;

    [Header("Debug stuff")]
    public Transform restaurantExit;

    [Header("Event")] 
    [SerializeField] private ScriptableSimpleEvent leaveWhenCustomersStopSpawning;
    [SerializeField, Tooltip("Tied to the above event. When the event is called we will start leaving after this many seconds ->")] private float timeOffsetBeforeLeaving = 5;

    private Camera cam;
    private NavMeshAgent nmagent;
    private NavMeshObstacle nmObstacle;
    private SphereCollider sCollider;
    private OrderFood orderFood;
    private MeshRenderer meshRenderer;
    private AlienAttributes attributes;
    
    private bool closeToHost = false;
    private Rigidbody rb;

    private bool isMovingToTable;
    private bool hasFinishedEating = false;
    private bool isSeated = false;

    public bool IsSeated => isSeated;
    public event Action<Customer> OnFinishedEating;

    [SerializeField] private Animator customerAnimator;
    


    private void Awake()
    {
        nmagent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        isSeated = false;
        isMovingToTable = false;
        //Get components
        cam = Camera.main;

        attributes = GetComponent<AlienAttributes>();
        orderFood = GetComponent<OrderFood>();
        nmObstacle = GetComponent<NavMeshObstacle>();
        sCollider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        
        leaveWhenCustomersStopSpawning.ScriptableEvent += HandleExitWhenRestaurantCloses;
    }

    private void OnDestroy()
    {
        if (gameObject.transform.parent.TryGetComponent(out SelectGroupOfCustomers sgoc))
        {
            sgoc.RemoveFromList(this);
        }

        leaveWhenCustomersStopSpawning.ScriptableEvent -= HandleExitWhenRestaurantCloses;
    }

    private void Update()
    {
        customerAnimator.SetFloat("x",nmagent.velocity.magnitude);
        
        HandleCustomerSelection();
        HandleMovingToTable();
    }

    void HandleExitWhenRestaurantCloses()
    {
        Invoke(nameof(ExitRestaurant), timeOffsetBeforeLeaving);
    }

    public void ChangeMovementspeed(int value)
    {
        nmagent.speed += value;
    }

    private void HandleMovingToTable()
    {   //Check if customer has reached the table or not
        if (!isMovingToTable) return;

        if (nmagent.pathPending ||
            nmagent.pathStatus == NavMeshPathStatus.PathInvalid ||
            nmagent.path.corners.Length == 0 || nmagent.remainingDistance >= 0.2f) return;

        isMovingToTable = false; //Not moving if we have reached table
        isSeated = true;
        
        //Customer state is now sitting
        attributes.ChangeCustomerState();
        
        //Show tutorial if it hasn't been shown before
        if (Tutorial.instance != null)
        {
            Tutorial.instance.ShowTutorialText(Tutorial.instance.GameState.hasBeenSeatedTutorial);
            if (Tutorial.instance.GameState.hasBeenSeatedTutorial)
            {
                Tutorial.instance.TurnOnAndMoveSpotlight();
            }
            Tutorial.instance.GameState.hasBeenSeatedTutorial = false;
        }

        //Start the food ordering process
        orderFood.Order();
        orderFood.SCollider = sCollider;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        nmagent.ResetPath();
        nmagent.enabled = false;
        nmObstacle.enabled = true;
        
        
        customerAnimator.SetBool("IsSeated",true);;
    }
    
    //todo maybe the player should handle this(?)
    private void HandleCustomerSelection()
    {
        if (!Input.GetKeyDown(KeyCode.Space) || !closeToHost || isSeated) return;
        //Set the current customer to be this customer when selected in our scriptable object
        
        currentAction.CurrentAction = CurrentAction.SeatingCustomer;
        if (transform.parent.TryGetComponent(out SelectGroupOfCustomers sgc))
        {
            MoveTutorialSpotlightIfTutorialIsActive();

            sgc.SelectCustomersInGroup();
        }
    }

    public void MoveToTable(Vector3 pos)
    {
        isMovingToTable = true;
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        nmagent.destination = pos;
        sCollider.radius = 0.5f; //So we can keep assigning to the table
    }

    //If we ever want to be able to assign a customer to a table without going to it
    //Should work without any changes, just call it when a customer is selected and then click a table
    private void ClickToAssignTable()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && closeToHost)
        {
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.transform.parent == null) return;

                if (hit.transform.parent.TryGetComponent(out Table table))
                {
                    Vector3 chairPos = table.GetEmptyChairPosition(tableSeater.CurrentCustomer);
                    if (chairPos != Vector3.positiveInfinity)
                    {
                        nmagent.destination = chairPos;
                        sCollider.radius = 0.5f; //So we can keep assigning to the table
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (currentAction.CurrentAction == CurrentAction.None && other.TryGetComponent(out PlayerMovement _) && !isSeated && !hasFinishedEating)
        {
            closeToHost = true;
            GetComponentInParent<SelectGroupOfCustomers>().HighlightGroup();
        }

        //Just testin'
        if (other.CompareTag("Finish") && hasFinishedEating)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        closeToHost = false;
        GetComponentInParent<SelectGroupOfCustomers>().UnhighlightGroup();
    }

    public void ExitRestaurant()
    {
        if (!nmagent.isActiveAndEnabled && !isSeated) return;

        //Disable the over-head order icon when customer is leaving
        orderFood.OrderUIImage.SetActive(false);
        //Invoke event to tell the table they were seated at that we should mark that chair as empty
        OnFinishedEating?.Invoke(this);
        hasFinishedEating = true;
        //Unfreeze customer to let them move towards the exit
        rb.constraints = RigidbodyConstraints.None;
        nmObstacle.enabled = false;
        nmagent.enabled = true;
        sCollider.radius = 0.5f;

        nmagent.destination = restaurantExit.transform.position;
    }

    //todo Debugging, should be split into it's own class later
    public void StartEatingFood()
    {
        StartCoroutine(EatFood());
    }
    
    public void HighlightCustomer()
    {
        meshRenderer.material = selectedMat;
    }
    
    public void UnhighlightCustomer()
    {
        meshRenderer.material = defaultMat;
    }
    
    private static void MoveTutorialSpotlightIfTutorialIsActive()
    {
        //Tutorial Stuff
        if (Tutorial.instance != null)
        {
            if (Tutorial.instance.GameState.hasStartedSeatingCustomer)
            {
                Tutorial.instance.TurnOnAndMoveSpotlight();
            }

            Tutorial.instance.GameState.hasStartedSeatingCustomer = false;
        }
    }
    
    //todo Maybe this should be in it's own class(?)
    IEnumerator EatFood()
    {
        yield return new WaitForSeconds(Random.Range(5f,10f));
        
        //Have to do this otherwise we get pushed when we re-activate the navmesh agent..
        nmObstacle.size = Vector3.zero; 
        yield return null;
        
        hasFinishedEating = true;

        ExitRestaurant();
    }
}
