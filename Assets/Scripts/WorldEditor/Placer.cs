using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    // TIle that is held by player
    public GameObject currentGameObject;

    List<GameObject> map = new List<GameObject>();
    public Transform selectedTransform;
    private List<GameObject> _allowedMoves  = new List<GameObject>();
    private GameObject[] _allowedToDestroy = new GameObject[]{};

    // For future
    private int indexOfPositions = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            place();
        }

    }

    void place() {
        _allowedToDestroy = GameObject.FindGameObjectsWithTag("allowedMoves");
        foreach (GameObject allowedMove in _allowedToDestroy) {
            Destroy(allowedMove);
        }

        GameObject addedTile = Instantiate(currentGameObject, selectedTransform.position, selectedTransform.rotation);
        
        map.Add(addedTile);
        // Need to empty allowedMoves array
        _allowedMoves = new List<GameObject>();
        for (int i = 0; i < addedTile.transform.childCount; i++) {
            GameObject allowedMove = addedTile.transform.GetChild(i).gameObject;
            _allowedMoves.Add(allowedMove);
        }
        indexOfPositions = 0;
        selectedTransform = _allowedMoves[indexOfPositions].transform;
    }
}
