using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DynamicControlCommon
{
    public interface IUserControl
    {
        string Name { get; }

        string Version { get; }
        UserControl Control { get; }
    }
}
