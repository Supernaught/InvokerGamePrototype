using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int maxhp;
	int hp;

	// Use this for initialization
	void Start () {
		maxhp = 10;
		hp = maxhp;
		UpdateHealthLabel ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Hit(){
		hp--;
		if (hp <= 0) {
			Die();		
		}

		Debug.Log ("hit");
		UpdateHealthLabel ();
	}

	void Die(){
		Debug.Log("gameover");
	}

	void UpdateHealthLabel(){
		if(hp > 0)
			GameObject.Find ("playerhp").GetComponent<GUIText> ().text = "HP " + hp;
		else
			GameObject.Find ("playerhp").GetComponent<GUIText> ().text = "GAME OVER";
	}
}
