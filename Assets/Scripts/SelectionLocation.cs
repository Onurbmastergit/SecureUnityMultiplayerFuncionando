using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SelectionLocation : MonoBehaviour
{
    Location location;
   public string nameLocation;
   public int woods;
   public int metals;
   public int stones;
    [SerializeField] Location[] locationList;

    public GameObject ballonRec;
    public GameObject mapImage;

   public TextMeshProUGUI locations;
   public TextMeshProUGUI metal;
   public TextMeshProUGUI stone;
   public TextMeshProUGUI wood;
    void Update()
    {
       
          for(int i = 0; i < locationList.Count(); i++)
          {
            if(nameLocation == locationList[i].Title)
            {
            location = locationList[i];
            nameLocation = location.Title;
            woods = location.Wood;
            metals = location.Metal;
            stones = location.Stone; 
            break;
            }
          }
          
        
    }
    public void OpenMap()
    {
        mapImage.SetActive(true);
    }
   public void ShowInformations()
   {
    ballonRec.SetActive(false);
    ballonRec.SetActive(true);
    locations.text = nameLocation;
    metal.text = metals.ToString();
    stone.text = stones.ToString();
    wood.text = woods.ToString();
   }
}
