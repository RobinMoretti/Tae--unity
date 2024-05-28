using Unity.Mathematics;
using UnityEngine;

public class RigidBodyTriggerCameraShaking : MonoBehaviour
{
    [SerializeField] CameraShakingController targetCamera;

    private void OnCollisionEnter(Collision other) {
        // Debug.Log("other.gameObject.name = " + other.gameObject.name );
        
        // if(other.gameObject.name != "FootCollider") return; 

        Vector3 impactVelocity = other.relativeVelocity;

        if(impactVelocity.magnitude > 2){
            targetCamera.shake(math.remap(1, 10, 0, 1, impactVelocity.magnitude));
        }
    }
}
