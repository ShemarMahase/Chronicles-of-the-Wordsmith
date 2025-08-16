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
            AudioClip[] audioClips = new AudioClip[cardCollecton[i].GetAudioText().Length];
            for(int j = 0; j < audioClips.Length; j++)
            {
                string audio = cardCollecton[i].GetAudioText()[j];
                string fileName = $"{cardCollecton[i].audioName}_audio_{j}";
                if (fileName != "" && !GTTS.AudioFileExists(fileName))
                {
                    GTTS.Download(audio, fileName);
                    string audioPath = GTTS.GetLastAudioPath();
                    audioClips[j] = LoadAudio(audioPath);
                }
                else
                {
                    audioClips[j] = LoadAudio($"Assets/Audio/{fileName}.mp3");
                }
            }
            AttachAudio(cardCollecton[i], audioClips, "audioClips");
        }
    }


    public AudioClip LoadAudio(string fileName)
    {
        if (fileName.Length == 0) return null;
        AudioClip audio = AssetDatabase.LoadAssetAtPath<AudioClip>(fileName);
        return audio;
    }

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
