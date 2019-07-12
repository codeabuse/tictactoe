using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
internal class Cell : MonoBehaviour
{
    public Vector2Int Position { get => position; }

    [SerializeField]
    private Vector2Int position;
    private MarkType cellMark = MarkType.Empty;
    private GameObject markObject;

    private Action<Vector2Int> onCellClicked;
    private bool mouseDown;
    private bool mouseOver;

    public void Init(Action<Vector2Int> onClickAction)
    {
        onCellClicked = onClickAction;
    }

    public void Clear()
    {
        if (cellMark != MarkType.Empty)
        {
            Destroy(markObject);
            markObject = null;
            cellMark = MarkType.Empty;
        }
    }

    public void MarkCell(MarkType mark)
    {
        cellMark = mark;
    }

    public void HighlightMark()
    {

    }

    public void SetMarkObject(GameObject mark)
    {
        markObject = mark;
    }

    private void OnMouseDown()
    {
        if (Game.ControlsAllowed)
        {
            mouseDown = true;
            StartCoroutine(ClickAnimation(1f));
        }
    }

    private void OnMouseEnter()
    {
        if (Game.ControlsAllowed)
        {
            mouseOver = true;
        }
    }

    private void OnMouseUpAsButton()
    {
        if (Game.ControlsAllowed && cellMark == MarkType.Empty)
        {
            onCellClicked.Invoke(position);
        }
        mouseDown = false;
    }

    private void OnMouseExit()
    {
        if (Game.ControlsAllowed)
        {
            mouseDown = false;
            mouseOver = false;
        }
    }

    private IEnumerator ClickAnimation(float time)
    {
        var material = GetComponent<MeshRenderer>().material;
        var selectedColor = Game.playerColors[(int)Game.GameBoard.CurrentPlayer.Mark - 1];
        selectedColor.a = 0.5f;
        material.color = selectedColor;
        var k = 1f;
        while (mouseOver && mouseDown)
        {
            yield return null;
        }
        while (material.color.a > 0)
        {
            var color = material.color;
            color.a = color.a - Time.deltaTime * k;
            material.color = color;
            yield return null;
        }
    }

    private IEnumerator Highlight()
    {
        if (!markObject) yield break;
        var mRenderer = markObject.GetComponent<MeshRenderer>();
    }
}
