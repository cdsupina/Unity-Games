using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {	
	//boolean to tell if the weapon is currently reloading
	public static bool isReloading;

	//reload timers to count down the reload time
	float reloadTimer;
	float reloadTime;
	
	//Rigidbody variables for the shot ammunition
	public Rigidbody pistolBullet;
	public Rigidbody shotgunShot;
	public Rigidbody machinegunShot;

	//variables to determine the starting velocity of the bullets
	public float pistolPower;
	public float shotgunPower;
	public float machinegunPower;
	
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//Inputs
		if(Input.GetButtonDown("Fire1") && InventoryManager.currentClip > 0 && InventoryManager.currentGunType != InventoryManager.machinegunGunType){
			Shoot ();
		}else if(Input.GetButton("Fire1") && InventoryManager.currentClip > 0 && InventoryManager.currentGunType == InventoryManager.machinegunGunType){
			Shoot ();
		}

		if((Input.GetButtonDown ("Reload") || InventoryManager.currentClip <= 0 || isReloading) && InventoryManager.canReload()){
			Reload ();
		}

	}


	//reloads the clip with a delay, according to the gun type
	void Reload(){
		if(!isReloading){
			//if it is the first iteration of the method, reload timers are reset
			reloadTime = 0;
			reloadTimer = InventoryManager.currentReloadTimer;
			isReloading = true;
		}
		//timer is iterated
		reloadTime += Time.deltaTime;
		//gun is reloaded and reloading boolean is set to false
		if(reloadTime >= reloadTimer){
			isReloading = false;
			InventoryManager.reloadBullets();
		}
	}

	//aborts reloading, shoots according to the type of gun currently being held
	void Shoot(){

		isReloading = false;

		if(InventoryManager.currentGunType == 1){
			transform.localPosition = new Vector3(0.387f, -0.084f, 0.958f);
			Rigidbody instance = Instantiate(pistolBullet, transform.position, transform.rotation) as Rigidbody;
			instance.AddForce(transform.TransformDirection (Vector3.forward) * pistolPower);
			InventoryManager.currentClip --;
			
		}

		if(InventoryManager.currentGunType == 2){
			transform.localPosition = new Vector3(0.387f, -0.142f, 2.043f);
			Rigidbody instance1 = Instantiate(shotgunShot,new Vector3(transform.position.x, transform.position.y - .1f, transform.position.z), transform.rotation) as Rigidbody;
			Rigidbody instance2 = Instantiate(shotgunShot,new Vector3 (transform.position.x, transform.position.y + .1f, transform.position.z), transform.rotation) as Rigidbody;
			Rigidbody instance3 = Instantiate(shotgunShot, transform.position, transform.rotation) as Rigidbody;
			Rigidbody instance4 = Instantiate(shotgunShot, transform.position, transform.rotation) as Rigidbody;
			Rigidbody instance5 = Instantiate(shotgunShot, transform.position, transform.rotation) as Rigidbody;
			Rigidbody instance6 = Instantiate(shotgunShot, transform.position, transform.rotation) as Rigidbody;
			instance1.AddForce(transform.TransformDirection(Vector3.forward) * shotgunPower);
			instance2.AddForce(transform.TransformDirection(Vector3.forward) * shotgunPower);
			instance3.AddForce(transform.TransformDirection(Vector3.forward) * shotgunPower);
			instance4.AddForce(transform.TransformDirection(Vector3.forward) * shotgunPower);
			instance5.AddForce(transform.TransformDirection(Vector3.forward) * shotgunPower);
			instance6.AddForce(transform.TransformDirection(Vector3.forward) * shotgunPower);
			InventoryManager.currentClip --;
		}

		if(InventoryManager.currentGunType == 3){
			transform.localPosition = new Vector3(0.387f, -0.224f, 2.235f);
			if(InventoryManager.machinegunFireTime > InventoryManager.machinegunFireTimer){
				Rigidbody instance = Instantiate(machinegunShot, transform.position, transform.rotation) as Rigidbody;
				instance.AddForce(transform.TransformDirection(Vector3.forward) * machinegunPower);
				InventoryManager.machinegunFireTime = 0;
				InventoryManager.currentClip --;
			}
		}
	}







}


