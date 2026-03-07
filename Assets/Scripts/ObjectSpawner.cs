using UnityEngine;
using System.Collections.Generic; // Importing the System.Collections.Generic namespace for using List

public class ObjectSpawner : MonoBehaviour
{

    [SerializeField] private Transform minPos; // Minimum position for spawning objects
    [SerializeField] private Transform maxPos; // Maximum position for spawning objects
    [SerializeField] private int waveNumber = 0; // Current wave number
    [SerializeField] private List<Wave> waves; // List of waves containing spawn data
    [System.Serializable] // Making the Wave class serializable for inspector editing
    public class Wave
    {
        [SerializeField] public GameObject Prefabs;
        [SerializeField] public float spawnInterval = 2f; // Time interval between spawns
        [SerializeField] public float spawnTimer = 0f; // Timer to track spawn intervals
        public int objectsPerWave; // Number of objects to spawn in this wave
        public int spawnedObjectsCount; // Counter for spawned objects in the current wave
    }

    void Update()
    {
        SpawnObject(); // Call the method to spawn objects
    }
    private void SpawnObject()
    {
        waves[waveNumber].spawnTimer += (Time.deltaTime * PlayerController.instance.boost); // Increment the timer by the time since last frame
        if (waves[waveNumber].spawnTimer >= waves[waveNumber].spawnInterval) // Check if it's time to spawn a new object
        {
            Instantiate(waves[waveNumber].Prefabs, RandomPos(), Quaternion.identity); // Spawn the asteroid prefab at the spawner's position
            waves[waveNumber].spawnTimer = 0f; // Reset the timer after spawning
            waves[waveNumber].spawnedObjectsCount++; // Increment the count of spawned objects in the current wave
        }
        if (waves[waveNumber].spawnedObjectsCount >= waves[waveNumber].objectsPerWave) // Check if the current wave's object limit is reached
        {
            waveNumber++; // Move to the next wave
            if (waveNumber >= waves.Count) // Check if there are more waves to spawn
            {
                waveNumber = 0; // Reset to the first wave if all waves are completed
            }
            waves[waveNumber].spawnedObjectsCount = 0; // Reset the count for the new wave
        }
    }
    public Vector2 RandomPos()
    {
        Vector2 spawnPos;
        spawnPos.x = minPos.position.x;
        spawnPos.y = Random.Range(minPos.position.y, maxPos.position.y); // Randomly select a y position within the defined range
        return spawnPos; // Return the randomly generated position
    }
}
