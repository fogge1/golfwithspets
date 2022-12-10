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
                      
        }
        //Debug.Log(selectedTransform);

    }

    

    public void addGhostTile() {
        Destroy(addedAllowed);
        addedAllowed = Instantiate(currentGameObject, addedTile.transform.position+addedTile.transform.up*-4, Quaternion.Euler(0, 0, 0));
        
        addedAllowed.transform.parent = addedTile.transform;
        addedAllowed.transform.localScale = new Vector3(1, 1, 1);
        addedAllowed.transform.rotation = addedTile.transform.rotation;

        if (addedTile.tag == "ramp") {
            addedAllowed.transform.position += new Vector3(0, 1.15f, 0);
        }

        switch(addedAllowed.tag) {
            case "straight":
                //addedTile.GetComponent<MeshRenderer> ().material = straight;
                // addedAllowed.transform.position += new Vector()
                addedAllowed.GetComponent<MeshRenderer>().material = straightTrans;
                // addedAllowed.transform.position = addedTile.transform.position + new Vector3(0, 0, 5);
                // addedAllowed.transform.position += new Vector3(0f, 0f, 1f);
            break;
            case "ramp":
                //addedTile.GetComponent<MeshRenderer> ().material = ramp;
                addedAllowed.GetComponent<MeshRenderer>().material = rampTrans;
            break;
            case "curve":
                //addedTile.GetComponent<MeshRenderer>().material = curve;
                //addedTile.transform.rotation *= Quaternion.Euler(0, 0, 90);
                addedAllowed.transform.rotation *= Quaternion.Euler(0, 0, 90);
                addedAllowed.GetComponent<MeshRenderer>().material = curveTrans;
            break;
            
        }
        // addedAllowed.tag = "allowedMoves";


        Transform allowedMove_ = addedTile.transform.GetChild(0);
        selectedTransform = allowedMove_;
        
    }

    void place() {
        
        //Debug.Log(selectedTransform);
        Transform allowedMove_ = addedTile.transform.GetChild(0);
        selectedTransform = allowedMove_;
        addedTile = Instantiate(currentGameObject, selectedTransform.position, selectedTransform.rotation);
        addGhostTile();
        
        //addedAllowed = Instantiate(currentGameObject, addedTile.transform.position+addedTile.transform.up*-4, Quaternion.Euler(0, 0, 0));

        
        switch(addedTile.tag) {
            case "straight":
                addedTile.GetComponent<MeshRenderer> ().material = straight;
            break;
            case "ramp":
                addedTile.GetComponent<MeshRenderer> ().material = ramp;
            break;
            case "curve":
                addedTile.GetComponent<MeshRenderer>().material = curve;
            break;
        }
    
        map.Add(addedTile);
    }
}
