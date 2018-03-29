using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using StaticValuesNamespace;
using UnityEngine.SceneManagement;

public class ReplayController : MonoBehaviour
{
    StreamWriter m_sw;
    StreamReader m_sr;
    GameObject[] players;
    int offset = 0;

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
            var line = m_sr.ReadLine();
            if (line != null)
            {
                /*
                //load all state
                String[] inputs = line.Split('|');
                m_Move.x = float.Parse(inputs[1]);
                m_Move.y = float.Parse(inputs[2]);
                m_Move.z = float.Parse(inputs[3]);
                m_Crouch = (inputs[4] == "True");
                m_Jump = (inputs[5] == "True");
                m_PickupAction = (inputs[6] == "True");
                m_InteractAction = (inputs[7] == "True");
                */

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
                m_sw.Write(player.transform.position.x + "|" + player.transform.position.y + "|" + player.transform.position.z + "|"
                            + player.transform.rotation.x + "|" + player.transform.rotation.y + "|" + player.transform.rotation.z + "|" + player.transform.rotation.w + "|");
            }
            m_sw.WriteLine();
        }
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
