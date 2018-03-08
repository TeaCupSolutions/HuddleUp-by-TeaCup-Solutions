using UnityEngine;
using UnityEngine.UI;

public class Pickupable : MonoBehaviour
{
    //Declare variables
    public float radius = 1f;
    public Transform interactionTransform;
    public Transform objectTransform;
    private GameObject[] players;
    private GameObject holder;
    public GameObject positionPointer;
    public Image positionPointerImage;
    bool objectIsPickedUp = false;
	public float dropHeight = 1;
	public	GameObject sound;
	public	AudioManager AM;
	public string name;
    private float initY;

    void OnDestroy()
    {
        if (holder)
        {
            holder.GetComponent<Player>().holding = "";
            if (positionPointer)
            {
                positionPointer.GetComponent<MeshRenderer>().enabled = false;
                positionPointerImage.enabled = false;
            }
        }
    }

    void Start()                                                                                             //This will run at the start of the program
    {
        players = GameObject.FindGameObjectsWithTag("Player");                                               //Get all of the objects in the game with the tag "player" and put them into the array of game objects
		sound=GameObject.FindGameObjectWithTag("AudioManager");
		AM=sound.GetComponent<AudioManager>();
        initY = this.transform.position.y;

    }

    void Update()                                                                                            //This will run repeatedly until the game ends
    {
        if (!objectIsPickedUp)                                                                               //If the object is not picked up...
        {
            foreach (GameObject player in players)                                                           //For each player in the game...
            {
                float distance = Vector3.Distance(player.transform.position, interactionTransform.position); //Get the distance betweent the player and the object
                if (distance <= radius &&
                    player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().getPickupActionState() && player.GetComponent<Player>().holding == "")                                                                  //If the distance is less than the radius and the player has pressed a button...  player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().getPickupActionState() returns true if the player has pressed the pick up button
                {
                    //Adjust the position of the object
                    this.EnableMeshColliders(false);
                    objectTransform.position = player.transform.position + new Vector3(1f, 1f, 0.2f);        //Set the position of the object to the position of the player plus a little difference
                    objectIsPickedUp = true;
					player.GetComponent<Player>().holding = name;
                    //Debug.Log (player.GetComponent<Player> ().holding);
                    holder = player;                                                                         //Set holder to player.  Holder will tell which player is holding the object.
                    if (positionPointer)
                    {
                        MeshRenderer mr = positionPointer.GetComponent<MeshRenderer>();
                        mr.enabled = true;
                        positionPointerImage.enabled = true;
                        mr.material.color = player.GetComponent<Player>().colour;
                    }
                    if (this.tag=="chair")
						AM.Play("chairPickUp");
					else if(this.tag=="couch")
						AM.Play("couchPickUp");
					else if(this.tag=="bin")
						AM.Play("binPickUp");
					else
						AM.Play("pickup");
					break;
                }
            }
        }
        
        else if (objectIsPickedUp &&
            holder.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().getPickupActionState())                                                                          //If the object is picked up and the player wants to drop it...
        {
            //Apply gravity to the object
            //objectTransform.position = holder.transform.position + new Vector3(1f, dropHeight, 0.2f);
            this.transform.position = new Vector3(this.transform.position.x,initY, this.transform.position.z);
            this.EnableMeshColliders(true);
            holder.GetComponent<Player>().holding = "";
            objectIsPickedUp = false;

            if (positionPointer)
            {
                MeshRenderer mr = positionPointer.GetComponent<MeshRenderer>();
                mr.enabled = false;
                positionPointerImage.enabled = false;
            }

            if (this.tag=="chair")
				AM.Play("chairDrop");
			else if(this.tag=="couch")
				AM.Play("couchDrop");
			else if(this.tag=="bin")
				AM.Play("binDrop");
			else if(this.tag=="plate")
				AM.Play("plateDrop");
			else
				AM.Play("drop");
        }
        
        else if (objectIsPickedUp)                                                                           //If the player hasn't given any more commands...
        {
            //Move the object with the player
            objectTransform.position = holder.transform.position + new Vector3(1f, 1f, 0.2f);
        }
    }

    private void EnableMeshColliders(bool isEnabled)
    {
        foreach (MeshCollider mc in this.GetComponents<MeshCollider>())
        {
            mc.enabled = isEnabled;
        }
        foreach (MeshCollider mc in this.GetComponentsInChildren<MeshCollider>())
        {
            mc.enabled = isEnabled;
        }
    }
}