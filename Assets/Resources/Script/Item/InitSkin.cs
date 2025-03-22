using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSkin : Singleton<InitSkin>
{

    public Transform waeponpos;
    public Transform head;
    public Transform shield;
    public SkinnedMeshRenderer pants;
    public int weaponId;
    public GameObject weaponItem;
    //public Transform indicaterHead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ResetSkinAndWeapon()
    {
        foreach (Transform obj in waeponpos)
        {
            Destroy(obj.gameObject);
        }
        foreach (Transform obj in head)
        {
            Destroy(obj.gameObject);
        }
        foreach (Transform obj in shield)
        {
            Destroy(obj.gameObject);
        }
            IntWeapon(1);

            IntPants(1);
    }



    public void RandomSkinBot()
    {
        IntWeapon(Random.Range(0, ItemData.Instance.waepons.Count));
        IntHair(Random.Range(0, ItemData.Instance.hat.Count));
        IntPants(Random.Range(0, ItemData.Instance.pants.Count));
        IntShield(Random.Range(0, ItemData.Instance.shield.Count));
    }


    public void PlayerEquipItems()
    {
        foreach (Transform obj in waeponpos)
        {
            Destroy(obj.gameObject);
        }
        foreach (Transform obj in head)
        {
            Destroy(obj.gameObject);
        }
        foreach (Transform obj in shield)
        {
            Destroy(obj.gameObject);
        }
        int idWeapons = ItemJsonDb.Instance.GetIdItemEquiped("Weapons");
        if (idWeapons > 0)
        {
            IntWeapon(idWeapons-1);
        }
        int idHats = ItemJsonDb.Instance.GetIdItemEquiped("Hat");
        if (idHats > 0)
        {
            IntHair(idHats - 1);
        }
        int idPants = ItemJsonDb.Instance.GetIdItemEquiped("Pants");
        if (idPants > 0)
        {
            IntPants(idPants - 1);
        }
        int idShield = ItemJsonDb.Instance.GetIdItemEquiped("Shield");
        if (idShield > 0)
        {
            IntShield(idShield - 1);
        }
    }

    public void IntWeapon(int id)
    {
        
        weaponId =id;
        GameObject weapon = ItemData.Instance.GetWeapons(id);
        weaponItem = Instantiate(weapon, waeponpos);
    }

    public void IntHair(int id)
    {
        GameObject hair= ItemData.Instance.GetHat(id);
        Instantiate(hair, head);
    }

    public void IntShield(int id)
    {
        GameObject shield = ItemData.Instance.GetShield(id);
        Instantiate(shield, this.shield);
    }

    public void IntPants(int id)
    {
        Material pants=ItemData.Instance.GetPants(id);
        this.pants.material = pants;
    }

}
