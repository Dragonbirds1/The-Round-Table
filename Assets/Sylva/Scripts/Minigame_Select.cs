using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Minigame_Select : MonoBehaviour
{

    [SerializeField] private List<string> minigameList = new();
    [SerializeField] private string mainScene;

    public void Trigger()
    {
        int randomNumber = Random.Range(0, minigameList.Count);

        SceneManager.LoadScene(minigameList[randomNumber]);
    }

    public void ReturnScene()
    {
        SceneManager.LoadScene(mainScene);
    }
}
