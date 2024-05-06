using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_DamageSkill : Skill
{ 
    public Skill_DamageStats _stats;
 


    public override void Start()
    {
        base.Start();
       
        
    }

    public override void Create(Transform spawnPoint, Vector2 direction)
    {
        base.Create(spawnPoint, direction);
        SkillName = _stats.SkillName; 
        _stats.currentCooldown = _stats.cooldown;
        
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Update()
    {
        base.Update(); 
    }
}
