using UnityEngine;

[System.Serializable]

public class TileData
{
    public float[] position;
    public float[] rotation;
    public string tag;

    public TileData(Tile tile) {
        Vector3 tilePos = tile.transform.position;
        Vector3 tileRot = tile.transform.eulerAngles;
        
        tag = tile.tag;
        position = new float[]
        {
            tilePos.x, tilePos.y, tilePos.z
        };

        rotation = new float[]
        {
            tileRot.x, tileRot.y, tileRot.z
        };


    }
}
