using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Stance", menuName = "Scriptable Objects/Stance")]
public class Stance : Card
{
    public enum StanceType
    {
        Normal,
        Offensive,
        Defensive,
        Specialist
    }
    [SerializeReference]
    protected List<Modifier> modifiers;
    [SerializeField] StatSheet stats;
    [SerializeField] StanceType stanceType;
    [SerializeField] Sprite image;
    [SerializeField] float noModPercent;

    private float[] probability;
    private int[] alias;

    public float GetStat(TurnManager.Stat stat)
    {
        return stats.GetStat(stat);
    }

    //uses Walker's Alias Method algorithm to set up fast lookups for probabilities for modifier selection
    public void NewStance()
    {
        //Calculate the total weight of all Modifiers
        int sum = 0;
        for (int i = 0; i < modifiers.Count; i++)
        {
            sum += modifiers[i].GetWeight();
        }
        probability = new float[modifiers.Count+1];
        alias = new int[modifiers.Count + 1];

        Queue<int> smalls = new Queue<int>();
        Queue<int> bigs = new Queue<int>();

        //calculate the needed weight for having No Modifiers selected
        float noModWeight = (sum / (1 - noModPercent)) - sum;
        Debug.Log(noModWeight);

        //calculate the average weight with no mod selected option
        float averageWeight = (sum+noModWeight) / (modifiers.Count+1);

        //Add weights below averageWeight to small queue, above to big queue
        for (int i = 0; i < modifiers.Count; i++)
        {
            if (modifiers[i].GetWeight() < averageWeight) smalls.Enqueue(i);
            else bigs.Enqueue(i);
        }

        if (noModWeight < averageWeight) smalls.Enqueue(modifiers.Count);
        else bigs.Enqueue(modifiers.Count);

        //Create a temporary weights array, safe to modify. 
        float[] weights = new float[modifiers.Count+1];
        for (int i = 0; i < modifiers.Count; i++)
        {
            weights[i] = modifiers[i].GetWeight();
        }
        weights[modifiers.Count] = noModWeight;

        //Pair small and big items and distribute weight accordingly
        while (smalls.Count > 0 && bigs.Count > 0)
        {
            int smallIdx = smalls.Dequeue();
            int bigIdx = bigs.Dequeue();

            probability[smallIdx] = weights[smallIdx] / averageWeight;
            alias[smallIdx] = bigIdx;

            weights[bigIdx] -= (averageWeight - weights[smallIdx]);

            if (weights[bigIdx] < averageWeight)
                smalls.Enqueue(bigIdx);
            else
                bigs.Enqueue(bigIdx);
        }

        //set any remaining pairs to have a 100% probability of selecting themselves
        while (bigs.Count > 0)
        {
            int idx = bigs.Dequeue();
            probability[idx] = 1.0f;
            alias[idx] = idx; 
        }
        while (smalls.Count > 0)
        {
            int idx = smalls.Dequeue();
            probability[idx] = 1.0f;
            alias[idx] = idx; 
        }

        PrintLists();
    }
    public void PrintLists()
    {
        Debug.Log("Printing " + text + " stance");
        Debug.Log(modifiers);
        string s = "";
        for (int i = 0; i < modifiers.Count; i++) s += modifiers[i].GetWeight() + ",";
        Debug.Log(s);
        s = "";
        for (int i = 0; i < probability.Length; i++) s += probability[i] + ",";
        Debug.Log(s);
        s = "";
        for (int i = 0; i < alias.Length; i++) s += alias[i] + ",";
        Debug.Log(s);
    }
    //returns a mod based on alias table probabilities, returns null if no mod was selected
    public Modifier GetMod()
    {
        int idx = Random.Range(0, alias.Length);
        float rand = Random.Range(0, 1f);
        if (rand <= probability[idx])
            return idx == modifiers.Count ? null : modifiers[idx];
        else
            return alias[idx] == modifiers.Count ? null : modifiers[alias[idx]];
    }

    public StanceType GetStanceType()
    {
        return stanceType;
    }

    public Sprite GetImage()
    {
        return image;
    }


}
