using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTiles : MonoBehaviour
{
    
    void Awake() {
        this.GetComponent<SaveSystem>().LoadTile();
    }
}
