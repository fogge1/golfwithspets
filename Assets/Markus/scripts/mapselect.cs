using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class mapselect : MonoBehaviour
{
    public Text TextBox;
    void start()
    {
        var dropdown = transform.GetComponent<Dropdown>();

        dropdown.options.Clear();

        List<string> items = new List<string>();
        items.Add("map1");
        items.Add("map2");
        foreach(var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData(){ text = item });
        }
        dropdown.onValueChanged.AddListener(delegate { dropdownItemSelected(dropdown); });

        DropdownItemSelected(dropdown);
    }
    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        TextBox.Text = dropdown.options[index].text;
    }
    
    

    void Update()
    {
        
    }
}
