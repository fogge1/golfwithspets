using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// 7:00

public class SaveSystem : MonoBehaviour
{
    [SerializeField] Tile straightPrefab;
    [SerializeField] Tile turnLeftPrefab;
    [SerializeField] Tile turnRightPrefab;
    [SerializeField] Tile rampPrefab;

    public static List<Tile> tiles = new List<Tile>();
    string SUB_PATH = "/tile";
    string SUB_COUNT_PATH = "/tile.count";

    void Awake()
    {
        LoadTile();
    }
    
    void OnApplicationQuit() {
        SaveTile();
    }

    void SaveTile() {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SUB_PATH;
        string countPath = Application.persistentDataPath + SUB_COUNT_PATH;

        FileStream countStream = new FileStream(countPath, FileMode.Create);

        formatter.Serialize(countStream, tiles.Count);
        countStream.Close();

        for (int i = 0; i < tiles.Count; i++) {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            TileData data = new TileData(tiles[i]);

            formatter.Serialize(stream, data);

            stream.Close();
        }
    }

    void LoadTile() {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SUB_PATH;
        string countPath = Application.persistentDataPath + SUB_COUNT_PATH;
        int tileCount = 0;

        if (File.Exists(countPath)) {
            FileStream countStream = new FileStream(countPath, FileMode.Open);

            tileCount = (int)formatter.Deserialize(countStream);
            countStream.Close();
        }
        else {
            Debug.LogError("Path not found in " + countPath);
        }

        for (int i = 0; i < tileCount; i++) {
            if (File.Exists(path + i)) {
                FileStream stream = new FileStream(path + i, FileMode.Open);
                TileData data = formatter.Deserialize(stream) as TileData;

                stream.Close();

                Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
                Quaternion rotation = Quaternion.Euler(data.rotation[0], data.rotation[1], data.rotation[2]);
                
                switch(data.tag) {
                    case "straight": 
                        SpawnTile(straightPrefab, position, rotation);
                    break;
                    case "curveLeft":
                        SpawnTile(turnLeftPrefab, position, rotation);
                    break;
                    case "curveRight":
                        SpawnTile(turnRightPrefab, position, rotation);
                    break;
                    case "ramp":
                        SpawnTile(rampPrefab, position, rotation);
                    break;
                }
            }
            else {
                Debug.LogError("Path not found in " + path + i);
            }
            
        }
    }

    void SpawnTile(Tile tilePrefab, Vector3 position, Quaternion rotation) {
        Tile tile = Instantiate(tilePrefab, position, rotation);
    }
}
