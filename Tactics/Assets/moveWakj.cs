using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveWakj : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject moveTarget;

    // Update is called once per frame
    void Update()
    {
        float step = 0.5f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, moveTarget.transform.position, step);
    }
}
