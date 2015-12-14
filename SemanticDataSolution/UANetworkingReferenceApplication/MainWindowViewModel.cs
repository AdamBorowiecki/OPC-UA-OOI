﻿
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

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
    }

    #region API
    /// <summary>
    /// Helper method that creates the consumer binding.
    /// </summary>
    /// <param name="variableName">Name of the variable.</param>
    /// <param name="encoding">The encoding.</param>
    /// <returns>IConsumerBinding.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">variableName</exception>
    public IConsumerBinding GetConsumerBinding(string variableName, BuiltInType encoding)
    {
      if (variableName == "Value1")
      {
        Value1 = new ConsumerBindingMonitoredValue<DateTime>(encoding);
        Value1.PropertyChanged += (x, y) => Trace($"{DateTime.Now.ToLongTimeString()}:{DateTime.Now.Millisecond} {variableName} = {((ConsumerBindingMonitoredValue<DateTime>)x).Value.ToString()}");
        return Value1;
      }
      else if (variableName == "Value2")
      {
        Value2 = new ConsumerBindingMonitoredValue<Int32>(encoding);
        Value2.PropertyChanged += (x, y) => Trace($"{DateTime.Now.ToLongTimeString()}:{DateTime.Now.Millisecond} {variableName} = {((ConsumerBindingMonitoredValue<Int32>)x).Value.ToString()}");
        return Value2;
      }
      else
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
          case BuiltInType.Null:
          case BuiltInType.ByteString:
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
    }
    #endregion
    private IConsumerBinding AddBinding<type>(string variableName, BuiltInType encoding)
    {
      ConsumerBindingMonitoredValue<type> _return = new ConsumerBindingMonitoredValue<type>(encoding);
      _return.PropertyChanged += (x, y) => Trace($"{DateTime.Now.ToLongTimeString()}:{DateTime.Now.Millisecond} {variableName} = {((ConsumerBindingMonitoredValue<type>)x).Value.ToString()}");
      return _return;
    }

    #region IConsumerViewModel Consumer User Interface ViewModel implementation
    /// <summary>
    /// Add the message to the <see cref="MainWindowViewModel.ConsumerLog"/>.
    /// </summary>
    /// <param name="message">The message to be added to the log <see cref="MainWindowViewModel.ConsumerLog"/>.</param>
    public void Trace(string message)
    {
      GalaSoft.MvvmLight.Threading.DispatcherHelper.RunAsync((() => ConsumerLog.Insert(0, message)));
    }
    /// <summary>
    /// Gets or sets the value1 - an example of OPC UA data binded to the <see cref="System.Windows.Controls.TextBox"/>.
    /// </summary>
    /// <value>The value1 represented by the <see cref="ConsumerBindingMonitoredValue"/>.</value>
    public ConsumerBindingMonitoredValue<DateTime> Value1
    {
      get
      {
        return b_Value1;
      }
      set
      {
        PropertyChanged.RaiseHandler<ConsumerBindingMonitoredValue<DateTime>>(value, ref b_Value1, "Value1", this);
      }
    }
    /// <summary>
    /// Gets or sets the value2 - an example of OPC UA data binded to the <see cref="System.Windows.Controls.TextBox"/>.
    /// </summary>
    /// <value>The value2.</value>
    public ConsumerBindingMonitoredValue<Int32> Value2
    {
      get
      {
        return b_Value2;
      }
      set
      {
        PropertyChanged.RaiseHandler<ConsumerBindingMonitoredValue<Int32>>(value, ref b_Value2, "Value2", this);
      }
    }
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
        if (PropertyChanged.RaiseHandler<int>(value, ref b_UDPPort, "UDPPort", this))
          Properties.Settings.Default.UDPPort = value;
      }
    }
    public int ConsumerBytesReceived
    {
      get
      {
        return b_ConsumerBytesReceived;
      }
      set
      {
        PropertyChanged.RaiseHandler<int>(value, ref b_ConsumerBytesReceived, "ConsumerBytesReceived", this);
      }
    }
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
    //private part
    private ObservableCollection<string> b_ConsumerLog;
    private string b_ConsumerErrorMessage;
    private ICommand b_ConsumerUpdateConfiguration;
    private int b_ConsumerFramesReceived;
    private int b_ConsumerBytesReceived;
    private int b_UDPPort;
    private ConsumerBindingMonitoredValue<DateTime> b_Value1;
    private ConsumerBindingMonitoredValue<Int32> b_Value2;
    #endregion

    #region Producer user interface
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
    //private part
    private int b_BytesSent;
    private int b_PackagesSent;
    private string b_RemoteHost;
    private int b_RemotePort;
    private ICommand b_ProducerRestart;
    private string b_ProducerErrorMessage;


    #endregion

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    #endregion

  }

}
