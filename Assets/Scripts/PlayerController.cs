using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] int playerID;
    [SerializeField] Transform transforEnemy;

    bool lookAtEnemey;

    void Start()
    {
        // moveAction = InputSystem.actions.FindAction("Move");
        // leftFootUp = InputSystem.actions.FindAction("leftFootUp");
    }

    private void Update()
    {
        if(playerID == 1){
            if(Input.GetKeyDown(KeyCode.A)){
                animator.SetTrigger("Kick");
                lookAtEnemey = false;
            }

            if(Input.GetKeyDown(KeyCode.B)){
                animator.SetTrigger("BackKick");
                lookAtEnemey = false;
            }

            if(Input.GetKeyDown(KeyCode.LeftArrow)){
                animator.SetTrigger("JumpLeft");
            }

            if(Input.GetKeyDown(KeyCode.RightArrow)){
                animator.SetTrigger("JumpRight");
            }

            if(Input.GetKeyDown(KeyCode.UpArrow)){
                animator.SetTrigger("JumpFront");
            }
        }

        if(lookAtEnemey){
            transform.LookAt(transforEnemy);
        }
    }

    /// <summary>
    /// trigger this at the end of any kick
    /// </summary>

    public void enableLookAt(){
        lookAtEnemey = true;
    }

    // private void OnCollisionEnter(Collision other) {
    //     Debug.Log("name = " + othe.name);
        
    // }
}
