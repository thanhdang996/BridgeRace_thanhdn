using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProps : MonoBehaviour
{
    protected BlockSpawner blockSpawner;
    [SerializeField] protected GameObject map;
    [SerializeField] protected Transform backPoint;
    [SerializeField] protected int atFloor = 1;
    [SerializeField] protected int numberBlockOwner;
    [SerializeField] protected List<Vector3> listToCollectBlock = new List<Vector3>();

    [SerializeField] private ColorType color;
    [SerializeField] private int indexPrefab;
    [SerializeField] protected Transform currentTargetBridge;
    //[SerializeField] protected Transform winPos;
    [SerializeField] protected bool reachEndPoint;
    [SerializeField] protected bool hasTarget;
    [SerializeField] protected bool isWin;

    // if human
    [SerializeField] protected bool isHuman;
    [SerializeField] PlayerMovement player;

    // anim
    private string currentAnimName;
    [SerializeField] private Animator anim;

    private Vector3 oriPos;


    private void Start()
    {
        UIManager.Instance.OnNextLevel += ResetFLoor;
        map = LevelManager.Instance.CurrentMap;

        OriPos();
        OnInit();
    }

    private void Update()
    {
        if (GameManager.Instance.IsStartGame)
        {
            if (isWin)
            {
                ChangeAnim("Win");
            }

            else if (player.IsNotMoving())
            {
                ChangeAnim("Idle");
            }
            else if (!player.IsNotMoving())
            {
                ChangeAnim("Run");
            }
        }
    }

    public void OriPos()
    {
        oriPos = transform.position;
    }

    private void ResetFLoor()
    {
        map = LevelManager.Instance.CurrentMap;

        numberBlockOwner = 0;
        Block[] blocks = backPoint.GetComponentsInChildren<Block>();
        foreach (Block item in blocks)
        {
            Destroy(item.gameObject);
        }
        atFloor = 1;
        OnInit();
        transform.position = oriPos;
        isWin = false;
        if (player != null)
        {
            player.enabled = true;
        }
        GameManager.Instance.HasOneWinning = false;
    }

    private void OnInit()
    {
        listToCollectBlock.Clear();
        blockSpawner = map.transform.GetChild(atFloor - 1).GetChild(1).GetComponent<BlockSpawner>();

        Transform allBridge = map.transform.GetChild(atFloor - 1).GetChild(2);
        int numberBridge = allBridge.childCount;
        currentTargetBridge = allBridge.GetChild(Random.Range(0, numberBridge)).Find("End Point");


        // gan list can collect tu blockSpawner tang moi
        listToCollectBlock = blockSpawner.DataBlockPrefab[indexPrefab].listBlockPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        // va cham block
        if (other.TryGetComponent(out Block block))
        {
            if (block.IsSameColor(color))
            {
                listToCollectBlock.Remove(block.transform.position);
                numberBlockOwner++;
                block.transform.SetParent(backPoint.transform);
                block.transform.localRotation = backPoint.transform.localRotation;
                block.transform.localPosition = new Vector3(0, numberBlockOwner * 0.3f, 0);
                hasTarget = false;
            }
            return;
        }

        // va cham step
        else if (other.TryGetComponent(out Step step))
        {
            if (numberBlockOwner <= 0 && !step.IsSameColor(color))
            {
                if (isHuman && player.DirectionZ > 0)
                {
                    step.TurnOnWall();
                }
                else if (isHuman && player.DirectionZ <= 0)
                {
                    step.TurnOffWall();
                }
                return;
            }
            if (step.IsSameColor(ColorType.None))
            {
                step.ChangeColor(color);
                if (isHuman && player.DirectionZ > 0)
                {
                    step.TurnOffWall();
                }
                RemoveBlockAndSetBackToGround();
                return;
            }

            if (step.IsSameColor(color))
            {
                if (isHuman && player.DirectionZ > 0)
                {
                    step.TurnOffWall();
                }
                return;
            }
            if (!step.IsSameColor(color))
            {
                step.ChangeColor(color);
                if (isHuman && player.DirectionZ > 0)
                {
                    step.TurnOffWall();
                }
                RemoveBlockAndSetBackToGround();
                return;
            }

        }

        // va cham end point
        else if (other.CompareTag("EndPoint") && other.GetComponent<EndPoint>().level - 1 == atFloor)
        {
            if (other.GetComponent<EndPoint>().isWinPos)
            {
                isWin = true;
                GameManager.Instance.HasOneWinning = true;
                if (!isHuman)
                {
                    GameManager.Instance.Lose = true;
                    UIManager.Instance.ShowPanelLose();
                }
                else
                {
                    UIManager.Instance.ShowPanelWin();
                }
                return;
            }
            reachEndPoint = true;
            atFloor++;
            OnInit();
            blockSpawner.GenerateBlock(indexPrefab, listToCollectBlock);
        }
    }

    private void RemoveBlockAndSetBackToGround()
    {
        numberBlockOwner--;
        Transform blockRemove = backPoint.GetChild(backPoint.childCount - 1);
        blockRemove.SetParent(blockSpawner.transform);
        blockRemove.GetComponent<Block>().LoadPos();
        listToCollectBlock.Add(blockRemove.transform.position);
    }

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            if (!string.IsNullOrEmpty(currentAnimName))
                anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(animName);
        }
    }
}
