using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageBox : MonoBehaviour
{
    public System.Action onMessageShown;

    [SerializeField]
    private TextMeshProUGUI message;
    [SerializeField]
    [Range(4f, 10f)]
    private float displayTime;
    [SerializeField]
    [Range(0.3f, 2f)]
    private float fadeOutTime;

    public void DisplayText(string text)
    {
        StopAllCoroutines();
        message.text = text;
        var color = message.color;
        color.a = 1f;
        message.color = color;
        StartCoroutine(HideText());
    }

    private void Start()
    {
        DisplayText("Welcome to TicTacToe!");
    }

    private IEnumerator HideText()
    {
        yield return new WaitForSeconds(displayTime);
        var color = message.color;
        while (message.color.a > 0)
        {
            color.a = color.a - Time.deltaTime * fadeOutTime;
            message.color = color;
            yield return null;
        }
        message.text = string.Empty;
        var action = onMessageShown;
        onMessageShown = null;
        action?.Invoke();
    }
}
