using Unity.Mathematics;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ScoreTouchableBodyPartController : MonoBehaviour
{
    [SerializeField] PlayerController playerControllerParent;

    int timer = 0;

    void Update(){
        timer ++;
    }

    private void OnCollisionEnter(Collision other) {
        print(other.gameObject.name);
        
        if(timer < 30) return;

        if(other.gameObject.name == "Head" || other.gameObject.name == "Bust"){ 
            playerControllerParent.addScore();
            timer = 0;
        }

        
    }
}
