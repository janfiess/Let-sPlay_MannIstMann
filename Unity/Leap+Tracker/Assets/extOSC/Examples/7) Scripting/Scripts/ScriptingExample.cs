/* Copyright (c) 2018 ExT (V.Sigalkin) */

using UnityEngine;

namespace extOSC.Examples
{
    public class ScriptingExample : MonoBehaviour
    {
        #region Private Vars

        private OSCTransmitter _transmitter;

        private OSCReceiver _receiver;
        public string remoteHost = "192.168.0.61"; 
        public int remotePort = 8002;

        private const string _oscAddress = "/1/fader1";               // Also, you cam use mask in address: /example/*/

        #endregion

        #region Unity Methods

        protected virtual void OnEnable()
        {
            // Creating a transmitter.
            _transmitter = gameObject.AddComponent<OSCTransmitter>();

            // Set remote host address.
            _transmitter.RemoteHost = remoteHost;    

            // Set remote port;
            _transmitter.RemotePort = remotePort;                             


            // Creating a receiver.
            _receiver = gameObject.AddComponent<OSCReceiver>(); 

            // Set local port.
            _receiver.LocalPort = 9002;              

            // Bind "MessageReceived" method to special address.
            _receiver.Bind(_oscAddress, MessageReceived); 
          
        }

        protected virtual void Update()
        {
            if (_transmitter == null) return;

            // Create message
            var message = new OSCMessage(_oscAddress);
            //message.AddValue(OSCValue.String("Hello, world!"));
            message.AddValue(OSCValue.Float(Random.Range(0f, 1f)));

            // Send message
            _transmitter.Send(message);
        }

        #endregion

        #region Protected Methods

        protected void MessageReceived(OSCMessage message)
        {
            string receivedVal = message.Values[0].StringValue;
            print("received string froum Touch");
            if(receivedVal == "warenwelt"){
                print("Warenwelt");

            }
            /* 
             if(receivedVal >= 0.9f){
                 print("high");
             }

             if(receivedVal <= 0.1f){
                 print("low");
             }
             */

        }

        #endregion
    }
}