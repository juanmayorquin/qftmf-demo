using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;


public class FinishedCinematic : MonoBehaviour
{
    
    [SerializeField] PlayableDirector playableDirector;
    private float currentTime = 0;
    private void Update() {
        
        currentTime += Time.deltaTime;

        if(currentTime >= playableDirector.duration)
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
