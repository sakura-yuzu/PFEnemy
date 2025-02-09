using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 5.0f;
		public GameObject MainCube;
    private Vector3 lastMousePosition;
		private Vector3 initialPosition;
		private Quaternion initialRotation;

    void Start()
    {
        // 初期位置と初期回転を保存
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            float rotationX = delta.x * rotationSpeed * Time.deltaTime;
            float rotationY = delta.y * rotationSpeed * Time.deltaTime;

						transform.RotateAround(MainCube.transform.position, Vector3.right, -rotationY);
						transform.RotateAround(MainCube.transform.position, Vector3.up, rotationX);

            lastMousePosition = Input.mousePosition;
        }
    }

		public void ResetCamera()
		{
			transform.position = initialPosition;
			transform.rotation = initialRotation;
		}
}