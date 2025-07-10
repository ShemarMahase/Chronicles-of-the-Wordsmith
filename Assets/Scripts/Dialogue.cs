using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    //creates an array of dialogue lines and serializes them for easy modularity
    [SerializeField] List<string> lines;

    public List<string> Lines
    {
        get { return lines; }
    }
}
