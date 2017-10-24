using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSpectrum : MonoBehaviour {

    private GameObject gameObject;
    private AudioSource audio;

    public int resolution = 1024;
    private GameObject Low, Mid, High;
    public float lowFreqThreshold = 14700, midFreqThreshold = 29400, highFreqThreshold = 44100;
    public float lowEnhance = 1f, midEnhance = 10f, highEnhance = 100f;

	// Use this for initialization
	void Start () {
        gameObject = GameObject.Find("Audio Source");
        audio = gameObject.GetComponent<AudioSource>();

        Low = GameObject.Find("Low");
        Mid = GameObject.Find("Mid");
        High = GameObject.Find("High");
	}
	
	// Update is called once per frame
	void Update () {
        var spectrum = audio.GetSpectrumData(resolution, 0, FFTWindow.BlackmanHarris);

        var deltaFreq = AudioSettings.outputSampleRate / resolution;
        float low = 0f, mid = 0f, high = 0f;

        for (var i = 0; i < resolution; ++i) {
            var freq = deltaFreq * i;
            if      (freq <= lowFreqThreshold)  low  += spectrum[i];
            else if (freq <= midFreqThreshold)  mid  += spectrum[i];
            else if (freq <= highFreqThreshold) high += spectrum[i];
        }

        low  *= lowEnhance;
        mid  *= midEnhance;
        high *= highEnhance;

        Low.transform.localScale  = new Vector3(Low.transform.localScale.x,  low,  Low.transform.localScale.z);
        Mid.transform.localScale  = new Vector3(Mid.transform.localScale.x,  mid,  Mid.transform.localScale.z);
        High.transform.localScale  = new Vector3(High.transform.localScale.x,  high,  High.transform.localScale.z);
	}
}
