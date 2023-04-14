using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //variable declaration
    public MapGenerator level;
    public static GameManager instance;
    public GameObject playerControllerPrefab;
    public GameObject tankPawnPrefab;
    public GameObject aiPawnPrefab;
    public Transform playerSpawnTransform;
    public Transform aiSpawnTransform;
    public List<PlayerController> players;
    public List<AIController> aiPlayers;
    public List<GameObject> aiControllerPrefabs;
    
    private PawnSpawnPoint randomAI;
    // Start is called before the first frame update
    private void Start()
    {
        level.GenerateMap();
        //spawn player randomly
        level.pawnSpawns = FindObjectsOfType<PawnSpawnPoint>();

               
        SpawnPlayer();
        
        for(int i = 0; i <= level.TotalAiToSpawn; i ++)
        {
            
            SpawnAi();
        }
        
    }
    //function to ensure this is the only game manager
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //function to spawn player
    public void SpawnPlayer()
    {
        playerSpawnTransform = RandomSpawnObject().transform;
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation) as GameObject;
        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();
        newController.pawn = newPawn;
    }
    public void SpawnAi()
    {
        randomAI = RandomSpawnObject();
        aiSpawnTransform = randomAI.transform;
        GameObject newAiObject = Instantiate(RandomAiPrefab(), Vector3.zero, Quaternion.identity) as GameObject;
        GameObject newAiPawnObject = Instantiate(aiPawnPrefab, aiSpawnTransform.position, Quaternion.identity) as GameObject;
        AIController newAIController = newAiObject.GetComponent<AIController>();
        Pawn newAIPawn = newAiPawnObject.GetComponent<Pawn>();
        newAIController.pawn = newAIPawn;
        newAIController.waypoints = randomAI.GetComponent<PawnSpawnPoint>().currentRoom.roomPatrol;

    }
    public GameObject RandomAiPrefab()
    {
      
        return aiControllerPrefabs[Random.Range(0, aiControllerPrefabs.Count)];
    }
    public PawnSpawnPoint RandomSpawnObject()
    {
        return level.pawnSpawns[Random.Range(0, level.pawnSpawns.Length)];
       
    }
}
