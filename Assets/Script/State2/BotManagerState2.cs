using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManagerState2 : MonoBehaviour
{

    public static BotManagerState2 instance;

    public GameObject[] weapon;
    public GameObject Player;

    private void Awake()
    {
        instance = this;
    }

    public void Attack() 
    {
        StartCoroutine(DoAttack());

    }

    public void BowAttack()
    {
        System.Random r = new System.Random();
        UIManager.instance.PlayerDamageTextst2.text = "-" + (r.Next(1, 2).ToString());
        UIManager.instance.PlayerHealthst2 -= (r.Next(1, 2));
        UIManager.instance.PlayerSliderst2.value -= (r.Next(1, 2));
        UIManager.instance.PlayerBarTextst2.text = UIManager.instance.PlayerHealthst2.ToString();
    }
    public void GunAttack()
    {
        System.Random r = new System.Random();
        UIManager.instance.PlayerDamageTextst2.text = "-" + (r.Next(2, 4).ToString());
        UIManager.instance.PlayerHealthst2 -= (r.Next(2, 4));
        UIManager.instance.PlayerSliderst2.value -= (r.Next(2, 4));
        UIManager.instance.PlayerBarTextst2.text = UIManager.instance.PlayerHealthst2.ToString();
    }
    public void RifleAttack()
    {
        System.Random r = new System.Random();
        UIManager.instance.PlayerDamageTextst2.text = "-" + (r.Next(4, 6).ToString());
        UIManager.instance.PlayerHealthst2 -= (r.Next(4, 6));
        UIManager.instance.PlayerSliderst2.value -= (r.Next(4, 6));
        UIManager.instance.PlayerBarTextst2.text = UIManager.instance.PlayerHealthst2.ToString();
    }
    public void SniperAttack()
    {
        System.Random r = new System.Random();
        UIManager.instance.PlayerDamageTextst2.text = "-" + (r.Next(6, 8).ToString());
        UIManager.instance.PlayerHealthst2 -= (r.Next(6, 8));
        UIManager.instance.PlayerSliderst2.value -= (r.Next(6, 8));
        UIManager.instance.PlayerBarTextst2.text = UIManager.instance.PlayerHealthst2.ToString();
    }
    public void BombAttack()
    {
        System.Random r = new System.Random();
        UIManager.instance.PlayerDamageTextst2.text = "-" + (r.Next(8, 10).ToString());
        UIManager.instance.PlayerHealthst2 -= (r.Next(8, 10));
        UIManager.instance.PlayerSliderst2.value -= (r.Next(8, 10));
        UIManager.instance.PlayerBarTextst2.text = UIManager.instance.PlayerHealthst2.ToString();
    }



    IEnumerator DoAttack() 
    {
        yield return new WaitForSeconds(3f);
        int rndWeapon = Random.Range(0, weapon.Length);

        for (int i = 0; i < weapon.Length; i++)
        {
            if (i != rndWeapon)
            {
                weapon[i].SetActive(false);
            }
            else
            {
                weapon[rndWeapon].SetActive(true);


            }
        }
        if (weapon[0].activeSelf)
        {
            StartCoroutine(Bow());
        }
        if (weapon[1].activeSelf)
        {
            StartCoroutine(Gun());
        }
        if (weapon[2].activeSelf)
        {
            StartCoroutine(Rifle());
        }
        if (weapon[3].activeSelf)
        {
            StartCoroutine(Sniper());
        }
        if (weapon[4].activeSelf)
        {
            StartCoroutine(Bomb());
        }
    }

    public void BowEvent()
    {
        Debug.Log("Bow");
        BowAttack();
        StartCoroutine(BowDamage());
        StartCoroutine(DamageTextClear());
        Destroy(Instantiate(Resources.Load("SwordHitRed"), new Vector3(Player.transform.position.x, Player.transform.position.y + 0.8f, Player.transform.position.z), Quaternion.identity), 1f);
        StartCoroutine(Dead());
    }
    public void GunEvent()
    {
        Debug.Log("Gun");
        GunAttack();
        StartCoroutine(GunDamage());
        StartCoroutine(DamageTextClear());
        Destroy(Instantiate(Resources.Load("SwordHitRed"), new Vector3(Player.transform.position.x, Player.transform.position.y + 0.8f, Player.transform.position.z), Quaternion.identity), 1f);
        StartCoroutine(Dead());
    }
    public void RifleEvent()
    {
        Debug.Log("Rifle");
        RifleAttack();
        StartCoroutine(RifleDamage());
        StartCoroutine(DamageTextClear());
        Destroy(Instantiate(Resources.Load("SwordHitRed"), new Vector3(Player.transform.position.x, Player.transform.position.y + 0.8f, Player.transform.position.z), Quaternion.identity), 1f);
        StartCoroutine(Dead());
    }
    public void SniperEvent()
    {
        Debug.Log("Sniper");
        SniperAttack();
        StartCoroutine(SniperDamage());
        StartCoroutine(DamageTextClear());
        Destroy(Instantiate(Resources.Load("SwordHitRed"), new Vector3(Player.transform.position.x, Player.transform.position.y + 0.8f, Player.transform.position.z), Quaternion.identity), 1f);
        StartCoroutine(Dead());
    }
    public void BombEvent()
    {
        Debug.Log("Greande");
        BombAttack();
        StartCoroutine(BombDamage());
        StartCoroutine(DamageTextClear());
        Destroy(Instantiate(Resources.Load("SwordHitRed"), new Vector3(Player.transform.position.x, Player.transform.position.y + 0.8f, Player.transform.position.z), Quaternion.identity), 1f);
        StartCoroutine(Dead());
    }

    IEnumerator DamageTextClear()
    {
        yield return new WaitForSeconds(0.5f);
        UIManager.instance.PlayerDamageTextst2.text = "";
    }
    IEnumerator Dead()
    {
        
        if (UIManager.instance.PlayerHealth <= 0)
        {
            UIManager.instance.BotCanvas.SetActive(false);
            UIManager.instance.PlayerCanvas.SetActive(false);
            UIManager.instance.gamePanel.SetActive(false);
            UIManager.instance.gameFailPanel.SetActive(true);
            Player.GetComponent<Animator>().SetBool("Nakawt", true);
            gameObject.GetComponent<Animator>().SetBool("Dance", true);
        }
        yield return new WaitForSeconds(0.2f);
    }



    //Bot Player Animation Clip Script
    IEnumerator Bow()
    {
        yield return new WaitForSeconds(0.5f);
        weapon[0].SetActive(true);
        Destroy(Instantiate(Resources.Load("SelectWeapon"), weapon[0].transform.position, Quaternion.identity), 0.25f);
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("BowControllerBot") as RuntimeAnimatorController;
        gameObject.GetComponent<Animator>().SetTrigger("Arrow");
        yield return new WaitForSeconds(4.5f);
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator Gun()
    {
        yield return new WaitForSeconds(0.5f);
        weapon[1].SetActive(true);
        Destroy(Instantiate(Resources.Load("SelectWeapon"), weapon[0].transform.position, Quaternion.identity), 0.25f);
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("GunControllerBot") as RuntimeAnimatorController;
        gameObject.GetComponent<Animator>().SetTrigger("Gun");
        yield return new WaitForSeconds(4f);
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator Rifle()
    {
        yield return new WaitForSeconds(0.5f);
        weapon[2].SetActive(true);
        Destroy(Instantiate(Resources.Load("SelectWeapon"), weapon[0].transform.position, Quaternion.identity), 0.25f);
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("RifleControllerBot") as RuntimeAnimatorController;
        gameObject.GetComponent<Animator>().SetTrigger("Rifle");
        yield return new WaitForSeconds(4f);
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator Sniper()
    {
        yield return new WaitForSeconds(0.5f);
        weapon[3].SetActive(true);
        Destroy(Instantiate(Resources.Load("SelectWeapon"), weapon[0].transform.position, Quaternion.identity), 0.25f);
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("SniperControllerBot") as RuntimeAnimatorController;
        gameObject.GetComponent<Animator>().SetTrigger("Sniper");
        yield return new WaitForSeconds(4f);
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator Bomb()
    {
        yield return new WaitForSeconds(0.5f);
        weapon[4].SetActive(true);
        Destroy(Instantiate(Resources.Load("SelectWeapon"), weapon[0].transform.position, Quaternion.identity), 0.25f);
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("GrenadeControllerBot") as RuntimeAnimatorController;
        gameObject.GetComponent<Animator>().SetTrigger("Grenade");
        yield return new WaitForSeconds(4f);
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }

    //Weapon Damage Animator Controller Player
    IEnumerator BowDamage()
    {
        yield return new WaitForSeconds(0.1f);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("BowDamageControllerP") as RuntimeAnimatorController;
        Player.GetComponent<Animator>().SetTrigger("Arrow");
        yield return new WaitForSeconds(4f);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator GunDamage()
    {
        yield return new WaitForSeconds(0.1f);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("GunDamageControllerP") as RuntimeAnimatorController;
        Player.GetComponent<Animator>().SetTrigger("Gun");
        yield return new WaitForSeconds(4f);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator RifleDamage()
    {
        yield return new WaitForSeconds(0.1f);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("RifleDamageControllerP") as RuntimeAnimatorController;
        Player.GetComponent<Animator>().SetTrigger("Rifle");
        yield return new WaitForSeconds(4f);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator SniperDamage()
    {
        yield return new WaitForSeconds(0.1f);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("SniperDamageControllerP") as RuntimeAnimatorController;
        Player.GetComponent<Animator>().SetTrigger("Sniper");
        yield return new WaitForSeconds(4f);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator BombDamage()
    {
        yield return new WaitForSeconds(0.1f);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("GrenadeDamageControllerP") as RuntimeAnimatorController;
        Player.GetComponent<Animator>().SetTrigger("Bomb");
        yield return new WaitForSeconds(4f);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
}
