using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public GameObject heading;

    Repeater _hor = new Repeater("Horizontal");
    Repeater _ver = new Repeater("Vertical");

    private bool shift = false;
    public static event EventHandler<InfoEventArgs<Point>> moveEvent;
    public static event EventHandler<InfoEventArgs<int>> fireEvent;
    string[] _buttons = new string[] { "Fire1", "Fire2", "Fire3" };

    public BattleController battleController;

    public int rC = 0;
    // Start is called before the first frame update
    void Update()
    {
        int x = _hor.Update();
        int y = _ver.Update();
        
        
        if (x != 0 || y != 0)
        {
            if (moveEvent != null)
                moveEvent(this, new InfoEventArgs<Point>(new Point(x, y)));
        }
        for (int i = 0; i < 3; ++i)
        {
            if (Input.GetButtonUp(_buttons[i]))
            {
                if (fireEvent != null)
                    fireEvent(this, new InfoEventArgs<int>(i));
            }
        }
        if(Input.GetKeyDown("q") && !shift)
        {
            StartCoroutine(rotate(1));
            rC += 1;
            if(rC == 4)
            {
                rC = 0;
            }
        }
        if (Input.GetKeyDown("e") && !shift)
        {
            StartCoroutine(rotate(-1));
            rC -= 1;
            if (rC == -1)
            {
                rC = 3;
            }
        }
    }

    IEnumerator rotate(int i)
    {
        shift = true;
        bool flip = false;
        Quaternion start = heading.transform.rotation;
        Quaternion destination = start * Quaternion.Euler(0f, 90f * i, 0f);
        float startTime = Time.time;
        float percentComplete = 0f;
        while (percentComplete <= 1.0f)
        {
            percentComplete = (Time.time - startTime) / 4;
            heading.transform.rotation = Quaternion.Slerp(start, destination, percentComplete);
            if (percentComplete > 0.5f && !flip)
            {
                if(i > 0)
                {
                    for (int k = 0; k < battleController.units.Count; k++)
                    {
                        battleController.units[k].GetComponent<Unit>().RotateLeft();
                    }
                }
                else
                {
                    for (int k = 0; k < battleController.units.Count; k++)
                    {
                        battleController.units[k].GetComponent<Unit>().RotateRight();
                    }
                }
                
                flip = true;
            }
            yield return null;
        }
        
        shift = false;
        yield return null;
    }
    // Update is called once per frame
    //void Update()
    //{
    //    Debug.Log(Input.GetAxisRaw("Horizontal"));
    //}
    class Repeater
    {
        const float threshold = 0.5f;
        const float rate = 0.25f;
        float _next;
        bool _hold;
        string _axis;
        public Repeater(string axisName)
        {
            _axis = axisName;
        }
        public int Update()
        {
            int retValue = 0;
            int value = Mathf.RoundToInt(Input.GetAxisRaw(_axis));
           
            if (value != 0)
            {
                if (Time.time > _next)
                {
                    retValue = value;
                    _next = Time.time + (_hold ? rate : threshold);
                    _hold = true;
                }
            }
            else
            {
                _hold = false;
                _next = 0;
            }
            return retValue;
        }
    }
}
