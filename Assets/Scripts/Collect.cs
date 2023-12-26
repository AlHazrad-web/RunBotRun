using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour {
   
    void Update()  {
        transform.RotateAround(transform.position, Vector3.up, 100 * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other) {
        GetComponent<SphereCollider>().enabled = false;
        UIGame.instance.Collect(gameObject);        
    }

}
