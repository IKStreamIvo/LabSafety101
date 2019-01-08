using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WorldButton : MonoBehaviour {

    public ButtonPressEvent ButtonPressCallback;
	public int eventTag;

	public void Press(){    
		ButtonPressCallback = new ButtonPressEvent();
		ButtonPressCallback.Invoke(eventTag);
	}

	public void Setup(UnityAction callback, int tag){
		
	}
}

[System.Serializable]
public class ButtonPressEvent : UnityEvent<int>{

}
