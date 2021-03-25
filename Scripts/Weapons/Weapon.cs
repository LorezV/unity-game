using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons.WeaponsData;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform bulletHolder;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private AudioClip shootSound;
        [SerializeField] private AudioClip reloadSound;
        private AudioSource _audioSource;
        private Animator _animator;

        private WeaponsDataAbstract _WeaponData;
        private short _clip;
        private float _timeToFire;

        public bool isReloading = false;
        private float _reloadTime;
        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
            _WeaponData = ScriptableObject.CreateInstance<Thompson>();
            _clip = _WeaponData.clip;
        }

        public short GetClip()
        {
            return _clip;
        }

        public short GetMaxClip()
        {
            return _WeaponData.clip;
        }

        void Update()
        {
            if (Input.GetButton("Fire1")) ShootRequest();
            if (Input.GetButton("Reload")) ReloadRequest();
            AimRequest(Input.GetButton("Aim"));

                 if (Time.time >= _reloadTime) isReloading = false;
        }

        private void AimRequest(bool aim)
        {
            if (isReloading)
            {
                Aim(false);
                return;
            };
            Aim(aim);
        }

        private void Aim(bool aim)
        {
            _animator.SetBool("isAiming", aim);
        }

        private void ReloadRequest()
        {
            if (isReloading) return;
            if (_clip == _WeaponData.clip) return;
            Reload();
        }

        private void Reload()
        {
            isReloading = true;
            _reloadTime = Time.time + reloadSound.length;
            _audioSource.PlayOneShot(reloadSound);
            _clip = _WeaponData.clip;
            _animator.SetTrigger("Reload");
        }

        private void ShootRequest()
        {
            
            if (Time.time < _timeToFire) return;
            if (_clip <= 0)
            {
                ReloadRequest();
                return;
            }
            if (isReloading) return;
            Shoot(); 
        }
    
        private void Shoot()
        {
            _timeToFire = Time.time + (1 / _WeaponData.fireRate);
            GameObject bullet = Instantiate(bulletPrefab, bulletHolder.position, bulletHolder.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * (_WeaponData.bulletPower), ForceMode.Impulse);
            _audioSource.PlayOneShot(shootSound);
            _clip -= 1;
        }
    }
}
