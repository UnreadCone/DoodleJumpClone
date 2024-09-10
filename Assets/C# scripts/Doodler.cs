using UnityEngine.SceneManagement;
using UnityEngine;

public class Doodle : MonoBehaviour
{
    public static Doodle instance;
    public Rigidbody2D DoodleRigid;
    public Vector2? pendingJump = null;
    private bool facingRight = true;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

        if (viewPos.x < -0.05 || viewPos.x > 1.05)
        {
            TeleportPlayer();
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            Vector3 acceleration = Input.acceleration;
            Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0f, 0f, -1f), Input.acceleration);
            Vector3 movement = rotateQuaternion * Vector3.up;
            DoodleRigid.AddForce(new Vector2(movement.x * 50f, 0));
            if (movement.x > 0 && !facingRight)
                Flip();
            else if (movement.x < 0 && facingRight)
                Flip();
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            float moveX = 0f;

            if (Input.GetKey(KeyCode.A))
            {
                moveX = -0.45f;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveX = 0.45f;
            }
            DoodleRigid.AddForce(new Vector2(moveX * 5f, 0));
            if (moveX > 0 && !facingRight)
                Flip();
            else if (moveX < 0 && facingRight)
                Flip();
        }
        if (pendingJump.HasValue)
        {
            DoodleRigid.AddForce(pendingJump.Value);
            pendingJump = null;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "DeadZone" || collision.collider.name == "Mob")
        {
            SceneManager.LoadScene(2);
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
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
