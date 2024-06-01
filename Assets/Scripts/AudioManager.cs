using EasyButtons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] groanSOunds;
    public AudioClip[] kicksSounds;
    public AudioClip[] stepsSounds;
    public GameObject fxPrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Button]
    public void createFX(Vector3 position, string type)
    {
        GameObject newSOund = Instantiate(fxPrefab);
        newSOund.transform.position = position;

        if(type == "groan")
        {
            newSOund.GetComponent<AudioSource>().clip = groanSOunds[Random.Range(0, groanSOunds.Length - 1)];
        }
        else if (type == "kick")
        {
            newSOund.GetComponent<AudioSource>().clip = kicksSounds[Random.Range(0, kicksSounds.Length - 1)];
        }
        else if (type == "step")
        {
            newSOund.GetComponent<AudioSource>().clip = stepsSounds[Random.Range(0, stepsSounds.Length - 1)];
        }


        newSOund.SetActive(true);
    }
}
