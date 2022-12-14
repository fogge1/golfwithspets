using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class mapselect : MonoBehaviour
{
    List<string> mapnames = new List<string>();
    List<string> mapstrings = new List<string>();

    void PopulateNames()//placeholder
    {
        mapnames.Clear();
        for ( int i = 0; i < 5; i++)
        {
            mapnames.Add(i.ToString());
        }
    }
    void Start()
    {
        PopulateNames();
        GameObject buttonTemple = transform.GetChild(0).gameObject;
        GameObject g;
        for (int i = 0; i < mapnames.Count; i++)
        {
            g = Instantiate(buttonTemple, transform);
            //g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = mapnames[i];
        }
        Destroy(buttonTemple);
    }
}
