using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapScript : MonoBehaviour
{
    public int playerID;
    public Text scoreLabel;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnClick()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        var script = camera.GetComponent<TrackableList>();

        if (script.isSnap())
        {
            Debug.Log("Player " + playerID + " snap");

            int score;
            if (int.TryParse(scoreLabel.text, out score))
            {
                ++score;
                scoreLabel.text = score.ToString();
            }
        }
    }
}
