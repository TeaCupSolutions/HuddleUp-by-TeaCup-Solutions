using UnityEngine;

public class Interactable : MonoBehaviour
{
    //Declare variables
    public float radius = 3f;
    public Transform interactionTransform;
    public Transform objectTransform;
    private GameObject[] players;
    private GameObject holder;
	bool objectIsPickedUp = false;
	public float dropHeight = 2;
	public	GameObject sound;
	public	AudioManager AM;

    void Start()                                                                                             //This will run at the start of the program
    {
        players = GameObject.FindGameObjectsWithTag("Player");                                               //Get all of the objects in the game with the tag "player" and put them into the array of game objects
		sound=GameObject.FindGameObjectWithTag("AudioManager");
		AM=sound.GetComponent<AudioManager>();
	}

    void Update()                                                                                            //This will run repeatedly until the game ends
    {
        if (!objectIsPickedUp)                                                                               //If the object is not picked up...
        {
            foreach (GameObject player in players)                                                           //For each player in the game...
            {
                float distance = Vector3.Distance(player.transform.position, interactionTransform.position); //Get the distance betweent the player and the object

                if (distance <= radius &&
                    player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().
                    getPickupActionState())                                                                  //If the distance is less than the radius and the player has pressed a button...  player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().getPickupActionState() returns true if the player has pressed the pick up button
                {
                    //Adjust the position of the object
                    objectTransform.position = player.transform.position + new Vector3(1f, 1f, 0.2f);        //Set the position of the object to the position of the player plus a little difference
                    objectIsPickedUp = true;
                    holder = player;                                                                         //Set holder to player.  Holder will tell which player is holding the object.
					AM.Play("pickup");
					break;
                }
            }
        }
        
        else if (objectIsPickedUp &&
            holder.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().
            getPickupActionState())                                                                          //If the object is picked up and the player wants to drop it...
        {
            //Apply gravity to the object
            //objectTransform.position = holder.transform.position + new Vector3(1f, dropHeight, 0.2f);
            objectIsPickedUp = false;
			AM.Play("drop");
        }
        
        else if (objectIsPickedUp)                                                                           //If the player hasn't given any more commands...
        {
            //Move the object with the player
            objectTransform.position = holder.transform.position + new Vector3(1f, 1f, 0.2f);
        }

        /*
        if (!objectIsPickedUp)
        {
            foreach (GameObject player in players)
            {
                float distance = Vector3.Distance(player.transform.position, interactionTransform.position);

                if (distance <= radius && Input.GetKeyDown(KeyCode.JoystickButton0))
                {
                    objectTransform.position = player.transform.position + new Vector3(0.2f, 1f, 0.2f);
                    objectIsPickedUp = true;
                    holder = player;
                }
            }
        }

        else if(objectIsPickedUp && Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            objectTransform.position = holder.transform.position + new Vector3(0.2f, 0f, 0.2f);
            objectIsPickedUp = false;
        }

        else if (objectIsPickedUp)
        {
            objectTransform.position = holder.transform.position + new Vector3(0.2f, 1f, 0.2f);
        }
        */
    }
}