using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    public float fireRate;
    public LineRenderer line;

    private float timer = 0f;

    protected override void Fire()
    {
        timer += Time.deltaTime;
        
        if (timer > fireRate)
        {
            muzzleFlash.Play();
            line.enabled = true;
            // 발사
            var randomPos = Random.insideUnitCircle;
            var targetPos = new Vector3(randomPos.x, randomPos.y, 50f);
            
            line.SetPosition(1, targetPos);
            timer -= fireRate;

            var dir = muzzle.rotation * targetPos;  //queternion * vector -> 벡터 값을 쿼터니언만큼 회전?? 내가 보고 있는 방향으로 회전
            
            // 그 레이저 안에 뭐가 부딪히면 트루, 안부딪히면 폴스 반환
            if (Physics.Raycast(muzzle.position, dir, out var hit, 100f, 1 << 7))
            {
               // 1<<7 == LayerMask.NameToLayer("Enemy")
               hit.transform.GetComponent<Enemy>().GetDamage(dmg);
               
            }
        }
    }

    protected override void OnRelease()
    {
        timer = 0f;
        line.enabled = false;
    }
}
