using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ParcelWeight : MonoBehaviour
{
    
    private void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = transform.parent.parent.gameObject.GetComponent<Parcel>().points.ToString() + " Kg";
    }

   
}
