using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VillageAdventure.Object
{
    public class TriggerController : MonoBehaviour
    {
        // 겹치는 순간 실행시킬 기능을 대리할 대리자
        private Action<Collider2D> enterEvent;
        // 겹침이 끝나는 순간 실행시킬 기능을 대리할 대리자
        private Action<Collider2D> exitEvent;
        private Action<Collider2D> stayEvent;
        private Collider2D coll;
        private void Start()
        {
            coll = GetComponent<Collider2D>();
        }
        public void Initialize(Action<Collider2D> OnEnter = null, Action<Collider2D> OnExit = null, Action<Collider2D> OnStay = null)
        {
            enterEvent = OnEnter;
            exitEvent = OnExit;
            stayEvent = OnStay;
        }
        // 특정 콜라이더와 겹치는 순간 실행
        private void OnTriggerEnter2D(Collider2D collision)
        {
            enterEvent?.Invoke(collision);
        }
        // 특정 콜라이더와 겹침이 끝나는 순간 실행
        private void OnTriggerExit2D(Collider2D collision)
        {
            exitEvent?.Invoke(collision);
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            stayEvent?.Invoke(collision);
        }
    }
}