using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchingController : MonoBehaviour
{
    [SerializeField] List<Button> buttonList;
    Matching card;
    Dictionary<int, int> correctPairs = new Dictionary<int, int>();
    static public event System.Action EndMinigame;

    void Start()
    {
        LineDrawer.SubmitResults += ReceiveResults;    
    }

    public void StartMatching()
    {
        int numPairs = card.matchingPairs.Count;
        EnableButtons(numPairs);
        LabelButtons(numPairs);
        //broadcast for line renderer to do its thang
    }

    void EnableButtons(int numPairs)
    {
        ClearButtons();
        for (int i = 0; i < numPairs * 2; i++)
        {
            buttonList[i].enabled = true;
        }
    }

    void ClearButtons()
    {
        foreach (Button button in buttonList) { button.enabled = false; }
    }
    // Updates button pair dictionary and labels buttons with corresponding text
    void LabelButtons(int numPairs)
    {
        int[] keys = FillNShuffle(0, (numPairs * 2) - 1);
        int[] pairs = FillNShuffle(1, numPairs * 2);
        int i = 0;
        foreach(string key in card.matchingPairs.Keys) // For every entry key and pair text to button then add button pair indexes to dictionary
        {
            Button keyButton = buttonList[keys[i]];
            Button pairButton = buttonList[pairs[i]];
            SetText(keyButton, key);
            SetText(pairButton, card.matchingPairs[key]);
            correctPairs.Add(keys[i], pairs[i]);
        }
    }
    //Sets text of a button
    void SetText(Button button, string text)
    {
        button.GetComponent<MatchButton>().text.text = text;
    }
    //fills an array from start to end with every other number then shuffles the resulting array
    int[] FillNShuffle(int start, int end)
    {
        int[] l = new int[(end+1)/2];
        for (int i = 0, j = start; j < end; i++, j += 2)
        {
            l[i] = j;
        }
        //standing shuffle
        return l;
    }

    public void SetCard(Card card)
    {
        this.card = (card as Matching);
    }

    public void ReceiveResults(Dictionary<int,int> dict)
    {
        //Compare pairs in dict to pairs in correctpairs to get a tally of how many correct out of how many possible
    }
}
