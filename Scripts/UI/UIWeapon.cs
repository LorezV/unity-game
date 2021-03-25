using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace UI
{
    public class UIWeapon : MonoBehaviour
    {
        private Weapon _script;
        [SerializeField] private Text curAmmoLabel;
        [SerializeField] private Text maxAmmoLabel;

        void Start()
        {
            _script = GetComponent<Weapon>();
        }

        void Update()
        {
            maxAmmoLabel.text = _script.GetMaxClip().ToString();
            curAmmoLabel.text = _script.GetClip().ToString();
        }
    }
}
