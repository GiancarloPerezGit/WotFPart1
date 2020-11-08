using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : ScriptableObject
{
    //save location and height of the floor of the board
    public List<Vector3> tiles;
    public List<Vector3> rotation;
    public List<bool> slope;
}