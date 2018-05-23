using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using StaticValuesNamespace;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;

public class ReplayController : MonoBehaviour
{
    StreamWriter m_sw;
    StreamReader m_sr;
    GameObject[] players;
    public ScoreController scoreController;
    int offset = 0;
    List<KeyValuePair<int, int>> scores = new List<KeyValuePair<int, int>>();

    // Use this for initialization
    void Start()
    {
        if (StaticValues.IsReplay)
        {
            if (File.Exists(StaticValues.ReplayBaseDir + "/" + StaticValues.ReplayName + "/input.txt"))
            {
                m_sr = File.OpenText(StaticValues.ReplayBaseDir + "/" + StaticValues.ReplayName + "/input.txt");
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            if (!Directory.Exists(StaticValues.ReplayBaseDir))
            {
                Directory.CreateDirectory(StaticValues.ReplayBaseDir);
            }
            if (!Directory.Exists(StaticValues.ReplayBaseDir + "/" + StaticValues.ReplayName))
            {
                Directory.CreateDirectory(StaticValues.ReplayBaseDir + "/" + StaticValues.ReplayName);
            }
            if (File.Exists(StaticValues.ReplayBaseDir + "/" + StaticValues.ReplayName + "/input.txt"))
            {
                File.Delete(StaticValues.ReplayBaseDir + "/" + StaticValues.ReplayName + "/input.txt");
                m_sw = File.CreateText(StaticValues.ReplayBaseDir + "/" + StaticValues.ReplayName + "/input.txt");
            }
            else
            {
                m_sw = File.CreateText(StaticValues.ReplayBaseDir + "/" + StaticValues.ReplayName + "/input.txt");
            }
        }
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticValues.IsReplay)
        {
			if (Time.timeScale == 0) {
				return;
			}

            var line = m_sr.ReadLine();
            if (line != null)
            {
                String[] inputs = line.Split('|');
                offset = 0;
                foreach (GameObject player in players)
                {
                    Vector3 v = new Vector3((float)double.Parse(inputs[offset]), (float)double.Parse(inputs[offset + 1]), (float)double.Parse(inputs[offset + 2]));
                    player.transform.position = v;
                    offset += 3;
                    Quaternion q = new Quaternion((float)double.Parse(inputs[offset]), (float)double.Parse(inputs[offset + 1]), (float)double.Parse(inputs[offset + 2]), (float)double.Parse(inputs[offset + 3]));
                    player.transform.rotation = q;
                    offset += 4;
                    player.GetComponent<ThirdPersonUserControl>().SetPickupActionState(bool.Parse(inputs[offset]));
                    offset++;
                    player.GetComponent<ThirdPersonUserControl>().SetInteractionActionState(bool.Parse(inputs[offset]));
                    offset++;
                    Animator anim = player.GetComponent<Animator>();
                    anim.SetFloat("Forward", (float)double.Parse(inputs[offset]), 0.1f, Time.deltaTime);
                    offset++;
                    anim.SetFloat("Turn", (float)double.Parse(inputs[offset]), 0.1f, Time.deltaTime);
                    offset++;
                    anim.SetBool("Crouch", bool.Parse(inputs[offset]));
                    offset++;
                    anim.SetBool("OnGround", bool.Parse(inputs[offset]));
                    offset++;
                    anim.SetFloat("Jump", (float)double.Parse(inputs[offset]));
                    offset++;
                    anim.SetFloat("JumpLeg", (float)double.Parse(inputs[offset]));
                    offset++;
                    anim.SetBool("Dance1", bool.Parse(inputs[offset]));
                    offset++;
                    anim.SetBool("Dance2", bool.Parse(inputs[offset]));
                    offset++;
                    anim.SetBool("Dance3", bool.Parse(inputs[offset]));
                    offset++;
                    anim.SetBool("Dance4", bool.Parse(inputs[offset]));
                    offset++;
                }

                int counter = int.Parse(inputs[offset]);
                offset += 1;
                for (int i = 0; i < counter; i++)
                {
                    scoreController.IncrementScore(int.Parse(inputs[offset]), int.Parse(inputs[offset+1]));
                    offset += 2;
                }
            }
            else
            {
                StartCoroutine(WaitBeforeExit(5));
            }
        }
        else
        {
            //save all state
            foreach (GameObject player in players)
            {
                Animator anim = player.GetComponent<Animator>();
                m_sw.Write(player.transform.position.x + "|" + player.transform.position.y + "|" + player.transform.position.z + "|"
                            + player.transform.rotation.x + "|" + player.transform.rotation.y + "|" + player.transform.rotation.z + "|" 
                            + player.transform.rotation.w + "|" + player.GetComponent<ThirdPersonUserControl>().getPickupActionState() + "|" 
                            + player.GetComponent<ThirdPersonUserControl>().getInteractionActionState() + "|"
                            + anim.GetFloat("Forward") + "|" + anim.GetFloat("Turn") + "|" + anim.GetBool("Crouch") + "|" + anim.GetBool("OnGround") + "|" + anim.GetFloat("Jump") + "|" + anim.GetFloat("JumpLeg") + "|"
                            + anim.GetBool("Dance1") + "|" + anim.GetBool("Dance2") + "|" + anim.GetBool("Dance3") + "|" + anim.GetBool("Dance4") + "|");
            }

            m_sw.Write(scores.Count + "|");
            if (scores.Count > 0)
            {
                foreach (KeyValuePair<int, int> score in scores)
                {
                    m_sw.Write(score.Key + "|" + score.Value + "|");
                }
            }
            m_sw.WriteLine();
            scores.Clear();
        }
    }

    public void AddScoreToReplay(int player, int stat)
    {
        scores.Add(new KeyValuePair<int, int>(player, stat));
    }

    void OnDestroy()
    {
        if (StaticValues.IsReplay)
        {
            m_sr.Close();
        }
        else
        {
            m_sw.Close();
        }
    }

    IEnumerator WaitBeforeExit(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(0);
    }
}
