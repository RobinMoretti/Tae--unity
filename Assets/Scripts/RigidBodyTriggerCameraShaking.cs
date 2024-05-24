using Unity.Mathematics;
using UnityEngine;

public class RigidBodyTriggerCameraShaking : MonoBehaviour
{
    [SerializeField] CameraShakingController targetCamera;

    private void OnCollisionEnter(Collision other) {
        Vector3 impactVelocity = other.relativeVelocity;

        if(impactVelocity.magnitude > 4){
            targetCamera.shake(math.remap(4, 30, 0, 1, impactVelocity.magnitude));
        }
    }
}
