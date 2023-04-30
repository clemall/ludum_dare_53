using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckColliderPoints : MonoBehaviour
{
    public LayerMask layer;
    public Truck truck;
    
    public  List<GameObject> listParcels = new List<GameObject>();

    public bool acceptParcel = true;

    
    void OnTriggerEnter2D(Collider2D col)
    {
        
        if ((layer.value & (1 <<col.gameObject.layer)) > 0 && acceptParcel)
        {
            int points = col.gameObject.GetComponent<Parcel>().points;
            truck.AddPoints(points);
            
            listParcels.Add(col.gameObject);
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        
        if ((layer.value & (1 <<col.gameObject.layer)) > 0 && acceptParcel)
        {
            int points = col.gameObject.GetComponent<Parcel>().points;
            truck.RemovePoints(points);
            
            listParcels.Remove(col.gameObject);
            
        }
    }
}
