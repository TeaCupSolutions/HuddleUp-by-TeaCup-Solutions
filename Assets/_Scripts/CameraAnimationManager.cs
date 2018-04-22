using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraAnimationManager : MonoBehaviour {

	Animator CameraObject;

	// Use this for initialization
	void Start () {

		CameraObject = GetComponent<Animator> ();
		
	}


	// Update is called once per frame
	void Update () {
		
	}


	public void Goto_Options(){
		
		CameraObject.SetBool ("Options", true);

	}

	public void ReturnFrom_Options(){


		CameraObject.SetBool ("Options", false);

	}

	public void Goto_Replay(){

		CameraObject.SetBool ("Replay", true);

	}

	public void ReturnFrom_Replay(){


		CameraObject.SetBool ("Replay", false);

	}

	public void Goto_Play(){

		CameraObject.SetBool ("Play", true);

	}

	public void ReturnFrom_Play(){


		CameraObject.SetBool ("Play", false);

	}

	public void Goto_StartGame(){

		CameraObject.SetBool ("StartGame", true);

	}




}
