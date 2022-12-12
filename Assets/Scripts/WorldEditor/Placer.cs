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
    // Direction from curve
    private int turnDir = 1;

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

        if (Input.GetKeyDown(KeyCode.R) && addedAllowed.tag == "curve") {
            turnDir *= -1;
            addGhostTile();
        }
    }

    

    public void addGhostTile() {
        Destroy(addedAllowed);
        int allowedTileOffset = -4;


        addedAllowed = Instantiate(currentGameObject, addedTile.transform.position+addedTile.transform.up*allowedTileOffset, Quaternion.Euler(0, 0, 0));
        
        addedAllowed.transform.parent = addedTile.transform;
        addedAllowed.transform.localScale = new Vector3(1, 1, 1);
        addedAllowed.transform.rotation = addedTile.transform.rotation;

        if (addedTile.tag == "ramp") {
            addedAllowed.transform.position += new Vector3(0, 1.15f, 0);
        }

        switch(addedAllowed.tag) {
            case "straight":
                
                addedAllowed.GetComponent<MeshRenderer>().material = straightTrans;
                
            break;
            case "ramp":
                //addedTile.GetComponent<MeshRenderer> ().material = ramp;
                addedAllowed.GetComponent<MeshRenderer>().material = rampTrans;
            break;
            case "curveRight":
                

                addedAllowed.transform.rotation *= Quaternion.Euler(0, 0, 90);
                addedAllowed.GetComponent<MeshRenderer>().material = curveTrans;
            break;
            case "curveLeft":
                

                addedAllowed.transform.rotation *= Quaternion.Euler(0, 0, -90);
                addedAllowed.GetComponent<MeshRenderer>().material = curveTrans;
            break;
            
        }

        Transform allowedMove_ = addedTile.transform.GetChild(0);
        selectedTransform = allowedMove_;
        
    }

    void place() {
        
        Transform allowedMove_ = addedTile.transform.GetChild(0);
        selectedTransform = allowedMove_;
        addedTile = Instantiate(currentGameObject, selectedTransform.position, selectedTransform.rotation);
        
        addGhostTile();
        
        
        switch(addedTile.tag) {
            case "straight":
                addedTile.GetComponent<MeshRenderer> ().material = straight;
            break;
            case "ramp":
                addedTile.GetComponent<MeshRenderer> ().material = ramp;
            break;
            case "curveRight":
            case "curveLeft":
                addedTile.GetComponent<MeshRenderer>().material = curve;
            break;
        }
    
        map.Add(addedTile);
    }
}
