using UnityEngine;
using System.Collections;

public class EndFacingState : BattleState
{
    Directions startDir;

    public override void Enter()
    {
        base.Enter();
        startDir = turn.actor.dir;
        SelectTile(turn.actor.tile.pos, turn.actor.tile.height);
    }

    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        InputController t = GameObject.FindObjectOfType<InputController>();
        int rC = t.rC;
        Directions a = Directions.East;
        if(rC == 0)
        {
            a = e.info.GetDirection();
        }
        else if (rC == 1)
        {
            switch(e.info.GetDirection())
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
            switch (e.info.GetDirection())
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
            switch (e.info.GetDirection())
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
        turn.actor.dir = a;
        turn.actor.Match();
    }

    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        switch (e.info)
        {
            case 0:
                owner.ChangeState<SelectUnitState>();
                break;
            case 1:
                turn.actor.dir = startDir;
                turn.actor.Match();
                owner.ChangeState<CommandSelectionState>();
                break;
        }
    }
}