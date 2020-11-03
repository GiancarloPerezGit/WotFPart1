using UnityEngine;
using System.Collections;
public class InitBattleState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Init());
    }
    IEnumerator Init()
    {
        board.Load(levelData);
        Point p = new Point((int)levelData.tiles[0].x, (int)levelData.tiles[0].z);
        SelectTile(p);
        SpawnTestUnits(); // This is new
        owner.round = owner.gameObject.AddComponent<TurnOrderController>().Round();
        yield return null;
        owner.ChangeState<CutSceneState>();
    }
    void SpawnTestUnits()
    {
        string[] jobs = new string[] { "Rogue", "Warrior", "Wizard" };
        for (int i = 0; i < jobs.Length; ++i)
        {
            GameObject instance = Instantiate(owner.heroPrefab) as GameObject;
            Stats s = instance.AddComponent<Stats>();
            s[StatTypes.LVL] = 1;
            GameObject jobPrefab = Resources.Load<GameObject>("Jobs/" + jobs[i]);
            GameObject jobInstance = Instantiate(jobPrefab) as GameObject;
            jobInstance.transform.SetParent(instance.transform);
            Job job = jobInstance.GetComponent<Job>();
            job.Employ();
            job.LoadDefaultStats();
            Point p = new Point((int)levelData.tiles[i].x, (int)levelData.tiles[i].z);
            Unit unit = instance.GetComponent<Unit>();
            unit.Place(board.GetTile(p));
            unit.Match();
            instance.AddComponent<WalkMovement>();
            units.Add(unit);
            //    Rank rank = instance.AddComponent<Rank>();
            //    rank.Init (10);
        }
        //string[] jobs = new string[] { "Rogue", "Warrior" };
        //for (int i = 0; i < jobs.Length -1; ++i)
        //{
        //    GameObject instance = Instantiate(owner.heroPrefab) as GameObject;
        //    Stats s = instance.AddComponent<Stats>();
        //    s[StatTypes.LVL] = 1;
        //    GameObject jobPrefab = Resources.Load<GameObject>("Jobs/" + jobs[i]);
        //    GameObject jobInstance = Instantiate(jobPrefab) as GameObject;
        //    jobInstance.transform.SetParent(instance.transform);
        //    Job job = jobInstance.GetComponent<Job>();
        //    job.Employ();
        //    job.LoadDefaultStats();
        //    Point p = new Point((int)levelData.tiles[i].x, (int)levelData.tiles[i].z);
        //    Unit unit = instance.GetComponent<Unit>();
        //    unit.Place(board.GetTile(p));
        //    unit.Match();
        //    instance.AddComponent<WalkMovement>();
        //    units.Add(unit);
        //    //    Rank rank = instance.AddComponent<Rank>();
        //    //    rank.Init (10);
        //}

        //GameObject guard = Instantiate(owner.guardPrefab);
        //Stats q = guard.AddComponent<Stats>();
        //q[StatTypes.LVL] = 1;
        //GameObject jobPefab = Resources.Load<GameObject>("Jobs/" + jobs[1]);
        //GameObject jobIstance = Instantiate(jobPefab) as GameObject;
        //jobIstance.transform.SetParent(guard.transform);
        //Job jb = jobIstance.GetComponent<Job>();
        //jb.Employ();
        //jb.LoadDefaultStats();
        //Unit enem = guard.GetComponent<Unit>();
        //Point t = new Point(3, 2);
        //enem.Place(board.GetTile(t));
        //enem.Match();
        //guard.AddComponent<WalkMovement>();
        //units.Add(enem);
    }
}