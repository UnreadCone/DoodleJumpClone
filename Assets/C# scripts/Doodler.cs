using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doodle : MonoBehaviour
{
    public static Doodle instance;
    public Rigidbody2D DoodleRigid;

    void Start()
    {
        if (instance == null)                               // пишем эти строчки, чтоб можно было корректно использовать переменные в других скриптах
        {
            instance = this;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)       // столкновение объекта  
    {
        if (collision.collider.name == "DeadZone" || collision.collider.name == "Mob")
        {
            SceneManager.LoadScene(2);
        }
    }

    public float moveSpeed = 5f;

    void Update()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

        if (viewPos.x < -0.10 || viewPos.x > 1.10)
        {
            TeleportPlayer();
        }
    }

    void TeleportPlayer()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

        if (viewPos.x < 0)
        {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1, viewPos.y, 10));
        }
        else
        {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0, viewPos.y, 10));
        }
    }
}