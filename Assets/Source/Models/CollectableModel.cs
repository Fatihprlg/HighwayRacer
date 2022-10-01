using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableModel : ObjectModel
{
    public void OnCollected()
    {
        gameObject.SetActive(false);
        ParticlesController.SetParticle(1, transform.position, VibrationTypes.Light);
    }
}
