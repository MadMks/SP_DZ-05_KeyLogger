using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyLogger
{
    public partial class MainForm : Form
    {
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



        public MainForm()
        {
            InitializeComponent();

            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
