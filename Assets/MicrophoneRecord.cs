using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneRecord : MonoBehaviour {

    // Use this for initialization
    AudioSource myAudioClip;// = GetComponent<AudioSource>();

    private GUIStyle guiStyle = new GUIStyle(); //create a new variable

    void Start() {
        Application.RequestUserAuthorization(UserAuthorization.Microphone);
       myAudioClip = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
		
	}
    void OnGUI() {

        guiStyle.fontSize = 80;
        GUILayout.Label("Microphone.IsRecording: " + Microphone.IsRecording(null), guiStyle);
        if (GUI.Button(new Rect(10, 80, 300, 300), "Record", guiStyle)) {
            myAudioClip.clip = Microphone.Start(null, true, 5, 44100);

        }
        if (GUI.Button(new Rect(10, 380, 300, 300), "Play", guiStyle)) {
            //SavWav.Save("myfile", myAudioClip);

            myAudioClip.Play();
        }
        if (GUI.Button(new Rect(10, 680, 300, 300), "Stop", guiStyle)) {
            Microphone.End(null);
        }
    }
}
