using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VillageAdventure;
using VillageAdventure.Enum;


/// ªË¡¶
public abstract class ActorController : MonoBehaviour
{

    void Start()
    {
		Init();
		
	}

    void Update()
    {

    }
	public abstract void Init();

	public string objTagName { get; set; }
	protected ActorState.State _state = ActorState.State.Idle;
	public virtual ActorState.State State
	{
		get { return _state; }
		set
		{
			_state = value;

			Animator anim = GetComponent<Animator>();
			switch (_state)
			{
				case ActorState.State.Dead:
					anim.CrossFade($"{objTagName}_Dead", 0.5f);
					break;
				case ActorState.State.Idle:
					anim.CrossFade($"{objTagName}_Idle", 0.5f);
					break;
				case ActorState.State.Move:
					anim.CrossFade($"{objTagName}_Move", 0.5f);
					break;
				case ActorState.State.Attack:
					anim.CrossFade($"{objTagName}_Attack", 0.5f);
					break;
				case ActorState.State.Alert:
					anim.CrossFade($"{objTagName}_Alert", 0.5f);
					break;
				case ActorState.State.Hurt:
					anim.CrossFade($"{objTagName}_Hurt", 0.5f);
					break;
			}
		}
	}
}
