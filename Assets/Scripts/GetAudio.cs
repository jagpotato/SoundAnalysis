using UnityEngine;
using System.Collections;
using System.Linq;

public class GetAudio : MonoBehaviour {

    private GameObject gameObject;
    private AudioSource audio;
    // private float[] waveData_ = new float[1024];

    public int resolution = 1024;
    public Transform lowMeter, midMeter, highMeter;
    public float lowFreqThreshold = 14700, midFreqThreshold = 29400, highFreqThreshold = 44100;
    public float lowEnhance = 1f, midEnhance = 10f, highEnhance = 100f;

	// Use this for initialization
	void Start () {
        gameObject = GameObject.Find("Audio Source");
        audio = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        /*
        audio.GetOutputData(waveData_, 1);
        var volume = waveData_.Select(x => x*x).Sum() / waveData_.Length;
        transform.localScale = Vector3.one * volume * 10;
        */

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

        transform.localScale  = new Vector3(low,  low,  low);
        // transform.localScale  = new Vector3(mid,  mid,  mid);
        // transform.localScale  = new Vector3(high,  high,  high);

	}
}