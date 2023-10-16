using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXHandler : MonoBehaviour
{
    public static VFXHandler Instance { get; private set; }
    public float explosionOffset;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    [SerializeField] private GameObject explosionParticleEffect;

    /// <summary>
    /// This method will spawn an explosion particle effect whenever called.
    /// </summary>
    public void SpawnExplosionVFX(Vector3 effectSpawnPosition)
    {
        Instantiate(explosionParticleEffect, new Vector3(effectSpawnPosition.x, effectSpawnPosition.y+explosionOffset,effectSpawnPosition.z), Quaternion.identity);
    }
}
