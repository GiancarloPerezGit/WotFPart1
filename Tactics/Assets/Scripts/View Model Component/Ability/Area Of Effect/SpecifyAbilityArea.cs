using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SpecifyAbilityArea : AbilityArea
{
    public int horizontal;
    public int vertical;
    Tile tile;
    public override List<Tile> GetTilesInArea(Board board, Point pos, float h)
    {
        tile = board.GetTile(pos, h);
        return board.Search(tile, ExpandSearch);
    }
    bool ExpandSearch(Tile from, Tile to)
    {
        return (from.distance + 1) <= horizontal && Mathf.Abs(to.height - tile.height) <= vertical;
    }
}