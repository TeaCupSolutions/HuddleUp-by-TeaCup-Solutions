using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using StaticValuesNamespace;

public class ReplayMenu : MonoBehaviour {
    public GameObject buttonPrefab;
    public Transform parent;

	public	GameObject sound;
	public	AudioManager AM;
    // Use this for initialization
    void Start () {
        DirectoryInfo dirInfo = new DirectoryInfo(StaticValues.ReplayBaseDir);

		//connecting to audiomnaer through tag
		sound=GameObject.FindGameObjectWithTag("AudioManager");
		AM=sound.GetComponent<AudioManager>();

        int count = 0;
        foreach (DirectoryInfo dir in dirInfo.GetDirectories()) {
            GameObject instance = Instantiate(buttonPrefab, parent);

            //set position
            RectTransform transf = instance.GetComponent<RectTransform>();
            transf.SetPositionAndRotation(new Vector3(transf.position.x, transf.position.y + count, transf.position.z), transf.rotation);
            count -= 60;

            //set name
            TextMeshProUGUI txt = instance.GetComponentInChildren<TextMeshProUGUI>();
			txt.fontStyle = FontStyles.Bold;

            txt.text = dir.Name;

            //attach click event
            instance.GetComponent<Button>().onClick.AddListener(delegate { HandleClick(txt.text); });
        }
	}

    public void HandleClick(string txt)
    {
		AM.Play("click");
        StaticValues.IsReplay = true;
        StaticValues.ReplayName = txt;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
