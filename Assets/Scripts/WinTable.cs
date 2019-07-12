using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WinTable
{
    public static readonly IReadOnlyList<Vector2Int[]> WinLines = new List<Vector2Int[]>()
    {
        new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(0,2) },
        new Vector2Int[] { new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(1,2) },
        new Vector2Int[] { new Vector2Int(2, 0), new Vector2Int(2, 1), new Vector2Int(2,2) },
        new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(2,0) },
        new Vector2Int[] { new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(2,1) },
        new Vector2Int[] { new Vector2Int(0, 2), new Vector2Int(1, 2), new Vector2Int(2,2) },
        new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(1, 1), new Vector2Int(2,2) },
        new Vector2Int[] { new Vector2Int(2, 0), new Vector2Int(1, 1), new Vector2Int(0,2) }
    }.AsReadOnly();
}
