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
    public Material straight;
    public Material straightTrans;
    public Material ramp;
    public Material rampTrans;
    public Material curve;
    public Material curveTrans;
    GameObject addedAllowed;

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

    void switchTile() {

    }

    void place() {
        _allowedToDestroy = GameObject.FindGameObjectsWithTag("allowedMoves");
        foreach (GameObject allowedMove in _allowedToDestroy) {
            Destroy(allowedMove);
        }

        GameObject addedTile = Instantiate(currentGameObject, selectedTransform.position, selectedTransform.rotation);
        
        addedAllowed = Instantiate(currentGameObject, addedTile.transform.position+addedTile.transform.up*-4, Quaternion.Euler(0, 0, 0));

        addedAllowed.transform.parent = addedTile.transform;
        addedAllowed.transform.localScale = new Vector3(1, 1, 1);
        addedAllowed.transform.rotation = addedTile.transform.rotation;
        
        switch(addedTile.tag) {
            case "straight":
                addedTile.GetComponent<MeshRenderer> ().material = straight;
                // addedAllowed.transform.position += new Vector()
                addedAllowed.GetComponent<MeshRenderer>().material = straightTrans;
                // addedAllowed.transform.position = addedTile.transform.position + new Vector3(0, 0, 5);
                // addedAllowed.transform.position += new Vector3(0f, 0f, 1f);
            break;
            case "ramp":
                addedTile.GetComponent<MeshRenderer> ().material = ramp;
                addedAllowed.transform.position += new Vector3(0, 1.15f, 0);
                addedAllowed.GetComponent<MeshRenderer>().material = rampTrans;
            break;
            case "curve":
                addedTile.GetComponent<MeshRenderer>().material = curve;
                addedTile.transform.rotation *= Quaternion.Euler(0, 0, 90);
                addedAllowed.GetComponent<MeshRenderer>().material = curveTrans;
            break;
        }
        addedAllowed.tag = "allowedMoves";
    



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
