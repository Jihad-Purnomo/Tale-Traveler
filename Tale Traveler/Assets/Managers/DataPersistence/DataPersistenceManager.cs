using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjetcs;
    private FileDataHandler dataHandler;
    public static DataPersistenceManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {

        }
        Instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjetcs = FindAllDataPersistenceObjects(); 
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // return right away if data persistence is disabled
        //if (disableDataPersistence)
        //{
        //    return;
        //}

        // load any saved data from a file using the data handler
        this.gameData = dataHandler.Load();// (selectedProfileId);

        // start a new game if the data is null and we're configured to initialize data for debugging purposes
        //if (this.gameData == null && initializeDataIfNull)
        //{
        //    NewGame();
        //}

        //if no data can be loaded, don't continue
        if (this.gameData == null)
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded.");
            return;
        }

        // push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjetcs)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        // return right away if data persistence is disabled
        //if (disableDataPersistence)
        //{
        //    return;
        //}

        // if we don't have any data to save, log a warning here
        //if (this.gameData == null)
        //{
        //    Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
        //    return;
        //}

        // pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjetcs)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        // timestamp the data so we know when it was last saved
        //gameData.lastUpdated = System.DateTime.Now.ToBinary();

        // save that data to a file using the data handler
        dataHandler.Save(gameData);//, selectedProfileId);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        // FindObjectsofType takes in an optional boolean to include inactive gameobjects
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
