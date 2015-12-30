﻿
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;
using UAOOI.SemanticData.UANetworking.ReferenceApplication.Controls;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication
{

  /// <summary>
  /// Class MainWindowViewModel - this class demonstrates how to create bindings to the properties that are holders of OPC UA values in the 
  /// Model View ViewModel pattern.
  /// </summary>
  internal class MainWindowViewModel : INotifyPropertyChanged, IProducerViewModel, IConsumerViewModel
  {

    public MainWindowViewModel()
    {
      b_UDPPort = Properties.Settings.Default.UDPPort;
      b_RemoteHost = Properties.Settings.Default.RemoteHostName;
      b_RemotePort = Properties.Settings.Default.RemoteUDPPortNumber;
      b_ConsumerLog = new ObservableCollection<string>();
      b_MulticastGroup = Properties.Settings.Default.DefaultMulticastGroup;
      b_MulticastGroupSelection = Properties.Settings.Default.JoinMulticastGroup;
      //Menu Files
      b_ConfigurationFolder = new ConfigurationFolderCommand();
      b_HelpDocumentation = new WebDocumentationCommand(Properties.Resources.HelpDocumentationUrl);
      //Menu Actions
      b_OpenConsumerConfiguration = new OpenFileCommand(Properties.Resources.ConfigurationDataConsumerFileName);
      b_OpenProducerConfiguration = new OpenFileCommand(Properties.Resources.ConfigurationDataProducerFileName);
      //Menu Help
      b_ReadMe = new OpenFileCommand(Properties.Resources.ReadMeFileName);
      b_TermsOfService = new WebDocumentationCommand(Properties.Resources.TermsOfServiceUrl);
      b_ViewLicense = new WebDocumentationCommand(Properties.Resources.ViewLicenseUrl);
      String _version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
      b_WindowTitle = $"OPC UA Example Application Rel. {_version} supporting PubSup protocol 1.10";
    }

    #region Window
    public string WindowTitle
    {
      get
      {
        return b_WindowTitle;
      }
      set
      {
        PropertyChanged.RaiseHandler<string>(value, ref b_WindowTitle, "WindowTitle", this);
      }
    }
    private string b_WindowTitle;
    #endregion

    #region menu
    public ICommand OpenConsumerConfiguration
    {
      get
      {
        return b_OpenConsumerConfiguration;
      }
      set
      {
        PropertyChanged.RaiseHandler<ICommand>(value, ref b_OpenConsumerConfiguration, "OpenConsumerConfiguration", this);
      }
    }
    public ICommand OpenProducerConfiguration
    {
      get
      {
        return b_OpenProducerConfiguration;
      }
      set
      {
        PropertyChanged.RaiseHandler<ICommand>(value, ref b_OpenProducerConfiguration, "OpenProducerConfiguration", this);
      }
    }
    public ICommand HelpDocumentation
    {
      get
      {
        return b_HelpDocumentation;
      }
      set
      {
        PropertyChanged.RaiseHandler<ICommand>(value, ref b_HelpDocumentation, "HelpDocumentation", this);
      }
    }
    public ICommand ConfigurationFolder
    {
      get
      {
        return b_ConfigurationFolder;
      }
      set
      {
        PropertyChanged.RaiseHandler<ICommand>(value, ref b_ConfigurationFolder, "ConfigurationFolder", this);
      }
    }
    public ICommand ReadMe
    {
      get
      {
        return b_ReadMe;
      }
      set
      {
        PropertyChanged.RaiseHandler<ICommand>(value, ref b_ReadMe, "ReadMe", this);
      }
    }
    public ICommand ViewLicense
    {
      get
      {
        return b_ViewLicense;
      }
      set
      {
        PropertyChanged.RaiseHandler<ICommand>(value, ref b_ViewLicense, "ViewLicense", this);
      }
    }
    public ICommand TermsOfService
    {
      get
      {
        return b_TermsOfService;
      }
      set
      {
        PropertyChanged.RaiseHandler<ICommand>(value, ref b_TermsOfService, "TermsOfService", this);
      }
    }
    //private
    private ICommand b_TermsOfService;
    private ICommand b_ViewLicense;
    private ICommand b_ReadMe;
    private ICommand b_OpenProducerConfiguration;
    private ICommand b_OpenConsumerConfiguration;
    private ICommand b_ConfigurationFolder;
    private ICommand b_HelpDocumentation;
    #endregion

    #region IConsumerViewModel 
    /// <summary>
    /// Saves the consumer user settings.
    /// </summary>
    public void SaveConsumerUserSettings()
    {
      Properties.Settings.Default.UDPPort = UDPPort;
      IPAddress _ma = GetMulticastGroupIPAddress();
      if (_ma == null)
        Properties.Settings.Default.JoinMulticastGroup = false;
      else
      {
        Properties.Settings.Default.JoinMulticastGroup = true;
        Properties.Settings.Default.DefaultMulticastGroup = _ma.ToString();
      }
    }
    /// <summary>
    /// Gets or sets the consumer received bytes.
    /// </summary>
    /// <value>The consumer received bytes.</value>
    public int ConsumerReceivedBytes
    {
      get
      {
        return b_ConsumerBytesReceived;
      }
      set
      {
        PropertyChanged.RaiseHandler<int>(value, ref b_ConsumerBytesReceived, "ConsumerReceivedBytes", this);
      }
    }
    /// <summary>
    /// Gets or sets the number of consumer received frames .
    /// </summary>
    /// <value>The consumer frames received.</value>
    public int ConsumerFramesReceived
    {
      get
      {
        return b_ConsumerFramesReceived;
      }
      set
      {
        PropertyChanged.RaiseHandler<int>(value, ref b_ConsumerFramesReceived, "ConsumerFramesReceived", this);
      }
    }
    /// <summary>
    /// Gets or sets the consumer update configuration command.
    /// </summary>
    /// <value>The consumer update configuration <see cref="ICommand" />.</value>
    public ICommand ConsumerUpdateConfiguration
    {
      get
      {
        return b_ConsumerUpdateConfiguration;
      }
      set
      {
        PropertyChanged.RaiseHandler<ICommand>(value, ref b_ConsumerUpdateConfiguration, "ConsumerUpdateConfiguration", this);
      }
    }
    /// <summary>
    /// Gets or sets the last consumer error message.
    /// </summary>
    /// <value>The consumer error message.</value>
    public string ConsumerErrorMessage
    {
      get
      {
        return b_ConsumerErrorMessage;
      }
      set
      {
        PropertyChanged.RaiseHandler<string>(value, ref b_ConsumerErrorMessage, "ConsumerErrorMessage", this);
      }
    }
    /// <summary>
    /// Add the message to the <see cref="MainWindowViewModel.ConsumerLog"/>.
    /// </summary>
    /// <param name="message">The message to be added to the log <see cref="MainWindowViewModel.ConsumerLog"/>.</param>
    public void Trace(string message)
    {
      GalaSoft.MvvmLight.Threading.DispatcherHelper.RunAsync((() => ConsumerLog.Insert(0, message)));
    }
    /// <summary>
    /// Helper method that creates the consumer binding.
    /// </summary>
    /// <param name="variableName">Name of the variable.</param>
    /// <param name="encoding">The encoding.</param>
    /// <returns>IConsumerBinding.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">variableName</exception>
    public IConsumerBinding GetConsumerBinding(string variableName, BuiltInType encoding)
    {
      IConsumerBinding _return = null;
      switch (encoding)
      {
        case BuiltInType.Boolean:
          _return = AddBinding<Boolean>(variableName, BuiltInType.Boolean);
          break;
        case BuiltInType.SByte:
          _return = AddBinding<SByte>(variableName, BuiltInType.SByte);
          break;
        case BuiltInType.Byte:
          _return = AddBinding<Byte>(variableName, BuiltInType.Byte);
          break;
        case BuiltInType.Int16:
          _return = AddBinding<Int16>(variableName, BuiltInType.Int16);
          break;
        case BuiltInType.UInt16:
          _return = AddBinding<UInt16>(variableName, BuiltInType.UInt16);
          break;
        case BuiltInType.Int32:
          _return = AddBinding<Int32>(variableName, BuiltInType.Int32);
          break;
        case BuiltInType.UInt32:
          _return = AddBinding<UInt32>(variableName, BuiltInType.UInt32);
          break;
        case BuiltInType.Int64:
          _return = AddBinding<Int64>(variableName, BuiltInType.Int64);
          break;
        case BuiltInType.UInt64:
          _return = AddBinding<UInt64>(variableName, BuiltInType.UInt64);
          break;
        case BuiltInType.Float:
          _return = AddBinding<float>(variableName, BuiltInType.Float);
          break;
        case BuiltInType.Double:
          _return = AddBinding<Double>(variableName, BuiltInType.Double);
          break;
        case BuiltInType.String:
          _return = AddBinding<String>(variableName, BuiltInType.String);
          break;
        case BuiltInType.DateTime:
          _return = AddBinding<DateTime>(variableName, BuiltInType.DateTime);
          break;
        case BuiltInType.Guid:
          _return = AddBinding<Guid>(variableName, BuiltInType.Guid);
          break;
        case BuiltInType.ByteString:
          _return = AddBinding<byte[]>(variableName, BuiltInType.ByteString);
          break;
        case BuiltInType.Null:
        case BuiltInType.XmlElement:
        case BuiltInType.NodeId:
        case BuiltInType.ExpandedNodeId:
        case BuiltInType.StatusCode:
        case BuiltInType.QualifiedName:
        case BuiltInType.LocalizedText:
        case BuiltInType.ExtensionObject:
        case BuiltInType.DataValue:
        case BuiltInType.Variant:
        case BuiltInType.DiagnosticInfo:
        case BuiltInType.Enumeration:
        default:
          throw new ArgumentOutOfRangeException("encoding");
      }
      return _return;
    }
    #endregion

    #region Consumer ViewModel implementation
    /// <summary>
    /// Gets or sets the UDP port.
    /// </summary>
    /// <value>The UDP port.</value>
    public int UDPPort
    {
      get
      {
        return b_UDPPort;
      }
      set
      {
        PropertyChanged.RaiseHandler<int>(value, ref b_UDPPort, "UDPPort", this);
      }
    }
    public ObservableCollection<string> ConsumerLog
    {
      get
      {
        return b_ConsumerLog;
      }
      set
      {
        PropertyChanged.RaiseHandler<ObservableCollection<string>>(value, ref b_ConsumerLog, "ConsumerLog", this);
      }
    }
    public string MulticastGroup
    {
      get
      {
        return b_MulticastGroup;
      }
      set
      {
        PropertyChanged.RaiseHandler<string>(value, ref b_MulticastGroup, "MulticastGroup", this);
      }
    }
    public bool MulticastGroupSelection
    {
      get
      {
        return b_MulticastGroupSelection;
      }
      set
      {
        PropertyChanged.RaiseHandler<bool>(value, ref b_MulticastGroupSelection, "MulticastGroupSelection", this);
      }
    }
    #endregion

    #region Producer ViewModel implementation
    public int BytesSent
    {
      get
      {
        return b_BytesSent;
      }
      set
      {
        PropertyChanged.RaiseHandler<int>(value, ref b_BytesSent, "BytesSent", this);
      }
    }
    public int PackagesSent
    {
      get
      {
        return b_PackagesSent;
      }
      set
      {
        PropertyChanged.RaiseHandler<int>(value, ref b_PackagesSent, "PackagesSent", this);
      }
    }
    public string RemoteHost
    {
      get
      {
        return b_RemoteHost;
      }
      set
      {
        if (PropertyChanged.RaiseHandler<string>(value, ref b_RemoteHost, "RemoteHost", this))
          Properties.Settings.Default.RemoteHostName = value;
      }
    }
    public int RemotePort
    {
      get
      {
        return b_RemotePort;
      }
      set
      {
        if (PropertyChanged.RaiseHandler<int>(value, ref b_RemotePort, "RemotePort", this))
          Properties.Settings.Default.RemoteUDPPortNumber = value;
      }
    }
    public ICommand ProducerRestart
    {
      get
      {
        return b_ProducerRestart;
      }
      set
      {
        PropertyChanged.RaiseHandler<ICommand>(value, ref b_ProducerRestart, "ProducerRestart", this);
      }
    }
    public string ProducerErrorMessage
    {
      get
      {
        return b_ProducerErrorMessage;
      }
      set
      {
        PropertyChanged.RaiseHandler<string>(value, ref b_ProducerErrorMessage, "ProducerErrorMessage", this);
      }
    }
    #endregion

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    #endregion

    #region private
    //types
    private class OpenFileCommand : ICommand
    {
      private string m_FileName;

      public OpenFileCommand(string fileName)
      {
        m_FileName = fileName;
      }
      public event EventHandler CanExecuteChanged;
      public bool CanExecute(object parameter)
      {
        return true;
      }
      public void Execute(object parameter)
      {
        string path = string.Empty;
        try
        {
          path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
          using (Process process = Process.Start(Path.Combine(path, m_FileName))) { }
        }
        catch (Exception _ex)
        {
          MessageBox.Show($"An error occurs during opening the file {path}. Error message: {_ex}", "Problem with opening a file !", MessageBoxButton.OK, MessageBoxImage.Error);
          return;
        }
      }
    }
    private class WebDocumentationCommand : ICommand
    {

      public WebDocumentationCommand(string url)
      {
        m_URL = url;
      }
      public event EventHandler CanExecuteChanged;
      public bool CanExecute(object parameter)
      {
        return true;
      }
      public void Execute(object parameter)
      {
        try
        {
          using (Process process = Process.Start(m_URL)) { }
        }
        catch (Exception _ex)
        {
          MessageBox.Show($"An error occurs during opening the web page at: {_ex}", "Problem with the website!", MessageBoxButton.OK, MessageBoxImage.Error);
          return;
        }
      }
      private readonly string m_URL;

    }
    private class ConfigurationFolderCommand : ICommand
    {
      public event EventHandler CanExecuteChanged;
      public bool CanExecute(object parameter)
      {
        return true;
      }
      public void Execute(object parameter)
      {
        string path = string.Empty;
        try
        {
          path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
          using (Process process = Process.Start(@path)) { }
        }
        catch (Win32Exception)
        {
          MessageBox.Show($"No configuration folder exists at: {path}.", "No Log folder !", MessageBoxButton.OK, MessageBoxImage.Stop);
          return;
        }
        catch (Exception _ex)
        {
          MessageBox.Show($"An error occurs during opening the folder {_ex}", "Problem with log folder !", MessageBoxButton.OK, MessageBoxImage.Error);
          return;
        }
      }
    }
    //vars
    //Consumer private part
    private bool b_MulticastGroupSelection;
    private string b_MulticastGroup;
    private ObservableCollection<string> b_ConsumerLog;
    private string b_ConsumerErrorMessage;
    private ICommand b_ConsumerUpdateConfiguration;
    private int b_ConsumerFramesReceived;
    private int b_ConsumerBytesReceived;
    private int b_UDPPort;
    //producer private part
    private int b_BytesSent;
    private int b_PackagesSent;
    private string b_RemoteHost;
    private int b_RemotePort;
    private ICommand b_ProducerRestart;
    private string b_ProducerErrorMessage;
    //methods
    private IConsumerBinding AddBinding<type>(string variableName, BuiltInType encoding)
    {
      ConsumerBindingMonitoredValue<type> _return = new ConsumerBindingMonitoredValue<type>(encoding);
      _return.PropertyChanged += (x, y) => Trace($"{DateTime.Now.ToLongTimeString()}:{DateTime.Now.Millisecond} {variableName} = {((ConsumerBindingMonitoredValue<type>)x).ToString()}");
      return _return;
    }
    private IPAddress GetMulticastGroupIPAddress()
    {
      try
      {
        if (!MulticastGroupSelection)
          return null;
        Controls.IPAddressValidationRule _vr = new IPAddressValidationRule();
        ValidationResult _res = _vr.Validate(MulticastGroup, CultureInfo.InvariantCulture);
        if (!_res.IsValid)
        {
          Trace($"Removed multicast group because of error {_res.ErrorContent}");
          MulticastGroupSelection = false;
          return null;
        }
        return IPAddress.Parse(MulticastGroup);
      }
      catch (Exception _ex)
      {
        Trace($"Removed multicast group because of exception: {_ex.GetType().Name} with the message: {_ex.Message}");
        MulticastGroupSelection = false;
        return null;
      }
    }
#if DEBUG
    internal IPAddress DebugGetMulticastGroupIPAddress()
    {
      return GetMulticastGroupIPAddress();
    }
#endif
    #endregion

  }

}
