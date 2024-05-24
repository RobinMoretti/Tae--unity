using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] Transform target;



    private void LateUpdate() {
        transform.position = target.position;
    }
}
