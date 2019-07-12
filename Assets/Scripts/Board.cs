using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Board : MonoBehaviour
{
    public Player CurrentPlayer { get => currentPlayer; }
    public Player NextPlayer { get => nextPlayer; }

    [SerializeField]
    private List<Cell> cells = new List<Cell>();
    [SerializeField]
    private GameObject[] marks = new GameObject[2];
    [SerializeField]
    private MessageBox messageBox;

    private int[,] boardState = new int[3, 3];
    private List<Player> players;
    private Player currentPlayer;
    private Player nextPlayer;

    private int currentTurn = 1;
    private int currentRound = 1;

    public void BeginGame(Player[] players)
    {
        this.players = new List<Player>(players);
        var randomNumber = Random.Range(0, 2);
        currentPlayer = players[randomNumber];
        nextPlayer = players[randomNumber == 0 ? 1 : 0];
        currentPlayer.SetPlayerMark(MarkType.X);
        nextPlayer.SetPlayerMark(MarkType.O);
        currentPlayer.roundsWon = 0;
        nextPlayer.roundsWon = 0;
        Game.EnableControls();
        currentPlayer.GetTurnControl();
        messageBox.DisplayText($"Game begins! {currentPlayer} makes first turn");
    }

    public void MakeTurn(Vector2Int position)
    {
        var cell = cells.Find(x => x.Position == position);
        boardState[position.x, position.y] = (int)currentPlayer.Mark;
        cell.MarkCell(currentPlayer.Mark);
        cell.SetMarkObject(
            Instantiate(
                marks[(int)currentPlayer.Mark - 1],
                cell.transform.position,
                cell.transform.rotation * Quaternion.Euler(45f, 0f, 0f)));
        CheckBoard((int)currentPlayer.Mark);
    }

    private void Awake()
    {
        Game.SetBoard(this);
        ClearBoard();
        //Game.InitTestPlayers();

        foreach (var cell in cells)
        {
            cell.Init(MakeTurn);
        }
    }

    private void Start()
    {
        
    }

    private void ClearBoard()
    {
        foreach (var cell in cells)
        {
            cell.Clear();
        }

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                boardState[x, y] = 0;
            }
        }
    }

    private void EndTurn()
    {
        var temp = currentPlayer;
        currentPlayer = nextPlayer;
        nextPlayer = temp;
        currentTurn++;
        currentPlayer.GetTurnControl();
        messageBox.DisplayText($"Turn {currentTurn}: {currentPlayer.GetColoredName()}");
    }

    private void InvertPlayersMarks()
    {
        if (currentPlayer != null && nextPlayer != null)
        {
            var temp = currentPlayer.Mark;
            currentPlayer.SetPlayerMark(nextPlayer.Mark);
            nextPlayer.SetPlayerMark(temp);
        }
    }

    private void CheckBoard(int playerMark)
    {
        if (currentTurn < 5)
        {
            EndTurn();
            return;
        }

        foreach (var winLine in WinTable.WinLines)
        {
            var check = 0;
            foreach (var position in winLine)
            {
                if (boardState[position.x, position.y] == playerMark) check++;
            }
            if (check == 3)
            {
                foreach (var position in winLine)
                {
                    cells.Find(cell => cell.Position == position).HighlightMark();
                }
                currentPlayer.roundsWon++;
                messageBox.DisplayText($"{currentPlayer.GetColoredName()} won the round!");
                StartCoroutine(EndRound());
                return;
            }
        }

        if (currentTurn == 9)
        {
            EndRound();
            return;
        }
        EndTurn();
    }

    private IEnumerator EndRound()
    {
        currentTurn = 1;
        yield return new WaitForSeconds(2);
        foreach (var player in players)
        {
            if (player.roundsWon == Game.RoundsToWin)
            {

                player.WriteGameResult(1);
                Opponent(player).WriteGameResult(-1);
                messageBox.DisplayText($"{player.GetColoredName()} won the game!");
                StartCoroutine(EndGame());
                yield break;
            }
        }
        InvertPlayersMarks();
        currentRound++;
        ClearBoard();
        messageBox.DisplayText($"Round {currentRound} begins! {players[0].GetColoredName()} {players[0].roundsWon}:{players[1].roundsWon} {players[1].GetColoredName()}");
    }

    private IEnumerator EndGame()
    {
        Game.DisableControls();
        currentRound = 1;
        currentTurn = 1;
        yield return new WaitForSeconds(3);
        ClearBoard();
    }

    private Player Opponent(Player target)
    {
        return players.Find(player => player.Name != target.Name);
    }
}
