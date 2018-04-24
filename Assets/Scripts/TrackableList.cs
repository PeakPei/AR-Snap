using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;


/// <summary>
/// Ryan Hall & Arif Khan| 21:40 - 00:51 |  18/04/2018 - 24/04/2018 
/// </summary>
public class TrackableList : MonoBehaviour
{
    string cardA = "n"; //store card ontop of pile
    string cardB = "n"; //store card 2nd to top card

    string cardName;
    string cardTypeTemp;    //store type in string before parsing
    int cardTempNum;        //after parse move into temporary integer
    bool isNum;             //boolean for try parse

    // Use this for initialization
    void Start()
    {

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
               
                if (cardA == cardB) //if most recent card and top card are equal types
                {
                    Debug.Log("Snap letters" + cardA + cardB);   //output to debug
                }
            }
        }
    }
}