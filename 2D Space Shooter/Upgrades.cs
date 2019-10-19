using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Upgrades : MonoBehaviour
{

    public Text currencyText;

    public int shopCurrency;

    private GameObject enemy;
    public int upgradeCount = 0;

    public int speed = 0;
    public int maxBullets = 0;
    public int damage = 0;
    public int hp = 0;
    public float cdRate = 0f;
    public float fireRate = 0f;
    public float beamDur = 0f;
    public int homingCount = 0;

    public int upgradeCost;

    public GameObject info;
    public GameObject closeScreen;
    public Text confirmationText;
    public Text closeText;
    public bool confirm = false;

    private UpgradeType upgradeType = UpgradeType.dmg;

    public void Awake() 
    {
        PlayerPrefs.SetInt("Currency", 100000);
        PlayerPrefs.SetInt("ShotCountMax", 0);
        PlayerPrefs.SetInt("ShotUpgradeCount", 0);
      
    }

    public void Start()
    {
        
        LoadPrefs();
    }

    public void UltUpgrades(string ut)
    {
        if (info)
        {
            string ct = "";


            UpgradeType u = UpgradeType.hp;

            switch (ut)
            {
                case "PriceBeam":
                    u = UpgradeType.beam;
                    if (PlayerPrefs.GetInt("BeamCount") < 1)
                        ct = "Unlock Ultimate Beam";
                    else ct = "Increase duration by .5";
                    break;
                case "PriceHoming":
                    u = UpgradeType.homing;
                    if (PlayerPrefs.GetInt("HomingCount") < 1)
                        ct = "Unlock Homing Missile";
                    else ct = "Increase number of missiles fired by 1";
                    break;
            }

            closeScreen.SetActive(false);
            info.SetActive(true);
            upgradeType = u;
            upgradeCost = PlayerPrefs.GetInt(ut);
            if (upgradeCost < 100)
                upgradeCost = 1000;
            ConfirmationText("Cost : " + upgradeCost.ToString("0") + "\ntotal upgrades : 4        " + ct);
            if (upgradeCost > 4000)
            {
                CloseTextAll();
                info.SetActive(false);
                CloseScreen(true);
            }
        }
    }
    public void UpgradeStuff(string ut)
    {
        if (info)
        {
            string ct = "";


            UpgradeType u = UpgradeType.hp;

            switch (ut)
            {
                case "PriceHP":
                    u = UpgradeType.hp;
                    ct = "Increases maximum health by 10";
                    ConfirmationText("Cost : " + upgradeCost.ToString("0") + "\ntotal upgrades : 4        " + ct);
                    break;
                case "PriceCDR":
                    u = UpgradeType.cdRate;
                    ct = "Decreases amount of time to recover bullets by .1";
                    ConfirmationText("Cost : " + upgradeCost.ToString("0") + "\ntotal upgrades : 4        " + ct);
                    break;
                case "PriceAmmo":
                    u = UpgradeType.maxAmmo;
                    ct = "Increases maximum bullet count by 1";
                    ConfirmationText("Cost : " + upgradeCost.ToString("0") + "\ntotal upgrades : 4        " + ct);
                    break;
                case "PriceDMG":
                    u = UpgradeType.dmg;
                    ct = "Increases bullet damage by 2";
                    ConfirmationText("Cost : " + upgradeCost.ToString("0") + "\ntotal upgrades : 4        " + ct);
                    break;
                case "PriceFire":
                    u = UpgradeType.atkRate;
                    ct = "Decreases delay for bullets by .1";
                    ConfirmationText("Cost : " + upgradeCost.ToString("0") + "\ntotal upgrades : 4        " + ct);
                    break;
                case "PriceSPD":
                    u = UpgradeType.spd;
                    ct = "Increases Ship Speed by 1";
                    ConfirmationText("Cost : " + upgradeCost.ToString("0") + "\ntotal upgrades : 4        " + ct);
                    break;
            }

            closeScreen.SetActive(false);
            info.SetActive(true);
            upgradeCost = PlayerPrefs.GetInt(ut);
            upgradeType = u;
            if (upgradeCost < 100)
            {
                upgradeCost = 100;
                ConfirmationText("Cost : " + upgradeCost.ToString("0") + "\ntotal upgrades : 4        " + ct);
            }
            if (upgradeCost == 400)
            {
                upgradeCost = 500;
                ConfirmationText("Cost : " + upgradeCost.ToString("0") + "\ntotal upgrades : 4        " + ct);
            }
            else if (upgradeCost > 500)
            {
                CloseScreen(true);
                CloseTextAll();
                info.SetActive(false);                
            }
        }
    }


    public void ConfirmationText(string s) 
    {
        confirmationText.text = s;
    }
    public void Confirmation() 
    {
        confirm = true;
        info.SetActive(false);
        shopCurrency = PlayerPrefs.GetInt("Currency");
        switch (upgradeType)
        {
            case UpgradeType.maxAmmo:
                upgradeCount = PlayerPrefs.GetInt("AmmoCount");
                maxBullets = PlayerPrefs.GetInt("AmmoMax");

                if (shopCurrency >= upgradeCost && upgradeCount < 4)
                {
                    shopCurrency = PlayerPrefs.GetInt("Currency");
                    shopCurrency -= upgradeCost;
                    upgradeCost += 100;
                    currencyText.text = "Cash : " + shopCurrency.ToString("0");

                    upgradeCount = PlayerPrefs.GetInt("AmmoCount");
                    upgradeCount++; // 10, 11, 12, 13, 15
                    maxBullets++;
                    if (upgradeCount != 0 && upgradeCount % 4 == 0)
                    {
                        maxBullets++;
                    }
                    PlayerPrefs.SetInt("PriceAmmo", upgradeCost);
                    PlayerPrefs.SetInt("Currency", shopCurrency);
                    PlayerPrefs.SetInt("AmmoCount", upgradeCount);
                    PlayerPrefs.SetInt("AmmoMax", maxBullets);
                    CloseScreen(true);
                    CloseTextAll();
                    //SetCloseText("yes");
                    Debug.Log("Bought Upgrade");
                }
                else if (PlayerPrefs.GetInt("AmmoCount") == 4)
                {
                    CloseScreen(true);
                    SetCloseText("Maxed Upgrades 4/4 \n Max bullets increased by 5.");
                }
                //info.SetActive(false);
                break;
  ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            case UpgradeType.cdRate:
                upgradeCount = PlayerPrefs.GetInt("CDRCount");
                cdRate = PlayerPrefs.GetFloat("CDR");

                if (shopCurrency >= upgradeCost && upgradeCount < 4)
                {
                    shopCurrency = PlayerPrefs.GetInt("Currency");
                    shopCurrency -= upgradeCost;
                    upgradeCost += 100;
                    currencyText.text = "Cash : " + shopCurrency.ToString("0");

                    upgradeCount = PlayerPrefs.GetInt("CDRCount");
                    upgradeCount++; // 10, 11, 12, 13, 15
                    cdRate += .1f;
                    if (upgradeCount != 0 && upgradeCount % 4 == 0)
                    {
                        cdRate += .1f;
                    }
                    PlayerPrefs.SetInt("PriceCDR", upgradeCost);
                    PlayerPrefs.SetInt("Currency", shopCurrency);
                    PlayerPrefs.SetInt("CDRCount", upgradeCount);
                    PlayerPrefs.SetFloat("CDR", cdRate);
                    CloseScreen(true);
                    CloseTextAll();
                    Debug.Log("Bought Upgrade");
                }
                else if (PlayerPrefs.GetInt("CDRCount") == 4)
                {
                    CloseScreen(true);
                    SetCloseText("Maxed Upgrades 4/4 \n Decreased bullet cooldown by .5");
                }
                break;
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            case UpgradeType.hp:
                upgradeCount = PlayerPrefs.GetInt("HPCount");
                hp = PlayerPrefs.GetInt("MaxHP");

                if (shopCurrency >= upgradeCost && upgradeCount < 4)
                {
                    shopCurrency = PlayerPrefs.GetInt("Currency");
                    shopCurrency -= upgradeCost;
                    upgradeCost += 100;
                    currencyText.text = "Cash : " + shopCurrency.ToString("0");

                    upgradeCount = PlayerPrefs.GetInt("HPCount");
                    upgradeCount++; // 10, 11, 12, 13, 15
                    hp++;
                    if (upgradeCount != 0 && upgradeCount % 4 == 0)
                    {
                        hp++;
                    }
                    PlayerPrefs.SetInt("PriceHP", upgradeCost);
                    PlayerPrefs.SetInt("Currency", shopCurrency);
                    PlayerPrefs.SetInt("HPCount", upgradeCount);
                    PlayerPrefs.SetInt("MaxHP", hp);
                    CloseScreen(true);
                    CloseTextAll();
                    Debug.Log("Bought Upgrade");
                }
                else if (PlayerPrefs.GetInt("HPCount") == 4)
                {
                    CloseScreen(true);
                    SetCloseText("Maxed Upgrades 4/4 \n Increased max hp by 50");
                }
                break;
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            case UpgradeType.dmg:
                upgradeCount = PlayerPrefs.GetInt("DMGCount");
                damage = PlayerPrefs.GetInt("DMG");

                if (shopCurrency >= upgradeCost && upgradeCount < 4)
                {
                    shopCurrency = PlayerPrefs.GetInt("Currency");
                    shopCurrency -= upgradeCost;
                    upgradeCost += 100;
                    currencyText.text = "Cash : " + shopCurrency.ToString("0");

                    upgradeCount = PlayerPrefs.GetInt("DMGCount");
                    upgradeCount++; // 10, 11, 12, 13, 15
                    damage++;
                    if (upgradeCount != 0 && upgradeCount % 4 == 0)
                    {
                        damage++;
                    }
                    PlayerPrefs.SetInt("PriceDMG", upgradeCost);
                    PlayerPrefs.SetInt("Currency", shopCurrency);
                    PlayerPrefs.SetInt("DMGCount", upgradeCount);
                    PlayerPrefs.SetInt("DMG", damage);
                    CloseScreen(true);
                    CloseTextAll();
                    Debug.Log("Bought Upgrade");
                }
                else if (PlayerPrefs.GetInt("DMGCount") == 4)
                {
                    CloseScreen(true);
                    SetCloseText("Maxed Upgrades 4/4 \n Increased damage by 5.");
                }
                break;
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            case UpgradeType.atkRate:
                upgradeCount = PlayerPrefs.GetInt("FireCount");
                fireRate = PlayerPrefs.GetFloat("FireRate");

                if (shopCurrency >= upgradeCost && upgradeCount < 4)
                {
                    shopCurrency = PlayerPrefs.GetInt("Currency");
                    shopCurrency -= upgradeCost;
                    upgradeCost += 100;
                    currencyText.text = "Cash : " + shopCurrency.ToString("0");


                    upgradeCount = PlayerPrefs.GetInt("FireCount");
                    upgradeCount++; // 10, 11, 12, 13, 15
                    fireRate += .1f;
                    if (upgradeCount != 0 && upgradeCount % 4 == 0)
                    {
                        fireRate += .1f;
                    }
                    PlayerPrefs.SetInt("PriceFire", upgradeCost);
                    PlayerPrefs.SetInt("Currency", shopCurrency);
                    PlayerPrefs.SetInt("FireCount", upgradeCount);
                    PlayerPrefs.SetFloat("FireRate", fireRate);
                    CloseScreen(true);
                    CloseTextAll();
                    Debug.Log("Bought Upgrade");
                }
                else if (PlayerPrefs.GetInt("SPDCount") == 4)
                {
                    CloseScreen(true);
                    SetCloseText("Maxed Upgrades 4/4 \n Increased attack speed by 5.");
                }
                break;
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            case UpgradeType.spd:
                upgradeCount = PlayerPrefs.GetInt("SPDCount");
                speed = PlayerPrefs.GetInt("SPD");

                if (shopCurrency >= upgradeCost && upgradeCount < 4)
                {
                    shopCurrency = PlayerPrefs.GetInt("Currency");
                    shopCurrency -= upgradeCost;
                    upgradeCost += 100;
                    currencyText.text = "Cash : " + shopCurrency.ToString("0");

                    upgradeCount = PlayerPrefs.GetInt("SPDCount");
                    upgradeCount++; // 10, 11, 12, 13, 15
                    speed++;
                    if (upgradeCount != 0 && upgradeCount % 4 == 0)
                    {
                        speed++;
                    }
                    PlayerPrefs.SetInt("PriceSPD", upgradeCost);
                    PlayerPrefs.SetInt("Currency", shopCurrency);
                    PlayerPrefs.SetInt("SPDCount", upgradeCount);
                    PlayerPrefs.SetInt("SPD", speed);
                    CloseScreen(true);
                    CloseTextAll();
                    Debug.Log("Bought Upgrade");
                }

                break;
                //////////////////////////////////////////////////
        case UpgradeType.beam:
             upgradeCount = PlayerPrefs.GetInt("BeamCount");
             beamDur = PlayerPrefs.GetFloat("Beam");
             if (shopCurrency >= upgradeCost && upgradeCount == 0)
             {
                 shopCurrency -= upgradeCost;
                 upgradeCost += 1000;
                 currencyText.text = "Cash : " + shopCurrency.ToString("0");

                 upgradeCount = PlayerPrefs.GetInt("BeamCount");
                 upgradeCount++;

                 PlayerPrefs.SetInt("PriceBeam", upgradeCost);
                 PlayerPrefs.SetInt("Currency", shopCurrency);
                 PlayerPrefs.SetInt("BeamCount", upgradeCount);
                 CloseScreen(true);
                 CloseTextAll();
             }

            else if (shopCurrency >= upgradeCost && upgradeCount < 4)
            {
                shopCurrency = PlayerPrefs.GetInt("Currency");
                shopCurrency -= upgradeCost;
                upgradeCost += 1000;
                currencyText.text = "Cash : " + shopCurrency.ToString("0");

                upgradeCount = PlayerPrefs.GetInt("BeamCount");
                upgradeCount++; // 10, 11, 12, 13, 15
                beamDur += .5f;
                if (upgradeCount != 0 && upgradeCount % 4 == 0)
                {
                    beamDur += .5f;
                }
                PlayerPrefs.SetInt("PriceBeam", upgradeCost);
                PlayerPrefs.SetInt("Currency", shopCurrency);
                PlayerPrefs.SetInt("BeamCount", upgradeCount);
                PlayerPrefs.SetFloat("Beam", beamDur);
                CloseScreen(true);
                CloseTextAll();
                Debug.Log("Bought Upgrade");
            }
            break;
        //////////////////////////////////////////////////
        case UpgradeType.homing:
             upgradeCount = PlayerPrefs.GetInt("HomingCount");
             beamDur = PlayerPrefs.GetFloat("Homing");
             if (shopCurrency >= upgradeCost && upgradeCount == 0)
             {
                 shopCurrency -= upgradeCost;
                 upgradeCost += 1000;
                 currencyText.text = "Cash : " + shopCurrency.ToString("0");

                 upgradeCount = PlayerPrefs.GetInt("HomingCount");
                 upgradeCount++;

                 PlayerPrefs.SetInt("PriceHoming", upgradeCost);
                 PlayerPrefs.SetInt("Currency", shopCurrency);
                 PlayerPrefs.SetInt("HomingCount", upgradeCount);
                 CloseScreen(true);
                 CloseTextAll();
             }

            else if (shopCurrency >= upgradeCost && upgradeCount < 4)
            {
                shopCurrency = PlayerPrefs.GetInt("Currency");
                shopCurrency -= upgradeCost;
                upgradeCost += 1000;
                currencyText.text = "Cash : " + shopCurrency.ToString("0");

                upgradeCount = PlayerPrefs.GetInt("HomingCount");
                upgradeCount++; // 10, 11, 12, 13, 15
                homingCount += 1;
                if (upgradeCount != 0 && upgradeCount % 4 == 0)
                {
                    homingCount += 1;
                }
                PlayerPrefs.SetInt("PriceHoming", upgradeCost);
                PlayerPrefs.SetInt("Currency", shopCurrency);
                PlayerPrefs.SetInt("HomingCount", upgradeCount);
                PlayerPrefs.SetInt("Homing", homingCount);
                CloseScreen(true);
                CloseTextAll();
                Debug.Log("Bought Upgrade");
            }
            break;
        }
        
            if (shopCurrency < upgradeCost)
            {
                closeScreen.SetActive(true);
                SetCloseText("Not Enough Cash for upgrade");
                Debug.Log("Not Enough Money");
            }
    }
    public void Cancel(bool b)
    {
        info.SetActive(b);
        closeScreen.SetActive(b);
    }
    public void CloseScreen(bool b) 
    {
        closeScreen.SetActive(b);
    }
    public void SetCloseText(string s) 
    {
        closeText.text = s;
    }
    public void CloseTextAll() 
    {
        switch (upgradeType)
        {
            case UpgradeType.maxAmmo:
                SetCloseText("Max Ammo Upgrade " + PlayerPrefs.GetInt("AmmoCount").ToString("0") + "/4\n" + "Max ammo increased by " + PlayerPrefs.GetInt("AmmoMax").ToString("0"));
                break;
            case UpgradeType.dmg:
                SetCloseText("Damage Upgrade " + PlayerPrefs.GetInt("DMGCount").ToString("0") + "/4\n" +"Damage increased by " + PlayerPrefs.GetInt("DMG").ToString("0"));
                break;
            case UpgradeType.atkRate:
                SetCloseText("FireRate Upgrade " + PlayerPrefs.GetInt("FireCount").ToString("0") + "/4\n"+ "Fire rate increased by " + PlayerPrefs.GetFloat("FireRate").ToString("0.0"));
                break;
            case UpgradeType.spd:
                SetCloseText("Speed Upgrade " + PlayerPrefs.GetInt("SPDCount").ToString("0") + "/4\n" + "Speed increased by " + PlayerPrefs.GetInt("SPD").ToString("0"));
                break;
            case UpgradeType.cdRate:
                SetCloseText("Bullet Cooldown Upgrade " + PlayerPrefs.GetInt("CDRCount").ToString("0") + "/4\n" + "Bullet cooldown reduced by " + PlayerPrefs.GetFloat("CDR").ToString("0.0"));
                break;
            case UpgradeType.hp:
                SetCloseText("Max Health Upgrade " + PlayerPrefs.GetInt("HPCount").ToString("0") + "/4\n" + "Max Health increased by " + PlayerPrefs.GetInt("MaxHP").ToString("0"));
                break;
            case UpgradeType.beam:
                if (upgradeCount == 1)
                    SetCloseText("Unlocked Special Beam!");
                else SetCloseText("Beam Upgrade " + PlayerPrefs.GetInt("BeamCount").ToString("0") + "/4\n" + "Beam Duration increased by " + PlayerPrefs.GetFloat("Beam").ToString("0.0"));
                break;
            case UpgradeType.homing:
                if (upgradeCount == 1)
                    SetCloseText("Unlocked Homing Missiles!");
                else SetCloseText("Homing Missile Upgrade " + PlayerPrefs.GetInt("HomingCount").ToString("0") + "/4\n" + "Homing Missile number increased by " + PlayerPrefs.GetInt("Homing").ToString("0"));
                break;
                

        }
    }
    public void LoadPrefs()
    {
        
        shopCurrency = PlayerPrefs.GetInt("Currency");
        currencyText.text = "Cash : " + shopCurrency.ToString("0");

    }
}

public enum UpgradeType
{
    dmg,
    atkRate,
    hp,
    spd,
    maxAmmo,
    cdRate,
    beam,
    homing
}

//public void UpgradeStuff(string ut)
//    {
//        if (info)
//        {
//            string ct = "";

            
//            UpgradeType u = UpgradeType.hp;

//            switch (ut)
//            {
//                case "PriceHP":
//                    u = UpgradeType.hp;
//                    ct = "Increases maximum health by 10";
//                    ConfirmationText("Cost : " + upgradeCost.ToString("0") + "\ntotal upgrades : 4        " + ct);
//                    break;
//                case "PriceCDR":
//                    u = UpgradeType.cdRate;
//                    ct = "Decreases amount of time to recover bullets by .1";
//                    ConfirmationText("Cost : " + upgradeCost.ToString("0") + "\ntotal upgrades : 4        " + ct);
//                    break;
//                case "PriceAmmo":
//                    u = UpgradeType.maxAmmo;
//                    ct = "Increases maximum bullet count by 1";
//                    ConfirmationText("Cost : " + upgradeCost.ToString("0") + "\ntotal upgrades : 4        " + ct);
//                    break;
//                case "PriceDMG":
//                    u = UpgradeType.dmg;
//                    ct = "Increases bullet damage by 2";
//                    ConfirmationText("Cost : " + upgradeCost.ToString("0") + "\ntotal upgrades : 4        " + ct);
//                    break;
//                case "PriceFire":
//                    u = UpgradeType.atkRate;
//                    ct = "Decreases delay for bullets by .1";
//                    ConfirmationText("Cost : " + upgradeCost.ToString("0") + "\ntotal upgrades : 4        " + ct);
//                    break;
//                case "PriceSPD":
//                    u = UpgradeType.spd;
//                    ct = "Increases Ship Speed by 1";
//                    ConfirmationText("Cost : " + upgradeCost.ToString("0") + "\ntotal upgrades : 4        " + ct);
//                    break;
//            }

//            closeScreen.SetActive(false);
//            info.SetActive(true);
//            upgradeCost = PlayerPrefs.GetInt(ut);
//            if (upgradeCost < 100)
//            {
//                upgradeCost = 100;
//                ConfirmationText("Cost : " + upgradeCost.ToString("0") + "\ntotal upgrades : 4        " + ct);
//            }
//            if (upgradeCost == 400)
//            {
//                upgradeCost = 500;
//                ConfirmationText("Cost : " + upgradeCost.ToString("0") + "\ntotal upgrades : 4        " + ct);
//            }
//            else if (upgradeCost > 500)
//            {
//                CloseTextAll();
//                info.SetActive(false);
//                CloseScreen(true);
//            }
//            upgradeType = u;
            
//        }
//    }