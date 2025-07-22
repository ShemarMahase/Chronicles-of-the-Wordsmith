using Mono.Cecil;
using NUnit.Framework.Constraints;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    //Singleton instance of turnManager
    public static TurnManager instance;

    //Events
    public static event EventHandler playerTurn;
    public static event System.Action<bool> playerAttack;
    public static event System.Action<Combat, float> defend;
    public static event EventHandler enemyTurn;
    public static event EventHandler initializeSelf;
    public static event EventHandler initiateShuffle;

    private bool[] checks = { false, false };
    Combat player;
    Combat enemy;
    public enum Stat
    {
        Attack,
        Defense,
        Health,
        MaxHealth

    }
    //UI and buttons
    public Button Act;
    public Button Shuffle;
    public Button Item;
    public RectTransform TranslationGame;
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
        StartCoroutine(InitializeCombat());
    }
    // Update is called once per frame
    void Update()
    {

    }
    //takes in an Attacker and damage amount and tries to launch an attack towards opposing force
    public void LaunchAttack(Combat attacker, float damage)
    {
        Combat target;
        if (attacker.GetName() == "Player") target = enemy;
        else target = player;
        defend?.Invoke(target, damage);
        setCheck(attacker, true);
    }
    //Adds combatant to turnmanager for easy reference
    public void AddCombatant(Combat combatant)
    {
        if (combatant.GetName() == "Player") player = combatant;
        else enemy = combatant;
    }
    //Sets check for corresponding combatants checks[0] is player checks[1] is enemy
    public void setCheck(Combat combatant, bool check)
    {
        Debug.Log(combatant.GetName() + " is " + combatant.GetName() + "and combtant is a player is " + (combatant.GetName() == "Player"));
        if (combatant.GetName() == "Player") checks[0] = check;
        else checks[1] = check;
    }
    //resets checks to false
    private void resetChecks()
    {
        checks[0] = false;
        checks[1] = false;
    }
    // tells player and enemy to send and set any relevant information needed for starting fight
    IEnumerator InitializeCombat()
    {
        Debug.Log("Initiating Combat");
        initializeSelf?.Invoke(instance, EventArgs.Empty);
        while (!(checks[0] && checks[1]))
        {
            yield return null;
        }
        Debug.Log("Checks Complete");
        resetChecks();
        StartCoroutine(playTurns());
    }
    //loops through player and enemy turns into someone is defeated
    IEnumerator playTurns()
    {
        Debug.Log("Starting with player turn");
        UIManager.instance.EnableActionUI();
        yield return WaitForPlayer();
        //playerTurn?.Invoke(instance, EventArgs.Empty);
        yield return WaitForPlayer();
    }
    //loops until player action is enacted
    IEnumerator WaitForPlayer()
    {
        while (!checks[0])
        {
            yield return null;
        }
    }
    //loops through until enemy action is completed
    IEnumerator WaitForEnemy()
    {
        while (!checks[0])
        {
            yield return null;
        }
    }

    public void PlayerAction()
    {
        UIManager.instance.DisableActionUI();
        playerTurn?.Invoke(instance, EventArgs.Empty);
    }

    public void ShufflePlayer()
    {
        Debug.Log("Doesn't do anything yet");
    }

    public void UseItem()
    {
        Debug.Log("Doesn't do anything yet");
    }

    public void TriggerPlayerAttack(bool fullDamage)
    {
        playerAttack?.Invoke(fullDamage);
    }
}
