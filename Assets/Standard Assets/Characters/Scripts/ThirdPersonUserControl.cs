//TO DO!!!!!
//to edit file so only one is needed by every thrid person controler for optimisation of inputs
//using pre calculated velocity makes it easier to store movement --less space used too
//move pickup here --controls currently in different place
//timing between phusics updates could cause issues - BIG ISSUES!!!
//pickup is still must definately fucked --cd/cr --pickup on replay very finecky

/** FIX FOR TIMING??? just an  idea which probably only works in theory
 * flatten - set input for next fixed update in update and wait till it happens until doing again
 * all inputs handled in update
 * all functionality handled in fixed update

 * Slow
 * flatten - runs update - waits for next fixed to apply inputs - runs update 
 * more fixed - every next fiex ignored

 * Fast
 * flatten - runs update - waits for next fixed to apply inputs - runs update
 * more updates - every update between ignored
**/

using UnityEngine;
using System;
using System.IO;
using StaticValuesNamespace;
using UnityEngine.SceneManagement;
using System.Collections;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        public int player;
        private ThirdPersonCharacter m_Character;
        private Transform m_Cam;
        private Vector3 m_CamForward;
        private Vector3 m_Move;
        private bool m_Jump;
        private bool m_PickupAction;
        Rigidbody m_Rigidbody;
        StreamWriter m_sw;
        StreamReader m_sr;

        private void Start()
        {
            if (StaticValues.IsReplay)
            {
                if (File.Exists(StaticValues.ReplayBaseDir + "/" + StaticValues.ReplayName + "/input" + this.player + ".txt"))
                {
                    m_sr = File.OpenText(StaticValues.ReplayBaseDir + "/" + StaticValues.ReplayName + "/input" + this.player + ".txt");
                }
                else {
                    SceneManager.LoadScene(0);
                }
            }
            else {
                if (!Directory.Exists(StaticValues.ReplayBaseDir))
                {
                    Directory.CreateDirectory(StaticValues.ReplayBaseDir);
                }
                if (!Directory.Exists(StaticValues.ReplayBaseDir + "/" + StaticValues.ReplayName))
                {
                    Directory.CreateDirectory(StaticValues.ReplayBaseDir + "/" + StaticValues.ReplayName);
                }
                if (File.Exists(StaticValues.ReplayBaseDir + "/" + StaticValues.ReplayName + "/input" + this.player + ".txt"))
                {
                    File.Delete(StaticValues.ReplayBaseDir + "/" + StaticValues.ReplayName + "/input" + this.player + ".txt");
                    m_sw = File.CreateText(StaticValues.ReplayBaseDir + "/" + StaticValues.ReplayName + "/input" + this.player + ".txt");
                }
                else
                {
                    m_sw = File.CreateText(StaticValues.ReplayBaseDir + "/" + StaticValues.ReplayName + "/input" + this.player + ".txt");
                }
            }

            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = Input.GetButtonDown("P" + this.player + "_Jump");
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            if (StaticValues.IsReplay) {
                var line = m_sr.ReadLine();
                if (line != null)
                {
                    String[] inputs = line.Split('|');
                    m_Character.Move(new Vector3(float.Parse(inputs[1]), float.Parse(inputs[2]), float.Parse(inputs[3])), (inputs[4] == "True"), (inputs[5] == "True"));
                    m_PickupAction = (inputs[6] == "True");
                }
                else {
                    StartCoroutine(WaitSeconds(3));
                    SceneManager.LoadScene(0);
                }    
            } else {
                // read inputs
                float h = Input.GetAxis("P"+ this.player +"_Horizontal");
                float v = Input.GetAxis("P" + this.player + "_Vertical");
                bool crouch = Input.GetButtonDown("P" + this.player + "_Crouch");
                m_PickupAction = Input.GetButtonDown("P" + this.player + "_Pickup");
                // calculate move direction to pass to character
                if (m_Cam != null)
                {
                    // calculate camera relative direction to move:
                    m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                    m_Move = v * m_CamForward + h * m_Cam.right;
                }
                else
                {
                    // we use world-relative directions in the case of no main camera
                    m_Move = v * Vector3.forward + h * Vector3.right;
                }

                m_sw.WriteLine(this.player + "|" + m_Move.x + "|" + m_Move.y + "|" + m_Move.z + "|" + crouch + "|" + m_Jump + "|" + m_PickupAction);

                // pass all parameters to the character control script
                m_Character.Move(m_Move, crouch, m_Jump);
                m_Jump = false;
            }
        }

        void OnDestroy()
        {
            if (StaticValues.IsReplay) {
                m_sr.Close();
            }
            else {
                m_sw.Close();
            }
        }

        public bool getPickupActionState() {
            return m_PickupAction;
        }

        IEnumerator WaitSeconds(int seconds)
        {
            yield return new WaitForSeconds(seconds);
        }
    }
}
