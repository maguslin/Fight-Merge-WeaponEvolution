using MoreMountains.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{

    public static BotManager instance;

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

    public void BranchAttack()
    {
        System.Random r = new System.Random();
        UIManager.instance.PlayerDamageText.text = "-" + (r.Next(1, 2).ToString());
        UIManager.instance.PlayerHealth -= (r.Next(1, 2));
        UIManager.instance.PlayerSlider.value -= (r.Next(1, 2));
        UIManager.instance.PlayerBarText.text = UIManager.instance.PlayerHealth.ToString();
    }
    public void BilliardAttack()
    {
        System.Random r = new System.Random();
        UIManager.instance.PlayerDamageText.text = "-" + (r.Next(2, 4).ToString());
        UIManager.instance.PlayerHealth -= (r.Next(2, 4));
        UIManager.instance.PlayerSlider.value -= (r.Next(2, 4));
        UIManager.instance.PlayerBarText.text = UIManager.instance.PlayerHealth.ToString();
    }
    public void KnifeAttack()
    {
        System.Random r = new System.Random();
        UIManager.instance.PlayerDamageText.text = "-" + (r.Next(4, 6).ToString());
        UIManager.instance.PlayerHealth -= (r.Next(4, 6));
        UIManager.instance.PlayerSlider.value -= (r.Next(4, 6));
        UIManager.instance.PlayerBarText.text = UIManager.instance.PlayerHealth.ToString();
    }
    public void SpearAttack()
    {
        System.Random r = new System.Random();
        UIManager.instance.PlayerDamageText.text = "-" + (r.Next(6, 8).ToString());
        UIManager.instance.PlayerHealth -= (r.Next(6, 8));
        UIManager.instance.PlayerSlider.value -= (r.Next(6, 8));
        UIManager.instance.PlayerBarText.text = UIManager.instance.PlayerHealth.ToString();
    }
    public void SwordAttack()
    {
        System.Random r = new System.Random();
        UIManager.instance.PlayerDamageText.text = "-" + (r.Next(8, 10).ToString());
        UIManager.instance.PlayerHealth -= (r.Next(8, 10));
        UIManager.instance.PlayerSlider.value -= (r.Next(8, 10));
        UIManager.instance.PlayerBarText.text = UIManager.instance.PlayerHealth.ToString();
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
            StartCoroutine(Branch());
        }
        if (weapon[1].activeSelf)
        {
            StartCoroutine(Billiard());
        }
        if (weapon[2].activeSelf)
        {
            StartCoroutine(Knife());
        }
        if (weapon[3].activeSelf)
        {
            StartCoroutine(Sword());
        }
        if (weapon[4].activeSelf)
        {
            StartCoroutine(Spear());

        }

    }

    public void BranchEvent()
    {
        Debug.Log("Branch");
        BranchAttack();
        MMVibrationManager.Haptic(HapticTypes.LightImpact);
        StartCoroutine(BranchDamage());
        StartCoroutine(DamageTextClear());
        Destroy(Instantiate(Resources.Load("SwordHitRed"), new Vector3(Player.transform.position.x, Player.transform.position.y + 0.8f, Player.transform.position.z), Quaternion.identity), 1f);
        StartCoroutine(Dead());
    }
    public void BilliardEvent()
    {
        Debug.Log("Billiard");
        BilliardAttack();
        MMVibrationManager.Haptic(HapticTypes.LightImpact);
        StartCoroutine(BilliardDamage());
        StartCoroutine(DamageTextClear());
        Destroy(Instantiate(Resources.Load("SwordHitRed"), new Vector3(Player.transform.position.x, Player.transform.position.y + 0.8f, Player.transform.position.z), Quaternion.identity), 1f);
        StartCoroutine(Dead());
    }
    public void KnifeEvent()
    {
        Debug.Log("Knife");
        KnifeAttack();
        MMVibrationManager.Haptic(HapticTypes.LightImpact);
        StartCoroutine(KnifeDamage());
        StartCoroutine(DamageTextClear());
        Destroy(Instantiate(Resources.Load("SwordHitRed"), new Vector3(Player.transform.position.x, Player.transform.position.y + 0.8f, Player.transform.position.z), Quaternion.identity), 1f);
        StartCoroutine(Dead());
    }
    public void SwordEvent()
    {
        Debug.Log("Sword");
        SwordAttack();
        MMVibrationManager.Haptic(HapticTypes.LightImpact);
        StartCoroutine(SwordDamage());
        StartCoroutine(DamageTextClear());
        Destroy(Instantiate(Resources.Load("SwordHitRed"), new Vector3(Player.transform.position.x, Player.transform.position.y + 0.8f, Player.transform.position.z), Quaternion.identity), 1f);
        StartCoroutine(Dead());
    }
    public void SpearEvent()
    {
        Debug.Log("Spear");
        SpearAttack();
        MMVibrationManager.Haptic(HapticTypes.LightImpact);
        StartCoroutine(SpearDamage());
        StartCoroutine(DamageTextClear());
        Destroy(Instantiate(Resources.Load("SwordHitRed"), new Vector3(Player.transform.position.x, Player.transform.position.y + 0.8f, Player.transform.position.z), Quaternion.identity), 1f);
        StartCoroutine(Dead());
    }

    IEnumerator DamageTextClear()
    {
        yield return new WaitForSeconds(0.5f);
        UIManager.instance.PlayerDamageText.text = "";
    }
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.2f);
        if (UIManager.instance.PlayerHealth <= 0)
        {
            UIManager.instance.BotCanvas.SetActive(false);
            UIManager.instance.PlayerCanvas.SetActive(false);
            UIManager.instance.gamePanel.SetActive(false);
            UIManager.instance.gameFailPanel.SetActive(true);
            Player.GetComponent<Animator>().SetBool("Nakawt", true);
            gameObject.GetComponent<Animator>().SetBool("Dance", true);
        }
    }



    //Bot Player Animation Clip Script
    IEnumerator Branch()
    {
        yield return new WaitForSeconds(1.1f);
        weapon[0].SetActive(true);
        Destroy(Instantiate(Resources.Load("SelectWeapon"), weapon[0].transform.position, Quaternion.identity), 0.25f);
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("BranchControllerBot") as RuntimeAnimatorController;
        gameObject.GetComponent<Animator>().SetTrigger("Branch");
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator Billiard()
    {
        yield return new WaitForSeconds(1.1f);
        weapon[1].SetActive(true);
        Destroy(Instantiate(Resources.Load("SelectWeapon"), weapon[1].transform.position, Quaternion.identity), 0.25f);
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("BilliardControllerBot") as RuntimeAnimatorController;
        gameObject.GetComponent<Animator>().SetTrigger("Billiard");
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator Knife()
    {
        yield return new WaitForSeconds(1.1f);
        weapon[2].SetActive(true);
        Destroy(Instantiate(Resources.Load("SelectWeapon"), weapon[2].transform.position, Quaternion.identity), 0.25f);
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("KnifeControllerBot") as RuntimeAnimatorController;
        gameObject.GetComponent<Animator>().SetTrigger("Knife");
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator Sword()
    {
        yield return new WaitForSeconds(1.1f);
        weapon[3].SetActive(true);
        Destroy(Instantiate(Resources.Load("SelectWeapon"), weapon[3].transform.position, Quaternion.identity), 0.25f);
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("SwordControllerBot") as RuntimeAnimatorController;
        gameObject.GetComponent<Animator>().SetTrigger("Sword");
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator Spear()
    {
        yield return new WaitForSeconds(1.25f);
        weapon[4].SetActive(true);
        Destroy(Instantiate(Resources.Load("SelectWeapon"), weapon[4].transform.position, Quaternion.identity), 0.25f);
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("SpearControllerBot") as RuntimeAnimatorController;
        gameObject.GetComponent<Animator>().SetTrigger("Spear");
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }

    //Weapon Damage Animator Controller Player
    IEnumerator BranchDamage()
    {
        yield return new WaitForSeconds(0.1f);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("BranchDamageControllerP") as RuntimeAnimatorController;
        Player.GetComponent<Animator>().SetTrigger("BranchDamage");
        yield return new WaitForSeconds(0.5f);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator BilliardDamage()
    {
        yield return new WaitForSeconds(0.1f);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("BilliardDamageControllerP") as RuntimeAnimatorController;
        Player.GetComponent<Animator>().SetTrigger("Billiard");
        yield return new WaitForSeconds(1f);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator KnifeDamage()
    {
        yield return new WaitForSeconds(0.1f);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("KnifeDamageControllerP") as RuntimeAnimatorController;
        Player.GetComponent<Animator>().SetTrigger("Knife");
        yield return new WaitForSeconds(1f);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator SwordDamage()
    {
        yield return new WaitForSeconds(0.1f);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("SwordDamageControllerP") as RuntimeAnimatorController;
        Player.GetComponent<Animator>().SetTrigger("Sword");
        yield return new WaitForSeconds(1f);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator SpearDamage()
    {
        yield return new WaitForSeconds(0.1f);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("SpearDamageControllerP") as RuntimeAnimatorController;
        Player.GetComponent<Animator>().SetTrigger("Spear");
        yield return new WaitForSeconds(1.1f);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
}
