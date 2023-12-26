using UnityEngine;

public class CameraController : MonoBehaviour {    
    void FixedUpdate() {
        if (CharacterController.instance != null) {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, CharacterController.instance.transform.position, 10 * Time.deltaTime);
        }
    }
}
