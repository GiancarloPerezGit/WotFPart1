using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public Image blackScreen;
    public static bool fade = false;
    public GameObject t;
    public GameObject s;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fade)
        {
            FadeoBlack();
            t.SetActive(false);
            s.SetActive(false);
            fade = false;
            gameObject.transform.MoveTo(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 14, gameObject.transform.position.z), 1);
            
        }
    }
    void FadeoBlack()
    {
        blackScreen.color = Color.black;
        blackScreen.canvasRenderer.SetAlpha(0.0f);
        blackScreen.CrossFadeAlpha(1.0f, 1, false);
    }

    void FadeFromBlack()
    {
        blackScreen.color = Color.black;
        blackScreen.canvasRenderer.SetAlpha(1.0f);
        blackScreen.CrossFadeAlpha(0.0f, 1, false);
    }
}
