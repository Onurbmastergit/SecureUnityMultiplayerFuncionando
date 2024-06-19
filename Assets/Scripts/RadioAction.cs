using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RadioAction : MonoBehaviour
{
public TextMeshProUGUI nameLocation;
public GameObject map;
[SerializeField] Location[] locationLists;
Location location;

public void Travel()
{
    for(int i = 0; i < locationLists.Count(); i++)
          {
            if(nameLocation.text == locationLists[i].Title)
            {
            location = locationLists[i];
            break;
            }
          }
    LevelManager.instance.selectedLocation = location;
    map.SetActive(false);        
}
}
