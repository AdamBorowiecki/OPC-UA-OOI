﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UANodeSetValidation;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UnitTest
{
  [TestClass]
  public class ContextUnitTest
  {
    [TestMethod]
    [ExpectedException(typeof(System.ArgumentNullException))]
    public void CreateUAModelContextNodeAliasNull()
    {
      UANodeSet _tm = TestData.CreateNodeSetModel();
      AddressSpaceContext<ModelDesign> _as = new AddressSpaceContext<ModelDesign>(x => { });
      UAModelContext<ModelDesign> _mc = new UAModelContext<ModelDesign>(null, _tm.NamespaceUris, _as);
    }
    [TestMethod]
    [ExpectedException(typeof(System.NotImplementedException))]
    public void CreateUAModelContextModelNamespaceUrisNull()
    {
      UANodeSet _tm = TestData.CreateNodeSetModel();
      UAModelContext<ModelDesign> _mc = new UAModelContext<ModelDesign>(_tm.Aliases, null, null);
      Assert.IsNotNull(_mc);
      Assert.IsNull(_mc.GetAddressSpaceContext);
    }
    [TestMethod]
    [ExpectedException(typeof(System.NotImplementedException))]
    public void CreateUAModelContextAddressSpaceContextNull()
    {
      UANodeSet _tm = TestData.CreateNodeSetModel();
      UAModelContext<ModelDesign> _mc = new UAModelContext<ModelDesign>(_tm.Aliases, _tm.NamespaceUris, null);
      Assert.IsNotNull(_mc);
      Assert.IsNull(_mc.GetAddressSpaceContext);
    }
    [TestMethod]
    [ExpectedException(typeof(System.NotImplementedException))]
    public void CreateUAModelContext()
    {
      UANodeSet _tm = TestData.CreateNodeSetModel();
      UAModelContext<ModelDesign> _mc = new UAModelContext<ModelDesign>(_tm.Aliases, _tm.NamespaceUris, null);
      Assert.IsNotNull(_mc);
      Assert.IsNull(_mc.GetAddressSpaceContext);
    }
  }
}