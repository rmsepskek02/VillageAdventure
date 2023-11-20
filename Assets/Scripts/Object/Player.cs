using System.Linq;
using UnityEngine;
using VillageAdventure.DB;
using VillageAdventure.StaticData;

namespace VillageAdventure.Object
{
    //using State = Enum.ActorState.State;
    public class Player : Actor
    {
        //public State State { get; private set; }

        public BoPlayer boPlayer;
        private FrictionJoint2D joint;
        private TriggerController trigger;
        private BuildTrigger buildTrigger;
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
            TestSpace();
            //Pause();
        }
        public void Update()
        {
            Pause();
        }
        private void TestSpace()
        {
            if (Input.GetKey(KeyCode.Space)){
                Debug.Log("SPACE");
                InGameManager.Instance.GetComponent<Grid>().CreateGrid();
            }
        }
        public override void Init()
        {
            base.Init();
            joint = GetComponent<FrictionJoint2D>();
            trigger = transform.GetComponentInChildren<TriggerController>();
            buildTrigger = transform.GetComponentInChildren<BuildTrigger>();
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
                void ActiveObj()
                {
                    if (Input.GetKey(KeyCode.G))
                        collision.transform.GetChild(0).gameObject.SetActive(true);
                }

                if (collision.CompareTag("Mine") 
                    || collision.CompareTag("Tree") 
                    || collision.CompareTag("Food"))
                    ActiveObj();
                else if (collision.CompareTag("Fishing"))
                {
                    time += Time.deltaTime;
                    if (Input.GetKey(KeyCode.G))
                    {
                        // 5초간 머물러야 상호작용 가능
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
                    // 목재가 없으면 리턴
                    if (InGameManager.Instance.tree <= 0)
                        return;
                    ActiveObj();
                }
                else if (collision.CompareTag("Fire"))
                {
                    // fish가 없으면 리턴
                    if (InGameManager.Instance.fish <= 0)
                        return;
                    ActiveObj();
                }
            }
        }

        private void SetBuildObjectPosition()
        {
            // Actor의 X 속력이 있다면
            if (!(Mathf.Approximately(rigid.velocity.x, 0)))
            {
                if (boActor.moveDirection.x > 0)
                    transform.GetChild(1).transform.position = this.gameObject.transform.position + new Vector3(1f, 0, 0);
                else if (boActor.moveDirection.x < 0)
                    transform.GetChild(1).transform.position = this.gameObject.transform.position + new Vector3(-1f, 0, 0);
            }
            // Actor의 Y 속력이 있다면
            if (!(Mathf.Approximately(rigid.velocity.y, 0)))
            {
                if (boActor.moveDirection.y > 0)
                    transform.GetChild(1).transform.position = this.gameObject.transform.position + new Vector3(0, 1.2f, 0);
                else if (boActor.moveDirection.y < 0)
                    transform.GetChild(1).transform.position = this.gameObject.transform.position + new Vector3(0, -1.2f, 0);
            }
        }

        private void Pause()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
                GameManager.Instance.TogglePause();
        }

        private void BuildObject()
        {
            if (transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().material.color == Color.red
                || transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite == null)
                return;

            if (Input.GetKey(KeyCode.V))
            {
                /// 오브젝트 생성
                // 오브젝트의 Index값을 받아옴 (Home, Field, ECT.H, ECT.F 중 어느 UI 버튼을 눌렀는가)
                GameObject buildObj = GameObject.Find("BuildObject");
                Transform homeObj = buildObj.transform.Find("HomeObject").gameObject.transform;
                Transform fieldObj= buildObj.transform.Find("FieldObject").gameObject.transform;

                if (InGameManager.Instance.sdTypeIndex == 0)
                {
                    SDObject sdObject = GameManager.SD.sdHomeObjects.Where(_ => _.index == InGameManager.Instance.sdIndex).SingleOrDefault();

                    if (InGameManager.Instance.tree < Mathf.Abs(sdObject.consumeTree) ||
                        InGameManager.Instance.mine < Mathf.Abs(sdObject.consumeMine) ||
                        InGameManager.Instance.food < Mathf.Abs(sdObject.consumeFood))
                        return; 
                    //CheckResource(sdObject);
                    // Index값에 해당하는 오브젝트를 생성
                    var sdObjectClone = Instantiate(Resources.Load<GameObject>(sdObject.resourcePath)).GetComponent<ScoreObject>();
                    sdObjectClone.Initialize(new BoScoreObject(sdObject));
                    // 생성위치
                    Transform sdObjectPosition = transform.GetChild(1).transform;
                    sdObjectClone.transform.position = sdObjectPosition.position;
                    sdObjectClone.transform.SetParent(homeObj);
                    InGameManager.Instance.tree += sdObject.consumeTree;
                    InGameManager.Instance.mine += sdObject.consumeMine;
                    InGameManager.Instance.food += sdObject.consumeFood;
                    InGameManager.Instance.score += sdObject.score;
                }
                else if (InGameManager.Instance.sdTypeIndex == 1)
                {
                    var sdObject = GameManager.SD.sdFieldObjects.Where(_ => _.index == InGameManager.Instance.sdIndex).SingleOrDefault();

                    if (InGameManager.Instance.tree < Mathf.Abs(sdObject.consumeTree) ||
                        InGameManager.Instance.mine < Mathf.Abs(sdObject.consumeMine) ||
                        InGameManager.Instance.food < Mathf.Abs(sdObject.consumeFood))
                        return;
                    var sdObjectClone = Instantiate(Resources.Load<GameObject>(sdObject.resourcePath)).GetComponent<ScoreObject>();
                    sdObjectClone.Initialize(new BoScoreObject(sdObject));
                    Transform sdObjectPosition = transform.GetChild(1).transform;
                    sdObjectClone.transform.position = sdObjectPosition.position;
                    sdObjectClone.transform.SetParent(fieldObj);

                    InGameManager.Instance.tree += sdObject.consumeTree;
                    InGameManager.Instance.mine += sdObject.consumeMine;
                    InGameManager.Instance.food += sdObject.consumeFood;
                    InGameManager.Instance.score += sdObject.score;
                }
                else if (InGameManager.Instance.sdTypeIndex == 2)
                {
                    var sdObject = GameManager.SD.sdCommonObjects.Where(_ => _.index == InGameManager.Instance.sdIndex).SingleOrDefault();
                    if (InGameManager.Instance.tree < Mathf.Abs(sdObject.consumeTree) ||
                        InGameManager.Instance.mine < Mathf.Abs(sdObject.consumeMine) ||
                        InGameManager.Instance.food < Mathf.Abs(sdObject.consumeFood))
                        return;
                    var sdObjectClone = Instantiate(Resources.Load<GameObject>(sdObject.resourcePath));
                    //sdObjectClone.Initialize(new BoScoreObject(sdObject));
                    Transform sdObjectPosition = transform.GetChild(1).transform;
                    sdObjectClone.transform.position = sdObjectPosition.position;
                    if(GameManager.Instance.currentScene == Enum.SceneType.House)
                    {
                        sdObjectClone.transform.SetParent(homeObj);
                    }
                    else if (GameManager.Instance.currentScene == Enum.SceneType.Field)
                    {
                        sdObjectClone.transform.SetParent(fieldObj);
                    }

                    InGameManager.Instance.tree += sdObject.consumeTree;
                    InGameManager.Instance.mine += sdObject.consumeMine;
                    InGameManager.Instance.food += sdObject.consumeFood;
                    InGameManager.Instance.score += sdObject.score;
                }
                else if (InGameManager.Instance.sdTypeIndex == 3)
                {
                    var sdObject = GameManager.SD.sdResourceObjects.Where(_ => _.index == InGameManager.Instance.sdIndex).SingleOrDefault();
                    if (InGameManager.Instance.tree < Mathf.Abs(sdObject.consumeTree) ||
                        InGameManager.Instance.mine < Mathf.Abs(sdObject.consumeMine) ||
                        InGameManager.Instance.food < Mathf.Abs(sdObject.consumeFood))
                        return;
                    var sdObjectClone = Instantiate(Resources.Load<GameObject>(sdObject.resourcePath));
                    //sdObjectClone.Initialize(new BoScoreObject(sdObject));
                    Transform sdObjectPosition = transform.GetChild(1).transform;
                    sdObjectClone.transform.position = sdObjectPosition.position;
                    sdObjectClone.transform.SetParent(fieldObj);

                    InGameManager.Instance.tree += sdObject.consumeTree;
                    InGameManager.Instance.mine += sdObject.consumeMine;
                    InGameManager.Instance.food += sdObject.consumeFood;
                    InGameManager.Instance.score += sdObject.score;
                }

                InGameManager.Instance.UIBuildActivated = false;
                // 청사진의 sprite는 다시 비우기
                transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = null;
            }
        }
        void CheckResource(SDObject sdObject)
        {
            Debug.Log("CHECK");
            if (InGameManager.Instance.tree < Mathf.Abs(sdObject.consumeTree) ||
                InGameManager.Instance.mine < Mathf.Abs(sdObject.consumeMine) ||
                InGameManager.Instance.food < Mathf.Abs(sdObject.consumeFood))
            {
                Debug.Log($"test === 0");
                buildTrigger.isEnoughResource = false;
                return;
            }
            else
            {
                Debug.Log($"test === 1");
                buildTrigger.isEnoughResource = true;
            }
        }
    }
}