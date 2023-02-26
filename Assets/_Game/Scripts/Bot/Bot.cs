using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : CharacterProps
{
    public enum StateBot
    {
        Idle, SeekingBlock, BuildBridge, Win
    }
    public StateBot stateBot;

    private NavMeshAgent navMeshAgent;
    [SerializeField] private Vector3 currentTargetPoint;
    [SerializeField] private int needBlockToBuild = 5;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (GameManager.Instance.HasOneWinning)
        {
            ResetBot();
            return;
        }

        if (GameManager.Instance.IsStartGame)
        {
            if (isWin)
            {
                stateBot = StateBot.Win;
                ChangeAnim("Win");
                return;
            }

            if (navMeshAgent.isStopped)
            {
                ChangeAnim("Idle");
            }
            else
            {
                ChangeAnim("Run");
            }

            if (reachEndPoint)
            {
                stateBot = StateBot.Idle;
                reachEndPoint = false;
            }

            if (stateBot == StateBot.Idle)
            {
                stateBot = StateBot.SeekingBlock;
                needBlockToBuild = Random.Range(4, 15);
                hasTarget = false;
            }

            if (stateBot == StateBot.SeekingBlock)
            {
                if (!hasTarget)
                {
                    if (numberBlockOwner < needBlockToBuild)
                    {
                        FindBlock();
                    }
                    else
                    {
                        stateBot = StateBot.BuildBridge;
                        MoveToTarget(currentTargetBridge.position);
                    }
                }
            }
            if (stateBot == StateBot.BuildBridge)
            {
                if (numberBlockOwner == 0)
                {
                    stateBot = StateBot.Idle;
                }
            }
        }


    }

    private void ResetBot()
    {
        stateBot = StateBot.Idle;
        navMeshAgent.ResetPath();
    }
    private void FindBlock()
    {
        if (listToCollectBlock.Count <= 0) return;
        int randomBlockToCollect = Random.Range(0, listToCollectBlock.Count);
        Vector3 blockPos = listToCollectBlock[randomBlockToCollect];
        currentTargetPoint = blockPos;
        MoveToTarget(currentTargetPoint);
        hasTarget = true;
    }
    private void MoveToTarget(Vector3 target)
    {
        navMeshAgent.SetDestination(target);
    }

}
