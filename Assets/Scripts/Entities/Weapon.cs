using UnityEngine;

public class Weapon
{
    private string weaponName;
    private float damage;
    private float bulletSpeed;

    private WeaponType weaponType = WeaponType.AssaultRifle;

    public Weapon(string _weaponName, float _damage, float _bulletSpeed)
    {
        this.weaponName = _weaponName;
        this.damage = _damage;
        this.bulletSpeed = _bulletSpeed;
    }

    public Weapon() { }

    public void Shoot(Bullet _bullet, PlayableObject _player, string _targetTag, float _timeToDie = 5.0f) 
    {
        //Debug.Log($"Shooting from weapon");
        Bullet tempBullet = GameObject.Instantiate(_bullet, _player.transform.position, _player.transform.rotation);
        tempBullet.SetBullet(damage, _targetTag, bulletSpeed);

        GameObject.Destroy(tempBullet.gameObject, 5);//destroy after 5s
    }



    public float GetDamage()
    {
        return damage;
    }

    
}
