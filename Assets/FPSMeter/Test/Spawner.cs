using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace FPSMeterTest {

	public class Spawner : MonoBehaviour {

		public int startCount = 10;
		public Text cubesCount;

		private int totalCount = 0;

		void Awake () {
			SpawnRandomCubes (startCount);
			totalCount += startCount;
			UpdateText ();
		}

		public void SpawnRandomCubes (int count) {
			for (int i = 0; i < count; i++) {
				GameObject go = GameObject.CreatePrimitive (PrimitiveType.Cube);
				go.transform.position = new Vector3 (Random.Range(-5f, 5f), Random.Range (-4f, 4f), 0f);
				go.transform.rotation = Quaternion.identity;
				go.AddComponent<Rotator> ();
			}
			totalCount += count;
			UpdateText ();
		}

		private void UpdateText () {
			cubesCount.text = "Count: " + totalCount.ToString ();
		}

	}

}