using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum BossState
{
   Idle,
   Run,
   Attack
}
public class Wizard : MonoBehaviour
{
   [Header("Animator")] 
   [SerializeField]private Animator animator;
   
   [Header("Move")] 
   [SerializeField] private float moveSpd = 3;
   [SerializeField] private float rangeRandomMove = 5;
   
   [Header("Normal Atk")] 
   [SerializeField] private float timeAtk = 3;
   [SerializeField] private float dmgAtk = 5;
   [SerializeField] private WizardBullet normalBullet;
   [SerializeField] private Transform barriel;
   
   [Header("Skill")] 
   [SerializeField] private float timeSkill;
   [SerializeField] private Ghost ghost;
   
   
   //Target
   private PlayerMovement player;

   //cached 
   //for random move
   private Vector2 startPos;
   private Vector2 moveDir = Vector2.right;
   private Vector2 faceDir;
   private float colliderRange = 1f;
   public BossState state = BossState.Idle;
   
   //for normal atk
   private float coolDownNormalAtk = 0;
   
   //for skill
   private float coolDownSkill = 0;
   
   private void Awake()
   {
      startPos = transform.position;
   }

   private void Start()
   {
      ObjectPoolManager.instance.PreCachePool(normalBullet.ExplosionFx, 10);
      ObjectPoolManager.instance.PreCachePool(ghost.GetComponent<Enemy>().ExplosionFx, 10);
   }

   private void OnEnable()
   {
      Reset();
   }

   private void Reset()
   {
      SetTargetPlayer(null);
      coolDownNormalAtk = 0;
   }

   private void Update()
   {
      //Neu player khong o trong room => player = null
      //random move
      if (player == null)
      {
         GetRandomDirMove();
         Move(moveDir);
         return;
      }
      //Neu player o trong phong
      coolDownNormalAtk -= Time.deltaTime;
      coolDownSkill -= Time.deltaTime;

      if (CanUseSkill())
      {
         coolDownSkill = timeSkill;
         UseSkill();
         return;
      }
       
      //Check xem co the danh thuong khong
      if (CanNormalAtk())
      {
         coolDownNormalAtk = timeAtk;
         NormalAtk();
         return;
      }
      
      
      if (state != BossState.Attack)
      {
         //Neu khong tan cong duoc => chase player
         ChasePlayer();
      }
   }
   

   private void ChasePlayer()
   {
      //Check neu player o ngay canh boss => dung yen
      //Neu khong thi duoi theo player
      bool nearPlayer = IsNearPlayer();
      if (nearPlayer)
      {
         Idle();
      }
      else
      {
         GetDirToChasePlayer();
         Move(moveDir);
      }
   }

   private void UseSkill()
   {
      if (state != BossState.Attack)
      {
         state = BossState.Attack;
         animator.SetTrigger(AnimationID.SkillAnim);
      }
   }

   public void SpawnGhost()
   {
      var obj = Instantiate(ghost);
      obj.transform.position = new Vector2(player.transform.position.x, transform.position.y - 5f);
      obj.gameObject.SetActive(true);
      obj.Init(player);
   }
   
   private void NormalAtk()
   {
      if (state != BossState.Attack)
      {
         state = BossState.Attack;
         animator.SetTrigger(AnimationID.NormalAtkAnim);
      }
   }

   public void ShootNormalBullet()
   {
      var bullet = Instantiate(normalBullet);
      bullet.transform.position = barriel.position;
      bullet.gameObject.SetActive(true);
      var dir = ((Vector2)(player.transform.position - barriel.transform.position)) + 0.5f * Vector2.up;
      float rot = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
      bullet.transform.rotation = Quaternion.Euler(0f, 0f, rot + 180);
      bullet.Init(dir.normalized, dmgAtk);
   }

   public void AttackDone()
   {
      ChasePlayer();
   }
   private bool CanNormalAtk()
   {
      return coolDownNormalAtk <= 0 && state != BossState.Attack;
   }
   
   private bool CanUseSkill()
   {
      return coolDownSkill <= 0 && state != BossState.Attack;
   }
   private void GetDirToChasePlayer()
   {
      if (player.transform.position.x >= transform.position.x + colliderRange)
      {
         moveDir = Vector2.right;
      }
      else if (player.transform.position.x <= transform.position.x - colliderRange)
      {
         moveDir = Vector2.left;
      }
      FaceToMoveDir(moveDir);
   }
   private bool IsNearPlayer()
   {
      return Mathf.Abs(player.transform.position.x - transform.position.x) <= colliderRange;
   }

   private void Idle()
   {
      moveDir = Vector2.zero;
      if (state != BossState.Idle)
      {
         animator.SetTrigger(AnimationID.IdleAnim);
         state = BossState.Idle;
      }
   }

   private void FaceToMoveDir(Vector2 dir)
   {
      if (faceDir != dir)
      {
         transform.localScale = new Vector3(dir.x > 0 ? 1 : -1, 1, 1);
         faceDir = dir;
      }
   }
   private void GetRandomDirMove()
   {
      float leftBound = startPos.x - rangeRandomMove;
      float rightBound = startPos.x + rangeRandomMove;

      if (transform.position.x < leftBound)
      {
         moveDir = Vector2.right;
         FaceToMoveDir(moveDir);
      }
      else if (transform.position.x > rightBound)
      {
         moveDir = Vector2.left;
         FaceToMoveDir(moveDir);
      }

   }
   private void Move(Vector3 dir)
   {
      if (dir.x != 0 && state != BossState.Run)
      {
         state = BossState.Run;
         animator.SetTrigger(AnimationID.RunAnim);
      }
      transform.Translate(dir * moveSpd * Time.deltaTime);
   }

   public void SetTargetPlayer(PlayerMovement target)
   {
      player = target;
   }
}
