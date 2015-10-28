﻿using System;
using UAOOI.SemanticData.DataManagement;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.Producer
{

  /// <summary>
  /// Class OPCUAServerProducerSimulator simulates interface to internal <see cref="CustomNodeManager"/> class.
  /// </summary>
  internal class OPCUAServerProducerSimulator : DataManagementSetup
  {
    #region creator
    internal static void CreateDevice(Action<IDisposable> toDispose, Action<string> trace, IProducerModelView modelView)
    {
      Current = new OPCUAServerProducerSimulator();
      Current.ConfigurationFactory = new ProducerConfigurationFactory();
      CustomNodeManager _simulator = new CustomNodeManager();
      toDispose(_simulator);
      Current.BindingFactory = _simulator;
      Current.EncodingFactory = _simulator;
      Current.MessageHandlerFactory = new ProducerMessageHandlerFactory(toDispose, trace, modelView);
      Current.Initialize();
      Current.Run();
      _simulator.Run();
    }
    #endregion
    /// <summary>
    /// Gets the current instance of the <see cref="OPCUAServerProducerSimulator"/>.
    /// </summary>
    /// <value>The current.</value>
    public static OPCUAServerProducerSimulator Current { get; private set; }

  }
}
