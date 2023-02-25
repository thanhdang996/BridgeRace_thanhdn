using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool isStartGame;

    public bool IsStartGame { get => isStartGame; set => isStartGame = value; }


    private bool hasOneWinning;
    public bool HasOneWinning { get => hasOneWinning; set => hasOneWinning = value; }

    private bool lose;
    public bool Lose { get => lose; set => lose = value; }



}
