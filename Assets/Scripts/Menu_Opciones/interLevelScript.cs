using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class interLevelScript : MonoBehaviour {


	void Start(){
		GameObject.FindGameObjectWithTag ("LevelText").GetComponent<Text> ().text = "Level " + Generador.dificultad;

	}

	public void onNextScript(){
		SceneManager.LoadScene ("level");
	}

	void Update(){
		if (Input.anyKeyDown) {

			onNextScript ();
		}

	}
}
