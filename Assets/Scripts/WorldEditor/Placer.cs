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
    public GameObject addedAllowed;
    public GameObject addedTile;
    int allowedTileOffset = -2;

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

    void changeMat(GameObject go, Material mat) {
        for (int i = 0; i < go.transform.childCount; i++) {
            GameObject part = go.transform.GetChild(i).gameObject;
            if (part.tag == "part") {
                part.GetComponent<MeshRenderer>().material = mat;
            }
        }
    }    

    public void addGhostTile() {
        Destroy(addedAllowed);

        
        addedAllowed = Instantiate(currentGameObject, addedTile.transform.position+addedTile.transform.forward*allowedTileOffset, Quaternion.Euler(0, 0, 0));
        
        addedAllowed.transform.parent = addedTile.transform;
        addedAllowed.transform.rotation = addedTile.transform.rotation;

        if (addedTile.tag == "ramp") {
            addedAllowed.transform.position += new Vector3(0, 1f, 0);
        }

        switch(addedAllowed.tag) {
            case "straight":
                changeMat(addedAllowed, straightTrans);                
            break;
            case "ramp":
                changeMat(addedAllowed, rampTrans);                
            break;
            case "curveRight":
                addedAllowed.transform.rotation *= Quaternion.Euler(0, 90, 0);
                changeMat(addedAllowed, curveTrans);                
            break;
            case "curveLeft":
                addedAllowed.transform.rotation *= Quaternion.Euler(0, -90, 0);
                changeMat(addedAllowed, curveTrans);          
            break;
            
        }
        
        Camera.main.GetComponent<CameraController>().target = addedAllowed;

        _allowedMoves = new List<GameObject>();
        for (int i = 0; i < addedTile.transform.childCount; i++) {
            GameObject allowedMove = addedTile.transform.GetChild(i).gameObject;
            if (allowedMove.name != "part") {
                selectedTransform = allowedMove.transform;
            }
        }
        
    }

    void place() {
        
        _allowedMoves = new List<GameObject>();
        for (int i = 0; i < addedTile.transform.childCount; i++) {
            GameObject allowedMove = addedTile.transform.GetChild(i).gameObject;
            if (allowedMove.tag != "part") {
                selectedTransform = allowedMove.transform;
            }
        }
        addedTile = Instantiate(currentGameObject, selectedTransform.position, selectedTransform.rotation);
        
        addGhostTile();

        switch(addedTile.tag) {
            case "straight":
                changeMat(addedTile, straight);                
            break;
            case "ramp":
                changeMat(addedTile, ramp);
            break;
            case "curveRight":
                changeMat(addedTile, curve);
            break;
            case "curveLeft":
                changeMat(addedTile, curve);
            break;
            
        }
        
    
        map.Add(addedTile);
    }
}
