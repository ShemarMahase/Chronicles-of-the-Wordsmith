using System;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    //Singleton instance of turnManager
    public static TurnManager instance;

    public static event EventHandler playerTurn;
    public static event EventHandler enemyTurn;
    public static event EventHandler initializeSelf;
    public enum Stat
    {
        Attack,
        Defense,
        Health,
        MaxHealth

    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initializeSelf?.Invoke(instance, EventArgs.Empty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeCombat()
    {
        
    }
}
