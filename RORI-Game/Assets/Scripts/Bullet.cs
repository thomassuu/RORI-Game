using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitFX;
    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag != this.tag) // bullets can't destroy each other!!
        {
            GameObject effect = Instantiate(hitFX, this.transform.position, Quaternion.identity);
            Destroy(effect, 0.25f);
            Destroy(this.gameObject);
        }
        
    }
}
