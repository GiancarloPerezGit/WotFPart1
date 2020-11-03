using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ActionSelectionState : BaseAbilityMenuState
{
    public static int category;
    string[] whiteMagicOptions = new string[] { "Cure", "Raise", "Holy" };
    string[] blackMagicOptions = new string[] { "Fire", "Ice", "Lightning" };
    protected override void LoadMenu()
    {
        if (menuOptions == null)
            menuOptions = new List<string>(3);
        if (category == 0)
        {
            menuTitle = "White Magic";
            SetOptions(whiteMagicOptions);
        }
        else
        {
            menuTitle = "Black Magic";
            SetOptions(blackMagicOptions);
        }
        abilityMenuPanelController.Show(menuTitle, menuOptions);
    }
    protected override void Confirm()
    {
        turn.hasUnitActed = true;
        if (turn.hasUnitMoved)
            turn.lockMove = true;
        //owner.ChangeState<CommandSelectionState>();
        print(category);
        switch (abilityMenuPanelController.selection)
        {
            case 0:
                fire();
                break;
            case 1:
                if(category == 1)
                {
                    turn.ability = turn.actor.GetComponentInChildren<LineAbilityRange>().gameObject;
                    AbilityTargetState.test = 1;
                    owner.ChangeState<AbilityTargetState>();
                }
                break;
            case 2:
                if (category == 1)
                {
                    turn.ability = turn.actor.GetComponentInChildren<InfiniteAbilityRange>().gameObject;
                    AbilityTargetState.test = 3;
                    owner.ChangeState<AbilityTargetState>();
                }
                break;
        }
        //owner.ChangeState<CommandSelectionState>();
    }
    protected override void Cancel()
    {
        owner.ChangeState<CategorySelectionState>();
    }
    void SetOptions(string[] options)
    {
        menuOptions.Clear();
        for (int i = 0; i < options.Length; ++i)
            menuOptions.Add(options[i]);
    }

    public override void Enter()
    {
        base.Enter();
        statPanelController.ShowPrimary(turn.actor.gameObject);
    }
    public override void Exit()
    {
        base.Exit();
        statPanelController.HidePrimary();
    }

    void fire()
    {
        if (category == 1)
        {
            print("TEST");
            turn.ability = turn.actor.GetComponentInChildren<ConeAbilityRange>().gameObject;
            AbilityTargetState.test = 2;
            owner.ChangeState<AbilityTargetState>();
        }
    }
}