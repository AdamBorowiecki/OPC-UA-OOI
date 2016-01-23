﻿
using CAS.UA.IServerConfiguration;
using System;
using System.ComponentModel.Composition;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.DataBindings.UnitTest.Exports
{

  /// <summary>
  /// Class ConfigurationEditorBase - a simple implementation of the <see cref="IConfigurationEditor"/>.
  /// </summary>
  [Export(typeof(IConfigurationEditor))]
  public class ConfigurationEditorBase : IConfigurationEditor
  {
    /// <summary>
    /// Creates the instance configurations.
    /// </summary>
    /// <param name="descriptors">The descriptors.</param>
    /// <param name="SkipOpeningConfigurationFile">if set to <c>true</c> skip opening configuration file.</param>
    /// <param name="CancelWasPressed">The cancel was pressed.</param>
    public virtual void CreateInstanceConfigurations(INodeDescriptor[] descriptors, bool SkipOpeningConfigurationFile, Action<bool> CancelWasPressed)
    {
      throw new NotImplementedException("CreateInstanceConfigurations is not implemented yet");
      //MessageBox.Show("CreateInstanceConfigurations is not implemented yet", "Library functionality", MessageBoxButton.OK, MessageBoxImage.Question);
    }
    /// <summary>
    /// Open configuration editor.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    public virtual void EditConfiguration(ConfigurationData configuration)
    {
      throw new NotImplementedException("EditConfiguration is not implemented yet");
      //MessageBox.Show("EditConfiguration is not implemented yet", "Library functionality", MessageBoxButton.OK, MessageBoxImage.Question);
    }

  }

}
