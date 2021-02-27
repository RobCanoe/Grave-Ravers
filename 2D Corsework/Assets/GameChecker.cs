
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameChecker : MonoBehaviour
{
    
    bool gameHasEnded = false;
    public float restartDelay = 1f;
    public GameObject completeLevelUI;
    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
    }

    public void EndGame()
    {
        if(gameHasEnded == false)
        {
            
            
            gameHasEnded = true;
            
            Debug.Log("GameOver");
            Restart();
            Invoke("Restart", restartDelay);
        }
        
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
