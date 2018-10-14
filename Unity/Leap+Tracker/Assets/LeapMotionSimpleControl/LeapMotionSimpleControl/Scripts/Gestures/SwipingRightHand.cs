/*******************************************************
 * Copyright (C) 2016 Ngan Do - dttngan91@gmail.com
 *******************************************************/
using UnityEngine;
using System.Collections;
using Leap;
using extOSC.Examples;

namespace LeapMotionSimpleControl
{
	public class SwipingRightHand : BehaviorHand
	{

		public string _oscAddress = "/1/fader2";               // Also, you cam use mask in address: /example/*/
		public OscManager oscManager;
		public OscCommandsFromTouchDesigner oscTouchDesigner;


		// Use this for initialization
		protected void Awake ()
		{
			base.Awake ();
			CurrentType = GestureManager.GestureTypes.SwipingRight;
			specificEvent = onSwipeEvent;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		protected override bool checkConditionGesture ()
		{
			Hand hand = GetCurrent1Hand ();
			if (hand != null) {
				if (isOpenFullHand (hand) && isMoveRight (hand)) {
					return true;
				}
			}
			return false;
		}

		void onSwipeEvent(){
			// TODO : your own logic 
			Debug.Log("Logic swipe right");
			oscTouchDesigner.SwipeRightMsg();
			
		}
	}
}