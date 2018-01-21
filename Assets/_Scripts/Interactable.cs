using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;
    public Transform objectTransform;
    private GameObject[] players;
    private GameObject holder;
	bool objectIsPickedUp = false;
	public float dropHeight = 2;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        if (!objectIsPickedUp)
        {
            foreach (GameObject player in players)
            {
                float distance = Vector3.Distance(player.transform.position, interactionTransform.position);

                if (distance <= radius && player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().getPickupActionState())
                {
                    objectTransform.position = player.transform.position + new Vector3(1f, 1f, 0.2f);
                    objectIsPickedUp = true;
                    holder = player;
                    break;
                    //(0.2f, 1f, 0.2f);
                    //17.37
                }
            }
        }

        else if (objectIsPickedUp && holder.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().getPickupActionState())
        {
            objectTransform.position = holder.transform.position + new Vector3(1f, dropHeight, 0.2f);
            objectIsPickedUp = false;
        }

        else if (objectIsPickedUp)
        {
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