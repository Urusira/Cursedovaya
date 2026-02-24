using UnityEngine;

public class MonsterMeat : PassiveItemTemplate
{
    [SerializeField] private float hpBuff;
    private float _baseHpBuff;
    
    protected override void Start()
    {
        _baseHpBuff = hpBuff;
        base.Start();
    }

    protected override void BuffApply()
    {
        base.BuffApply();
        buffs.hpBuff(hpBuff);
    }

    protected override void recalcItemStatsForCurrentLevel()
    {
        hpBuff = _baseHpBuff * currentItemLevel.levelCharsMultiplier;
        base.recalcItemStatsForCurrentLevel();
    }
}
