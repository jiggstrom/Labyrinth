using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour {
    public int FadeTime = 10;
    public UnityEngine.UI.Image Image;
    private int direction = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (direction == 0) return;

        var color = Image.color;
        if ((color.a < 1 && direction == 1) || (color.a > 0 && direction == -1))
            color.a += Time.deltaTime * direction / FadeTime;
        else
            direction = 0;
        Image.color = color;
	}

    public void FadeToBlack()
    {
        direction = 1;
    }
}
