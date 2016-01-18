﻿
using CAS.UA.IServerConfiguration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.DataBindings.UnitTest
{

  [TestClass]
  public class UANetworkingConfigurationEditorUnitTest
  {

    [TestMethod]
    [TestCategory("DataBindings_UANetworkingConfigurationEditor")]
    public void GetIServerConfigurationTestMethod()
    {
      FileInfo _fileInfo = new FileInfo("UAOOI.DataBindings.dll");
      Assert.IsTrue(_fileInfo.Exists);
      Assembly _pluginAssembly = null;
      IConfiguration _serverConfiguration = null;
      GetIServerConfiguration(_fileInfo, out _pluginAssembly, out _serverConfiguration);
      Assert.IsNotNull(_pluginAssembly);
      Assert.IsNotNull(_serverConfiguration);
      UANetworkingConfigurationEditor _editor = (UANetworkingConfigurationEditor)_serverConfiguration;
      Assert.IsNotNull(_editor);
      Assert.IsNotNull(_editor.ConfigurationEditor);
    }
    [TestMethod]
    [TestCategory("DataBindings_UANetworkingConfigurationEditor")]
    public void GetMyIServerConfigurationTestMethod()
    {
      FileInfo _fileInfo = new FileInfo("UAOOI.DataBindings.dll");
      Assert.IsTrue(_fileInfo.Exists);
      Assembly _pluginAssembly = null;
      IConfiguration _serverConfiguration = null;
      GetIServerConfiguration(_fileInfo, out _pluginAssembly, out _serverConfiguration);
      Assert.IsNotNull(_pluginAssembly);
      Assert.IsNotNull(_serverConfiguration);
      UANetworkingConfigurationEditor _editor = (UANetworkingConfigurationEditor)_serverConfiguration;
      Assert.IsNotNull(_editor);
      Assert.IsNotNull(_editor.ConfigurationEditor);
    }
    [TestMethod]
    [TestCategory("DataBindings_UANetworkingConfigurationEditor")]
    public void DefaultFileNameTestMethod()
    {
      UANetworkingConfigurationEditor _mc = new UANetworkingConfigurationEditor();
      Assert.IsNotNull(_mc);
      string _fileName = _mc.DefaultFileName;
      FileInfo _fi = new FileInfo(_fileName);
      Assert.AreEqual<string>(".uasconfig", _fi.Extension);
      Assert.AreEqual<string>("UANetworkingConfiguration.uasconfig", _fi.Name);
    }
    [TestMethod]
    [TestCategory("DataBindings_UANetworkingConfigurationEditor")]
    public void CreateDefaultConfigurationTestMethod()
    {
      UANetworkingConfigurationEditor _newConfiguration = new UANetworkingConfigurationEditor();
      Assert.IsNotNull(_newConfiguration);
      _newConfiguration.CreateDefaultConfiguration();
      Assert.IsNotNull(_newConfiguration.CurrentConfiguration);
      ConfigurationData _CurrentConfiguration = _newConfiguration.CurrentConfiguration;
      Assert.IsNotNull(_CurrentConfiguration.DataSets);
      Assert.AreEqual<int>(0, _CurrentConfiguration.DataSets.Length);
      Assert.IsNotNull(_CurrentConfiguration.MessageHandlers);
      Assert.AreEqual<int>(0, _CurrentConfiguration.MessageHandlers.Length);
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void GetInstanceConfigurationNullTestMethod()
    {
      UANetworkingConfigurationEditor _newConfiguration = new UANetworkingConfigurationEditor();
      Assert.IsNotNull(_newConfiguration);
      IInstanceConfiguration _newInstanceConfiguration = _newConfiguration.GetInstanceConfiguration(null);
    }
    [TestMethod]
    [TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    public void GetInstanceConfigurationNoConfigurationTestMethod()
    {
      UANetworkingConfigurationEditor _newConfiguration = new UANetworkingConfigurationEditor();
      Assert.IsNotNull(_newConfiguration);
      NodeDescriptor _nd =  NodeDescriptor.GetTestInstance();
      IInstanceConfiguration _newInstanceConfiguration = _newConfiguration.GetInstanceConfiguration(_nd);
      Assert.IsNotNull(_newInstanceConfiguration);
      IInstanceConfiguration _nxtInstanceConfiguration = _newConfiguration.GetInstanceConfiguration(_nd);
      Assert.AreNotSame(_newInstanceConfiguration, _nxtInstanceConfiguration);
      Assert.AreEqual<string>(_newInstanceConfiguration.ToString(), _nxtInstanceConfiguration.ToString());
    }
    //[TestMethod]
    //[TestCategory("Configuration_UANetworkingConfigurationUnitTest")]
    //public void GetInstanceConfigurationTestMethod()
    //{
    //  //create hard coded configuration 
    //  DerivedUANetworkingConfiguration _newConfiguration = new DerivedUANetworkingConfiguration();
    //  Assert.IsNotNull(_newConfiguration);
    //  _newConfiguration.DefaultConfigurationLoader = ReferenceConfiguration.LoadConsumer;
    //  bool _ConfigurationFileChanged = false;
    //  _newConfiguration.OnModified += (x, y) => { Assert.IsTrue(y.ConfigurationFileChanged); _ConfigurationFileChanged = y.ConfigurationFileChanged; };
    //  _newConfiguration.CreateDefaultConfiguration();
    //  Assert.IsTrue(_ConfigurationFileChanged);
    //  Assert.IsNotNull(_newConfiguration.CurrentConfiguration);
    //  //test GetInstanceConfiguration
    //  INodeDescriptor _nd = new NodeDescriptor(new XmlQualifiedName("NodeDescriptor", "NodeDescriptorNS"));
    //  IInstanceConfiguration _newInstanceConfiguration = _newConfiguration.GetInstanceConfiguration(_nd);
    //  Assert.IsNotNull(_newInstanceConfiguration);
    //}

    private static void GetIServerConfiguration(FileInfo info, out Assembly pluginAssembly, out IConfiguration serverConfiguration)
    {
      string iName = typeof(IConfiguration).ToString();
      pluginAssembly = Assembly.LoadFrom(info.FullName);
      serverConfiguration = null;
      foreach (Type pluginType in pluginAssembly.GetExportedTypes())
        //Only look at public types
        if (pluginType.IsPublic && !pluginType.IsAbstract && pluginType.GetInterface(iName) != null)
          try
          {
            serverConfiguration = (IConfiguration)Activator.CreateInstance(pluginType);
          }
          catch (TargetInvocationException _ex)
          {
            throw new ApplicationException(String.Format("The server configuration plug-in {0}/{1} cannot be loaded. Contact the vendor to get current version of this component", pluginType.FullName, info.Name), _ex);
          }
    }
  }

}

