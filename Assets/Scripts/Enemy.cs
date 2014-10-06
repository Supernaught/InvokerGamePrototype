using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
	public Transform square, smallsquare;
	public Transform particleDie;
	public int colorCount;

	Color[] colors = new Color[4]; // constant colors. change later to class

	public Queue<Color> combination, defaultCombi; // hp data
	public Queue<Transform> hpColors, defaultHpColors; // used to display hp

	// movement
	float speed = 0.04f;

	void Start () {
		colors[0] = new Color(256f/256f, 60f/256f, 60f/256f, 256f/256f); // red
		colors[1] = new Color(255/256f, 222/256f, 84/256f, 255/256f); // yellow
		colors[2] = new Color(2/256f, 249/256f, 106/256f, 255/256f); // green
		colors[3] = new Color(0/256f, 149/256f, 255/256f, 255/256f); // blue

		colorCount = Random.Range(4,4);

		combination = new Queue<Color> ();
		defaultCombi = new Queue<Color> ();
		hpColors = new Queue<Transform> ();

		for(int i = 0 ; i < colorCount ; i ++){
			Transform go = (Transform) Instantiate(square, this.gameObject.transform.position + new Vector3((0.5f * i) - (0.5f * colorCount/2f) + 0.25f, 0, 0), Quaternion.identity);
			go.parent = this.gameObject.transform;
			go.name = "square" + i;
			Color c = colors[Random.Range(0,4)];
			go.GetComponent<SpriteRenderer>().color = c;

			combination.Enqueue(c);
			defaultCombi.Enqueue(c);
			hpColors.Enqueue(go);
			defaultHpColors = hpColors;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (-speed, 0, 0);
	}

	public void HitColor(int hitCol){
		if (combination.Peek () == colors[hitCol]){
			combination.Dequeue ();
			Transform go = hpColors.Peek();
//			Destroy(go.gameObject);
			Transform small = (Transform) Instantiate(smallsquare, go.transform.position, Quaternion.identity);
			small.name = "smallsquare";
			small.parent = this.gameObject.transform;
		}

		if(combination.Count == 0)
			ResetFullHP();
//			Die ();
	}

	void ResetFullHP(){
		Transform[] ts = this.gameObject.transform.GetComponentsInChildren <Transform>();
		foreach (Transform t in ts) {
			if(t.name == "smallsquare")
				Destroy (t.gameObject);
		}

		combination = defaultCombi;

		hpColors = defaultHpColors;
	}

	void OnCollisionEnter2D(Collision2D col) {
		GameObject.Find ("player").GetComponent<Player> ().Hit ();
		Die ();
	}

	void Die(){
		Destroy (this.gameObject);
		Instantiate (particleDie, transform.position, Quaternion.identity);
	}

}
