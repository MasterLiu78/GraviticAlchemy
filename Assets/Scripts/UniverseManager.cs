using UnityEngine;

namespace AGTUniverse
{
    /// <summary>
    /// Handles multiscale camera controls and scene scale switching.
    /// Attach to a scene-level manager object and assign roots.
    /// </summary>
    public class UniverseManager : MonoBehaviour
    {
        private enum ScaleLevel
        {
            Universe = 0,
            Earth = 1
        }

        [Header("Camera")]
        [SerializeField] private Camera targetCamera;
        [SerializeField] private Transform orbitTarget;
        [SerializeField] private float orbitSensitivity = 4f;
        [SerializeField] private float zoomSensitivity = 250f;
        [SerializeField] private float minZoomDistance = 5f;
        [SerializeField] private float maxZoomDistance = 4000f;

        [Header("Scale Roots")]
        [SerializeField] private GameObject universeRoot;
        [SerializeField] private GameObject earthRoot;

        [Header("Scale Switch")]
        [SerializeField] private float earthViewThreshold = 80f;
        [SerializeField] private float universeViewThreshold = 130f;
        [SerializeField] private KeyCode universeScaleKey = KeyCode.Alpha1;
        [SerializeField] private KeyCode earthScaleKey = KeyCode.Alpha2;

        [Header("Earth View Target")]
        [SerializeField] private Transform earthOrbitTarget;
        [SerializeField] private float earthDefaultDistance = 40f;

        private float yaw = 20f;
        private float pitch = 20f;
        private float zoomDistance = 450f;
        private ScaleLevel currentScale = ScaleLevel.Universe;

        private void Awake()
        {
            if (targetCamera == null)
            {
                targetCamera = Camera.main;
            }

            if (orbitTarget == null)
            {
                orbitTarget = transform;
            }

            ApplyScaleVisibility();
            UpdateCameraTransform();
        }

        private void Update()
        {
            HandleOrbitInput();
            HandleZoomInput();
            HandleScaleInput();
            AutoScaleFromZoom();
            UpdateCameraTransform();
        }

        private void HandleOrbitInput()
        {
            if (!Input.GetMouseButton(1))
            {
                return;
            }

            yaw += Input.GetAxis("Mouse X") * orbitSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * orbitSensitivity;
            pitch = Mathf.Clamp(pitch, -80f, 80f);
        }

        private void HandleZoomInput()
        {
            float scroll = Input.mouseScrollDelta.y;
            if (Mathf.Abs(scroll) < Mathf.Epsilon)
            {
                return;
            }

            zoomDistance -= scroll * zoomSensitivity * Time.deltaTime * 60f;
            zoomDistance = Mathf.Clamp(zoomDistance, minZoomDistance, maxZoomDistance);
        }

        private void HandleScaleInput()
        {
            if (Input.GetKeyDown(universeScaleKey))
            {
                SetScale(ScaleLevel.Universe);
            }
            else if (Input.GetKeyDown(earthScaleKey))
            {
                SetScale(ScaleLevel.Earth);
            }
        }

        private void AutoScaleFromZoom()
        {
            if (currentScale == ScaleLevel.Universe && zoomDistance <= earthViewThreshold)
            {
                SetScale(ScaleLevel.Earth);
            }
            else if (currentScale == ScaleLevel.Earth && zoomDistance >= universeViewThreshold)
            {
                SetScale(ScaleLevel.Universe);
            }
        }

        private void SetScale(ScaleLevel level)
        {
            currentScale = level;

            if (level == ScaleLevel.Earth && earthOrbitTarget != null)
            {
                orbitTarget = earthOrbitTarget;
                zoomDistance = Mathf.Min(zoomDistance, earthDefaultDistance);
            }
            else
            {
                orbitTarget = transform;
                zoomDistance = Mathf.Max(zoomDistance, universeViewThreshold + 10f);
            }

            ApplyScaleVisibility();
        }

        private void ApplyScaleVisibility()
        {
            if (universeRoot != null)
            {
                universeRoot.SetActive(currentScale == ScaleLevel.Universe);
            }

            if (earthRoot != null)
            {
                earthRoot.SetActive(currentScale == ScaleLevel.Earth);
            }
        }

        private void UpdateCameraTransform()
        {
            if (targetCamera == null || orbitTarget == null)
            {
                return;
            }

            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
            Vector3 offset = rotation * new Vector3(0f, 0f, -zoomDistance);
            targetCamera.transform.position = orbitTarget.position + offset;
            targetCamera.transform.rotation = rotation;
        }
    }
}