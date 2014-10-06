using UnityEngine;
using System.Collections;

public class Play : MonoBehaviour {
	public Transform prefab_enemy;

	float timer;

	// Use this for initialization
	void Start () {
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		PlayerControls ();
		TouchControls ();
		SpawnRandomEnemies ();
	}

	void SpawnRandomEnemies(){
		if(timer > Random.Range (1f,3f)){
			Transform go = (Transform) Instantiate (prefab_enemy, new Vector3 (12.23f,Random.Range(-3,4),0), Quaternion.identity);
			go.tag = "MovingEnemy";
			timer = 0;
		}
	}

	void TouchControls(){
		if(Input.GetMouseButtonDown(0)){
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

			if (hit != null && hit.collider != null) {
				int i = 0;
				if(hit.collider.name == "control1")
					i = 0;
				else if(hit.collider.name == "control2")
					i = 2;
				else if(hit.collider.name == "control3")
					i = 3;
				else if(hit.collider.name == "control4")
					i = 1;

				HitColor (i);
			}
		}
	}
		
	void PlayerControls(){
		if(Input.GetKeyDown(KeyCode.F)){
			HitColor (0);
		}
		if(Input.GetKeyDown(KeyCode.G)){
			HitColor (2);
		}
		if(Input.GetKeyDown(KeyCode.H)){
			HitColor (3);
		}
		if(Input.GetKeyDown(KeyCode.J)){
			HitColor (1);
		}
	}

	void HitColor(int hitCol){
		Enemy e = GetNearestEnemy ();
		e.HitColor (hitCol);
	}

	Enemy GetNearestEnemy(){
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("MovingEnemy");
		GameObject nearest = enemies[0];

		foreach (GameObject e in enemies) {
			if(e.transform.position.x < nearest.transform.position.x)
				nearest = e;
		}

		return nearest.GetComponent<Enemy>();
	}

	void UpdateColors(){
//		GameObject.Find("square1").gameObject.GetComponent<SpriteRenderer>().color = combination[0];
//		GameObject.Find("square2").gameObject.GetComponent<SpriteRenderer>().color = combination[1];
//		GameObject.Find("square3").gameObject.GetComponent<SpriteRenderer>().color = combination[2];
//		GameObject.Find("square4").gameObject.GetComponent<SpriteRenderer>().color = combination[3];
	}

	void OnGUI(){
		
	}
}
