
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    [Header("Major stats")]
    public Stat strength;//1 point increase damage by 1 point and crit.power by 1%
    public Stat agillity;//1 point increase evasion by 1% and crit.chance by 1 %
    public Stat intelligence;//1 point increase magic damage by 1% and magic resistance by 1
    public Stat vitality;//1 point increase health by 3 or 5 points

    [Header("Offensive stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower;   //default value = 150


    [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;
    public Stat magicResistance;

    [Header("Magic stats")]
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightingDamage;

    public bool isIgnited;
    public bool isChilled;
    public bool isShocked;




    [SerializeField] private int currentHealth;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHealth = maxHealth.GetValue();

        critPower.SetDefaultValue(150);

    }

    public virtual void DoDamage(CharacterStats _targetstats)
    {
        if (TargetCanAvoidAttack(_targetstats))
        {
            return;
        }
        int totalDamage = damage.GetValue() + strength.GetValue();

        if (CanCrit())
        {
            totalDamage = CalculateCritalDamage(totalDamage);
        }


        totalDamage = CheckTargetArmor(_targetstats, totalDamage);

        DoMagicalDamage(_targetstats);
        //_targetstats.takeDamage(totalDamage);
    }

    public virtual void DoMagicalDamage(CharacterStats _targetstats)
    {
        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightingDamage = lightingDamage.GetValue();

        int totalMagicalDamage = _fireDamage + _iceDamage + _lightingDamage + intelligence.GetValue();
        totalMagicalDamage = CheckTargetResistance(_targetstats, totalMagicalDamage);

        _targetstats.takeDamage(totalMagicalDamage);



    }

    private static int CheckTargetResistance(CharacterStats _targetstats, int totalMagicalDamage)
    {
        totalMagicalDamage -= _targetstats.magicResistance.GetValue() + (_targetstats.intelligence.GetValue() * 1);

        totalMagicalDamage = Mathf.Clamp(totalMagicalDamage, 0, int.MaxValue);
        return totalMagicalDamage;
    }

    public void ApplyAilment(bool _ignite, bool _chill, bool _shock)
    {
        if(isIgnited||isChilled||isShocked)
        {
            return;
        }
        isIgnited = _ignite;
        isChilled = _chill;
        isShocked = _shock;
    }

    public virtual void takeDamage(int _damage)
    {
        currentHealth -= _damage;

        //Debug.Log(_damage);

        if (currentHealth < 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        //throw new NotImplementedException();
    }
    private int CheckTargetArmor(CharacterStats _targetstats, int totalDamage)
    {
        totalDamage -= _targetstats.armor.GetValue();
        totalDamage = Mathf.Clamp(totalDamage, 1, int.MaxValue);
        return totalDamage;
    }

    private bool TargetCanAvoidAttack(CharacterStats _targetstats)
    {
        int totalEvasion = _targetstats.evasion.GetValue() + _targetstats.agillity.GetValue();

        if (Random.Range(0, 100) < totalEvasion)
        {
            Debug.Log("ATTACK AVOIDED");
            return true;
        }
        return false;
    }

    private bool CanCrit()
    {
        int totalCritChance = critChance.GetValue() + agillity.GetValue();

        if(Random.Range(0, 100) <= totalCritChance)
        {
            return true;
        }
        return false;
    }

    private int CalculateCritalDamage(int _damage)
    {
        float totalCritPower = (critPower.GetValue() + strength.GetValue()) * .01f;

        float critDamage = _damage * totalCritPower;

        return Mathf.RoundToInt(critDamage);
    }

}
