﻿#pragma checksum "..\..\..\..\View\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "FFE4F395CEE6646B0BA11076D866D1A8E9436D28"
//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

using FrameResizer;
using MaterialDesignThemes.MahApps;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace FrameResizer {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.DialogHost FinishedDialog;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CloseDialogButton;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SelectFileButton;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SelectFolderButton;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox SelectedFileListBox;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SelectOutputFolderButton;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox OutputFolderTextBox;
        
        #line default
        #line hidden
        
        
        #line 149 "..\..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton WidthRadioButton;
        
        #line default
        #line hidden
        
        
        #line 156 "..\..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.DecimalUpDown WidthDecimalUpDown;
        
        #line default
        #line hidden
        
        
        #line 170 "..\..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton HeightRadioButton;
        
        #line default
        #line hidden
        
        
        #line 176 "..\..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.DecimalUpDown HeightDecimalUpDown;
        
        #line default
        #line hidden
        
        
        #line 196 "..\..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox BorderColorTextBox;
        
        #line default
        #line hidden
        
        
        #line 217 "..\..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.DecimalUpDown BorderSizeDecimalUpDown;
        
        #line default
        #line hidden
        
        
        #line 225 "..\..\..\..\View\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ExecuteButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FrameResizer;component/view/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.FinishedDialog = ((MaterialDesignThemes.Wpf.DialogHost)(target));
            return;
            case 2:
            this.CloseDialogButton = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\..\View\MainWindow.xaml"
            this.CloseDialogButton.Click += new System.Windows.RoutedEventHandler(this.CloseDialogButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.SelectFileButton = ((System.Windows.Controls.Button)(target));
            
            #line 72 "..\..\..\..\View\MainWindow.xaml"
            this.SelectFileButton.Click += new System.Windows.RoutedEventHandler(this.SelectFileButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SelectFolderButton = ((System.Windows.Controls.Button)(target));
            
            #line 82 "..\..\..\..\View\MainWindow.xaml"
            this.SelectFolderButton.Click += new System.Windows.RoutedEventHandler(this.SelectFolderButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.SelectedFileListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 6:
            this.SelectOutputFolderButton = ((System.Windows.Controls.Button)(target));
            
            #line 116 "..\..\..\..\View\MainWindow.xaml"
            this.SelectOutputFolderButton.Click += new System.Windows.RoutedEventHandler(this.SelectOutputFolderButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.OutputFolderTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.WidthRadioButton = ((System.Windows.Controls.RadioButton)(target));
            
            #line 154 "..\..\..\..\View\MainWindow.xaml"
            this.WidthRadioButton.Checked += new System.Windows.RoutedEventHandler(this.WidthRadioButton_Checked);
            
            #line default
            #line hidden
            return;
            case 9:
            this.WidthDecimalUpDown = ((MaterialDesignThemes.Wpf.DecimalUpDown)(target));
            return;
            case 10:
            this.HeightRadioButton = ((System.Windows.Controls.RadioButton)(target));
            
            #line 175 "..\..\..\..\View\MainWindow.xaml"
            this.HeightRadioButton.Checked += new System.Windows.RoutedEventHandler(this.HeightRadioButton_Checked);
            
            #line default
            #line hidden
            return;
            case 11:
            this.HeightDecimalUpDown = ((MaterialDesignThemes.Wpf.DecimalUpDown)(target));
            return;
            case 12:
            this.BorderColorTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 13:
            this.BorderSizeDecimalUpDown = ((MaterialDesignThemes.Wpf.DecimalUpDown)(target));
            return;
            case 14:
            this.ExecuteButton = ((System.Windows.Controls.Button)(target));
            
            #line 234 "..\..\..\..\View\MainWindow.xaml"
            this.ExecuteButton.Click += new System.Windows.RoutedEventHandler(this.ExecuteButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

