using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons.WeaponsData
{
    public abstract class WeaponsDataAbstract : ScriptableObject
    {
        public float fireRate;
        public short clip;
        public float bulletPower;

        public WeaponsDataAbstract()
        {
            fireRate = 1;
            clip = 1;
            bulletPower = 1000;
        }
    }
}
