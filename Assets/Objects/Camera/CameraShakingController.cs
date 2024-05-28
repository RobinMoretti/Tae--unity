using System.Collections;
using UnityEngine;
using Cinemachine;
using EasyButtons;
using Unity.Mathematics;

public class CameraShakingController : MonoBehaviour
{
    Vector2 initialShakingFourchette;

    float shakeValue;
    CinemachineVirtualCamera cinemachineVirtualCamera;
    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private void Start() {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        initialShakingFourchette = new Vector2(cinemachineBasicMultiChannelPerlin.m_AmplitudeGain, cinemachineBasicMultiChannelPerlin.m_FrequencyGain);
    }

    float amplitudeGainTarget;
    float frequencyGainTarget;
    bool isShaking = false;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="strength">0 to 1</param>
    [Button]
    public void shake(float strength){

        if(isShaking == false){
            amplitudeGainTarget = math.remap(0,1, 0.3f, 6, strength);
            frequencyGainTarget = math.remap(0,1, 1, 100, strength);
            StartCoroutine(_shake());
        }

        isShaking = true;
    }

    IEnumerator _shake(){
        yield return null;

        float elapsedTime = 0; 
        float waitTime = 0.1f;

        while (elapsedTime < waitTime)
        {
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(initialShakingFourchette.x, amplitudeGainTarget, (elapsedTime / waitTime));
            cinemachineBasicMultiChannelPerlin.m_FrequencyGain = Mathf.Lerp(initialShakingFourchette.y, frequencyGainTarget, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }  

        yield return new WaitForSeconds(0.2f);
        
        elapsedTime = 0; 
        waitTime = 0.1f;

        while (elapsedTime < waitTime)
        {
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(amplitudeGainTarget, initialShakingFourchette.x, (elapsedTime / waitTime));
            cinemachineBasicMultiChannelPerlin.m_FrequencyGain = Mathf.Lerp(frequencyGainTarget, initialShakingFourchette.y, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }  

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0.3f;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 1f;
        isShaking = false;
        // // Make sure we got there
        // transform.position = Gotoposition;
    }


}
