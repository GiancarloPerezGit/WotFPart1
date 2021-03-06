﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public abstract class Movement : MonoBehaviour
{
    public int range { get { return stats[StatTypes.MOV]; } }
    public int jumpHeight { get { return stats[StatTypes.JMP]; } }
    protected Stats stats;
    protected Unit unit;
    protected Transform jumper;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
        jumper = transform.Find("Jumper");
    }

    protected virtual void Start()
    {
        stats = GetComponent<Stats>();
    }

    public virtual List<Tile> GetTilesInRange(Board board)
    {
        List<Tile> retValue = board.Search(unit.tile, ExpandSearch);
        Filter(retValue);
        return retValue;
    }

    protected virtual bool ExpandSearch(Tile from, Tile to)
    {
        return (from.distance + 1) <= range;
    }

    protected virtual void Filter(List<Tile> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
            if (tiles[i].content != null)
                tiles.RemoveAt(i);
    }

    public abstract IEnumerator Traverse(Tile tile);

    protected virtual IEnumerator Turn(Directions dir)
    {
        //if (dir == Directions.North)
        //{
        //    unit.nSprite.SetActive(true);
        //    unit.eSprite.SetActive(false);
        //    unit.sSprite.SetActive(false);
        //    unit.wSprite.SetActive(false);
        //}
        //else if (dir == Directions.East)
        //{
        //    unit.nSprite.SetActive(false);
        //    unit.eSprite.SetActive(true);
        //    unit.sSprite.SetActive(false);
        //    unit.wSprite.SetActive(false);
        //}
        //else if (dir == Directions.South)
        //{
        //    unit.nSprite.SetActive(false);
        //    unit.eSprite.SetActive(false);
        //    unit.sSprite.SetActive(true);
        //    unit.wSprite.SetActive(false);
        //}
        //else if (dir == Directions.West)
        //{
        //    unit.nSprite.SetActive(false);
        //    unit.eSprite.SetActive(false);
        //    unit.sSprite.SetActive(false);
        //    unit.wSprite.SetActive(true);
        //}
        
        //TransformLocalEulerTweener t = (TransformLocalEulerTweener)transform.RotateToLocal(dir.ToEuler(), 0.25f, EasingEquations.EaseInOutQuad);

        // When rotating between North and West, we must make an exception so it looks like the unit
        // rotates the most efficient way (since 0 and 360 are treated the same)
        //if (Mathf.Approximately(t.startValue.y, 0f) && Mathf.Approximately(t.endValue.y, 270f))
        //    t.startValue = new Vector3(t.startValue.x, 360f, t.startValue.z);
        //else if (Mathf.Approximately(t.startValue.y, 270) && Mathf.Approximately(t.endValue.y, 0))
        //    t.endValue = new Vector3(t.startValue.x, 360f, t.startValue.z);
        unit.dir = dir;
        unit.Turn(); 
        //while (t != null)
        yield return null;
    }

}