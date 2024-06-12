using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBullet : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsEnemies;

    [Range(0f,1f)]
    public float bouncies;
    public bool useGravity;

    public int explosionDamage;
    public float explosionRadius;
    public float explosionForce;

    public int maxCollisions;
    public float maxLifeTime;
    public bool explodeOnTouch = true;

    int collsions;
    PhysicMaterial physicMaterial;

    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        if (collsions > maxCollisions) Explode();
        maxLifeTime -= Time.deltaTime;
        if (maxLifeTime <= 0) Explode();
    }

    private void Explode()
    {
        if (explosion != null)
        {
            GameObject instantiatedExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(instantiatedExplosion, 0.4f);
        }

        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRadius, whatIsEnemies);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<HealthManager>().TakeDamage(explosionDamage);

            if (enemies[i].GetComponent<Rigidbody>())
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }

        Invoke("Delay", 0.05f);
    }

    private void Delay()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collsions++;

        if (collision.collider.CompareTag("Enemy") && explodeOnTouch)
        {
            collision.collider.GetComponent<HealthManager>().TakeDamage(explosionDamage);
            Explode();
        }
    }

    private void Setup()
    {
        physicMaterial = new PhysicMaterial();
        physicMaterial.bounciness = bouncies;
        physicMaterial.frictionCombine = PhysicMaterialCombine.Minimum;
        physicMaterial.bounceCombine = PhysicMaterialCombine.Maximum;

        GetComponent<SphereCollider>().material = physicMaterial;

        rb.useGravity = useGravity;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
