using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Movment Movment;
    public float takenvalue;
    public void Update()
    {
        takenvalue = Movment.speed;
        
    }
    public void gameSelect()
    {
        SceneManager.LoadScene(1);
    }

    public void Levels(GameObject gameObject)
    {

        gameObject.SetActive(true);

    }


    public void LoadLevel(int Scene)
    {
        SceneManager.LoadScene(Scene);


    }


}
