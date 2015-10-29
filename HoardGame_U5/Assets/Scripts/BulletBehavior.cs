using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour {

	// Use this for initialization
	
	//used to count up to the despawn timer in the Despawn() method
	float despawnTime;
	
	//Time until bullet despawns
	public int despawnTimer;

	void Start () {
		despawnTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		Despawn ();
	}


	//Makes the bullet despawn after a set time in the inspector
	void Despawn(){
		
		//time that passes every frame is added to the value of the despawn time variable
		despawnTime += Time.deltaTime;
		
		//destroys the bullet when despawnTime is greater than the despawnTImer
		if(despawnTime >= despawnTimer){
			GameObject.Destroy(this.gameObject);
		}
	}
}
