using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Player
{
    public string Name { get => name; }
    public int Wins { get => wins; }
    public int Losses { get => losses; }
    public int Draws { get => draws; }
    public MarkType Mark { get => mark; }

    private readonly string name;
    private int wins;
    private int losses;
    private int draws;
    private MarkType mark;

    public int roundsWon;

    public Player(string name)
    {
        this.name = name;
    }

    public virtual void GetTurnControl()
    {

    }

    public void SetPlayerMark(MarkType mark)
    {
        this.mark = mark;
    }

    public void WriteGameResult(int result)
    {
        switch (result)
        {
            case -1: losses++; break;
            case 0: draws++; break;
            case 1: wins++; break;
            default: throw new System.ArgumentException("Value should be between -1 and 1 inclusively", "result");
        }
    }

    public string GetColoredName()
    {
        return mark != MarkType.Empty ? name.PaingToHTML(Game.playerColors[(int)mark - 1]) : name;
    }

    public override string ToString()
    {
        return name;
    }
}

public enum MarkType
{
    Empty,
    X,
    O
}