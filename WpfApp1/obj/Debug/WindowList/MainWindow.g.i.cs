﻿#pragma checksum "..\..\..\WindowList\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E9533086814837145DAA9CB17BC4EB36"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace WpfApp1.WindowList {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\WindowList\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListView;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\WindowList\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteButton;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\WindowList\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem New_Account;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\WindowList\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Edit_Accounts;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\WindowList\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem HelpList;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\WindowList\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label InfoBox;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\WindowList\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressBar;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp1;component/windowlist/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\WindowList\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 7 "..\..\..\WindowList\MainWindow.xaml"
            ((WpfApp1.WindowList.MainWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Main_Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 17 "..\..\..\WindowList\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseBtn);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 20 "..\..\..\WindowList\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ListView = ((System.Windows.Controls.ListView)(target));
            
            #line 23 "..\..\..\WindowList\MainWindow.xaml"
            this.ListView.AddHandler(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, new System.Windows.RoutedEventHandler(this.GridViewColumnHeaderClickedHandler));
            
            #line default
            #line hidden
            return;
            case 5:
            this.DeleteButton = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\WindowList\MainWindow.xaml"
            this.DeleteButton.Click += new System.Windows.RoutedEventHandler(this.DeleteItem);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 37 "..\..\..\WindowList\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Plot);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 38 "..\..\..\WindowList\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ChangeTable_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 39 "..\..\..\WindowList\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 41 "..\..\..\WindowList\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseExit);
            
            #line default
            #line hidden
            return;
            case 10:
            this.New_Account = ((System.Windows.Controls.MenuItem)(target));
            
            #line 44 "..\..\..\WindowList\MainWindow.xaml"
            this.New_Account.Click += new System.Windows.RoutedEventHandler(this.NewAcc);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 45 "..\..\..\WindowList\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MyAccountButton);
            
            #line default
            #line hidden
            return;
            case 12:
            this.Edit_Accounts = ((System.Windows.Controls.MenuItem)(target));
            
            #line 46 "..\..\..\WindowList\MainWindow.xaml"
            this.Edit_Accounts.Click += new System.Windows.RoutedEventHandler(this.EditAccounts);
            
            #line default
            #line hidden
            return;
            case 13:
            this.HelpList = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 14:
            
            #line 49 "..\..\..\WindowList\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowErrLog);
            
            #line default
            #line hidden
            return;
            case 15:
            this.InfoBox = ((System.Windows.Controls.Label)(target));
            return;
            case 16:
            this.ProgressBar = ((System.Windows.Controls.ProgressBar)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

