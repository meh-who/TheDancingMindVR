using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSync : MonoBehaviour
{
    public Transform[] sourceObjects;
    public Transform[] targetObjects;

    private void LateUpdate()
    {
        // Make sure the source and target lists have the same number of elements
        if (sourceObjects.Length != targetObjects.Length)
        {
            Debug.LogError("Source and target lists must have the same number of elements.");
            return;
        }

        // Loop through each object in the source list and update the corresponding object in the target list
        for (int i = 0; i < sourceObjects.Length; i++)
        {
            Transform source = sourceObjects[i];
            Transform target = targetObjects[i];

            // Set the position and rotation of the target object to match the source object
            target.position = source.position;
            target.rotation = source.rotation;
        }
    }
}
