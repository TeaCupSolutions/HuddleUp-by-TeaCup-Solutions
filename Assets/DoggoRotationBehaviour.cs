using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoRotationBehaviour : MonoBehaviour {
    Vector3 prevPos;
    void Update()
    {
        if (prevPos != null)
        {
            Vector3 targetDir = transform.position - prevPos;

            // The step size is equal to speed times frame time.
            float step = 1 * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            Debug.DrawRay(transform.position, newDir, Color.red);

            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);
        }
        prevPos = transform.position;
    }
}