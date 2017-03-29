using UnityEngine;

public class ChargedParticle : MonoBehaviour {

    public float charge = 1;

    private void Start() {
        UpdateColor();
    }
    public void UpdateColor() {
        Color color = charge > 0 ? Color.blue : Color.red;
        GetComponent<Renderer>().material.color = color;
    }
}
