using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;


/// <summary>
/// Ryan Hall & Arif Khan| 21:40 - 00:51 |  18/04/2018 - 24/04/2018 
/// </summary>
public class TrackableList : MonoBehaviour
{
    public int gameModeNumber;

    public int maxSnaps;
    int currentSnapCount;

    string cardA = "n"; //store card ontop of pile
    string cardB = "n"; //store card 2nd to top card

    string cardName;
    string cardTypeTemp;    //store type in string before parsing
    int cardTempNum;        //after parse move into temporary integer

    // Use this for initialization
    void Start()
    {
        PlayerPrefs.SetInt("GameModeNumber", gameModeNumber);
    }

    void Update()
    {
        // Get the Vuforia StateManager
        StateManager sm = TrackerManager.Instance.GetStateManager();

        // Query the StateManager to retrieve the list of
        // currently 'active' trackables 
        //(i.e. the ones currently being tracked by Vuforia)
        IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours();

        // Iterate through the list of active trackables
        foreach (TrackableBehaviour card in activeTrackables)
        {
            //Debug.Log("Tracking: " + card.TrackableName);

            if (cardName != card.TrackableName.ToString())  //if stored card name is not equal to the tracked name being read
            {
                cardName = card.TrackableName.ToString();                   //turns trackable name into string and stores it in card name
                cardTypeTemp = cardName[cardName.Length - 1].ToString();    //gets last char off of cardname string and converts char into string to store in to cardtypetemp
                
                    Debug.Log("Card registed:" + cardTypeTemp);
                
               if(cardA == "n") //if cardA is marked empty
                {
                    cardA = cardTypeTemp;   //set cardA to card that has just been read
                }
               else if(cardA != "n"  && cardB == "n")   //if card is not marked empty and card B is marked empty
                {
                    cardB = cardTypeTemp;   //set cardB to the card just read
                }
               else if(cardA != "n" && cardB != "n") //if card a and card B are both marked not empty
                {   
                    cardB = cardA;  //set card b to card a, storing the most recent card
                    cardA = cardTypeTemp;   //set card a to the card just read
                }
            }
        }
    }

    public bool isSnap()
    {
        if (cardA == cardB) //if most recent card and top card are equal types
        {
            if (currentSnapCount < maxSnaps)
            {
                Debug.Log("Snap letters " + cardA + cardB);   //output to debug
                ++currentSnapCount;
            }
            else
            {
                // End game mode
                // Get the scores
                int score1;
                int score2;

                GameObject score1Text = GameObject.Find("Snap1ScoreText");
                GameObject score2Text = GameObject.Find("Snap2ScoreText");

                // Try to parse the text in the score labels into integers
                if (!int.TryParse(score1Text.GetComponent<Text>().text, out score1))
                {
                    Debug.LogError("Score could not be parsed");
                }

                if (!int.TryParse(score2Text.GetComponent<Text>().text, out score2))
                {
                    Debug.LogError("Score could not be parsed");
                }

                // Get the highest score
                int highscore = 0;

                // Compare scores and take the highest (or score 2 if drawn since they will be equal)
                if (score1 > score2)
                {
                    highscore = score1;
                }
                else
                {
                    highscore = score2;
                }

                // Display winner
                if (score1 > score2)
                {
                    // Player 1 wins

                }
                else if (score2 < score1)
                {
                    // Player 2 wins

                }
                else
                {
                    // Draw

                }

                // Set final score in playerprefs
                PlayerPrefs.SetInt("FinalScore" + gameModeNumber, highscore);
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}