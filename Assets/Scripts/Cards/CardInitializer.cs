using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CardInitializer : MonoBehaviour
{
    const string TranslationPath = "Assets/Scriptable Objects/Cards/Translation Cards";
    const string MultipleChoicePath = "Assets/Scriptable Objects/Cards/Multiple Choice";
    const string MatchingPath = "Assets/Scriptable Objects/Cards/Matching";
    const string StoryPath = "Assets/Scriptable Objects/Cards/Story";
    string[] allPaths = { TranslationPath, MultipleChoicePath, MatchingPath, StoryPath };

    [SerializeField] AllCards cardDataBase;
    [SerializeField] AudioDownloader audioDownloader;

    public void Start()
    {
       InitializeAllCards();
       audioDownloader.DownloadAudios();
    }

    public void InitializeAllCards()
    {
        #if UNITY_EDITOR
        List<Card> cards;
        cardDataBase.cards.Clear();
        foreach (string path in allPaths)
        {
            cards = GetAllCardsFromFolder(path);
            cardDataBase.cards.AddRange(cards);
        }
        // Mark the asset as dirty to save the changes
        EditorUtility.SetDirty(cardDataBase);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Successfully initialized and saved all cards to the database!");
        #endif
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
