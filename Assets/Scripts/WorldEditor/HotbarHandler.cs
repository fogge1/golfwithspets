using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarHandler : MonoBehaviour
{
    // Start is called before the first frame update

    // list of prefab tiles
    public GameObject[] Hotbar;
    
    private GameObject _heldItem;
    private int heldItemIndex = 0;
    
    // Get Placer script
    public Placer placer;

    void Start()
    {   
        _heldItem = Hotbar[heldItemIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            changeHeldTile(1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            changeHeldTile(-1);
        }
    }

    void changeHeldTile(int dir) {
        heldItemIndex += dir;
        
        if (heldItemIndex+1 > Hotbar.Length) {
            heldItemIndex = 0;
        }
        else if (heldItemIndex < 0) {
            heldItemIndex = Hotbar.Length-1;
        }

        _heldItem = Hotbar[heldItemIndex];
        Debug.Log(_heldItem.name);

        placer.currentGameObject = _heldItem;
    }
}
