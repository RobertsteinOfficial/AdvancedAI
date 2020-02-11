using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AiController : MonoBehaviour
{
    public AiStatistics myStats;
    public AiState actualState;
    public float myActualStamina = 100;
    public float myActualSatiety = 100;
    public GameObject[] workingPositions;
    [HideInInspector] public float stateTimer = 0;

    //LOCAL
    bool playerIsInSight = false;
    GameObject player_ref;

    #region Monobehaviour
    void Start()
    {
        Initialize();
        InvokeRepeating("PollSenses", 0.5f, 0.5f);
    }

    void Update()
    {
        stateTimer += Time.deltaTime;
        actualState.UpdateState(this);

        //MOCKUP
        if(playerIsInSight)
            Debug.Log(EvaluateSight());
    }
    #endregion

    private void Initialize()
    {
        player_ref = FindObjectOfType<PlayerController>().gameObject;
    }

    public void ChangeState(AiState newState)
    {
        if (newState != actualState)
            stateTimer = 0;
        actualState = newState;
    }


    #region Sense Detecting
    private void PollSenses()
    {
        playerIsInSight = CheckPlayerInSight();
    }

    private bool EvaluateSight()
    {
        float dice = Random.Range(0f, 1f);
        float distance = Vector3.Distance(player_ref.transform.position, transform.position);
        float distancePercentage = distance / myStats.sightRange;
        float probability = myStats.sightDissipation.Evaluate(distancePercentage);
        if (dice > probability)
            return true;
        else
            return false;
    }

    private bool CheckPlayerInSight()
    {
        float distance = Vector3.Distance(player_ref.transform.position, transform.position);
        if (distance <= myStats.sightRange)
        {
            Vector3 dirToPlayer = player_ref.transform.position - transform.position;
            float angleToPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleToPlayer <= myStats.sightAngle / 2)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + Vector3.up * 1.6f, dirToPlayer, out hit))
                {
                    if (hit.transform.tag == "Player")
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
    #endregion
}
