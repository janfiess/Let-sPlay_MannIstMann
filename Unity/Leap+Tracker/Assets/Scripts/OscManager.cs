using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;
using extOSC.Examples;
using TMPro;
using LeapMotionSimpleControl;

public class OscManager : MonoBehaviour {

		// osc

	#region Private Vars

	[HideInInspector] public extOSC.OSCTransmitter _transmitter;

	[HideInInspector] public extOSC.OSCReceiver _receiver;
	public MenuManager menuManager;
	public string remoteHost = "127.0.0.1"; 
	public int remotePort = 8003;
	public int localPort = 9002;

	//receive osc
	public string oscAddress_galyOrJeraiah = "/map_galyOrJeraiah";
	public string oscAddress_slideRight = "/waren_slideRight";
	public string oscAddress_slideLeft = "/waren_slideLeft";
	

	// private const string _oscAddress = "/1/fader1";               // Also, you cam use mask in address: /example/*/

	#endregion


	public GameObject galyJeraiah;
	
	// osc

	protected virtual void Start()
	{
		// Creating a transmitter.
		_transmitter = gameObject.AddComponent<extOSC.OSCTransmitter>();

		// Set remote host address.
		_transmitter.RemoteHost = remoteHost;    

		// Set remote port;
		_transmitter.RemotePort = remotePort;                             


		// Creating a receiver.
		_receiver = gameObject.AddComponent<extOSC.OSCReceiver>(); 

		// Set local port.
		_receiver.LocalPort = localPort;              

		// Bind "MessageReceived" method to special address.
		
		_receiver.Bind(oscAddress_galyOrJeraiah, SwitchGalyAndJeraiah); 
		_receiver.Bind(oscAddress_slideRight, SlideRight); 
		_receiver.Bind(oscAddress_slideLeft, SlideLeft); 
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected void SwitchGalyAndJeraiah(extOSC.OSCMessage message)
	{
		float receivedVal = message.Values[0].FloatValue;
		print("received string froum Touch");
		print(receivedVal);
		if(receivedVal < 0.5f) galyJeraiah.GetComponent<TextMeshPro>().text= "GALY";
		if(receivedVal >= 0.5f) galyJeraiah.GetComponent<TextMeshPro>().text= "JERAIAH";

	}

	protected void SlideRight(extOSC.OSCMessage message)
	{
		float receivedVal = message.Values[0].FloatValue;
		// print(receivedVal);
		if(receivedVal < 0.5f){
			print("slide right via osc");
			menuManager.SwipeLeft();
		}
		
	}

	protected void SlideLeft(extOSC.OSCMessage message)
	{
		float receivedVal = message.Values[0].FloatValue;
		// print(receivedVal);
		if(receivedVal < 0.5f){
			print("slide left via osc");
			menuManager.SwipeRight();
		}

	}   
}
