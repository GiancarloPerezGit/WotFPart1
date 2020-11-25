using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockNail : MonoBehaviour
{
    Vector3 t;
    public bool s = true;
    public GameObject move;
    public GameObject move2;
    public GameObject location;
    public float g = 2f;
    private bool b = false;
    public float soFar = 0;
    private Vector3 e;
    public bool side;
    public lockNail k;
    private Vector3 maxStep;
    // Start is called before the first frame update
    private void Start()
    {
        t = transform.localPosition;
    }
    // Update is called once per frame
    void Update()
    {
        float a = Vector3.Distance(move.transform.position, location.transform.position);
        
        if(a < 0.3f && !b)
        {
            transform.localPosition = t;
        }
        else
        {
            if (side || !k.b)
            {
                //transform.position = move2.transform.position;
                float step = g * Time.deltaTime;
                if (soFar < 0.3)
                {
                    if (side && k.b)
                    {

                    }
                    else
                    {
                        e = move2.transform.position + new Vector3(0, 0.1f, 0);
                    }

                }
                else
                {
                    e = move2.transform.position;
                }
                transform.position = Vector3.MoveTowards(transform.position, e, 0.3f);
                soFar += step;
                t = transform.localPosition;
                if (transform.position.Equals(move2.transform.position))
                {
                    b = false;
                    soFar = 0;
                }
                else
                {
                    b = true;

                }
            }
        }
        
    }
}
