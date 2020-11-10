using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //This variable determines how much variety in height is possible. At 0.25, a height of 1 would require 4 stacked tiles
    public const float stepHeight = 0.125f;
    
    //x and z value
    public Point pos;

    //y value
    public int height;

    //Object above tile
    public GameObject content;

    public Directions dir;

    public bool slope = false;
    //Previous tile
    [HideInInspector] public Tile prev;

    //Distance traveled so far
    [HideInInspector] public int distance;

    public Vector3 rotation;

    //Center of the tile. For placing models on.
    public Vector3 center
    {
        get
        {
            return new Vector3(pos.x, height * stepHeight, pos.y);
        }
    }

    //Update tile to match the new height/pos
    void Match()
    {
        transform.localPosition = new Vector3(pos.x, height * stepHeight, pos.y);
        //transform.localScale = new Vector3(1, height * stepHeight, 1);
        transform.localRotation = Quaternion.Euler(rotation);
    }

    //Following methods allow the board creator to create a unique topography
    //Extend tile
    public void Grow()
    {
        height++;
        Match();
    }

    //Shrink tile
    public void Shrink()
    {
        height--;
        Match();
    }

    //Following methods are for allowing custom map creation
    //Set the xyz using a Point and an Int
    public void Load(Point p, int h)
    {
        pos = p;
        height = h;
        Match();
    }

    //Set the xyz using a Unity Vector
    public void Load(Vector3 v, bool b, Vector3 q)
    {
        
        slope = b;
        rotation = q;
        if (slope)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        Load(new Point((int)v.x, (int)v.z), (int)v.y);
    }

    
}
