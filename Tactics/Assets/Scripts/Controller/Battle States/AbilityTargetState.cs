using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AbilityTargetState : BattleState
{
    List<Tile> tiles;
    AbilityRange ar;

    public override void Enter()
    {
        base.Enter();
        ar = turn.ability.GetComponent<AbilityRange>();
        SelectTiles();
        statPanelController.ShowPrimary(turn.actor.gameObject);
        if (ar.directionOriented)
            RefreshSecondaryStatPanel(pos, currentTile.height);
    }

    public override void Exit()
    {
        base.Exit();
        board.DeSelectTiles(tiles);
        statPanelController.HidePrimary();
        statPanelController.HideSecondary();
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

        if (ar.directionOriented)
        {
            ChangeDirection(e.info);
        }
        else
        {
            //SelectTile(e.info + pos, turn.actor.tile.height);
            //RefreshSecondaryStatPanel(pos, turn.actor.tile.height);
            int i = 0;
            for (i = 0; i < 100; i++)
            {
                if (board.GetTile(p + pos, i) == null)
                {

                }
                else
                {
                    SelectTile(p + pos, i);
                    RefreshSecondaryStatPanel(pos, i);
                    break;
                }
            }
        }
    }

    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        if (e.info == 0)
        {
            if (ar.directionOriented || tiles.Contains(board.GetTile(pos, currentTile.height)))
                owner.ChangeState<ConfirmAbilityTargetState>();
        }
        else
        {
            owner.ChangeState<CategorySelectionState>();
        }
    }

    void ChangeDirection(Point p)
    {
        Directions dir = p.GetDirection();
        InputController t = GameObject.FindObjectOfType<InputController>();
        int rC = t.rC;
        Directions a = Directions.East;
        if (rC == 0)
        {
            a = dir;
        }
        else if (rC == 1)
        {
            switch (dir)
            {
                case Directions.North:
                    a = Directions.East;
                    break;
                case Directions.East:
                    a = Directions.South;
                    break;
                case Directions.South:
                    a = Directions.West;
                    break;
                case Directions.West:
                    a = Directions.North;
                    break;
            }
        }
        else if (rC == 2)
        {
            switch (dir)
            {
                case Directions.North:
                    a = Directions.South;
                    break;
                case Directions.East:
                    a = Directions.West;
                    break;
                case Directions.South:
                    a = Directions.North;
                    break;
                case Directions.West:
                    a = Directions.East;
                    break;
            }
        }
        else if (rC == 3)
        {
            switch (dir)
            {
                case Directions.North:
                    a = Directions.West;
                    break;
                case Directions.East:
                    a = Directions.North;
                    break;
                case Directions.South:
                    a = Directions.East;
                    break;
                case Directions.West:
                    a = Directions.South;
                    break;
            }
        }
        if (turn.actor.dir != a)
        {
            board.DeSelectTiles(tiles);
            turn.actor.dir = a;
            turn.actor.Match();
            SelectTiles();
        }
    }

    void SelectTiles()
    {
        tiles = ar.GetTilesInRange(board);
        board.SelectTiles(tiles);
    }
}