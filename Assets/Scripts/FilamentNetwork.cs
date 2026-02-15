using System.Collections.Generic;
using UnityEngine;

namespace AGTUniverse
{
    /// <summary>
    /// Procedurally creates a lightweight cosmic web made of nodes and filament lines.
    /// </summary>
    public class FilamentNetwork : MonoBehaviour
    {
        [Header("Generation")]
        [SerializeField] private int nodeCount = 40;
        [SerializeField] private int linksPerNode = 2;
        [SerializeField] private float radius = 450f;
        [SerializeField] private int randomSeed = 1337;

        [Header("Visuals")]
        [SerializeField] private float nodeScale = 5f;
        [SerializeField] private float filamentWidth = 0.6f;
        [SerializeField] private Material filamentMaterial;
        [SerializeField] private Material nodeMaterial;

        private readonly List<GameObject> generatedObjects = new();
        private readonly List<Vector3> nodes = new();

        private void Start()
        {
            Regenerate();
        }

        [ContextMenu("Regenerate Cosmic Web")]
        public void Regenerate()
        {
            ClearGenerated();
            GenerateNodePositions();
            BuildNodeObjects();
            BuildFilamentLinks();
        }

        private void ClearGenerated()
        {
            for (int i = generatedObjects.Count - 1; i >= 0; i--)
            {
                if (generatedObjects[i] != null)
                {
                    Destroy(generatedObjects[i]);
                }
            }

            generatedObjects.Clear();
            nodes.Clear();
        }

        private void GenerateNodePositions()
        {
            Random.InitState(randomSeed);

            for (int i = 0; i < nodeCount; i++)
            {
                Vector3 point = Random.insideUnitSphere * radius;
                nodes.Add(point);
            }
        }

        private void BuildNodeObjects()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                GameObject node = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                node.name = $"CosmicNode_{i:00}";
                node.transform.SetParent(transform, false);
                node.transform.localPosition = nodes[i];
                node.transform.localScale = Vector3.one * nodeScale;

                if (nodeMaterial != null)
                {
                    Renderer renderer = node.GetComponent<Renderer>();
                    renderer.sharedMaterial = nodeMaterial;
                }

                HoverInfo info = node.AddComponent<HoverInfo>();
                SetHoverInfo(info, node.name, "Cosmic Web Node", "Universe");
                generatedObjects.Add(node);
            }
        }

        private void BuildFilamentLinks()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                List<int> nearest = FindNearestNodes(i, linksPerNode);
                foreach (int j in nearest)
                {
                    if (j <= i)
                    {
                        continue;
                    }

                    GameObject filamentObject = new($"Filament_{i:00}_{j:00}");
                    filamentObject.transform.SetParent(transform, false);

                    LineRenderer line = filamentObject.AddComponent<LineRenderer>();
                    line.positionCount = 2;
                    line.useWorldSpace = false;
                    line.SetPosition(0, nodes[i]);
                    line.SetPosition(1, nodes[j]);
                    line.widthMultiplier = filamentWidth;
                    line.material = filamentMaterial;
                    line.numCapVertices = 2;

                    // Needed for hover raycasts.
                    BoxCollider box = filamentObject.AddComponent<BoxCollider>();
                    ConfigureLineCollider(box, nodes[i], nodes[j]);

                    HoverInfo info = filamentObject.AddComponent<HoverInfo>();
                    SetHoverInfo(info, filamentObject.name, "Plasma Filament", "Universe");
                    generatedObjects.Add(filamentObject);
                }
            }
        }

        private List<int> FindNearestNodes(int index, int count)
        {
            List<(float sqrDistance, int idx)> distances = new();
            Vector3 source = nodes[index];

            for (int i = 0; i < nodes.Count; i++)
            {
                if (i == index)
                {
                    continue;
                }

                float sqrDistance = (source - nodes[i]).sqrMagnitude;
                distances.Add((sqrDistance, i));
            }

            distances.Sort((a, b) => a.sqrDistance.CompareTo(b.sqrDistance));

            List<int> nearest = new();
            int limit = Mathf.Min(count, distances.Count);
            for (int i = 0; i < limit; i++)
            {
                nearest.Add(distances[i].idx);
            }

            return nearest;
        }

        private void ConfigureLineCollider(BoxCollider colliderComponent, Vector3 a, Vector3 b)
        {
            Vector3 midpoint = (a + b) * 0.5f;
            Vector3 localDirection = b - a;
            float length = localDirection.magnitude;

            colliderComponent.transform.localPosition = midpoint;
            colliderComponent.transform.localRotation = Quaternion.FromToRotation(Vector3.right, localDirection.normalized);
            colliderComponent.size = new Vector3(length, filamentWidth * 3f, filamentWidth * 3f);
            colliderComponent.center = Vector3.zero;
        }

        private static void SetHoverInfo(HoverInfo info, string objectName, string type, string scale)
        {
            info.Configure(objectName, type, scale);
        }
    }
}