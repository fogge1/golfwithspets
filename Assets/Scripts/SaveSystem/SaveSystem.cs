using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

// 7:00

public class SaveSystem : MonoBehaviour
{
    [SerializeField] Tile straightPrefab;
    [SerializeField] Tile turnLeftPrefab;
    [SerializeField] Tile turnRightPrefab;
    [SerializeField] Tile rampPrefab;
    [SerializeField] Tile endPrefab;
    [SerializeField] Material greenMat;

    public static List<Tile> tiles = new List<Tile>();
    const string MAP_INDEX_PATH = "/map.count";
    string SUB_PATH = "/tiles";

    public void SaveTile() {
        //SeriouslyDeleteAllSaveFiles();
        int mapIndex = 0;

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SUB_PATH + mapIndex;
        string countPath = Application.persistentDataPath + SUB_PATH + mapIndex +".count";


        FileStream countStream = new FileStream(countPath, FileMode.Create);

        formatter.Serialize(countStream, tiles.Count);
        countStream.Close();

        for (int i = 0; i < tiles.Count; i++) {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            TileData data = new TileData(tiles[i]);

            formatter.Serialize(stream, data);

            stream.Close();
        }

        SceneManager.LoadScene(1);
    }

    public void LoadTile() {
        int mapIndex = 0;

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SUB_PATH + mapIndex;
        string countPath = Application.persistentDataPath + SUB_PATH + mapIndex + ".count";
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
                    case "end":
                        SpawnTile(endPrefab, position, rotation);
                    break;
                }
            }
            else {
                Debug.LogError("Path not found in " + path + i);
            }
            
        }
    }

    // public void SeriouslyDeleteAllSaveFiles()
    // {
    //     string path = Application.persistentDataPath + SUB_PATH;
    //     string countPath = Application.persistentDataPath + SUB_PATH + ".count";
    //     DirectoryInfo directory = new DirectoryInfo(path);
    //     directory.Delete(true);
    //     DirectoryInfo directoryCount = new DirectoryInfo(countPath);
    //     directoryCount.Delete(true);
    //     Directory.CreateDirectory(path);
    //     Directory.CreateDirectory(countPath);
    // }

    public int GetMaps() {
        BinaryFormatter formatter = new BinaryFormatter();
        string mapPath = Application.persistentDataPath + MAP_INDEX_PATH;

        int mapIndex = 1;

        while (true) {
            if (File.Exists(mapPath + mapIndex)) {
                mapIndex++;
            }
            else {
                Debug.Log("test");
                return mapIndex;
            }
        }
        
    }

    void SpawnTile(Tile tilePrefab, Vector3 position, Quaternion rotation) {
        Tile tile = Instantiate(tilePrefab, position, rotation);
        changeMat(tile, greenMat);
    }

    void changeMat(Tile go, Material mat) {
        for (int i = 0; i < go.transform.childCount; i++) {
            GameObject part = go.transform.GetChild(i).gameObject;
            if (part.tag == "part") {
                part.GetComponent<MeshRenderer>().material = mat;
            }
        }
    }    
}
