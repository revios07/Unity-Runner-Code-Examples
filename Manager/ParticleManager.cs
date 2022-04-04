using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private static ParticleManager instance;

    [SerializeField]
    private int _particlePoolCount = 25;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject _particlePickPrefab;
    [SerializeField]
    private GameObject _burnParticlePrefab;
    [SerializeField]
    private GameObject _particleEndGamePrefab;
    [SerializeField]
    private GameObject _particleDeadByTriggerPrefab;

    public enum ParticleType
    {
        PickParticle,
        BurnParticle,
        DeadParticle
    }

    //Pools
    private Queue<ParticleSystem> _pickParticles = new Queue<ParticleSystem>();
    private Queue<ParticleSystem> _burnParticles = new Queue<ParticleSystem>();
    private Queue<ParticleSystem> _deadByTriggerParticles = new Queue<ParticleSystem>();

    private void Awake()
    {
        if (instance)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        CreatePooling(_pickParticles, _particlePickPrefab, _particlePoolCount);
        CreatePooling(_burnParticles, _burnParticlePrefab, _particlePoolCount);
        CreatePooling(_deadByTriggerParticles, _particleDeadByTriggerPrefab, 5);
    }

    public static ParticleManager GetSingleton()
    {
        return instance;
    }

    private void CreatePooling(Queue<ParticleSystem> particlePoolRef, GameObject particlePrefabToQueue, int howMuchCreateThisObject)
    {
        for (int i = 0; i < howMuchCreateThisObject; ++i)
        {
            particlePoolRef.Enqueue(Instantiate(particlePrefabToQueue, Vector3.up * 500f, Quaternion.identity).GetComponent<ParticleSystem>());
        }

        Debug.Log(particlePoolRef.Count + " Particle Created To Pool");
    }

    public ParticleSystem RequestParticle(ParticleType particleType)
    {
        switch (particleType)
        {
            case ParticleType.BurnParticle:
                {
                    //ReLoad To Pool Object
                    ParticleSystem returnParticle = _burnParticles.Dequeue();
                    _burnParticles.Enqueue(returnParticle);

                    return returnParticle;
                }
            case ParticleType.PickParticle:
                {
                    //ReLoad To Pool Object
                    ParticleSystem returnParticle = _pickParticles.Dequeue();
                    _pickParticles.Enqueue(returnParticle);

                    return returnParticle;
                }
            case ParticleType.DeadParticle:
                {
                    ParticleSystem returnParticle = _deadByTriggerParticles.Dequeue();
                    _deadByTriggerParticles.Enqueue(returnParticle);

                    return returnParticle;
                }
            default:
                Debug.LogError("Pool Empty");
                return null;
        }
    }

    public void FastParticleRequestToDeadAtPos(Vector3 particlePos)
    {
        //Particle
        ParticleSystem particle = ParticleManager.instance.RequestParticle(ParticleManager.ParticleType.DeadParticle);
        particle.transform.position = particlePos + Vector3.forward * 0.1f + Vector3.up * 0.75f;
        particle.gameObject.SetActive(true);
        particle.Play();
    }
}
