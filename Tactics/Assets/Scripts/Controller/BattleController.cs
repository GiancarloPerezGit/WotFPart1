using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : StateMachine
{
    public CameraRig cameraRig;
    public Board board;
    public LevelData levelData;
    public Transform tileSelectionIndicator;
    public Point pos;
    public float height;
    public GameObject heroPrefab;
    public GameObject guardPrefab;
    //public Unit currentUnit;
    
    public AbilityMenuPanelController abilityMenuPanelController;
    public Turn turn = new Turn();
    public List<Unit> units = new List<Unit>();
    public StatPanelController statPanelController;
    public IEnumerator round;
    public HitSuccessIndicator hitSuccessIndicator;
    public Tile currentTile { get { return board.GetTile(pos, tileSelectionIndicator.position.y/0.125f); } }
    void Start()
    {
        ChangeState<InitBattleState>();
    }
}