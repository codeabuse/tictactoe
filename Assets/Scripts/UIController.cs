using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField p1NameInput;
    [SerializeField]
    private TMP_InputField p2NameInput;
    [SerializeField]
    private Button aiPlayerButton;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private GameObject gameplayUI;
    [SerializeField]
    private TMP_Text player1Name;
    [SerializeField]
    private TMP_Text player2Name;
    [SerializeField]
    private MessageBox messageBox;
    [SerializeField]
    private Slider roundsToWinSlider;
    [SerializeField]
    private TMP_Text roundsToWinCountText;
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private GameObject[] virtualCameras;

    private bool aiPlayerSelected;
    private string p2SavedName;
    private string aiPlayerName = "[Computer]";


    private void Start()
    {
        aiPlayerButton.onClick.AddListener(() =>
        {
            aiPlayerSelected = !aiPlayerSelected;
            if (aiPlayerSelected)
            {
                p2SavedName = p2NameInput.text;
            }
            p2NameInput.text = aiPlayerSelected ? aiPlayerName : p2SavedName;
            aiPlayerButton.GetComponentInChildren<TextMeshProUGUI>().text = 
                aiPlayerSelected ? "HUMAN PLAYER" : "AI PLAYER";
            p2NameInput.interactable = !aiPlayerSelected;
        });
        roundsToWinSlider.onValueChanged.AddListener((value) =>
        {
            Game.RoundsToWin = (int)value;
            roundsToWinCountText.text = value.ToString();
        });
        playButton.onClick.AddListener(() =>
        {
            Play(p1NameInput.text, p2NameInput.text);
            Fade(false);
        });
    }

    private void Play(string p1, string p2)
    {
        if (p1NameInput.text.Length < 3 && p2NameInput.text.Length < 3)
        {
            messageBox.DisplayText("Name should contain at leas 3 symbols");
            return;
        }
        var player1 = new Player(p1NameInput.text);
        var player2 = aiPlayerSelected ? new AIPlayer(aiPlayerName) : new Player(p2NameInput.text);
        Game.SetActivePlayer(player1, 0);
        Game.SetActivePlayer(player2, 1);
        Game.GameBoard.BeginGame(new [] { player1, player2 });
        messageBox.DisplayText(
            $"Game begins!\n{player1.GetColoredName()} vs. {player2.GetColoredName()}\n{Game.RoundsToWin} rounds to win");
    }

    //TODO: tie fade out func to game end event
    private void Fade(bool show)
    {
        StartCoroutine(DoFade(show));
    }

    private IEnumerator DoFade(bool show)
    {
        var targetAlpha = show ? 1f : 0f;
        while (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, Time.deltaTime * 2.5f);
            yield return null;
        }
    }
}
