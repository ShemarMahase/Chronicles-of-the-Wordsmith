using UnityEngine;
using UnityEngine.XR;

public class EnemyCombat : Combat
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TurnManager.initializeSelf += InitializeSelf;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializeSelf(object sender, System.EventArgs e)
    {

    }
}
