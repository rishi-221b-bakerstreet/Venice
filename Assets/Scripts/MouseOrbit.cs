using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse Orbit")]
public class MouseOrbit : MonoBehaviour
{
	public Transform target;
	public float distance = 10.0f;

	public float xSpeed = 250.0f;
	public float ySpeed = 120.0f;

	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;

	private float x = 0.0f;
	private float y = 0.0f;

	public  float minFov = 15f;
	public float  maxFov = 90f;
	public float sensitivity = 30f;

	private void Start()
	{
		var angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;

		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}

	void Update () {
	   float fov = Camera.main.fieldOfView;
	   fov = fov - Input.GetAxis("Mouse ScrollWheel") * sensitivity;
	   fov = Mathf.Clamp(fov, minFov, maxFov);
	   Camera.main.fieldOfView = fov;
	 }

	private void LateUpdate()
	{

		if (target && Input.GetMouseButton(0) && !Input.GetKey("left alt"))
		{
			x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
			y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

			y = ClampAngle(y, yMinLimit, yMaxLimit);

			var rotation = Quaternion.Euler(y, x, 0f);
			var position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

			transform.rotation = rotation;
			transform.position = position;
		}


	}

	private static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp(angle, min, max);
	}
}