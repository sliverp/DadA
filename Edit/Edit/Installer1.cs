using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace Edit
{
    [RunInstaller(true)]
    public partial class Installer1 : System.Configuration.Install.Installer
    {
        public Installer1()
        {
            InitializeComponent();
        }
        protected override void OnAfterInstall(IDictionary savedState)
        {
            string toolPath = System.Windows.Forms.Application.StartupPath + "\\Edit.exe";

            string extension = ".dada";

            string fileType = "Dada Program File";

            string fileContent = "text/plain";
            //获取信息
            Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(extension);

            if (registryKey != null && registryKey.OpenSubKey("shell") != null && registryKey.OpenSubKey("shell").OpenSubKey("open") != null &&
                registryKey.OpenSubKey("shell").OpenSubKey("open").OpenSubKey("command") != null)
            {
                var varSub = registryKey.OpenSubKey("shell").OpenSubKey("open").OpenSubKey("command");
                var varValue = varSub.GetValue("");

                if (Object.Equals(varValue, toolPath + " %1"))
                {
                    return;
                }
            }
            //删除
            Microsoft.Win32.Registry.ClassesRoot.DeleteSubKeyTree(extension, false);
            //文件注册
            registryKey = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(extension);
            registryKey.SetValue("文件类型", fileType);
            registryKey.SetValue("Content Type", fileContent);
            //设置默认图标
            Microsoft.Win32.RegistryKey iconKey = registryKey.CreateSubKey("DefaultIcon");
            iconKey.SetValue("", System.Windows.Forms.Application.StartupPath + "\\1.ico");
            //设置默认打开程序路径
            registryKey = registryKey.CreateSubKey("shell\\open\\command");
            registryKey.SetValue("", toolPath + " %1");
            //关闭
            registryKey.Close();
        }
        public override void Install(IDictionary stateSaver)
        {
       
            base.Install(stateSaver);
        }
        protected override void OnBeforeInstall(IDictionary savedState)
        {
           
            base.OnBeforeInstall(savedState);
        }
        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
        }
        public override void Rollback(IDictionary savedState)
        {
            
            base.Rollback(savedState);
        }
        public void LogWrite(string str)
        {
           
            
        }

    }
}
