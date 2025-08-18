
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Text;
using System.IO;
using System;
using static UnityEditor.LightingExplorerTableColumn;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class GoogleTTS : MonoBehaviour
{
    private string apiKey = "YOUR_API_KEY";
    public GTTSKey key;
    private string LastAudioPath;

    private void Start()
    {
        apiKey = key.m_Key;
    }

    public IEnumerator DownloadTTS(string text, string fileName, System.Action<string> onComplete)
    {
        Debug.Log($"Downloading {text} to {fileName}.mp3...");
        string json = $@"{{
            ""input"": {{""text"": ""{text}""}},
            ""voice"": {{""languageCode"": ""es-US"", ""name"": ""es-US-Chirp3-HD-Algenib""}},
            ""audioConfig"": {{""audioEncoding"": ""MP3""}}
        }}";

        using (var request = new UnityWebRequest("https://texttospeech.googleapis.com/v1/text:synthesize?key=" + apiKey, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var response = JsonUtility.FromJson<TTSResponse>(request.downloadHandler.text);
                byte[] audioData = Convert.FromBase64String(response.audioContent);

                string path = Path.Combine(Application.dataPath, "Audio", fileName + ".mp3");
                LastAudioPath = $"Assets/Audio/{fileName}.mp3";
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.WriteAllBytes(path, audioData);

                #if UNITY_EDITOR
                AssetDatabase.Refresh();
                #endif

                Debug.Log($"Saved: {fileName}.mp3");
            }
            else
            {
                Debug.Log($"Downloading: {fileName}.mp3 Failed!");
            }
        }
        onComplete(LastAudioPath);
    }

    public bool AudioFileExists(string fileName)
    {
        string path = Path.Combine(Application.dataPath, "Audio", fileName + ".mp3");
        return File.Exists(path);
    }

    public string GetLastAudioPath()
    {
        return LastAudioPath;
    }

    public void Download(string text, string fileName, System.Action<string> onComplete) => StartCoroutine(DownloadTTS(text, fileName,onComplete));
}

[System.Serializable]
public class TTSResponse
{
    public string audioContent;
}