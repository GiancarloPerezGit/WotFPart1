using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTargetState : BattleState
{
    List<Tile> tiles;
    AbilityRange ar;
    public static int test = 0;
    public override void Enter()
    {
        base.Enter();
        ar = turn.ability.GetComponent<AbilityRange>();
        SelectTiles();
        statPanelController.ShowPrimary(turn.actor.gameObject);
        if (ar.directionOriented)
            RefreshSecondaryStatPanel(pos);
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
        if (ar.directionOriented)
        {
            ChangeDirection(e.info);
        }
        else
        {
            SelectTile(e.info + pos);
            RefreshSecondaryStatPanel(pos);
        }
    }

    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        if (e.info == 0)
        {
            if (ar.directionOriented || tiles.Contains(board.GetTile(pos)))
                owner.ChangeState<ConfirmAbilityTargetState>();
        }
        else
        {
            owner.ChangeState<CategorySelectionState>();
        }
        //if (e.info == 0)
        //{
        //    turn.hasUnitActed = true;
        //    if (turn.hasUnitMoved)
        //        turn.lockMove = true;
        //    if (test == 1)
        //    {
        //        CutSceneState.data = Resources.Load<ConversationData>("Conversations/NoMana1");
        //        owner.ChangeState<CutSceneState>();
        //    }
        //    else if (test == 2)
        //    {
        //        CutSceneState.data = Resources.Load<ConversationData>("Conversations/NoMana2");
        //        owner.ChangeState<CutSceneState>();
        //    }
        //    else if (test == 3)
        //    {
        //        CutSceneState.data = Resources.Load<ConversationData>("Conversations/NoMana3");
        //        CutSceneState.fade = true;
        //        owner.ChangeState<CutSceneState>();
        //    }
        //    else
        //    {
        //        owner.ChangeState<CommandSelectionState>();
        //    }

        //}
        //else
        //{
        //    owner.ChangeState<CategorySelectionState>();
        //}
    }
    void ChangeDirection(Point p)
    {
        Directions dir = p.GetDirection();
        if (turn.actor.dir != dir)
        {
            board.DeSelectTiles(tiles);
            if (dir == Directions.North)
            {
                turn.actor.nSprite.SetActive(true);
                turn.actor.eSprite.SetActive(false);
                turn.actor.sSprite.SetActive(false);
                turn.actor.wSprite.SetActive(false);
            }
            else if (dir == Directions.East)
            {
                turn.actor.nSprite.SetActive(false);
                turn.actor.eSprite.SetActive(true);
                turn.actor.sSprite.SetActive(false);
                turn.actor.wSprite.SetActive(false);
            }
            else if (dir == Directions.South)
            {
                turn.actor.nSprite.SetActive(false);
                turn.actor.eSprite.SetActive(false);
                turn.actor.sSprite.SetActive(true);
                turn.actor.wSprite.SetActive(false);
            }
            else if (dir == Directions.West)
            {
                turn.actor.nSprite.SetActive(false);
                turn.actor.eSprite.SetActive(false);
                turn.actor.sSprite.SetActive(false);
                turn.actor.wSprite.SetActive(true);
            }
            turn.actor.dir = dir;
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