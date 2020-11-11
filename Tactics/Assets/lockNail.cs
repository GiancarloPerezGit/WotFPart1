using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockNail : MonoBehaviour
{
    Vector3 t;
    // Start is called before the first frame update
    private void Start()
    {
        t = transform.localPosition;
    }
    // Update is called once per frame
    void Update()
    {
        transform.localPosition = t;
    }
}
