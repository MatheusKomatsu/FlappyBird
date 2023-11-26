using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    private float cooldown = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
         var gameManager = GameManager.Instance;
        // Ignore if game is over
        if (gameManager.IsGameOver()){
            return;
        }
        // Update cooldown
        cooldown -= Time.deltaTime;
        if (cooldown <= 0f){
           
            cooldown = gameManager.obstacleInterval;
            // Spawn obstacle from prefab list
            int prefabIndex = Random.Range(0,gameManager.obstaclePrefabs.Count);
            GameObject prefab = gameManager.obstaclePrefabs[prefabIndex];
            // Get position where obstacle can spawn
            float x = gameManager.obstacleOffsetX;
            float y = Random.Range(gameManager.obstacleOffsetY.x,gameManager.obstacleOffsetY.y);
            float z = 0.865f;
            Vector3 position = new Vector3(x,y,z);
            // Get rotation
            Quaternion rotation = prefab.transform.rotation;
            // Instantiate obstacle
            Instantiate(prefab, position,rotation);
        }
    }
}
