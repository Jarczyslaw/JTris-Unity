using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;

public class FPSMeter : MonoBehaviour {
	
	public float updateInterval = 1f;
	public Text fpsText;
	
	private float nextUpdate = 0f;
	private int frames = 0;
	private float timeAccu = 0f;

	private StringBuilder stringBuilder = new StringBuilder();

	private Canvas parentCanvas;
	
	void Awake () {
		parentCanvas = GetComponent<Canvas> ();
		if (parentCanvas == null) {
			Debug.LogError ("FPSMeter: to make this script work attach it to canvas object");
			gameObject.SetActive (false);
			return;
		}

		if (fpsText == null) {
			Debug.LogError ("FPSMeter: no text object found");
			gameObject.SetActive (false);
			return;
		}

		Reset ();
		MoveToFront ();
		DisplayData (0f, System.GC.GetTotalMemory (false) / 1048576f);
	}

	void Update () {
		frames++;
		timeAccu += Time.unscaledDeltaTime;
		if (Time.unscaledTime >= nextUpdate) {
			float fps = frames / timeAccu;

			DisplayData (fps, System.GC.GetTotalMemory (false) / 1048576f);

			Reset ();
		}
	}

	private void Reset () {
		timeAccu = 0;
		frames = 0;
		nextUpdate += updateInterval;
	}

	private void DisplayData (float fps, float memory) {
		stringBuilder.Length = 0;
		stringBuilder.AppendFormat("FPS: {0:F1}, ", fps);
		stringBuilder.AppendFormat("GC: {0:F1} MB", System.GC.GetTotalMemory(false) / 1048576f);

		fpsText.text = stringBuilder.ToString();
	}

	private void MoveToFront() {
		Canvas[] canvases = GameObject.FindObjectsOfType (typeof(Canvas)) as Canvas[];
		int maxOrder = 0;
		foreach (Canvas c in canvases) {
			if (c != parentCanvas) {
				maxOrder = Mathf.Max (maxOrder, c.sortingOrder);
			}
		}
		parentCanvas.sortingOrder = maxOrder + 1;
	}
}


