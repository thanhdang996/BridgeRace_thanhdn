using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    private int level = 1;
    public int Level { get => level; set => level = value; }

    public override void Awake()
    {
        base.Awake();
        LoadGameData();
    }

    public void LoadGameData()
    {
        Level = PlayerPrefs.GetInt("level", 1);
    }

    public void SaveGameData()
    {
        PlayerPrefs.SetInt("level", Level);
    }

    private bool isStartGame;
    public bool IsStartGame { get => isStartGame; set => isStartGame = value; }


    private bool hasOneWinning;
    public bool HasOneWinning { get => hasOneWinning; set => hasOneWinning = value; }

    private bool lose;
    public bool Lose { get => lose; set => lose = value; }



}
