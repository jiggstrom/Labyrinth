using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RotateRandomXY : MonoBehaviour
{
    [MenuItem("Tools/Transform Tools/Rotate random %r")]

    static void Rotate()
    {
        Transform[] transforms = Selection.transforms;
        foreach (Transform myTransform in transforms)
        {
            Vector3 targetRotation = new Vector3((float)Random.Range(0,360), (float)Random.Range(0, 360), myTransform.eulerAngles.z);
            myTransform.eulerAngles = targetRotation;
        }
    }
}
