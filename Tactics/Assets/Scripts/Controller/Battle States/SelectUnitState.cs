using UnityEngine;
using System.Collections;

public class SelectUnitState : BattleState 
{
	public override void Enter ()
	{
		base.Enter ();
		StartCoroutine("ChangeCurrentUnit");
	}

	public override void Exit ()
	{
		base.Exit ();
		statPanelController.HidePrimary();
	}

	IEnumerator ChangeCurrentUnit ()
	{
		owner.round.MoveNext();
		SelectTile(turn.actor.tile.pos, turn.actor.tile.height);
		RefreshPrimaryStatPanel(pos, turn.actor.tile.height);
		yield return null;
		owner.ChangeState<CommandSelectionState>();
	}
}