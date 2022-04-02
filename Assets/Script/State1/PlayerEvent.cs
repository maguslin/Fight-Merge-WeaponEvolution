using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class PlayerEvent : MonoBehaviour
{
    public GameObject BotPlayer;

    private void Update()
    {

    }

    public void BranchAttack()
    {
        System.Random r = new System.Random();
        UIManager.instance.damageText.text = "-" + (r.Next(1, 3).ToString());
        UIManager.instance.Bothealth -= (r.Next(1, 3));
        UIManager.instance.slider.value -= (r.Next(1, 3));
        UIManager.instance.barText.text = UIManager.instance.Bothealth.ToString();
    }
    public void BilliardAttack()
    {
        System.Random r = new System.Random();
        UIManager.instance.damageText.text = "-" + (r.Next(3, 6).ToString());
        UIManager.instance.Bothealth -= (r.Next(3, 6));
        UIManager.instance.slider.value -= (r.Next(3, 6));
        UIManager.instance.barText.text = UIManager.instance.Bothealth.ToString();
    }
    public void KnifeAttack()
    {
        System.Random r = new System.Random();
        UIManager.instance.damageText.text = "-" + (r.Next(6, 9).ToString());
        UIManager.instance.Bothealth -= (r.Next(6, 9));
        UIManager.instance.slider.value -= (r.Next(6, 9));
        UIManager.instance.barText.text = UIManager.instance.Bothealth.ToString();
    }
    public void SpearAttack()
    {
        System.Random r = new System.Random();
        UIManager.instance.damageText.text = "-" + (r.Next(9, 12).ToString());
        UIManager.instance.Bothealth -= (r.Next(9, 12));
        UIManager.instance.slider.value -= (r.Next(9, 12));
        UIManager.instance.barText.text = UIManager.instance.Bothealth.ToString();
    }
    public void SwordAttack()
    {
        System.Random r = new System.Random();
        UIManager.instance.damageText.text = "-" + (r.Next(12, 15).ToString());
        UIManager.instance.Bothealth -= (r.Next(12, 15));
        UIManager.instance.slider.value -= (r.Next(12, 15));
        UIManager.instance.barText.text = UIManager.instance.Bothealth.ToString();
    }


    public void BranchEvent()
    {
        Debug.Log("Branch");
        BranchAttack();
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        StartCoroutine(BranchDamage());
        StartCoroutine(DamageTextClear());
        Destroy(Instantiate(Resources.Load("SwordHitRed"), new Vector3(BotPlayer.transform.position.x, BotPlayer.transform.position.y + 0.8f, BotPlayer.transform.position.z), Quaternion.identity), 1f);
        StartCoroutine(Dead());
    }
    public void BilliardEvent()
    {
        Debug.Log("Billiard");
        BilliardAttack();
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        StartCoroutine(BilliardDamage());
        StartCoroutine(DamageTextClear());
        Destroy(Instantiate(Resources.Load("SwordHitRed"), new Vector3(BotPlayer.transform.position.x, BotPlayer.transform.position.y + 0.8f, BotPlayer.transform.position.z), Quaternion.identity), 1f);
        StartCoroutine(Dead());
    }
    public void KnifeEvent()
    {
        Debug.Log("Knife");
        KnifeAttack();
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        StartCoroutine(KnifeDamage());
        StartCoroutine(DamageTextClear());
        Destroy(Instantiate(Resources.Load("SwordHitRed"), new Vector3(BotPlayer.transform.position.x, BotPlayer.transform.position.y + 0.8f, BotPlayer.transform.position.z), Quaternion.identity), 1f);
        StartCoroutine(Dead());
    }
    public void SwordEvent()
    {
        Debug.Log("Sword");
        SwordAttack();
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        StartCoroutine(SwordDamage());
        StartCoroutine(DamageTextClear());
        Destroy(Instantiate(Resources.Load("SwordHitRed"), new Vector3(BotPlayer.transform.position.x, BotPlayer.transform.position.y + 0.8f, BotPlayer.transform.position.z), Quaternion.identity), 1f);
        StartCoroutine(Dead());
    }
    public void SpearEvent()
    {
        Debug.Log("Spear");
        SpearAttack();
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        StartCoroutine(SpearDamage());
        StartCoroutine(DamageTextClear());
        Destroy(Instantiate(Resources.Load("SwordHitRed"), new Vector3(BotPlayer.transform.position.x, BotPlayer.transform.position.y + 0.8f, BotPlayer.transform.position.z), Quaternion.identity), 1f);
        StartCoroutine(Dead());
    }


    IEnumerator DamageTextClear()
    {
        yield return new WaitForSeconds(0.5f);
        UIManager.instance.damageText.text = "";
    }
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.2f);
        if (UIManager.instance.Bothealth <= 0)
        {
            Debug.Log("State 1 Completed");

            UIManager.instance.BotCanvas.SetActive(false);
            UIManager.instance.PlayerCanvas.SetActive(false);
            BotPlayer.GetComponent<Animator>().SetBool("Nakawt", true);
            Destroy(BotPlayer.GetComponent<BotManager>());
            gameObject.GetComponent<Animator>().SetBool("Dance", true);
            StartCoroutine(StateDone());
        }
    }




    //Weapon Damage Animator Controller Bot Player
    IEnumerator BranchDamage()
    {
        BotPlayer.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("BranchDamageControllerBot") as RuntimeAnimatorController;
        BotPlayer.GetComponent<Animator>().SetTrigger("BranchDamage");
        yield return new WaitForSeconds(4f);
        BotPlayer.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator BilliardDamage()
    {
        BotPlayer.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("BilliardDamageControllerBot") as RuntimeAnimatorController;
        BotPlayer.GetComponent<Animator>().SetTrigger("Billiard");
        yield return new WaitForSeconds(4f);
        BotPlayer.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator KnifeDamage()
    {
        BotPlayer.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("KnifeDamageControllerBot") as RuntimeAnimatorController;
        BotPlayer.GetComponent<Animator>().SetTrigger("Knife");
        yield return new WaitForSeconds(4f);
        BotPlayer.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator SwordDamage()
    {
        BotPlayer.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("SwordDamageControllerBot") as RuntimeAnimatorController;
        BotPlayer.GetComponent<Animator>().SetTrigger("Sword");
        yield return new WaitForSeconds(4f);
        BotPlayer.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator SpearDamage()
    {
        BotPlayer.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("SpearDamageControllerBot") as RuntimeAnimatorController;
        BotPlayer.GetComponent<Animator>().SetTrigger("Spear");
        yield return new WaitForSeconds(4f);
        BotPlayer.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }

    IEnumerator StateDone()
    {
        UIManager.instance.barText.text = "0";
        UIManager.instance.StateAnim.GetComponent<Animator>().SetBool("State",true);
        yield return new WaitForSeconds(2f);
        UIManager.instance.State1.SetActive(false);
        UIManager.instance.State2.SetActive(true);
        UIManager.instance.StateAnim.SetActive(false);
    }
}
