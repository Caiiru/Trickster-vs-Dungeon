using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

/*    TO DO LATER:
      Limite de cast

*/
public class WaterJetScript : Skill_DamageSkill
{
   private bool wasCreated = false;
   private bool canDamage = false;
   private ScriptableStats playerStats;

   [SerializeField]private float skillcooldownTime;

   [Header("SFX settings")] [SerializeField]
   private ParticleSystem _particleSystem;
   public override void Start()
   {
      base.Start();
      _particleSystem = GetComponentInChildren<ParticleSystem>();
   }
   Transform _spawnPoint; 
   public override void Create(Transform spawnPoint, Vector2 direction)
   {

      playerStats = GameManager.getInstance().getPlayerEntity()
         .gameObject.GetComponent<PlayerController>().getStats();
      
      _spawnPoint = spawnPoint;
      changePositionAndRotation(spawnPoint.position,direction);
      base.Create(spawnPoint, direction);
      wasCreated = true;
      skillcooldownTime = _stats.cooldown; 
      _particleSystem.Play();
      GameManager.getInstance().getPlayerEntity().gameObject.GetComponent<PlayerController>().ChangeMovementSpeedByMultiply(.2f); // Muda pra 20% da velocidade atual
    }
   
   public override void Execute()
   {
      base.Execute();
      if (playerStats.canMove)
      {
         playerStats.isAiming = true;
         //playerStats.canMove = false;
      }
        

      if (Input.GetKey(KeyCode.F))
      {
            
            changePositionAndRotation(_spawnPoint.position, 
                GameManager.getInstance().
                getPlayerEntity().gameObject.GetComponent<PlayerController>()
                .getStats().aimingDirection);
         
         if (skillcooldownTime > _stats.cooldown)
         {
            if (wasCreated)
            {  
               canDamage = true;
               skillcooldownTime = 0;
            }
         }

         if (skillcooldownTime <= _stats.cooldown)
         {
            skillcooldownTime += 0.5f * Time.deltaTime;
            
         }
      }
      else
      {
         playerStats.isAiming = false;
            //playerStats.canMove = true;
         GameManager.getInstance().getPlayerEntity().gameObject.GetComponent<PlayerController>().ChangeMovementSpeedByMultiply(5); // Volta ao normal, 100%

            Destroy(this.gameObject);
      }
   }

   public void setCanDamageFalse()
   {
      canDamage = false;
   }

   public int getDamageTick()
   {
      if(canDamage)
         return _stats.SkillDamage;

      return 0;
   }

   public override void Update()
   {
      base.Update();
      Execute();
   } 
   
   private void changePositionAndRotation(Vector2 position, Vector2 dir)
   {
      transform.position = position;
      if (dir.x == -1)
      {
         this.transform.rotation = Quaternion.Euler(0, dir.x * 180,0);
      }
      else if(dir.x == 1)
      {
         this.transform.rotation = Quaternion.Euler(0, 0 ,0);
      }
   }
}
