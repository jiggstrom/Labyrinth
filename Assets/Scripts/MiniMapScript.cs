using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapScript : MonoBehaviour {

    public Transform player;

	// Update is called once per frame
	void LateUpdate () {
        var t = player.position;
        t.y = transform.position.y;
        transform.position = t;

	}
}
