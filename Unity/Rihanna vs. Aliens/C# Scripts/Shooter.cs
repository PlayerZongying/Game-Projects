using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject gun = null;
    [SerializeField] GameObject projectile = null;
    GameObject projectileParent;
    const string PROJECTILE_PARENT_NAME = "Projectiles";

    [SerializeField] AudioClip[] shootSFXs = null;

    AttackerSpawner myLaneSpawner;
    Animator animator;

    private void Start()
    {
        CreatProjectileParent();
        SetLaneSpawner();
        animator = GetComponent<Animator>();
    }

    private void CreatProjectileParent()
    {
        projectileParent = GameObject.Find(PROJECTILE_PARENT_NAME);
        if (!projectileParent)
        {
            projectileParent = new GameObject(PROJECTILE_PARENT_NAME);
        }
    }

    private void Update()
    {
        if (IsAttackerInLane())
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private void SetLaneSpawner()
    {
        AttackerSpawner[] attackerSpawers = FindObjectsOfType<AttackerSpawner>();
        foreach (AttackerSpawner spawner in attackerSpawers)
        {
            bool isCloseEnough = (Mathf.Abs(spawner.transform.position.y - transform.position.y) <= Mathf.Epsilon);
            if (isCloseEnough)
            {
                myLaneSpawner = spawner;
            }

        }
    }

    private bool IsAttackerInLane()
    {
        if(myLaneSpawner.transform.childCount <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Fire()
    {
        GameObject newProjectile = Instantiate(projectile, gun.transform.position, Quaternion.identity);
        newProjectile.transform.parent = projectileParent.transform;

        AudioClip shootSFX = shootSFXs[Random.Range(0, shootSFXs.Length)];

        if (shootSFX)
        {
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position);
        }
    }
}
