using UnityEngine;
using System;
using System.IO;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        public int player;
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        Rigidbody m_Rigidbody;
        StreamWriter m_sw;
        StreamReader m_sr;

        //TO DO!!!!!
        //m_replay to be set in menu along with choosing of files later/ multiple runs --folder structure???
        //to edit file so only one is needed by every thrid person controler for optimisation of inputs
        //using pre calculated velocity makes it easier to store movement --less space used too
        //move pickup here --controls currently in different place
        //must check timing between phusics updates could cause issues - BIG ISSUES!!!
        public bool m_replay = true;


        private void Start()
        {

            if (m_replay)
            {
                if (File.Exists("RecordedInput/input" + this.player + ".txt"))
                {
                    m_sr = File.OpenText("RecordedInput/input" + this.player + ".txt");
                }
                else {
                    Debug.Log("replay File not found xD needs an exception at some point");
                }
            }
            else {
                if (!Directory.Exists("RecordedInput"))
                {
                    Directory.CreateDirectory("RecordedInput");
                }
                if (File.Exists("RecordedInput/input" + this.player + ".txt"))
                {
                    File.Delete("RecordedInput/input" + this.player + ".txt");
                    m_sw = File.CreateText("RecordedInput/input" + this.player + ".txt");
                }
                else
                {
                    m_sw = File.CreateText("RecordedInput/input" + this.player + ".txt");
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
            if (m_replay) {
                var line = m_sr.ReadLine();
                if (line != null)
                {
                    String[] inputs = line.Split('|');
                    m_Character.Move(new Vector3(float.Parse(inputs[1]), float.Parse(inputs[2]), float.Parse(inputs[3])), (inputs[4] == "True"), (inputs[5] == "True"));
                }
                else {
                    //reached end
                }    
            } else {
                // read inputs
                float h = Input.GetAxis("P"+ this.player +"_Horizontal");
                float v = Input.GetAxis("P" + this.player + "_Vertical");
                bool crouch = Input.GetButtonDown("P" + this.player + "_Crouch");

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

                m_sw.WriteLine(this.player + "|" + m_Move.x + "|" + m_Move.y + "|" + m_Move.z + "|" + crouch + "|" + m_Jump);

                // pass all parameters to the character control script
                m_Character.Move(m_Move, crouch, m_Jump);
                m_Jump = false;
            }
        }

        void OnDestroy()
        {
            if (m_replay) {
                m_sr.Close();
            }
            else {
                m_sw.Close();
            }
        }
    }
}
