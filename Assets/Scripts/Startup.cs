using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

public class Startup : MonoBehaviour
{
    public PlayableDirector director;
    // Start is called before the first frame update
    void Awake()
    {
        director.paused += _ =>
        {
            Debug.Log("Director paused");
        };
        director.stopped += _ =>
        {
            Debug.Log("Director stopped");
        };
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("duration" + director.duration);
        Debug.Log("time" + director.time);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(director.state == PlayState.Playing && Math.Abs(director.duration - director.time) < 0.1f)
            {
                director.Stop();
            }
            else
            {
                director.Resume();
            }
        }
    }
}
