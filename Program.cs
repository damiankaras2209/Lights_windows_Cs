using SharpHook;
using SharpHook.Native;

namespace Lights_windows; 

public static class LightsWindows {
    private const string Address = "http://192.168.0.201";
    private static readonly HttpClient Client = new ();
    private static bool _ctrlPressed;
    private static bool _altPressed;

    private static void SwitchOne(int i) {
        Client.PostAsync(Address + $"/switch/bulb_{i}/toggle", null);
    }

    private static void SwitchAll() {
        Client.PostAsync(Address + "/button/all/press", null);
    }
        
    public static void Main(string[] args) {
            
        var hook = new TaskPoolGlobalHook();

        hook.KeyPressed += (sender, eventArgs) => {
            switch (eventArgs.Data.KeyCode) {
                case KeyCode.VcLeftControl:
                    _ctrlPressed = true;
                    break;
                case KeyCode.VcLeftAlt:
                    _altPressed = true;
                    break;
                case KeyCode.VcNumPad0:
                case KeyCode.VcInsert:
                    if (_ctrlPressed && _altPressed)
                        SwitchAll();
                    break;
                case KeyCode.VcNumPad1:
                case KeyCode.VcEnd:
                    if (_ctrlPressed && _altPressed)
                        SwitchOne(1);
                    break;
                case KeyCode.VcNumPad2:
                case KeyCode.VcDown:
                    if(_ctrlPressed && _altPressed)
                        SwitchOne(2);
                    break;
                case KeyCode.VcNumPad3:
                case KeyCode.VcPageDown:
                    if(_ctrlPressed && _altPressed)
                        SwitchOne(3);
                    break;
                case KeyCode.VcNumPad4:
                case KeyCode.VcLeft:
                    if(_ctrlPressed && _altPressed)
                        SwitchOne(4);
                    break;
                case KeyCode.VcNumPad5:
                case KeyCode.VcClear:
                    if(_ctrlPressed && _altPressed)
                        SwitchOne(5);
                    break;
                case KeyCode.VcNumPad6:
                case KeyCode.VcRight:
                    if(_ctrlPressed && _altPressed)
                        SwitchOne(6);
                    break;
            }
        };

        hook.KeyReleased += (sender, eventArgs) => {
            switch (eventArgs.Data.KeyCode) {
                case KeyCode.VcLeftControl:
                    _ctrlPressed = false;
                    break;
                case KeyCode.VcLeftAlt:
                    _altPressed = false;
                    break;
            }
        };

        hook.Run();

    }
}