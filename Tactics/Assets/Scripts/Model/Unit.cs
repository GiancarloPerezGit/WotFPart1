using UnityEngine;
using System.Collections;
public class Unit : MonoBehaviour
{
    public Tile tile { get; protected set; }
    public Directions dir;
    public GameObject nSprite;
    public GameObject eSprite;
    public GameObject sSprite;
    public GameObject wSprite;

    public void Place(Tile target)
    {
        // Make sure old tile location is not still pointing to this unit
        if (tile != null && tile.content == gameObject)
            tile.content = null;

        // Link unit and tile references
        tile = target;

        if (target != null)
            target.content = gameObject;
    }
    public void Match()
    {
        transform.localPosition = tile.center;
        if (dir == Directions.North)
        {
            nSprite.SetActive(true);
            eSprite.SetActive(false);
            sSprite.SetActive(false);
            wSprite.SetActive(false);
        }
        else if (dir == Directions.East)
        {
            nSprite.SetActive(false);
            eSprite.SetActive(true);
            sSprite.SetActive(false);
            wSprite.SetActive(false);
        }
        else if (dir == Directions.South)
        {
            nSprite.SetActive(false);
            eSprite.SetActive(false);
            sSprite.SetActive(true);
            wSprite.SetActive(false);
        }
        else if (dir == Directions.West)
        {
            nSprite.SetActive(false);
            eSprite.SetActive(false);
            sSprite.SetActive(false);
            wSprite.SetActive(true);
        }
    }
}