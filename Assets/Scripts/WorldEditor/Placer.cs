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
    [SerializeField] Material _mat;
    [SerializeField] Material _matT;
    public GameObject addedAllowed;
    public GameObject addedTile;
    int allowedTileOffset = -2;

    [SerializeField] private CameraController _cameraScript;
    [SerializeField] private GameObject _saveBtn;

    // Start is called before the first frame update
    void Start()
    {
        _saveBtn.SetActive(false);
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
                changeMat(addedAllowed, _matT);                
            break;
            case "ramp":
                changeMat(addedAllowed, _matT);                
            break;
            case "curveRight":
                addedAllowed.transform.rotation *= Quaternion.Euler(0, 90, 0);
                changeMat(addedAllowed, _matT);                
            break;
            case "curveLeft":
                addedAllowed.transform.rotation *= Quaternion.Euler(0, -90, 0);
                changeMat(addedAllowed, _matT);          
            break;
            case "end":
                addedAllowed.transform.rotation *= Quaternion.Euler(0, 180, 0);
                changeMat(addedAllowed, _matT);          
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
                changeMat(addedTile, _mat);                
            break;
            case "ramp":
                changeMat(addedTile, _mat);
            break;
            case "curveRight":
                changeMat(addedTile, _mat);
            break;
            case "curveLeft":
                changeMat(addedTile, _mat);
            break;
            case "end":
                addedAllowed.transform.rotation *= Quaternion.Euler(0, 180, 0);
                changeMat(addedTile, _mat);
                _saveBtn.SetActive(true);
                _cameraScript.enabled = false;
                Destroy(addedAllowed);
            break;
            
        }
        
    
        map.Add(addedTile);
    }
}
