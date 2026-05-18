using UnityEngine;
using UnityEngine.SceneManagement;
public class MainHealthAndCollisionHandler : MonoBehaviour
{
    private MainSnakeTailHandler tailManager;

    void Start()
    {
        tailManager = GetComponent<MainSnakeTailHandler>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("snakeBodyColilision_Death"))
        {
            Die("Wall");
        }
        if (collision.gameObject.CompareTag("wallCollision_Death"))
        {
            Die("Wall");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tailManager != null && tailManager.IsSelfCollision(other.gameObject))
        {
            Die("Tail");
            return;
        }
        if (other.gameObject.CompareTag("wallCollision_Death"))
        {
            Die("Wall");
        }
    }

    private void Die(string causeOfDeath)
    {
        Debug.Log("Game Over! The snake crashed into a " + causeOfDeath);
        
        if (MainSoundManager.Instance != null)
        {
            MainSoundManager.Instance.PlaySFX("Crash");
        }
        
        if (MainGameModeController.Instance != null)
        {
            MainGameModeController.Instance.EndGame("Crashed into " + causeOfDeath);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
