using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class AIPlayer : Player
{
    public AIPlayer(string name) : base(name)
    {

    }

    public override void GetTurnControl()
    {
        Game.DisableControls();
    }

    private IEnumerator AITurn()
    {
        yield return new WaitForSeconds(1);

    }
}
