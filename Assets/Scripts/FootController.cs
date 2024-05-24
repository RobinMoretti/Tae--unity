using Unity.Collections;
using UnityEngine;

public class FootController : MonoBehaviour
{
    public bool grounded = true;
    [SerializeField] Rigidbody rigidbody;
    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Ground"){
            // rigidbody.useGravity = false;
            // rigidbody.isKinematic = true;
            grounded = true;
        }
    }

    public void move(float x, float y, float z){
        
        rigidbody.AddForce(new Vector3(x,y,z), ForceMode.Acceleration);
    }




}
