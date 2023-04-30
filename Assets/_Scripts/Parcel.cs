using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parcel : MonoBehaviour
{
    public int points = 5;
    private AudioSource soundParcel;
    void Awake()
    {
        soundParcel = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[collision.contactCount];
        collision.GetContacts(contacts);
        float totalImpulse = 0;
        foreach (ContactPoint2D contact in contacts) {
            totalImpulse += contact.normalImpulse;
        }
        
        if (totalImpulse > 40.0f)
        {
            soundParcel.Play();
        }
        
    }
}
