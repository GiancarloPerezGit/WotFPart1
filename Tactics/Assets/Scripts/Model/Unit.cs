using UnityEngine;
using System.Collections;
public class Unit : MonoBehaviour
{
    public Tile tile { get; protected set; }
    public Directions dir;
    private GameObject nSprite;
    private GameObject eSprite;
    private GameObject sSprite;
    private GameObject wSprite;
    private GameObject nSprite2;
    private GameObject eSprite2;
    private GameObject sSprite2;
    private GameObject wSprite2;
    public int rotateCount = 0;
    //public static int rC = 0;
    private int activeSprite;
    public Transform[] children;
    public int adjust = 0;
    private void Awake()
    {
        children = transform.GetChild(0).GetComponentsInChildren<Transform>();
        nSprite = children[1].gameObject;
        eSprite = children[2].gameObject;
        sSprite = children[3].gameObject;
        wSprite = children[4].gameObject;
        nSprite2 = children[5].gameObject;
        eSprite2 = children[6].gameObject;
        sSprite2 = children[7].gameObject;
        wSprite2 = children[8].gameObject;
        //Match();
    }
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
            if (rotateCount == 0)
            {
                activateSprite(1);
            }
            else if (rotateCount == 1)
            {
                activateSprite(7);
            }
            else if (rotateCount == 2)
            {
                activateSprite(4);
            }
            else if (rotateCount == 3)
            {
                activateSprite(6);
            }
        }
        else if (dir == Directions.East)
        {
            if (rotateCount == 0)
            {
                activateSprite(2);
            }
            else if (rotateCount == 1)
            {
                activateSprite(6);
            }
            else if (rotateCount == 2)
            {
                activateSprite(3);
            }
            else if (rotateCount == 3)
            {
                activateSprite(7);
            }

        }
        else if (dir == Directions.South)
        {
            if (rotateCount == 0)
            {
                activateSprite(3);
            }
            else if (rotateCount == 1)
            {
                activateSprite(5);
            }
            else if (rotateCount == 2)
            {
                activateSprite(2);
            }
            else if (rotateCount == 3)
            {
                activateSprite(8);
            }
        }
        else if (dir == Directions.West)
        {
            if (rotateCount == 0)
            {
                activateSprite(4);
            }
            else if (rotateCount == 1)
            {
                activateSprite(8);
            }
            else if (rotateCount == 2)
            {
                activateSprite(1);
            }
            else if (rotateCount == 3)
            {
                activateSprite(9);
            }
        }
    }

    public void Turn()
    {
        //transform.localPosition = tile.center;
        if (dir == Directions.North)
        {
            if (rotateCount == 0)
            {
                activateSprite(1);
            }
            else if (rotateCount == 1)
            {
                activateSprite(7);
            }
            else if (rotateCount == 2)
            {
                activateSprite(4);
            }
            else if (rotateCount == 3)
            {
                activateSprite(6);
            }
        }
        else if (dir == Directions.East)
        {
            if(rotateCount == 0)
            {
                activateSprite(2);
            }
            else if(rotateCount == 1)
            {
                activateSprite(6);
            }
            else if (rotateCount == 2)
            {
                activateSprite(3);
            }
            else if (rotateCount == 3)
            {
                activateSprite(7);
            }

        }
        else if (dir == Directions.South)
        {
            if (rotateCount == 0)
            {
                activateSprite(3);
            }
            else if (rotateCount == 1)
            {
                activateSprite(5);
            }
            else if (rotateCount == 2)
            {
                activateSprite(2);
            }
            else if (rotateCount == 3)
            {
                activateSprite(8);
            }
        }
        else if (dir == Directions.West)
        {
            if (rotateCount == 0)
            {
                activateSprite(4);
            }
            else if (rotateCount == 1)
            {
                activateSprite(8);
            }
            else if (rotateCount == 2)
            {
                activateSprite(1);
            }
            else if (rotateCount == 3)
            {
                activateSprite(9);
            }
        }
    }

    public void activateSprite(int i)
    {
        activeSprite = i;
        for(int j = 1; j < 9; j++)
        {
            if (j == i)
            {
                children[j].gameObject.SetActive(true);
            }
            else
            {
                children[j].gameObject.SetActive(false);
            }
        }
    }

    public void RotateLeft()
    {
        if(rotateCount == 0)
        {
            switch(activeSprite)
            {
                case 1:
                    activateSprite(7);
                    break;
                case 2:
                    activateSprite(6);
                    break;
                case 3:
                    activateSprite(5);
                    break;
                case 4:
                    activateSprite(8);
                    break;
            }
            adjust = 4;
        }
        else if (rotateCount == 1)
        {
            switch (activeSprite)
            {
                case 5:
                    activateSprite(2);
                    break;
                case 6:
                    activateSprite(3);
                    break;
                case 7:
                    activateSprite(4);
                    break;
                case 8:
                    activateSprite(1);
                    break;
            }
            adjust = 0;
        }
        else if (rotateCount == 2)
        {
            switch (activeSprite)
            {
                case 1:
                    activateSprite(5);
                    break;
                case 2:
                    activateSprite(8);
                    break;
                case 3:
                    activateSprite(7);
                    break;
                case 4:
                    activateSprite(6);
                    break;
            }
            adjust = 4;
        }
        else if (rotateCount == 3)
        {
            switch (activeSprite)
            {
                case 5:
                    activateSprite(4);
                    break;
                case 6:
                    activateSprite(1);
                    break;
                case 7:
                    activateSprite(2);
                    break;
                case 8:
                    activateSprite(3);
                    break;
            }
            adjust = 0;
        }
        rotateCount += 1;
        
        if (rotateCount == 4)
        {
            rotateCount = 0;
        }
    }

    public void RotateRight()
    {
        if (rotateCount == 0)
        {
            switch (activeSprite)
            {
                case 1:
                    activateSprite(6);
                    break;
                case 2:
                    activateSprite(7);
                    break;
                case 3:
                    activateSprite(8);
                    break;
                case 4:
                    activateSprite(5);
                    break;
            }
            adjust = 4;
        }
        else if (rotateCount == 1)
        {
            switch (activeSprite)
            {
                case 5:
                    activateSprite(3);
                    break;
                case 6:
                    activateSprite(2);
                    break;
                case 7:
                    activateSprite(1);
                    break;
                case 8:
                    activateSprite(4);
                    break;
            }
            adjust = 0;
        }
        else if (rotateCount == 2)
        {
            switch (activeSprite)
            {
                case 1:
                    activateSprite(8);
                    break;
                case 2:
                    activateSprite(5);
                    break;
                case 3:
                    activateSprite(6);
                    break;
                case 4:
                    activateSprite(7);
                    break;
            }
            adjust = 4;
        }
        else if (rotateCount == 3)
        {
            switch (activeSprite)
            {
                case 5:
                    activateSprite(2);
                    break;
                case 6:
                    activateSprite(4);
                    break;
                case 7:
                    activateSprite(3);
                    break;
                case 8:
                    activateSprite(2);
                    break;
            }
            adjust = 0;
        }
        rotateCount -= 1;
        
        if(rotateCount == -1)
        {
            rotateCount = 3;
        }
    }

}