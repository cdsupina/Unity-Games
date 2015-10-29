using UnityEngine;
using System.Collections;

public class GunValues : MonoBehaviour {

	public string objectName;
	public int gunType;
	public int bullets;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void transferToInventory(){
		if(InventoryManager.currentSlot == 1){
			InventoryManager.gunType1 = gunType;
			InventoryManager.bullets1 = bullets;
		}
		if(InventoryManager.currentSlot == 2){
			InventoryManager.gunType2 = gunType;
			InventoryManager.bullets2 = bullets;
		}

		GameObject.Destroy (this.gameObject);
	}

}
