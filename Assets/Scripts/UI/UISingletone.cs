using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillageAdventure.Enum;
using VillageAdventure.Util;

namespace VillageAdventure
{
    public class UISingletone : Singleton<UISingletone>
    {
        
        private void Update()
        {

        }

        private void CheckScene()
        {
            if (GameManager.Instance.currentScene == SceneType.Title)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }
}
