using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //variable declaration
    public enum GameState { Title, MainMenu, Options, GamePlay, GameOver, Credits };
    public GameState currentState;
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
    public List<Powerup> allPowerups;
    public GameObject TitleScreenStateObject;
    public GameObject MainMenuScreenStateObject;
    public GameObject OptionsScreenStateObject;
    public GameObject CreditsScreenStateObject;
    public GameObject GamePlayScreenStateObject;
    public GameObject GameOverScreenStateObject;

    private PawnSpawnPoint randomAI;
    // Start is called before the first frame update
    private void Start()
    {
        
     

    }
    private void Update()
    {
        
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
        currentState = GameState.GamePlay;
        DoGamePlayState();
    }
    private void DeactivateAllStates()
    {
        TitleScreenStateObject.SetActive(false);
        MainMenuScreenStateObject.SetActive(false);
        OptionsScreenStateObject.SetActive(false);
        CreditsScreenStateObject.SetActive(false);
        GamePlayScreenStateObject.SetActive(false);
        GameOverScreenStateObject.SetActive(false);
    }
    public void ActivateTitleScreen()
    {  
        DeactivateAllStates();     
        TitleScreenStateObject.SetActive(true);    
    }
    public void ActivateMainMenuScreen()
    {
        DeactivateAllStates();
        MainMenuScreenStateObject.SetActive(true);
    }
    public void ActivateOptionsScreen()
    {
        DeactivateAllStates();
        OptionsScreenStateObject.SetActive(true);
    }
    public void ActivateCreditsScreen()
    {
        DeactivateAllStates();
        CreditsScreenStateObject.SetActive(true);
    }
    public void ActivateGamePlayScreen()
    {
        DeactivateAllStates();
        GamePlayScreenStateObject.SetActive(true);
    }
    public void ActivateGameOverScreen()
    {
        DeactivateAllStates();
        GameOverScreenStateObject.SetActive(true);
    }
    protected void TitleState()
    {
        ActivateTitleScreen();
    }
    public void DoTitleState()
    {
        TitleState();
    }
    protected void MainMenuState()
    {
        ActivateMainMenuScreen();
    }
    public void DoMainMenuState()
    {
        MainMenuState();
    }
    protected void OptionsState()
    {
        ActivateOptionsScreen();
    }
    public void DoOptionsState()
    {
        OptionsState();
    }
    protected void GamePlayState()
    {
        ActivateGamePlayScreen();
        if (allPowerups != null)
        {
            allPowerups.Clear();
        }
        if (aiPlayers != null)
        {
            foreach (AIController ai in aiPlayers)
            {
                Destroy(ai.pawn);
                Destroy(ai);
            }
            aiPlayers.Clear();
        }
        if (players != null)
        {
            foreach (PlayerController player in players)
            {
                Destroy(player.pawn);
            }
        }
        level.DestroyMap();
        level.GenerateMap();
        level.pawnSpawns = FindObjectsOfType<PawnSpawnPoint>();
        if (players != null && players.Count > 0)
        {
            SpawnPlayerPawns();
        }
        else
        {

            SpawnPlayer();
        }
        foreach (PlayerController player in players)
        {
            player.playerScore = 0;
        }

        for (int i = 0; i <= level.TotalAiToSpawn; i++)
        {

            SpawnAi();
        }

    }
    public void SpawnPlayerPawns()
    {
        foreach (PlayerController player in players)
        {
            if (player.playerLives > 0)
            {
                playerSpawnTransform = RandomSpawnObject().transform;
                GameObject newPawnObject = Instantiate(tankPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation) as GameObject;
                Pawn newPawn = newPawnObject.GetComponent<Pawn>();
                player.pawn = newPawn;
                newPawn.controller = player;
                
            }
        }
    }
    public void SpawnPlayerControllers()
    {
        GameObject newPlayerObject = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        Controller newController = newPlayerObject.GetComponent<Controller>();
        PlayerController newPlayerController = newPlayerObject.GetComponent<PlayerController>();
        

        players.Add(newPlayerController);

    }
    public void DoGamePlayState()
    {
        GamePlayState();
    }
    protected void GameOverState()
    {
        ActivateGameOverScreen();
    }
    public void DoGameOverState()
    {
        GameOverState();
    }
    protected void CreditsState()
    {
        ActivateCreditsScreen();
    }
    public void DoCreditsState()
    {
        CreditsState();
    }
    //function to spawn player
    public void SpawnPlayer()
    {
        playerSpawnTransform = RandomSpawnObject().transform;
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation) as GameObject;
        Controller newController = newPlayerObj.GetComponent<Controller>();
        players.Add((PlayerController)newController);
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();
        newController.pawn = newPawn;
        newPawn.controller = newController;

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
        newAIPawn.controller = newAIController;
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
