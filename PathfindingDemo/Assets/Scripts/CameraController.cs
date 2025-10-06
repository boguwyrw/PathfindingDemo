using Unity.Cinemachine;
using UnityEngine;

namespace pathfinding.demo
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera cinemachineCamera;
        [SerializeField] private CinemachineRotationComposer rotationComposer;

        private float moveSpeed = 10.0f;
        private float rotationSpeed = 10.0f;
        private float zoomValue = 5.0f;
        private float zoomMax = 100.0f;
        private float zoomMin = 10.0f;
        private float zoomFieldOfView = 50.0f;

        private void Update()
        {
            CameraMovement();
            CameraRotation();
            CameraZoom();
        }

        private void CameraMovement()
        {
            Vector3 inputMoveDirection = new Vector3();

            if (Input.GetKey(KeyCode.W))
            {
                inputMoveDirection.z = +1f;
            }

            if (Input.GetKey(KeyCode.S))
            {
                inputMoveDirection.z = -1f;
            }

            if (Input.GetKey(KeyCode.D))
            {
                inputMoveDirection.x = +1f;
            }

            if (Input.GetKey(KeyCode.A))
            {
                inputMoveDirection.x = -1f;
            }

            Vector3 moveVector = transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x;

            transform.position += moveVector * moveSpeed * Time.deltaTime;
        }

        private void CameraRotation()
        {
            Vector3 rotationVector = new Vector3();

            if (Input.GetKey(KeyCode.Q))
            {
                rotationVector.x = +1f;
            }

            if (Input.GetKey(KeyCode.E))
            {
                rotationVector.x = -1f;
            }

            rotationComposer.TargetOffset += rotationVector * rotationSpeed * Time.deltaTime;
        }

        private void CameraZoom()
        {
            if (Input.mouseScrollDelta.y > 0 && cinemachineCamera.Lens.FieldOfView < zoomMax)
            {
                zoomFieldOfView += zoomValue;
            }
            if (Input.mouseScrollDelta.y < 0 && cinemachineCamera.Lens.FieldOfView > zoomMin)
            {
                zoomFieldOfView -= zoomValue;
            }

            cinemachineCamera.Lens.FieldOfView = zoomFieldOfView;
        }
    }
}