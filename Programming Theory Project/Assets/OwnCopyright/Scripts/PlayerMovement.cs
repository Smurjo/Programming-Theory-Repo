using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector3 buyPosition;
    [SerializeField] private Vector3 sellPosition;
    [SerializeField] private GameEvents gameEvents;

    Animator animator;
    int animatorSpeedHash = 0;
    NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void OnEnable()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animatorSpeedHash = Animator.StringToHash("Speed_f");

        gameEvents.playerWantsToBuyEvent += goToBuy;
        gameEvents.playerWantsToSellEvent += goToSell;
    }
    void OnDisable()
    {
        gameEvents.playerWantsToBuyEvent -= goToBuy;
        gameEvents.playerWantsToSellEvent -= goToSell;
    }
    private void LateUpdate()
    {
        animator.SetFloat(animatorSpeedHash, navMeshAgent.velocity.magnitude);
    }
    public void goToBuy()
    {
        navMeshAgent.destination = buyPosition;
    }
    public void goToSell()
    {
        navMeshAgent.destination = sellPosition;
    }
}
