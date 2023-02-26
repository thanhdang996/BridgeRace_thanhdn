using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterProps
{
    [SerializeField] private PlayerMovement playerMovement;

    public PlayerMovement PlayerMovement => playerMovement;

    private void Update()
    {
        HandleUpdateAnim();
    }
    private void HandleUpdateAnim()
    {
        if (GameManager.Instance.IsStartGame)
        {
            if (isWin)
            {
                ChangeAnim("Win");
                return;
            }

            if (PlayerMovement.IsNotMoving())
            {
                ChangeAnim("Idle");
                return;
            }
            if (!PlayerMovement.IsNotMoving())
            {
                ChangeAnim("Run");
                return;
            }
        }
    }
}
