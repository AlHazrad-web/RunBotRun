using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (!CharacterController.instance.death) {
            CharacterController.instance.Death();
        }       
    }
    void OnTriggerStay(Collider other) {
        if (!CharacterController.instance.death) {
            CharacterController.instance.Death();
        }
    }  

}
