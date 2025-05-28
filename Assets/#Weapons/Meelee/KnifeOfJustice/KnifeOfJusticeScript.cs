using UnityEngine;

public class KnifeOfJusticeScript : Weapons
{
    public Animator anim;
    bool canUse = true;
    public GameObject Heatbox;
    public TrailRenderer Trail;
    private void Start()
    {
        Heatbox.SetActive(false);
        Trail.emitting = false;
    }
    public override void UseWeapon()
    {
        if (canUse)
        {
            CurrentAmount--;
            Trail.emitting = true;
            Heatbox.SetActive(true);
            canUse = false;
            anim.SetTrigger("Use");
            Invoke("wait",0.5f);
        }
       
    }
    public void wait()
    {
        Trail.emitting = false;
        Amount--;
        Heatbox.SetActive(false);
        canUse = true;
    }
    private void Update()
    {
        if (CurrentAmount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
