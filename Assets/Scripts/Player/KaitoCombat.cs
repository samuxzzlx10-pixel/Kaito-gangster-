using UnityEngine;

public sealed class KaitoCombat : MonoBehaviour
{
    [SerializeField] private Camera aimCamera;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField, Min(1)] private int magazineSize = 12;
    [SerializeField, Min(1)] private int damage = 25;
    [SerializeField, Min(0.05f)] private float shotsPerSecond = 3f;
    [SerializeField, Min(1f)] private float range = 60f;
    [SerializeField] private LayerMask hitMask = ~0;

    private bool fireHeld;
    private float nextShotTime;
    public int Ammo { get; private set; }
    public int MagazineSize => magazineSize;
    public System.Action<int, int> AmmoChanged;

    private void Awake()
    {
        if (aimCamera == null) aimCamera = Camera.main;
        Ammo = magazineSize;
        AmmoChanged?.Invoke(Ammo, magazineSize);
    }

    private void Update()
    {
        if (fireHeld && Time.time >= nextShotTime)
            Fire();
    }

    public void SetFireHeld(bool held) => fireHeld = held;

    public void Reload()
    {
        Ammo = magazineSize;
        AmmoChanged?.Invoke(Ammo, magazineSize);
    }

    private void Fire()
    {
        if (Ammo <= 0 || aimCamera == null) return;
        nextShotTime = Time.time + 1f / shotsPerSecond;
        Ammo--;
        AmmoChanged?.Invoke(Ammo, magazineSize);
        if (muzzleFlash != null) muzzleFlash.Play();

        Ray ray = new Ray(aimCamera.transform.position, aimCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, range, hitMask, QueryTriggerInteraction.Ignore))
        {
            // Placeholder target feedback for the prototype; production combat can use an IDamageable interface.
            hit.collider.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}
