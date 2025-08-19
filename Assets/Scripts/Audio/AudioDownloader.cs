using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEditor.Connect;
using System.Collections;



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
    }

    public void DownloadAudios()
    {
        StartCoroutine(ProcessCards());
    }

    public IEnumerator ProcessCards()
    {
        GTTS = GetComponent<GoogleTTS>();
        List<Card> cardCollecton = CardManager.CardCollection.cards;
        for (int i = 0; i < cardCollecton.Count; i++) // Loop through every card
        {
            AudioClip[] audioClips = new AudioClip[cardCollecton[i].GetAudioText().Length]; //get all audios in a card
            Debug.Log($"total length of audioClips is {audioClips.Length}");
            int count = 0;
            for (int j = 0; j < audioClips.Length; j++) //loop through those audios
            {
                int localj = j;
                string audio = cardCollecton[i].GetAudioText()[j];
                string fileName = $"{cardCollecton[i].audioName}_audio_{j}";
                if (fileName != "" && !GTTS.AudioFileExists(fileName)) //If the audio doesnt exists then download
                {
                    GTTS.Download(audio, fileName, (audioPath) =>
                    {
                        Debug.Log("Current J is: " + j);
                        Debug.Log("audioclips.length is: "+audioClips.Length);
                        audioClips[localj] = LoadAudio(audioPath);
                        count++;
                    });
                }
                else
                {
                    audioClips[localj] = LoadAudio($"Assets/Audio/{fileName}.mp3");
                    count++;
                }
            }
            yield return new WaitUntil(() => count == audioClips.Length);
            AttachAudio(cardCollecton[i], audioClips, "audioClips");
        }
    }

    // Converts a path to an audio to an AudioClip
    public AudioClip LoadAudio(string fileName)
    {
        if (fileName.Length == 0) return null;
        AudioClip audio = AssetDatabase.LoadAssetAtPath<AudioClip>(fileName);
        return audio;
    }

    //Takes an array of audioclips and attaches it to the associate scriptable object
    public void AttachAudio(ScriptableObject card,AudioClip[] audioClips, string fieldName)
    {
        #if UNITY_EDITOR
        SerializedObject serializedObject = new SerializedObject(card);
        SerializedProperty arrayProperty = serializedObject.FindProperty(fieldName);

        // Set the array size
        arrayProperty.arraySize = audioClips.Length;

        // Populate each element
        for (int i = 0; i < audioClips.Length; i++)
        {
            arrayProperty.GetArrayElementAtIndex(i).objectReferenceValue = audioClips[i];
        }

        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(card);
        AssetDatabase.SaveAssets();
        #endif
    }
}
