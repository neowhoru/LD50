using System;


using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;

    public GameObject bulletPrefab;
    public AudioClip shootSound;
    public AudioSource audioSource;
    // Start is called before the first frame update

    public bool canShoot = true;
    public SimpleCamShake camshake;

    public Animator shootingflashAnimator;
    private AnimationScript anim;

    public const String FLASHS_SHOOT_ANIM = "ShootingFlash";
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<AnimationScript>();
        camshake = GetComponent<SimpleCamShake>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isShootPressed = Input.GetButton("Fire2") || Input.GetKey(KeyCode.LeftControl);
        if (isShootPressed)
        {
            if (canShoot)
            {
                Shoot();    
            }
            
        }
    }
    
    public void Shoot()
    {
        canShoot = false;
        camshake.ShakeCamera();
        int randomVal = Random.Range (-5, 5);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        if (firePoint.rotation.y == 0)
        {
            bullet.GetComponent<SpriteRenderer>().flipX = true;
        }
        anim.SetTrigger("shoot");
        //bullet.transform.Rotate(Vector3.up * randomVal);
        Invoke(nameof(EnableWeapon), 0.1f);
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);    
        }
    }

    public void EnableWeapon()
    {
        canShoot = true;
    }
}
