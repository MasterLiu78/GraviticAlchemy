using UnityEngine;
using UnityEngine.UI;

namespace AGTUniverse
{
    /// <summary>
    /// Performs hover raycasts and updates a left-side UI text panel.
    /// </summary>
    public class InfoPanelController : MonoBehaviour
    {
        [SerializeField] private Camera worldCamera;
        [SerializeField] private Text infoText;
        [SerializeField] private LayerMask raycastMask = ~0;
        [SerializeField] private float rayDistance = 100000f;

        private const string IdleMessage = "Hover over an object to inspect it.";

        private void Awake()
        {
            if (worldCamera == null)
            {
                worldCamera = Camera.main;
            }

            SetPanelText(IdleMessage);
        }

        private void Update()
        {
            if (worldCamera == null)
            {
                return;
            }

            Ray ray = worldCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, raycastMask))
            {
                HoverInfo info = hit.collider.GetComponentInParent<HoverInfo>();
                if (info != null)
                {
                    SetPanelText(info.GetSummary());
                    return;
                }
            }

            SetPanelText(IdleMessage);
        }

        private void SetPanelText(string value)
        {
            if (infoText != null && infoText.text != value)
            {
                infoText.text = value;
            }
        }
    }
}