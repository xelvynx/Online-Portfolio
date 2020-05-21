using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipHandler : MonoBehaviour
{

    //#region Testing variables

    //public Button weapon1;
    //public Button weapon2;
    //public Loot loot1;
    //public Loot loot2;
    //#endregion
    //public EquippedGear equippedGear;
    //public void Start()
    //{
    //    weapon1.onClick.AddListener(() => EquipGearAction(loot1) );
    //    weapon2.onClick.AddListener(() => EquipGearAction(loot2) );
    //}
    //public void EquippedGear() 
    //{
    //    if (!equippedGear.weapon.isEquipped) 
    //    {

    //    }
    //}
    //public void EquipGearAction(Loot loot)
    //{
    //    if (loot != null)
    //    {
    //        EquipGear(ItemManager.Instance.equipmentList.GetGear(loot.itemType, loot.itemName));
    //    }
    //}
    //public void EquipGear(Equipment equip)
    //{
    //    switch (equip.gearType)
    //    {
    //        case GearType.Weapon:
    //            if(equippedGear.weapon.name == equip.name) 
    //            {
    //                gameObject.GetComponent<BaseCharacter>().stats.RemoveStats(equippedGear.weapon.stats);
    //                equippedGear.weapon = null;
    //                break;
    //            }
    //            if(equippedGear.weapon != null) 
    //            {
    //                gameObject.GetComponent<BaseCharacter>().stats.RemoveStats(equippedGear.weapon.stats);
    //            }
    //            //equippedGear.weapon = equip;
    //            gameObject.GetComponent<BaseCharacter>().stats.AddStats(equip.stats);
    //            Debug.Log("Weapon");
    //            break;
    //        case GearType.Helmet:
    //            Debug.Log("helmet");
    //            break;
    //        case GearType.Chest:
    //            Debug.Log("Chest");
    //            break;
    //        case GearType.Hand:
    //            Debug.Log("Hand");
    //            break;
    //        case GearType.Foot:
    //            Debug.Log("Foot");
    //            break;

    //        default:
    //            break;
    //    }
    //}

}
