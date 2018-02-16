using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StaticValuesNamespace;

public class ReplayUIBehaviour : MonoBehaviour {

    public Image replayImg;

    void Start()
    {
        if (StaticValues.IsReplay)
        {
            StartBlinking();
        }
        else
        {
            this.gameObject.SetActive(false);
        }
        
    }


    IEnumerator Blink()
    {
        while (true)
        {
            switch (replayImg.color.a.ToString())
            {
                case "0":
                    replayImg.color = new Color(replayImg.color.r, replayImg.color.g, replayImg.color.b, 1);
                    yield return new WaitForSeconds(0.5f);
                    break;
                case "1":
                    replayImg.color = new Color(replayImg.color.r, replayImg.color.g, replayImg.color.b, 0);
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
        }
    }

    void StartBlinking()
    {
        StopAllCoroutines();
        StartCoroutine("Blink");
    }

    void StopBlinking()
    {
        StopCoroutine("Blink");
    }
}