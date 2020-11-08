using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveTargetState : BattleState
{
    List<Tile> tiles;
    //public static int rC = 0;
    public override void Enter()
    {
        base.Enter();
        Movement mover = turn.actor.GetComponent<Movement>();
        tiles = mover.GetTilesInRange(board);
        board.SelectTiles(tiles);
        RefreshPrimaryStatPanel(pos);
    }

    public override void Exit()
    {
        base.Exit();
        board.DeSelectTiles(tiles);
        tiles = null;
        statPanelController.HidePrimary();
    }

    protected override void OnMove(object sender, InfoEventArgs<Point> e)
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
        SelectTile(p + pos);
        RefreshPrimaryStatPanel(pos);
    }

    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        if (e.info == 0)
        {
            if (tiles.Contains(owner.currentTile))
                owner.ChangeState<MoveSequenceState>();
        }
        else
        {
            owner.ChangeState<CommandSelectionState>();
        }
    }
}