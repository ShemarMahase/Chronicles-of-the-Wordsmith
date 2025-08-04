using System.Text;
using UnityEngine;

public class FuzzyString
{
    //Gets a score based on the similarity of two strings
    public float GetFuzzyCost(string s1, string s2)
    {
        s1 = s1.Normalize(System.Text.NormalizationForm.FormC).ToLower();
        s2 = s2.Normalize(System.Text.NormalizationForm.FormC).ToLower();
        float[,] matrix = new float[s1.Length + 1, s2.Length + 1];
        for (int i = 0; i <= s1.Length; i++)
        {
            matrix[i, 0] = i;
        }
        for (int i = 0; i <= s2.Length; i++)
        {
            matrix[0, 1] = i;
        }

        for (int i = 1; i <= s1.Length; i++)
        {
            for (int j = 1; j <= s2.Length; j++)
            {
                float left = matrix[i, j - 1];
                float top = matrix[i - 1, j - 1];
                float diagonal = matrix[i - 1, j - 1];
                matrix[i, j] = Mathf.Min(left, top, diagonal) + GetCost(s1[i - 1], s2[j - 1]);
            }
        }
        return matrix[s1.Length, s2.Length];
    }

    //Determines the cost between two characters
    private float GetCost(char c1, char c2)
    {
        if (c1 == c2) return 0;

        if (c1.ToString().Normalize(NormalizationForm.FormD)[0] == c2.ToString().Normalize(NormalizationForm.FormD)[0]) return .1f;

        return 1f;
    }
}
