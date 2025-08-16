using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEditor.Connect;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class AudioDownloader : MonoBehaviour
{
    GoogleTTS GTTS;
    public CardManager CardManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GTTS = GetComponent<GoogleTTS>();
        List<Card> cardCollecton = CardManager.CardCollection;
        for (int i = 0; i < cardCollecton.Count; i++)
        {
            string textToSpeak = cardCollecton[i].text;
            string fileName = cardCollecton[i].audioName;
            if (fileName != "" && !GTTS.AudioFileExists(fileName))
            {
                GTTS.Download(textToSpeak, fileName);
                string audioPath = GTTS.GetLastAudioPath();
                AttachAudio(cardCollecton[i], audioPath, "audioName");
            }
        }
    }

    public void AttachAudio(ScriptableObject card,string audioPath, string fieldName)
    {
        #if UNITY_EDITOR
        if (audioPath.Length == 0) return;
        AudioClip audio = AssetDatabase.LoadAssetAtPath<AudioClip>(audioPath);
        // This works perfectly in Play Mode in the editor
        SerializedObject serializedObject = new SerializedObject(card);
        SerializedProperty property = serializedObject.FindProperty(fieldName);
        property.objectReferenceValue = audio;
        serializedObject.ApplyModifiedProperties();

        // Mark as dirty and save - this will persist after stopping Play Mode
        EditorUtility.SetDirty(card);
        AssetDatabase.SaveAssets();

        Debug.Log($"Successfully attached {audio.name} to Card {card.name}");
        #endif
    }
}
