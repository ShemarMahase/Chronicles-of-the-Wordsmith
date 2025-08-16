using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CardInitializer : MonoBehaviour
{
    const string TranslationPath = "Assets/Scriptable Objects/Cards/Translation Cards";
    const string MultipleChoicePath = "Assets/Scriptable Objects/Cards/Multiple Choice";
    const string MatchingPath = "Assets/Scriptable Objects/Cards/Matching";
    const string StoryPath = "Assets/Scriptable Objects/Cards/Story";
    List<Card> AllCards = new List<Card>();
    string[] allPaths = {TranslationPath, MultipleChoicePath, MatchingPath,StoryPath};

    public void InitializeAllCards()
    {
        List<Card> cards;
        AllCards.Clear();
        foreach(string path in allPaths)
        {
            cards = GetAllCardsFromFolder(path);
            AllCards.AddRange(cards);
        }
    }

    List<Card> GetAllCardsFromFolder(string folderPath)
    {
        List<Card> results = new List<Card>();

        #if UNITY_EDITOR
        // Find ALL ScriptableObjects in the folder
        string[] guids = AssetDatabase.FindAssets("t:ScriptableObject", new[] { folderPath });

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            ScriptableObject asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);

            if (asset != null)
            {
                results.Add((asset as Card));
            }
        }
        #endif

        return results;
    }
}
