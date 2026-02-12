using UnityEngine;
using UnityEngine.SceneManagement;

public class next : MonoBehaviour
{
    public bool page = false;
    public GameObject page1;
    public GameObject page2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    public void Tutorial()
    {
        page= !page;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (page == false)
            {
                page1.SetActive(false);
                page2.SetActive(true);
            }
            else
            {
                page1.SetActive(true);
                page2.SetActive(false);
            }
        }
    }
}


