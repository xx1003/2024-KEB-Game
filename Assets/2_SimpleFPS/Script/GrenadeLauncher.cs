using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : Weapon
{
    public float launchForce;
    public GameObject grenade;
    
    protected override void Fire()
    {
        muzzleFlash.Play();

        var tempGrenade = Instantiate(grenade, muzzle.position, transform.rotation);
        tempGrenade.GetComponent<Grenade>().Init(dmg);

        tempGrenade.GetComponent<Rigidbody>().AddForce(muzzle.forward * launchForce, ForceMode.Impulse);
    }

    protected override void OnRelease()
    {
        
    }
}
