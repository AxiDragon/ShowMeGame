using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleManager : MonoBehaviour
{
    private static PlayerParticleManager instance;
    [SerializeField] private ParticleSystem shootEffect;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private ParticleSystem poofEffect;
    [SerializeField] private AudioSource explosionSound;

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

    public static void PoofEffect(Vector3 pos)
    {
        Instantiate(instance.poofEffect, pos, Quaternion.identity);
    }

    public static void DeathEffect(Vector3 pos, float size)
    {
        ParticleSystem p = Instantiate(instance.deathEffect, pos, Quaternion.identity);
        p.transform.localScale = Vector3.one * size;
    }

    public static void ExplosionSound(Vector3 pos, float impact)
    {
        AudioSource exp = Instantiate(instance.explosionSound, pos, Quaternion.identity);
        exp.pitch = Mathf.Lerp(2f, 3f, impact / 3f);
        exp.volume = Mathf.Lerp(.5f, 1f, impact / 3f);
        exp.maxDistance = Mathf.Lerp(25f, 250f, impact / 3f);
    }
}
