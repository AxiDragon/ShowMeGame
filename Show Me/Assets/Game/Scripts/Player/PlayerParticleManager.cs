using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleManager : MonoBehaviour
{
    private static PlayerParticleManager instance;
    [SerializeField] private ParticleSystem shootEffect;
    [SerializeField] private ParticleSystem hitEffect;

    private void Awake()
    {
        instance = this;
    }

    public static void ShootEffect(Vector3 pos)
    {
        Instantiate(instance.shootEffect, pos, Quaternion.identity);
    }

    public static void ShootEffect(Transform t)
    {
        Instantiate(instance.shootEffect, t);
    }

    public static void HitEffect(Vector3 pos)
    {
        Instantiate(instance.hitEffect, pos, Quaternion.identity);
    }
    
    public static void HitEffect(Vector3 pos, float pow)
    {
        ParticleSystem p = Instantiate(instance.hitEffect, pos, Quaternion.identity);
        p.transform.localScale = Vector3.one * pow;
    }
}