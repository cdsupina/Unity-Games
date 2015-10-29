using UnityEngine;
using System.Collections;

public class DebugGUI : MonoBehaviour {

	void OnGUI(){
		GUI.Box(new Rect(0,0,1920,50),"Magazine: " + InventoryManager.currentClip + " Bullets: " + InventoryManager.currentBullets);
	}
}
