using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementIdentifierArrow : MonoBehaviour {
    public Canvas holderCanvas;
    public GameObject holder;
    private Vector3 finalPos;
    private Vector3 initPos;
    private int directionOfArrowMovement = 1;
    // Use this for initialization
    void Start () {
        initPos = this.transform.localPosition;
        finalPos = new Vector3(0, -0.63f, 0);
	}
	
	// Update is called once per frame
	void Update () {
        Camera cam = Camera.main;

        holderCanvas.transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward,
            cam.transform.rotation * Vector3.up);

        MeshRenderer mr;
        if (mr = holder.GetComponent<MeshRenderer>()) {
            this.GetComponent<Image>().color = mr.material.color;
        }

        if(directionOfArrowMovement == 1)
        {
            transform.localPosition = Vector3.Lerp(this.transform.localPosition, finalPos, 0.3f);
            if (transform.localPosition == finalPos)
            {
                directionOfArrowMovement = -1;
            }
        }
        else
        {
            transform.localPosition = Vector3.Lerp(this.transform.localPosition, initPos, 0.3f);
            if (transform.localPosition == initPos)
            {
                directionOfArrowMovement = 1;
            }
        }
    }
}
