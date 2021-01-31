using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float walkSpeed;
	[SerializeField] private float sprintSpeed;

	[SerializeField] private float cameraSensitivity;

	//[SerializeField] private 

	[SerializeField] private Camera headCam;
	[SerializeField] private NPCDialogue dialouge;

	private float interactionCooldown = 1f;
	private float maxInteractionCooldown = 0.25f;

	private float standardFOV;
	private float sprintFOV;
	private float targetFOV;
	private float changeFOVSpeed = 2f;


	public bool IsInTextBox { get; set; } = false;

	private Vector3 cameraPos;

	private Vector3 cameraEulers;

	private float cameraVerticalClamp = 45;

	[SerializeField] private AnimationCurve bounceCurve;
	[SerializeField] private float bounceStrength;
	[SerializeField] private float bounceSpeed;
	private float bounceTimer = 0;

	[SerializeField] private TabletScript tablet;
	private bool isControlling = true;

	void Start() {
		Cursor.lockState = CursorLockMode.Locked;

		cameraPos = headCam.transform.localPosition;

		standardFOV = headCam.fieldOfView;
		sprintFOV = standardFOV + 20;
	}

	void Update() {
		if (!isControlling) {
			if (!tablet.IsOpen) {
				isControlling = true;
				Cursor.lockState = CursorLockMode.Locked;
			} else return;
		}

		if (IsInTextBox) return;

		UpdateMovement();
		UpdateCamera();
		if (interactionCooldown <= 0) Interact();
		else if (!IsInTextBox) interactionCooldown -= Time.deltaTime;
	}

	private void UpdateMovement() {
		Vector3 movementDelta = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

		float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

		targetFOV = Input.GetKey(KeyCode.LeftShift) && movementDelta.magnitude > 0 ? sprintFOV : standardFOV;
		headCam.fieldOfView = Mathf.Lerp(headCam.fieldOfView, targetFOV, Time.deltaTime * changeFOVSpeed);

		bounceTimer += Time.deltaTime * bounceSpeed;
		headCam.transform.localPosition = cameraPos + (Vector3.up * bounceCurve.Evaluate(bounceTimer % 1) * bounceStrength * movementDelta.magnitude);

		
		// Collision Detection
		Vector3 projectedZPosition = transform.position;
		projectedZPosition += transform.forward * movementDelta.z * speed * Time.deltaTime;

		Vector3 positionDeltaZ = projectedZPosition - transform.position;
		if (!Physics.BoxCast(transform.position, Vector3.one * 0.5f, positionDeltaZ.normalized, Quaternion.identity, positionDeltaZ.magnitude, ~LayerMask.NameToLayer("City"))) {
			transform.position = projectedZPosition;
		}

		Vector3 projectedXPosition = transform.position;
		projectedXPosition += transform.right * movementDelta.x * speed * Time.deltaTime;

		Vector3 positionDeltaX = projectedXPosition - transform.position;
		if (!Physics.BoxCast(transform.position, Vector3.one * 0.5f, positionDeltaX.normalized, Quaternion.identity, positionDeltaX.magnitude, ~LayerMask.NameToLayer("City"))) {
			transform.position = projectedXPosition;
		}

	}

	private void UpdateCamera() {

		cameraEulers += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * cameraSensitivity * Time.deltaTime;

		float x = cameraEulers.x % 180;
		if (x < -cameraVerticalClamp) {
			x = -cameraVerticalClamp;
		} else if (x > cameraVerticalClamp) {
			x = cameraVerticalClamp;

			isControlling = false;
			tablet.Open();
			Cursor.lockState = CursorLockMode.None;
		}

		cameraEulers = new Vector3(x, cameraEulers.y, cameraEulers.z);

		transform.localRotation = Quaternion.Euler(0, cameraEulers.y, 0);
		headCam.transform.localRotation = Quaternion.Euler(cameraEulers.x, 0, 0);
	}

	private void Interact() {
		if (Input.GetMouseButtonDown(0)) {
			InteractTalk();
		} else if (Input.GetMouseButtonDown(1)) {
			InteractArrest();
		}
	}

	private void InteractTalk() {
		if (Physics.Raycast(headCam.transform.position, headCam.transform.forward, out RaycastHit hit, 10)) {
			NPCBase npc = hit.collider.GetComponent<NPCBase>();
			if (npc) {
				interactionCooldown = maxInteractionCooldown;

				string forcedLines = npc.OnInteract();
				if (forcedLines != null) dialouge.SetDialogue(forcedLines);
				else dialouge.GetDialogue(npc);

				npc.SetTargetPositionOffPath(transform.position + transform.forward);
			}
		};
	}

	private void InteractArrest() {
		if (Physics.Raycast(headCam.transform.position, headCam.transform.forward, out RaycastHit hit, 10)) {
			NPCBase npc = hit.collider.GetComponent<NPCBase>();
			if (npc) {
				interactionCooldown = maxInteractionCooldown;

				npc.OnArrest();

				npc.SetTargetPositionOffPath(transform.position + transform.forward);
			}
		};
	}

}
