using System.Linq;
using UnityEngine;
using VillageAdventure.DB;

namespace VillageAdventure.Object
{
    //using State = Enum.ActorState.State;
    public class Player : Actor
    {
        //public State State { get; private set; }

        public BoPlayer boPlayer;
        private FrictionJoint2D joint;
        private TriggerController trigger;
        private float time = 0;

        public override void Initialize(BoActor boPlayer)
        {
            base.Initialize(boPlayer);
            this.boPlayer = boPlayer as BoPlayer;
        }

        public override void Execute()
        {
            base.Execute();
            BuildObject();
        }

        public override void Init()
        {
            base.Init();
            joint = GetComponent<FrictionJoint2D>();
            trigger = transform.GetComponentInChildren<TriggerController>();
            ActiveByPlayer();
        }
        public override void OnMove()
        {
            base.OnMove();
            SetBuildObjectPosition();
            //SetState(Mathf.Approximately(boPlayer.moveDirection.x, 0) ? State.Idle : State.Move);
            //SetState(Mathf.Approximately(boPlayer.moveDirection.y, 0) ? State.Idle : State.Move);
        }
        private void ActiveByPlayer()
        {
            trigger.Initialize(null, null, OnStayObj);

            void OnStayObj(Collider2D collision)
            {
                if (collision.CompareTag("Mine"))
                {
                    /// Resource ����������� Ű�Է��� ����????
                    if (Input.GetKey(KeyCode.G))
                        collision.transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (collision.CompareTag("Tree"))
                {
                    if (Input.GetKey(KeyCode.G))
                        collision.transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (collision.CompareTag("Food"))
                {
                    if (Input.GetKey(KeyCode.G))
                        collision.transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (collision.CompareTag("Fishing"))
                {
                    time += Time.deltaTime;
                    if (Input.GetKey(KeyCode.G))
                    {
                        // 5�ʰ� �ӹ����� ��ȣ�ۿ� ����
                        if (time >= 5.0f)
                        {
                            collision.transform.GetChild(0).gameObject.SetActive(true);
                            //GameManager.Instance.fish++;
                            time = 0;
                        }
                    }
                }
                else if (collision.CompareTag("UnFire"))
                {
                    // ���簡 ������ ����
                    if (InGameManager.Instance.tree <= 0)
                        return;
                    if (Input.GetKey(KeyCode.G))
                        collision.transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (collision.CompareTag("Fire"))
                {
                    // fish�� ������ ����
                    if (InGameManager.Instance.fish <= 0)
                        return;
                    if (Input.GetKey(KeyCode.G))
                        collision.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }

        private void SetBuildObjectPosition()
        {
            // Actor�� X �ӷ��� �ִٸ�
            if (!(Mathf.Approximately(rigid.velocity.x, 0)))
            {
                if (boActor.moveDirection.x > 0)
                    transform.GetChild(1).transform.position = this.gameObject.transform.position + new Vector3(1f, 0, 0);
                else if (boActor.moveDirection.x < 0)
                    transform.GetChild(1).transform.position = this.gameObject.transform.position + new Vector3(-1f, 0, 0);
            }
            // Actor�� Y �ӷ��� �ִٸ�
            if (!(Mathf.Approximately(rigid.velocity.y, 0)))
            {
                if (boActor.moveDirection.y > 0)
                    transform.GetChild(1).transform.position = this.gameObject.transform.position + new Vector3(0, 1.2f, 0);
                else if (boActor.moveDirection.y < 0)
                    transform.GetChild(1).transform.position = this.gameObject.transform.position + new Vector3(0, -1.2f, 0);
            }
        }

        private void BuildObject()
        {
            if (transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().material.color == Color.red
                || transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite == null)
                return;

            if (Input.GetKey(KeyCode.V))
            {
                /// ������Ʈ ����
                // ������Ʈ�� Index���� �޾ƿ� (Home, Field, ECT.H, ECT.F �� ��� UI ��ư�� �����°�)
                if (InGameManager.Instance.sdTypeIndex == 0)
                {
                    var sdObject = GameManager.SD.sdHomeObjects.Where(_ => _.index == InGameManager.Instance.sdIndex).SingleOrDefault();

                    if (InGameManager.Instance.tree < Mathf.Abs(sdObject.consumeTree) ||
                        InGameManager.Instance.mine < Mathf.Abs(sdObject.consumeMine) ||
                        InGameManager.Instance.food < Mathf.Abs(sdObject.consumeFood))
                        return;
                    // Index���� �ش��ϴ� ������Ʈ�� ����
                    var sdObjectClone = Instantiate(Resources.Load<GameObject>(sdObject.resourcePath));
                    // ������ġ
                    Transform sdObjectPosition = transform.GetChild(1).transform;
                    sdObjectClone.transform.position = sdObjectPosition.position;

                    InGameManager.Instance.tree += sdObject.consumeTree;
                    InGameManager.Instance.mine += sdObject.consumeMine;
                    InGameManager.Instance.food += sdObject.consumeFood;
                    InGameManager.Instance.score += sdObject.addScore;
                }
                else if (InGameManager.Instance.sdTypeIndex == 1)
                {
                    var sdObject = GameManager.SD.sdFieldObjects.Where(_ => _.index == InGameManager.Instance.sdIndex).SingleOrDefault();

                    if (InGameManager.Instance.tree < Mathf.Abs(sdObject.consumeTree) ||
                        InGameManager.Instance.mine < Mathf.Abs(sdObject.consumeMine) ||
                        InGameManager.Instance.food < Mathf.Abs(sdObject.consumeFood))
                        return;
                    var sdObjectClone = Instantiate(Resources.Load<GameObject>(sdObject.resourcePath));
                    Transform sdObjectPosition = transform.GetChild(1).transform;
                    sdObjectClone.transform.position = sdObjectPosition.position;

                    InGameManager.Instance.tree += sdObject.consumeTree;
                    InGameManager.Instance.mine += sdObject.consumeMine;
                    InGameManager.Instance.food += sdObject.consumeFood;
                    InGameManager.Instance.score += sdObject.addScore;
                }
                else if (InGameManager.Instance.sdTypeIndex == 2)
                {
                    var sdObject = GameManager.SD.sdCommonObjects.Where(_ => _.index == InGameManager.Instance.sdIndex).SingleOrDefault();
                    if (InGameManager.Instance.tree < Mathf.Abs(sdObject.consumeTree) ||
                        InGameManager.Instance.mine < Mathf.Abs(sdObject.consumeMine) ||
                        InGameManager.Instance.food < Mathf.Abs(sdObject.consumeFood))
                        return;
                    var sdObjectClone = Instantiate(Resources.Load<GameObject>(sdObject.resourcePath));
                    Transform sdObjectPosition = transform.GetChild(1).transform;
                    sdObjectClone.transform.position = sdObjectPosition.position;

                    InGameManager.Instance.tree += sdObject.consumeTree;
                    InGameManager.Instance.mine += sdObject.consumeMine;
                    InGameManager.Instance.food += sdObject.consumeFood;
                    InGameManager.Instance.score += sdObject.addScore;
                }
                else if (InGameManager.Instance.sdTypeIndex == 3)
                {
                    var sdObject = GameManager.SD.sdResourceObjects.Where(_ => _.index == InGameManager.Instance.sdIndex).SingleOrDefault();
                    if (InGameManager.Instance.tree < Mathf.Abs(sdObject.consumeTree) ||
                        InGameManager.Instance.mine < Mathf.Abs(sdObject.consumeMine) ||
                        InGameManager.Instance.food < Mathf.Abs(sdObject.consumeFood))
                        return;
                    var sdObjectClone = Instantiate(Resources.Load<GameObject>(sdObject.resourcePath));
                    Transform sdObjectPosition = transform.GetChild(1).transform;
                    sdObjectClone.transform.position = sdObjectPosition.position;

                    InGameManager.Instance.tree += sdObject.consumeTree;
                    InGameManager.Instance.mine += sdObject.consumeMine;
                    InGameManager.Instance.food += sdObject.consumeFood;
                    InGameManager.Instance.score += sdObject.addScore;
                }

                InGameManager.Instance.UIBuildActivated = false;
                // û������ sprite�� �ٽ� ����
                transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = null;
            }
        }
    }
}