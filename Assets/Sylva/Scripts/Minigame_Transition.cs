using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Minigame_Transition : MonoBehaviour
{

    public static Minigame_Transition main;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject endPoint;
    [SerializeField] private GameObject levelManager;

    [SerializeField] private float moveSpeed = 10f;

    private Transform target;
    public bool hitEndPoint;
    private float pauseTime;
    private bool moving;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseTime = 0f;
        hitEndPoint = false;
        target = endPoint.transform;    
    }

    private void Awake()
    {
        main = this;
    }
    //top pos is y 489 on canvas
    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            if (Vector2.Distance(target.position, transform.position) > 1f && !hitEndPoint)
            {
                hitEndPoint = false;
                Vector2 direction = (target.position - transform.position).normalized;

                rb.linearVelocity = direction * moveSpeed;
            }
            else
            {
                Vector2 direction = (target.position - transform.position).normalized;
                rb.linearVelocity = direction * 0;
                hitEndPoint = true;
                pauseTime += Time.deltaTime;
                if (pauseTime >= 4f)
                {
                    levelManager.GetComponent<Minigame_Select>().Trigger();
                }
            }
        }
    }

    public void Trigger()
    {
        moving = true;
    }
}
