using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using MessageBox = System.Windows.Forms.MessageBox;

namespace FT_Edited_version
{
    public class HotKey
    {
        int keyid = 10;
        Dictionary<int, HotKeyCallBackHanlder> keymap = new Dictionary<int, HotKeyCallBackHanlder>();
        public delegate void HotKeyCallBackHanlder();
        public enum HotkeyModifiers
        {
            None = 0,
            control = 1,
            space = 2
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool RegisterHotKey(IntPtr hWnd, int id, int modifiers, System.Windows.Forms.Keys vk);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public void Regist(IntPtr hWnd, int modifiers, System.Windows.Forms.Keys vk, HotKeyCallBackHanlder callBack)
        {
            int id = keyid++;
            if (!RegisterHotKey(hWnd, id, modifiers, vk))
                throw new Exception("ERROR!");
            keymap[id] = callBack;
        }

        public void UnRegist(IntPtr hWnd, HotKeyCallBackHanlder callBack)
        {
            foreach (KeyValuePair<int, HotKeyCallBackHanlder> var in keymap)
            {
                if (var.Value == callBack)
                    UnregisterHotKey(hWnd, var.Key);
            }
        }

        public void ProcessHotKey(System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x312)
            {
                int id = m.WParam.ToInt32();
                HotKeyCallBackHanlder callback;
                if (keymap.TryGetValue(id, out callback))
                    callback();
            }
        }

        internal void Regist(bool handlesScrolling, int v, Action<object, EventArgs> showMenuItem_Click)
        {
            throw new NotImplementedException();
        }
    }
}
