using UnityEngine;
using System.Collections;

namespace FPSMeterTest {

	public class Rotator : MonoBehaviour {

		private Vector3 randomRotation;

		void Awake() {
			randomRotation = Random.rotation.eulerAngles;
		}

		void Update () {
			transform.Rotate (randomRotation * Time.deltaTime);
		}
	}

}
