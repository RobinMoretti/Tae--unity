using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerController : MonoBehaviour
{
    InputAction moveAction, leftFootUp;
    [SerializeField] Transform hips, footLeft, footRight;
    [SerializeField] FootController footLeftController, footRightController;
    [SerializeField] float moveVelocity, upVelocity;
    Vector3 initialHipsPosition;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        leftFootUp = InputSystem.actions.FindAction("leftFootUp");
        initialHipsPosition = hips.position;
    }

    private void FixedUpdate()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        float leftFootUpValue = leftFootUp.ReadValue<float>();
        
        Vector3 middleFootPos = Vector3.Lerp(footLeft.transform.position, footRight.transform.position, 0.5f);
        hips.position = new Vector3(middleFootPos.x, initialHipsPosition.y, middleFootPos.z);
        
        footLeftController.move(moveValue.x * moveVelocity, leftFootUpValue * upVelocity, moveValue.y * moveVelocity);
        if(leftFootUpValue > 0){
            // footLeft.position += new Vector3(moveValue.x * moveVelocity, leftFootUpValue * upVelocity, moveValue.y * moveVelocity);
        }
        // hips.position = new Vector3(initialHipsPosition.x + (moveValue.x * hipsOffsetScale), initialHipsPosition.y, initialHipsPosition.z + (moveValue.y * hipsOffsetScale));
    }
}
