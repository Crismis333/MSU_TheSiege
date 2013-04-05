using UnityEngine;
using System.Collections;

public class Location : MonoBehaviour {

    public string name, gains, losses;
    [Multiline]
    public string description;
    public float difficulty_soldier, difficulty_length, difficulty_pits, difficulty_obstacles, difficulty_catapults;
    public Location[] locations;
    private GameObject[] linerenderes;

    void Start()
    {
        GameObject lr;
        linerenderes = new GameObject[locations.Length];
        for(int i = 0; i < locations.Length; i++) {
            lr = new GameObject();
            lr.AddComponent<LineRenderer>();
            SetupLineRenderer(lr.GetComponent<LineRenderer>(),locations[i]);
            linerenderes[i] = lr;
        }
    }

    private void SetupLineRenderer(LineRenderer lr, Location lo)
    {
        Material m = new Material(Shader.Find("Self-Illumin/Diffuse"));
        m.color = Color.white;
        lr.material = m;
        lr.receiveShadows = false;
        lr.SetWidth(0.01F, 0.01F);
        lr.SetVertexCount(2);
        Vector3 nl = this.gameObject.transform.position;
        nl.y += 0.1f;
        lr.SetPosition(0, nl);
        nl = lo.gameObject.transform.position;
        nl.y += 0.1f;
        lr.SetPosition(1, nl);
    }
}
