using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC.Examples;

public class OscCommandsFromTouchDesigner : MonoBehaviour {

	public string _oscAddress_rightSwipe = "/1/fader2"; 
	public string _oscAddress_leftSwipe = "/1/fader1";
	
	OscManager oscManager;
	
	// Use this for initialization
	void Awake(){
		oscManager = GetComponent<OscManager>();
	}
	void Start () {
		

		// oscManager._receiver.Bind(_oscAddress_galyOrJeraiah, MessageReceived);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SwipeRightMsg(){
		if (oscManager._transmitter == null) return;

		// Create message
		var message = new extOSC.OSCMessage(_oscAddress_rightSwipe);
		//message.AddValue(OSCValue.String("Hello, world!"));
		message.AddValue(extOSC.OSCValue.Float(Random.Range(0f, 1f)));

		// Send message
		oscManager._transmitter.Send(message);

	}

	public void SwipeLeftMsg(){
		if (oscManager._transmitter == null) return;

		// Create message
		var message = new extOSC.OSCMessage(_oscAddress_leftSwipe);
		//message.AddValue(OSCValue.String("Hello, world!"));
		message.AddValue(extOSC.OSCValue.Float(Random.Range(0f, 1f)));

		// Send message
		oscManager._transmitter.Send(message);

	}
}
