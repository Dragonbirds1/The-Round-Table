using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Bomb_Manager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<GameObject> playerList = new();
    [SerializeField] private GameObject bombPrefab;

    [Header("Variables")]
    [SerializeField] private float bombTimer;

    private float bombTime;
    private GameObject taggedPlayer;
    private bool startBomb;
    private GameObject bombObject;

    //use this when a player collides with a different player, providing that one of them has the bomb
    public void Trigger(GameObject newPlayer)
    {
        if (startBomb)
        {
            Destroy(bombObject);
        }
        taggedPlayer = newPlayer;
        bombObject = Instantiate(bombPrefab, taggedPlayer.transform.position, Quaternion.identity);
    }

    //preferably use this after a timer so the players can get ready for the minigame
    public void StartBomb()
    {
        
        Trigger(playerList[Random.Range(0, playerList.Count)]);
        startBomb = true;
    }

    private void Update()
    {
        if (startBomb)
        {
            bombTime += Time.deltaTime;
            if (bombTime >= bombTimer)
            {
                Debug.Log("kaboom"); //replace this with code that makes the bomb explode and eliminate the player.
                Destroy(bombObject);
            }
        }
    }

    public void EndBomb()
    {
        if (bombObject != null)
        {
            Destroy(bombObject);
        }
        startBomb = false;
    }
}
