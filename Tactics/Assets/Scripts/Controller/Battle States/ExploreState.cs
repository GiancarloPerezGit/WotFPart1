using UnityEngine;
using System.Collections;

public class ExploreState : BattleState 
{
	public override void Enter ()
	{
		base.Enter ();
		RefreshPrimaryStatPanel(pos, turn.actor.tile.height);
	}

	public override void Exit ()
	{
		base.Exit ();
		statPanelController.HidePrimary();
	}

	protected override void OnMove (object sender, InfoEventArgs<Point> e)
	{
        InputController t = GameObject.FindObjectOfType<InputController>();
        int rC = t.rC;
        int w = e.info.x;
        int z = e.info.y;
        int x;
        int y;
        if (rC == 1)
        {

            x = z;
            y = -w;
        }
        else if (rC == 2)
        {
            x = -w;
            y = -z;
        }
        else if (rC == 3)
        {
            x = -z;
            y = w;
        }
        else
        {
            x = w;
            y = z;
        }

        Point p = new Point(x, y);
        //SelectTile(p + pos, turn.actor.tile.height);
        int i = 0;
        for (i = 0; i < 10000; i++)
        {
            if (board.GetTile(p + pos, i) == null)
            {

            }
            else
            {
                SelectTile(p + pos, i);
                break;
            }
        }
        RefreshPrimaryStatPanel(pos, turn.actor.tile.height);
	}
	
	protected override void OnFire (object sender, InfoEventArgs<int> e)
	{
		if (e.info == 0)
			owner.ChangeState<CommandSelectionState>();
	}

    protected override void OnCycle(object sender, InfoEventArgs<int> e)
    {
        for(int i = (int)(tileSelectionIndicator.position.y/0.125f) + 1; i < 10000; i++)
        {
            if(board.GetTile(pos, i) == null)
            {

            }
            else
            {
                SelectTile(pos, i);
                //print(currentTile.height);
                return;
            }
        }
        for (int i = (int)(tileSelectionIndicator.position.y / 0.125f) - 1; i >= 0; i--)
        {
            if (board.GetTile(pos, i) == null)
            {

            }
            else
            {
                SelectTile(pos, i);
                return;
            }
        }

    }
}