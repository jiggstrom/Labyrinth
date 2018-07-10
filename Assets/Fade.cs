using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {
    public float FadeSpeed = 0.8f;
    public Texture2D Texture;

    private int drawdepth = -1000;
    private float alpha = 1.0f;
    private int direction = 0;

	void OnGUI () {
        if (direction == 0) return;

        alpha += Time.deltaTime * direction * FadeSpeed;
        alpha = Mathf.Clamp01(alpha);

        if ((alpha == 0 && direction == -1) || (alpha == 1 && direction ==1)) direction = 0;

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawdepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture);
	}

    public float FadeToBlack()
    {
        alpha = 0f;
        direction = 1;
        return FadeSpeed;
    }

    public void FadeIn()
    {
        alpha = 1f;
        direction = -1;
    }
}
