using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using UnityEditor.VersionControl;

public class Mqtt_Client : MonoBehaviour {

	// singleton instance
	private static Mqtt_Client s_instance;
	// unity interface. store instance as singleton reference.
    
	private void Awake()
	{
		if (Mqtt_Client.s_instance == null) {
			Mqtt_Client.s_instance = this;
			DontDestroyOnLoad (this.gameObject);
		} else {
			Destroy (this.gameObject); 
		}
	}

	private void Start () {
		MQTTBehaviour.Connect ();

		if (MQTTBehaviour.Client != null) {	
			MQTTBehaviour.Client.MqttMsgPublishReceived += OnNewMqttMessage;
		}
		// publish
		Invoke("PublishMsg",2);
	}

	void PublishMsg()
	{
		string message = "huhu";
		string subTopic1 = "mySubtopic1";
		MQTTBehaviour.Publish (subTopic1, message);
		print("Message published: " + message);
	}

	// receiving messages
	public static void OnNewMqttMessage(object sender, MqttMsgPublishEventArgs e)
	{ 
		// handle message received 
		string topic = e.Topic.Substring (MQTTBehaviour.TopicPrefix.Length);
		string message = Encoding.UTF8.GetString (e.Message);
		//Debug.Log (e.Message.Length + " bytes recieved on topic " + topic);
		print("Topic: " + topic + " | Message: " + message);

		if (topic.Contains ("mySubtopic1")) {
			print("do something when received topic \"mySubtopic1\"");
		} 

		if (topic.Contains ("mySubtopic2")) {
			print("do something ELSE when received topic \"mySubtopic2\"");
		} 
	}
}