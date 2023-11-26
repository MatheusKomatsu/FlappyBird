using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;    

public class GameManager : MonoBehaviour
{
    // Game Manager that can't be changed by others
    public static GameManager Instance {get;private set;}
    // The List of obstacles "prefabs" was renamed to "obstaclePrefabs"
    [FormerlySerializedAs("prefabs")]
    public List<GameObject> obstaclePrefabs;
    // Set interval, speed and position for spawning for the obstacles
    public float obstacleInterval = 1f;
    public float obstacleSpeed = 10;
    public float obstacleOffsetX = 0;
    public Vector2 obstacleOffsetY = new Vector2(0,0);
    // Player score
    [HideInInspector] 
    public int score; 
    // Used to check if game is over
    private bool isGameOver = false; 
    // Start is called before the first frame update
    void Awake(){
        // Singleton
        if (Instance != null && Instance != this){
            Destroy(this);
        }else{
            Instance = this;
        }
    }
    public bool IsGameOver(){
        return isGameOver;
    }
    public bool IsGameActive(){
        return !isGameOver;
    }
    public void EndGame(){
        // Set flag
        isGameOver = true;
        // Print Message
        Debug.Log("Game Over... Your score was: " +score );
        // Reload Scene
        StartCoroutine(ReloadScene(2));
        
    }
    private IEnumerator ReloadScene(float delay){
        // Wait 2 seconds (delay)
        yield return new WaitForSeconds(delay);
        // Reload scene
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
        Debug.Log("Reload scene please!!");
    }
}
