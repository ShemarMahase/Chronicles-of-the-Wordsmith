using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

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
            if(fileName != "" && !GTTS.AudioFileExists(fileName)) GTTS.Download(textToSpeak, fileName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
