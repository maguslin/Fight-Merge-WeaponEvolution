using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class PlayerEventState2 : MonoBehaviour
{
    public GameObject BotPlayer;

    private void Update()
    {

    }

    public void ArrowAttack()
    {
        System.Random r = new System.Random();
        UIManager.instance.damageTextst2.text = "-" + (r.Next(1, 3).ToString());
        UIManager.instance.Bothealthst2 -= (r.Next(1, 3));
        UIManager.instance.sliderst2.value -= (r.Next(1, 3));
        UIManager.instance.barTextst2.text = UIManager.instance.Bothealthst2.ToString();
    }
    public void GunAttack()
    {
        System.Random r = new System.Random();
        UIManager.instance.damageTextst2.text = "-" + (r.Next(3, 6).ToString());
        UIManager.instance.Bothealthst2 -= (r.Next(3, 6));
        UIManager.instance.sliderst2.value -= (r.Next(3, 6));
        UIManager.instance.barTextst2.text = UIManager.instance.Bothealthst2.ToString();
    }
    public void RifleAttack()
    {
        System.Random r = new System.Random();
        UIManager.instance.damageTextst2.text = "-" + (r.Next(6, 9).ToString());
        UIManager.instance.Bothealthst2 -= (r.Next(6, 9));
        UIManager.instance.sliderst2.value -= (r.Next(6, 9));
        UIManager.instance.barTextst2.text = UIManager.instance.Bothealthst2.ToString();
    }
    public void SniperAttack()
    {
        System.Random r = new System.Random();
        UIManager.instance.damageTextst2.text = "-" + (r.Next(9, 12).ToString());
        UIManager.instance.Bothealthst2 -= (r.Next(9, 12));
        UIManager.instance.sliderst2.value -= (r.Next(9, 12));
        UIManager.instance.barTextst2.text = UIManager.instance.Bothealthst2.ToString();
    }
    public void BombAttack()
    {
        System.Random r = new System.Random();
        UIManager.instance.damageTextst2.text = "-" + (r.Next(12, 15).ToString());
        UIManager.instance.Bothealthst2 -= (r.Next(12, 15));
        UIManager.instance.sliderst2.value -= (r.Next(12, 15));
        UIManager.instance.barTextst2.text = UIManager.instance.Bothealthst2.ToString();
    }


    public void ArrowEvent()
    {
        Debug.Log("Bow");
        ArrowAttack();
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        StartCoroutine(ArrowDamage());
        StartCoroutine(DamageTextClear());
        Destroy(Instantiate(Resources.Load("SwordHitRed"), new Vector3(BotPlayer.transform.position.x, BotPlayer.transform.position.y + 0.8f, BotPlayer.transform.position.z), Quaternion.identity), 1f);
        StartCoroutine(Dead());
    }
    public void GunEvent()
    {
        Debug.Log("Billiard");
        GunAttack();
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        StartCoroutine(GunDamage());
        StartCoroutine(DamageTextClear());
        Destroy(Instantiate(Resources.Load("SwordHitRed"), new Vector3(BotPlayer.transform.position.x, BotPlayer.transform.position.y + 0.8f, BotPlayer.transform.position.z), Quaternion.identity), 1f);
        StartCoroutine(Dead());
    }
    public void RifleEvent()
    {
        Debug.Log("Knife");
        RifleAttack();
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        StartCoroutine(RifleDamage());
        StartCoroutine(DamageTextClear());
        Destroy(Instantiate(Resources.Load("SwordHitRed"), new Vector3(BotPlayer.transform.position.x, BotPlayer.transform.position.y + 0.8f, BotPlayer.transform.position.z), Quaternion.identity), 1f);
        StartCoroutine(Dead());
    }
    public void SniperEvent()
    {
        Debug.Log("Sword");
        SniperAttack();
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        StartCoroutine(SniperDamage());
        StartCoroutine(DamageTextClear());
        Destroy(Instantiate(Resources.Load("SwordHitRed"), new Vector3(BotPlayer.transform.position.x, BotPlayer.transform.position.y + 0.8f, BotPlayer.transform.position.z), Quaternion.identity), 1f);
        StartCoroutine(Dead());
    }
    public void BombEvent()
    {
        Debug.Log("Bomb");
        BombAttack();
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        StartCoroutine(BombDamage());
        StartCoroutine(DamageTextClear());
        Destroy(Instantiate(Resources.Load("SwordHitRed"), new Vector3(BotPlayer.transform.position.x, BotPlayer.transform.position.y + 0.8f, BotPlayer.transform.position.z), Quaternion.identity), 1f);
        StartCoroutine(Dead());
    }


    IEnumerator DamageTextClear()
    {
        yield return new WaitForSeconds(0.5f);
        UIManager.instance.damageTextst2.text = "";
    }
    IEnumerator Dead()
    {
        
        if (UIManager.instance.Bothealthst2 <= 0)
        {
            UIManager.instance.BotCanvasst2.SetActive(false);
            UIManager.instance.PlayerCanvasst2.SetActive(false);
            UIManager.instance.gamePanel.SetActive(false);
            UIManager.instance.gameWinPanel.SetActive(true);
            BotPlayer.GetComponent<Animator>().SetBool("Nakawt", true);
            Destroy(BotPlayer.GetComponent<BotManagerState2>());
            gameObject.GetComponent<Animator>().SetBool("Dance", true);
        }
        yield return new WaitForSeconds(0.2f);
    }




    //Weapon Damage Animator Controller Bot Player
    IEnumerator ArrowDamage()
    {
        BotPlayer.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("BowDamageControllerBot") as RuntimeAnimatorController;
        BotPlayer.GetComponent<Animator>().SetTrigger("Arrow");
        yield return new WaitForSeconds(4f);
        BotPlayer.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator GunDamage()
    {
        BotPlayer.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("GunDamageControllerBot") as RuntimeAnimatorController;
        BotPlayer.GetComponent<Animator>().SetTrigger("Gun");
        yield return new WaitForSeconds(4f);
        BotPlayer.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator RifleDamage()
    {
        BotPlayer.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("RifleDamageControllerBot") as RuntimeAnimatorController;
        BotPlayer.GetComponent<Animator>().SetTrigger("Rifle");
        yield return new WaitForSeconds(4f);
        BotPlayer.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator SniperDamage()
    {
        BotPlayer.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("SniperDamageControllerBot") as RuntimeAnimatorController;
        BotPlayer.GetComponent<Animator>().SetTrigger("Sniper");
        yield return new WaitForSeconds(4f);
        BotPlayer.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator BombDamage()
    {
        BotPlayer.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("GrenadeDamageControllerBot") as RuntimeAnimatorController;
        BotPlayer.GetComponent<Animator>().SetTrigger("Bomb");
        yield return new WaitForSeconds(4f);
        BotPlayer.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
}
