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
        public Transform m_Cam;
        private Vector3 m_CamForward;
        private Vector3 m_Move;
        private Vector2 m_Dance;
        private bool m_Jump, m_Crouch;
        private bool m_PickupAction;
        private bool m_InteractAction;
        private float m_h = 0, m_v = 0;
        Rigidbody m_Rigidbody;

        private void Start()
        {
            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
                if (!StaticValues.IsReplay)
                {
                    // read inputs
                    float h = Input.GetAxis("P" + this.player + "_Horizontal");
                    float v = Input.GetAxis("P" + this.player + "_Vertical");
                    m_Dance = new Vector2(Input.GetAxis("P" + this.player + "_Dance1"), Input.GetAxis("P" + this.player + "_Dance2"));
                    m_PickupAction = Input.GetButtonDown("P" + this.player + "_Pickup");
                    m_InteractAction = Input.GetButtonDown("P" + this.player + "_Interact");
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

                if (!m_Jump)
                {
                    m_Jump = Input.GetButtonDown("P" + this.player + "_Jump");
                }

                if (!m_Crouch)
                {
                    m_Crouch = Input.GetButtonDown("P" + this.player + "_Crouch");
                }
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            m_Character.Move(m_Move, m_Crouch, m_Jump, m_Dance);
            if (m_Jump)
            {
                m_Jump = false;
            }

            if (m_Crouch)
            {
                m_Crouch = false;
            }
        }

        public bool getPickupActionState() {
            return m_PickupAction;
        }

        public bool getInteractionActionState()
        {
            return m_InteractAction;
        }

        public void SetPickupActionState(bool set)
        {
            m_PickupAction = set;
        }

        public void SetInteractionActionState(bool set)
        {
            m_InteractAction = set;
        }
    }
}
