using DynamicControlCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControl1
{
    public class Class1 : IUserControl
    {
        public System.Windows.Controls.UserControl Control
        {
            get
            {
                return new UserControl1();
            }
        }

        public string Name
        {
            get
            {
                System.Reflection.AssemblyTitleAttribute asmttl =
                    (System.Reflection.AssemblyTitleAttribute)
                    Attribute.GetCustomAttribute(
                        System.Reflection.Assembly.GetExecutingAssembly(),
                        typeof(System.Reflection.AssemblyTitleAttribute));

                return asmttl.Title;
            }
        }

        public string Version
        {
            get
            {
                System.Diagnostics.FileVersionInfo ver =
                    System.Diagnostics.FileVersionInfo.GetVersionInfo(
                    System.Reflection.Assembly.GetExecutingAssembly().Location);

                return ver.ProductVersion;
            }
        }
    }
}
