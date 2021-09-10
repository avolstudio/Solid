using System.Threading.Tasks;
using Solid.Attributes;
using Solid.Behaviours;
using Solid.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Solid.PopUps
{
    [ResourcePath("PopUps/LogoPopUp")]
    public class LogoPopUp : PopUp
    {
        [SerializeField] private float ShowTime = 5f;

        [SerializeField] private  Text _text;

        [SerializeField] private Image _foldingScreen;

        protected override void OnAwake(params object[] parameters)
        {
            if (parameters.Length == 0) return;
            
            ShowTime = (float) parameters[0];
        }
        

        protected override async Task OnShow()
        {
            var lerpOperation =  Operation.Create<LerpPosition,Vector3>(_foldingScreen.gameObject,lockThread:true,
                parameters: new object[] {_foldingScreen.transform.position,new Vector3(_foldingScreen.transform.position.x + Screen.width + Screen.width / 2,_foldingScreen.transform.position.y,_foldingScreen.transform.position.z), ShowTime});

            await lerpOperation;
            
            Close();
        }

        private void MoveFoldingScreen(float xPosition)
        {
            var position = _foldingScreen.transform.position;
            
            var newPosition = new Vector3(xPosition,position.y,position.z);

            _foldingScreen.transform.position = newPosition;
        }
    }
}