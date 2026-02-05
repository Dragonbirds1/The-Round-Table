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

    public void Trigger(GameObject newPlayer)
    {
        taggedPlayer = newPlayer;
    }
}
