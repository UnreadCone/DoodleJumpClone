using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class RandomMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public float boundary = 1 / 2f;
    public Vector3 targetScale = new Vector3(1.821959f, 1.821959f, 1.0f);
    private Vector3 initialPosition;
    private bool isScaling = false;
    void Start()
    {
        initialPosition = transform.position;
    }
    void Update()
    {
        if (Vector3.Distance(initialPosition, transform.position) < boundary)
        {
            Vector3 randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
            transform.Translate(randomDirection.normalized * speed * Time.deltaTime);
        }
        else
        {
            transform.position = initialPosition;
            if (!isScaling)
            {
                StartCoroutine(ScaleOverTime(1.0f)); // Start scaling
            }
        }
    }
    IEnumerator ScaleOverTime(float time)
    {
        isScaling = true;
        Vector3 originalScale = transform.localScale;
        float currentTime = 0.0f;
        do
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        transform.localScale = targetScale;
        isScaling = false;
    }
    public void OnCollisionEnter2D(Collision2D collision)       // столкновение объекта  
    {
        if (collision.collider.name == "ROBOT")
        {
            SceneManager.LoadScene(2);
        }
    }
}