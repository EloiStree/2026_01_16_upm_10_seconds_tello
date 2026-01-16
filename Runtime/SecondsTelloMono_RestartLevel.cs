using UnityEngine;

public class SecondsTelloMono_RestartLevel : MonoBehaviour
{


    [ContextMenu("Restart Level")]
    public void RestartTheLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }



}
