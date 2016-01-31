using DynamicControlCommon;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows.Controls;

namespace DynamicControl.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        private ObservableCollection<IUserControl> _ControlList = new ObservableCollection<IUserControl>();
        /// <summary>
        /// ControlList
        /// </summary>
        public ObservableCollection<IUserControl> ControlList
        {
            get
            {
                return _ControlList;
            }
            set
            {
                _ControlList = value;
                RaisePropertyChanged("ControlList");
            }
        }

        private RelayCommand _LoadCommand = null;
        public RelayCommand LoadCommand
        {
            get
            {
                return _LoadCommand ?? new RelayCommand(() =>
                {
                    //プラグインフォルダ
                    string folder = Path.GetDirectoryName(
                        System.Reflection.Assembly
                        .GetExecutingAssembly().Location);

                    //IPlugin型の名前
                    string ipluginName = typeof(IUserControl).FullName;

                    //.dllファイルを探す
                    string[] dlls =
                        Directory.GetFiles(folder, "*.dll");

                    foreach (var dll in dlls)
                    {
                        var asm = Assembly.LoadFrom(dll);

                        foreach (var t in asm.GetTypes())
                        {
                            //アセンブリ内のすべての型について、
                            //プラグインとして有効か調べる
                            if (t.IsClass && t.IsPublic && !t.IsAbstract &&
                                t.GetInterface(ipluginName) != null)
                            {

                                var control = Activator.CreateInstance(t) as IUserControl;
                                if (control != null)
                                {
                                    ControlList.Add(control);
                                }
                            }
                        }
                    }

                });
            }
        }
    }
}