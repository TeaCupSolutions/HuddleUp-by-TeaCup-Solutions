using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoController : MonoBehaviour {
    float timeCounter = 0;
    Vector3 prevPos;
    Vector3 originalPos;
    // Use this for initialization
    void Start () {
        originalPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        timeCounter += Time.deltaTime * 100;
        transform.position = Quaternion.AngleAxis(timeCounter, Vector3.up) * new Vector3(1.2f, 0f) + originalPos;
        prevPos = transform.position;
    }
}
