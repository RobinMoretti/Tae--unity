using Unity.Mathematics;
using UnityEngine;

public class ScoreTouchableBodyPartController : MonoBehaviour
{
    [SerializeField] PlayerController playerControllerParent;

    [SerializeField] int timer = 0;

    private void Start()
    {
        playerControllerParent = GetComponentInParent<PlayerController>();
    }

    void Update(){
        if(timer < 40) timer++;
    }

    private void OnCollisionEnter(Collision other) {
        
        if(timer < 10) return;

        if (other.gameObject.name == "Head" || other.gameObject.name == "Buste"){ 
            playerControllerParent.addScore();
            timer = 0;
        }

        
    }
}
