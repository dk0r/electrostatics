using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

    private float cycleInterval = 0.01f;

    private List<ChargedParticle> chargedParticles;
    private List<MovingChargedParticle> movingChargedParticles;

    // Use this for initialization
    void Start() {
        chargedParticles = new List<ChargedParticle>(FindObjectsOfType<ChargedParticle>());
        movingChargedParticles = new List<MovingChargedParticle>(FindObjectsOfType<MovingChargedParticle>());

       foreach(MovingChargedParticle mcp in movingChargedParticles) {
            StartCoroutine(Cycle(mcp));
        }
    }

    public IEnumerator Cycle(MovingChargedParticle mcp) {
        while (true) {
            ApplyMagneticForce(mcp);
            yield return new WaitForSeconds(cycleInterval);
        }
    }

    private void ApplyMagneticForce(MovingChargedParticle mcp) {

        Vector3 newForce = Vector3.zero;

        foreach (ChargedParticle cp in chargedParticles) {
            if (cp == mcp) continue;

            float distance = Vector3.Distance(mcp.transform.position, cp.transform.position);
            float force = 1000 * mcp.charge * cp.charge / Mathf.Pow(distance, 2);

            Vector3 direction = mcp.transform.position - cp.transform.position;
            direction.Normalize();

            newForce += force * direction * cycleInterval;

            if (float.IsNaN(newForce.x))
                newForce = Vector3.zero;

            mcp.rb.AddForce(newForce);

        }
    }


	// Update is called once per frame
	void Update () {
		
	}
}
