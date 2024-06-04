using UnityEngine;

public class AbilityShop : MonoBehaviour
{
    private Hero hero;

   public AbilityShop()
    {
        hero = Hero.Instance;
    }

    public void BuyFireBall()
    {
        if (hero != null)
        {
            if (!hero.fireball)
            {
                if (hero._souls >= 20)
                {
                    hero.fireball = true;
                    hero._souls -= 20;
                    // Ability unlocked
                }
                else
                {
                    // Not enough souls
                }
            }
        }
    }

    public void BuyHeal()
    {
        if (hero != null)
        {
            if (!hero.heal)
            {
                if (hero._souls >= 25)
                {
                    hero.heal = true;
                    hero._souls -= 25;
                    // Ability unlocked
                }
                else
                {
                    // Not enough souls
                }
            }
        }
    }

    public void BuyPush()
    {
        if (hero != null)
        {
            if (!hero.push)
            {
                if (hero._souls >= 25)
                {
                    hero.push = true;
                    hero._souls -= 25;
                    // Ability unlocked
                }
                else
                {
                    // Not enough souls
                }
            }
        }
    }
}