using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScreen : MonoBehaviour {

	public Button[] buttons;

	private bool menuEnabled;
	private bool menuVisible;

	RectTransform recTrans;
	Vector3 showPos;
	Vector3 hidePos;

	// Use this for initialization
	void Start () {
		menuEnabled = true;
		menuVisible = true;

		recTrans = GetComponent<RectTransform> ();

		showPos = recTrans.position;
		hidePos = showPos + Vector3.down * Screen.height;

	}


	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7)) {
			menuEnabled = !menuEnabled;
		}

		if (menuEnabled) {
			recTrans.position = Vector3.MoveTowards (recTrans.position, showPos, 10f);
			if (Vector3.Distance (recTrans.position, (Vector3.zero)) < 0.01f)
				menuVisible = true;
		}
		if (!menuEnabled) {
			recTrans.position = Vector3.MoveTowards (recTrans.position, hidePos, 10f);
			if (Vector3.Distance (recTrans.position, (Vector3.down * Screen.height)) < 0.01f)
				menuVisible = false;
		}

		Time.timeScale = menuEnabled ? 0f : 1f;
	}


	public void Play()
	{
		menuEnabled = false;
	}

	public void Reset()
	{
		GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterMovement>().Spawn ();
		menuEnabled = false;
	}

	public void Exit()
	{
		Application.Quit();
	}
}
