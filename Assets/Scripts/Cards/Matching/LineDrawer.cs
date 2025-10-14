using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LineDrawer : MonoBehaviour
{
    LineRenderer[] lines = new LineRenderer[8];
    public Material lineMaterial;
    bool currentlyMatching = false;
    int currentIndex = 0;
    Vector2 startPos = Vector2.zero;
    Vector2 endPos = Vector2.zero;
    Dictionary<int, int> idToLine = new Dictionary<int, int>();
    Queue<int> availableLines = new Queue<int>();
    public static event System.Action<Dictionary<int, int>> SubmitResults;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MatchingController.EndMinigame += MiniGameEnd;
        for (int i = 0; i < lines.Length; i++)
        {
            availableLines.Enqueue(i);
            GameObject lineObj = new GameObject($"Line_{i}");
            lines[i] = lineObj.AddComponent<LineRenderer>();

            // Setup
            lines[i].material = lineMaterial;
            lines[i].startWidth = .3f;
            lines[i].endWidth = .1f;
            lines[i].positionCount = 2;
            lines[i].enabled = false;
            lines[i].sortingLayerName = "LineRenderer";
        }
    }

    //Each button when pressed sends itself and its unique id
    public void ButtonSelected(Button button, int id)
    {
        Debug.Log($"button press Id:{id}");
        if (idToLine.ContainsKey(id))
        {
            if (idToLine[id] == currentIndex)
            {
                UndoCurrentMatch(id); //If you select the same button twice, stop matching
                return;
            }
            else UndoMatch(id); // if button already has a pair undo that pair
        }
        if (currentlyMatching)
        {
            FinishMatch(id, button); // Completes current Match
        }
        else
        {
            StartNewMatch(id, button); // Starts a New Match
        }
    }

    private void UndoCurrentMatch(int id)
    {
        Debug.Log("Canceling Match");
        idToLine.Remove(id);
        currentlyMatching = false;
        availableLines.Enqueue(currentIndex);
        currentIndex = -1;
    }

    private void FinishMatch(int id, Button button)
    {
        Debug.Log("Completing a match");
        idToLine[id] = currentIndex;
        startPos = button.transform.position;
        currentlyMatching = false;
    }

    private void StartNewMatch(int id, Button button)
    {
        Debug.Log("Starting a new match");
        currentlyMatching = true;
        currentIndex = availableLines.Dequeue();
        idToLine[id] = currentIndex;
        lines[currentIndex].enabled = true;
        startPos = button.transform.position;
        StartCoroutine(DrawLine());
    }
    //If both keys are present in dictionary that means a Match was made. This function removes both entries from the dictionary
    void UndoMatch(int id)
    {
        Debug.Log("Undoing existing Match");
        int tempIdx = idToLine[id];
        idToLine.Remove(id);
        idToLine.Remove(idToLine.FirstOrDefault(pair => pair.Value == tempIdx).Key);
        lines[tempIdx].enabled = false;
        availableLines.Enqueue(tempIdx);
    }

    void UndoAll()
    {
        availableLines.Clear();
        for (int i = 0; i < lines.Length; i++) { 
            lines[i].enabled = false; 
            availableLines.Enqueue(i);
        }

    }

    //Triggers every frame to visually draw lines
    IEnumerator DrawLine()
    {
        while (currentlyMatching)
        {
            Vector3 screenPos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10f));
            endPos = worldPos;
            lines[currentIndex].SetPosition(0, startPos);
            lines[currentIndex].SetPosition(1, endPos);
            yield return null;
        }
    }
    //sends current dictionary of made lines to MatchController
    void SendResults()
    {
        //actually build a dictionary of pairs using idToLine
        SubmitResults?.Invoke(idToLine);
    }
    //When either time runs out or submit button is pressed, end the minigame
    public void MiniGameEnd()
    {
        SendResults();
    }
}
