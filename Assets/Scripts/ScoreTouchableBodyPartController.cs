using Unity.Mathematics;
using UnityEngine;

public class ScoreTouchableBodyPartController : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.name != "FootCollider") return;
        // win point !!

        Debug.Log("print one point !!! = ");
        
    }
}
