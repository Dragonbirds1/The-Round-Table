using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Minigame_Select : MonoBehaviour
{
    [SerializeField] private List<string> minigameList = new();

    public void Trigger()
    {
        int randomNumber = Random.Range(0, minigameList.Count);

        SceneManager.LoadScene(minigameList[randomNumber]);
    }
}
