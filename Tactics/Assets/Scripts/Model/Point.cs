using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Point
{
    //Holds x coordinate of point
    public int x;
    //Holds z coordinate of point
    public int y;

    //Constructor for points
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    //The following methods allow for common operations using the Point struct
    //Add the x and y values of one point to the respective values on the other point
    public static Point operator +(Point a, Point b)
    {
        return new Point(a.x + b.x, a.y + b.y);
    }

    //Subtract the x and y values of one point to the respective values on the other point
    public static Point operator -(Point p1, Point p2)
    {
        return new Point(p1.x - p2.x, p1.y - p2.y);
    }

    //Compare the x and y values of one point to the respective values on the other point
    public static bool operator ==(Point a, Point b)
    {
        return a.x == b.x && a.y == b.y;
    }

    //Compare the x and y values of one point to the respective values on the other point
    public static bool operator !=(Point a, Point b)
    {
        return !(a == b);
    }

    //Same as the == operator but allows the use of the Equals method
    public override bool Equals(object obj)
    {
        if (obj is Point)
        {
            Point p = (Point)obj;
            return x == p.x && y == p.y;
        }
        return false;
    }

    public bool Equals(Point p)
    {
        return x == p.x && y == p.y;
    }

    public override int GetHashCode()
    {
        return x ^ y;
    }

    //For debugging purposes
    public override string ToString()
    {
        return string.Format("({0},{1})", x, y);
    }
}
