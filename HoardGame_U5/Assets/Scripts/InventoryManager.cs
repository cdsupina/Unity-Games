using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {
	//stores an integer value according to the currnt weapon slot
	public static int currentSlot = 1;

	//Renderer variable that stores the current gun renderer
	Renderer currentRend;

	//Stores a current gun type integer
	public static int currentGunType;

	//Stores the amount of ammmo in the clip of the equipped gun
	public static int currentClip;

	//Stores the amount of ammo in the bullet stash for the equipped gun
	public static int currentBullets;

	//stores the size of the clip in the equipped gun
	public static int currentClipSize;

	//stores the reload timer of the equippe dgun
	public static float currentReloadTimer;
	

	public static int gunType1 = 0;
	public static int bullets1;
	public static int slot1Clip;


	public static int gunType2 = 0;
	public static int bullets2;
	public static int slot2Clip;


	public GameObject pistolWeapon;
	Renderer pistolRend;
	public static int pistolGunType = 1;
	public static int pistolClipSize = 7;
	public static float pistolReloadTimer = 2.2f;

	public GameObject shotgunWeapon;
	Renderer shotgunRend;
	public static int shotgunGunType = 2;
	public static int shotgunClipSize = 4;
	public static float shotgunReloadTimer = 2.9f;

	public GameObject machinegunWeapon;
	Renderer machinegunRend;
	public static int machinegunGunType = 3;
	public static int machinegunClipSize = 20;
	public static float machinegunReloadTimer = 3.1f;
	public static float machinegunFireTimer = 0.2f;
	public static float machinegunFireTime = 0f;

	public static string targetName = "";
	public GameObject textObject;
	Text targetText;

	public Rigidbody pistolDrop;
	public Rigidbody shotgunDrop;
	public Rigidbody machinegunDrop;

	public int dropForce = 100;
	public float pickUpDistance = 0;

	public GameObject spawnLocation;
	Vector3 spawnPos;

	void Start () {
		targetText = textObject.GetComponent<Text> ();
		pistolRend = pistolWeapon.GetComponent<Renderer> ();
		shotgunRend = shotgunWeapon.GetComponent<Renderer> ();
		machinegunRend = machinegunWeapon.GetComponent<Renderer> ();

		currentSlot = 1;
		targetText.text = "";
	}

	void Update () {


		detectItems ();
		iterateFireTimer ();
		//gun slot input
		if(Input.GetButtonDown("GunSlot1")){
			if(currentSlot == 2){
				bullets2 = currentBullets;
				slot2Clip = currentClip;
			}

			currentSlot = 1;
			currentGunType = gunType1;
			currentBullets = bullets1;
			currentClip = slot1Clip;
			changeGun(currentGunType);
		}

		if(Input.GetButtonDown("GunSlot2")){
			if(currentSlot == 1){
				bullets1 = currentBullets;
				slot1Clip = currentClip;
			}

			currentSlot = 2;
			currentGunType = gunType2;
			currentBullets = bullets2;
			currentClip = slot2Clip;
			changeGun(currentGunType);
		}
	}

	void dropGun(){

		if(currentGunType != 0){
			spawnPos = spawnLocation.transform.position;
			Shooter.isReloading = false;
			GunValues dropValues;

			if(currentGunType == pistolGunType){
				Rigidbody drop = Instantiate(pistolDrop,spawnPos,transform.rotation) as Rigidbody;
				drop.AddForce (transform.TransformDirection (Vector3.forward) * dropForce);
				dropValues = drop.GetComponent<GunValues> ();
				dropValues.gunType = currentGunType;
				dropValues.bullets = currentBullets + currentClip;
			}
			if(currentGunType == shotgunGunType){
				Rigidbody drop = Instantiate(shotgunDrop,spawnPos,transform.rotation) as Rigidbody;
				drop.AddForce (transform.TransformDirection (Vector3.forward) * dropForce);
				dropValues = drop.GetComponent<GunValues> ();
				dropValues.gunType = currentGunType;
				dropValues.bullets = currentBullets + currentClip;
			}
			if(currentGunType == machinegunGunType){
				Rigidbody drop = Instantiate(machinegunDrop,spawnPos,transform.rotation) as Rigidbody;
				drop.AddForce (transform.TransformDirection (Vector3.forward) * dropForce);
				dropValues = drop.GetComponent<GunValues> ();
				dropValues.gunType = currentGunType;
				dropValues.bullets = currentBullets + currentClip;
			}


			currentGunType = 0;
			currentBullets = 0;
			currentClip = 0;
		}


	}

	//changes values depending on the guntype that is in the slot aligned with the pressed key
	void changeGun(int guntype){

		Shooter.isReloading = false;

		pistolRend.enabled = false;
		shotgunRend.enabled = false;
		machinegunRend.enabled = false;

		if(guntype == pistolGunType){
			currentClipSize = pistolClipSize;
			currentReloadTimer = pistolReloadTimer;
			currentRend = pistolRend;
		}
		if(guntype == shotgunGunType){
			currentClipSize = shotgunClipSize;
			currentReloadTimer = shotgunReloadTimer;
			currentRend = shotgunRend;
		}
		if(guntype == machinegunGunType){
			currentClipSize = machinegunClipSize;
			currentReloadTimer = machinegunReloadTimer;
			currentRend = machinegunRend;
		}

		if(guntype != 0){
			currentRend.enabled = true;
		}
	}
	
	//returns true if the current gun has bullets and the clip has had bullets subtracted from it
	public static bool canReload(){
		if(currentBullets > 0 && currentClip < currentClipSize){
			return true;
		}else{
			return false;
		}
	}

	//reloads bullets into the clip of the equipped bullets
	public static void reloadBullets(){
		while((currentClip < currentClipSize) && (currentBullets > 0)){
			currentClip ++;
			currentBullets--;
		}
	}
	
	//transfers the gun to the appropriate inventory slot
	void pickUpItem(GameObject target){

		if(target.tag == "Ammo"){
			AmmoValues targetValues = target.GetComponent<AmmoValues>();
			if(currentGunType == targetValues.ammoType){
				currentBullets += targetValues.ammoCount;
				Destroy(target.gameObject);
			}
		}

		if(target.tag == "Weapon"){
			dropGun();
			GunValues targetValues = target.GetComponent<GunValues> ();
			targetValues.transferToInventory ();
			if(currentSlot == 1){
				currentGunType = gunType1;
				currentBullets = bullets1;
			}
			if(currentSlot == 2){
				currentGunType = gunType2;
				currentBullets = bullets2;
			}
			
			changeGun (currentGunType);
		}

	}
	
	//Detects items using a spherecast from the camera if "PickUp" button is pressed pickUpItem() is invoked
	void detectItems(){
		targetText.text = "";
		RaycastHit hit;
		Debug.DrawRay (transform.position, transform.forward * 3);
		if(Physics.SphereCast(transform.position, 0.5f, transform.forward * 3, out hit, 1 << 8)){

			if(hit.collider.tag == "Weapon" || hit.collider.tag == "Ammo"){
				GameObject target = hit.collider.gameObject;
				pickUpDistance = Vector3.Distance(transform.position, target.transform.position);
				if(pickUpDistance < 3){
					targetName = target.name;
					Debug.Log (targetName);
					targetText.text = "Pick up " + targetName + " (E)";
					
					if(Input.GetButtonDown("PickUp")){
						pickUpItem(target);	
					}
				}

			}
		}
	}

	void iterateFireTimer(){
		if(currentGunType == machinegunGunType && machinegunFireTime < machinegunFireTimer){
			machinegunFireTime += Time.deltaTime;
		}
	}


}
