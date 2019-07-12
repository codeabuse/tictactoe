using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KeyMap
{
    public static readonly IList<KeyCode> Player1DefaultKeys = new List<KeyCode>()
    {
        KeyCode.Q, KeyCode.W, KeyCode.E,
        KeyCode.A, KeyCode.S, KeyCode.D,
        KeyCode.Z, KeyCode.X, KeyCode.C
    }.AsReadOnly();

    public static readonly IList<KeyCode> Player2DefaultKeys = new List<KeyCode>()
    {
        KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9,
        KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6,
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3
    }.AsReadOnly();
}
