using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarHandler : MonoBehaviour
{
    // Start is called before the first frame update

    // list of prefab tiles
    public GameObject[] Tiles;
    
    private GameObject _heldItem;
    private int heldItemIndex = 0;

    [SerializeField] private GameObject _hotbar;
    private GameObject _currentItem;
    
    
    // Get Placer script
    public Placer placer;

    void Start()
    {   
        _heldItem = Tiles[heldItemIndex];
        _currentItem = _hotbar.transform.GetChild(0).gameObject;
        _currentItem.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            changeHeldTile(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            changeHeldTile(1);
        }
    }

    void changeHeldTile(int dir) {
        heldItemIndex += dir;
        
        if (heldItemIndex+1 > Tiles.Length) {
            heldItemIndex = 0;
        }
        else if (heldItemIndex < 0) {
            heldItemIndex = Tiles.Length-1;
        }

        _heldItem = Tiles[heldItemIndex];
        Debug.Log(_heldItem.name);
        
        placer.currentGameObject = _heldItem;
        changeHotbar();
        placer.addGhostTile();

    }

    void changeHotbar() {
        _currentItem.GetComponent<RectTransform>().sizeDelta = new Vector2(75, 75);

        for (int i = 0; i < _hotbar.transform.childCount; i++) {
            GameObject hotbarItem = _hotbar.transform.GetChild(i).gameObject;
            if (_heldItem.tag == hotbarItem.tag) {
                Debug.Log("test");
                _currentItem = hotbarItem;
                _currentItem.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
            }
        }
    }
}
