using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject currentWeaponPrefab;
    [SerializeField] private InputActionReference attack;

    private IWeapon currentWeapon;

    private void Awake()
    {
        if (currentWeaponPrefab != null)
        {
            currentWeapon = Instantiate(currentWeaponPrefab, transform).GetComponent<IWeapon>();
        }

        currentWeapon.Init(tag);
    }

    private void OnEnable()
    {
        attack.action.Enable();
    }

    void Update()
    {
        if (attack.action.triggered)
        {
            if(currentWeapon != null && currentWeapon.CanFire())
            {
                currentWeapon.Fire();
            }
        }
    }


    private void OnDisable()
    {
        attack.action.Disable();
    }
}
