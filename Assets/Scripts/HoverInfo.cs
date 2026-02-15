using UnityEngine;

namespace AGTUniverse
{
    /// <summary>
    /// Metadata attached to scene objects that can be inspected by hovering.
    /// </summary>
    public class HoverInfo : MonoBehaviour
    {
        [SerializeField] private string displayName = "Unnamed Object";
        [SerializeField] private string objectType = "Unknown";
        [SerializeField] private string scaleLevel = "Universe";

        public string DisplayName => displayName;
        public string ObjectType => objectType;
        public string ScaleLevel => scaleLevel;

        public string GetSummary()
        {
            return $"Name: {displayName}\nType: {objectType}\nScale: {scaleLevel}";
        }

        public void Configure(string nameValue, string typeValue, string scaleValue)
        {
            displayName = nameValue;
            objectType = typeValue;
            scaleLevel = scaleValue;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (string.IsNullOrWhiteSpace(displayName))
            {
                displayName = gameObject.name;
            }
        }
#endif
    }
}
