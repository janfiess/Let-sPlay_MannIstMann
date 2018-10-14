/*******************************************************
 * Copyright (C) 2016 Ngan Do - dttngan91@gmail.com
 *******************************************************/
using UnityEngine;
using System.Collections;
using Leap;
using extOSC.Examples;

namespace LeapMotionSimpleControl
{
	
	public class SwipingLeftHand : BehaviorHand
	{



        public string _oscAddress = "/1/fader1";               // Also, you cam use mask in address: /example/*/
		public OscManager oscManager;
		public OscCommandsFromTouchDesigner oscTouchDesigner;

     


		// Use this for initialization
		protected void Awake ()
		{
			base.Awake ();
			CurrentType = GestureManager.GestureTypes.SwipingLeft;
			// add your custom event 
			specificEvent = onSwipeEvent;
		}



		protected override bool checkConditionGesture ()
		{
			Hand hand = GetCurrent1Hand ();
			if (hand != null) {
				if (isOpenFullHand (hand) && isMoveLeft (hand)) {
					return true;
				}
			}
			return false;
		}

		void onSwipeEvent(){
			// TODO : your own logic 
			Debug.Log("Logic swipe left oh my god");

			oscTouchDesigner.SwipeLeftMsg();
		}
	}
}