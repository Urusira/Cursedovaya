using UnityEngine;

public class Durarmor : PassiveItemTemplate
{
    [SerializeField] private float defBuff;
    private float _baseDefBuff;
    
    protected override void Start()
    {
        _baseDefBuff = defBuff;
        base.Start();
    }

    protected override void BuffApply()
    {
        base.BuffApply();
        buffs.defBuff(defBuff);
    }

    protected override void recalcItemStatsForCurrentLevel()
    {
        defBuff = _baseDefBuff * currentItemLevel.levelCharsMultiplier;
        base.recalcItemStatsForCurrentLevel();
    }
}
