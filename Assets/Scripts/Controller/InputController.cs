using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VillageAdventure.Controller
{
    public class InputController
    {
        public delegate void InputButtonEvent();
        public delegate void InputAxisEvent(float value);

        public List<KeyValuePair<string, AxisHandler>> inputAxes
            = new List<KeyValuePair<string, AxisHandler>>();
        public List<KeyValuePair<string, ButtonHandler>> inputButtons
            = new List<KeyValuePair<string, ButtonHandler>>();

        public void AddAxis(string key, InputAxisEvent axisEvent)
        {
            inputAxes.Add(new KeyValuePair<string, AxisHandler>(key, new AxisHandler(axisEvent)));
        }

        public void AddButton(string key, params InputButtonEvent[] buttonEvnets)
        {
            inputButtons.Add(new KeyValuePair<string, ButtonHandler>(key,
                new ButtonHandler(buttonEvnets[0], buttonEvnets[1], buttonEvnets[2], buttonEvnets[3])));
        }
        public class AxisHandler
        {
            private InputAxisEvent axisEvent;

            public AxisHandler(InputAxisEvent axisEvent)
            {
                this.axisEvent = axisEvent;
            }

            public void GetAxisValue(float value)
            {
                axisEvent?.Invoke(value);
            }
        }

        public class ButtonHandler
        {
            private InputButtonEvent downEvent;
            private InputButtonEvent upEvent;
            private InputButtonEvent pressEvent;
            private InputButtonEvent notPressEvent;

            public ButtonHandler(InputButtonEvent downEvent, InputButtonEvent upEvent,
                InputButtonEvent pressEvent, InputButtonEvent notPressEvent)
            {
                this.downEvent = downEvent;
                this.upEvent = upEvent;
                this.pressEvent = pressEvent;
                this.notPressEvent = notPressEvent;
            }
            public void OnDown()
            {
                //downEvent != null ÀÌ¸י Invoke
                downEvent?.Invoke();
            }
            public void OnUp()
            {
                upEvent?.Invoke();
            }
            public void OnPress()
            {
                pressEvent?.Invoke();
            }
            public void OnNotPress()
            {
                notPressEvent?.Invoke();
            }
        }
    }
}