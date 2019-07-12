using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


internal static class Game
{
    //TODO: make some basic events to control game flow
    public static int RoundsToWin
    {
        get => roundsToWin;
        set => roundsToWin = Mathf.Clamp(value, 2, 100);
    }
    public static bool ControlsAllowed { get => controlsAllowed; }
    public static Board GameBoard { get => gameBoard; }

    public static readonly Color[] playerColors = new Color[] { Color.red, Color.blue };

    private static int roundsToWin = 2;
    private static Player[] activePlayers = new Player[2];
    private static Board gameBoard;
    private static bool controlsAllowed;

    public static void SetActivePlayer(Player player, int id)
    {
        activePlayers[id] = player;
    }

    public static void SetBoard(Board board)
    {
        gameBoard = board;
    }

    public static void EnableControls()
    {
        controlsAllowed = true;
    }

    public static void DisableControls()
    {
        controlsAllowed = false;
    }

    public static void InitTestPlayers()
    {
        SetActivePlayer(new Player("John"), 0);
        SetActivePlayer(new Player("Jane"), 1);
        gameBoard.BeginGame(activePlayers);
    }

    private static void SetRoundsCount(int rounds)
    {
        roundsToWin = Mathf.Clamp(rounds, 1, 32);
    }
}