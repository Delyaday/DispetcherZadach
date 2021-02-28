using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DispetcherZadach
{
    class Model
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);
       
        public void GetMoreLifes(Process process)
        {
            IntPtr openedProcess = OpenProcess(0x001F0FFF, false, process.Id);
            IntPtr adress = (IntPtr)0x005A1290;
            byte[] lifes = BitConverter.GetBytes(1000);

            UIntPtr written = new UIntPtr(4);
            WriteProcessMemory(openedProcess, adress, lifes, 4, out written);
        }
        
        public ObservableCollection<Process> GetProcesses(string filter)
        {
            ObservableCollection<Process> result = new ObservableCollection<Process>();
            Process[] bufer = Process.GetProcesses();
            int len = bufer.Length;
            for (int i = 0; i < len; i++)
            {
                if (filter != null)
                {
                    if (bufer[i].ProcessName.IndexOf(filter) >= 0)
                        result.Add(bufer[i]);
                }
                else
                {
                    result.Add(bufer[i]);
                }
            }
            return result;
        }
    }
}
