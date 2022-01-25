using System;
using UnityEngine;
using UnityEngine.UI;
using Variables;

public class UpgradeSystem : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private GameObject playerReference;

    [SerializeField] private IntReference aliensReference;
    [SerializeField] private Button upgradeButton;

    [Header("Movement Speed")] [SerializeField]
    private int upgrade1GoalMS;

    [SerializeField] private int upgrade2GoalMS;
    [SerializeField] private int upgrade3GoalMS;

    [Header("Customer Movement Speed")] [SerializeField]
    private int upgrade1GoalCMS;

    [SerializeField] private int upgrade2GoalCMS;
    [SerializeField] private int upgrade3GoalCMS;

    private int upgradesAvailable;


    private void Start()
    {
        upgradeButton.interactable = false;
    }

    public void CheckAliensFed()
    {
        if (aliensReference.GetValue() == upgrade1GoalMS)
        {
            Debug.Log($"You have fed {upgrade1GoalMS} aliens now, noice");
            upgradesAvailable++;
        }

        if (aliensReference.GetValue() == upgrade2GoalMS)
        {
            Debug.Log($"You have fed {upgrade2GoalMS} aliens now, noice");
            //Change values to whatever benchmark I guess?
            upgradesAvailable++;
        }

        if (aliensReference.GetValue() == upgrade3GoalMS)
        {
            Debug.Log($"You have fed {upgrade3GoalMS} aliens now, noice");
            //Change values to whatever benchmark I guess?
            upgradesAvailable++;
        }

        CheckButton();
    }

    public void CustomerThing()
    {
    }

    private void CheckButton()
    {
        if (upgradesAvailable > 0)
        {
            upgradeButton.interactable = true;
        }
    }

    public void UpgradeMS()
    {
        Debug.Log("Upgraded something");
        //Adjust movement depenign on how to adjust it i guess
        upgradesAvailable--;

        if (upgradesAvailable == 0)
        {
            upgradeButton.interactable = false;
        }
    }

    public void UpgradeCustomerMS()
    {
    }
}