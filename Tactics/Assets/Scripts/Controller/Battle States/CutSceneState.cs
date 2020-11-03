using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CutSceneState : BattleState
{
    ConversationController conversationController;
    public static ConversationData data;
    public static bool fade = false;
    protected override void Awake()
    {
        base.Awake();
        conversationController = owner.GetComponentInChildren<ConversationController>();
        data = Resources.Load<ConversationData>("Conversations/IntroScene");
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (data)
            Resources.UnloadAsset(data);
    }
    public override void Enter()
    {
        base.Enter();
        conversationController.Show(data);
    }
    protected override void AddListeners()
    {
        base.AddListeners();
        ConversationController.completeEvent += OnCompleteConversation;
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        ConversationController.completeEvent -= OnCompleteConversation;
    }
    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        base.OnFire(sender, e);
        conversationController.Next();
    }
    void OnCompleteConversation(object sender, System.EventArgs e)
    {
        if(fade)
        {
            FadeToBlack.fade = fade;
        }
        owner.ChangeState<SelectUnitState>();
    }
}