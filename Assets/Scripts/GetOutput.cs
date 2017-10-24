using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GetOutput : MonoBehaviour {

    private GameObject gameObject;
    private AudioSource audio;
    private float[] waveData_ = new float[1024];

	// Use this for initialization
	void Start () {
        gameObject = GameObject.Find("Audio Source");
        audio = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        audio.GetOutputData(waveData_, 1);
        var volume = waveData_.Select(x => x*x).Sum() / waveData_.Length;
        transform.localScale = Vector3.one * volume * 5;
	}
}
