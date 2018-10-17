using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVanish : MonoBehaviour {

    public void VanishInSeconds(int secs)
    {
        StartCoroutine(doVanish(secs));
    }

    private IEnumerator doVanish(int secs)
    {
        yield return new WaitForSeconds(secs);
        Destroy(gameObject);
    }
}
