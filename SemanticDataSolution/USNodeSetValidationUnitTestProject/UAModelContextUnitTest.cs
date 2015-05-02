﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UANodeSetValidation;
using UAOOI.SemanticData.UANodeSetValidation.XML;
using UAOOI.SemanticData.UnitTest.Helpers;

namespace UAOOI.SemanticData.UnitTest
{
  [TestClass]
  public class UAModelContextUnitTest
  {
    [TestMethod]
    [ExpectedException(typeof(System.ArgumentNullException))]
    public void CreateUAModelContextNodeAliasNull()
    {
      UANodeSet _tm = TestData.CreateNodeSetModel();
      AddressSpaceContext _as = new AddressSpaceContext(x => { });
      UAModelContext _mc = new UAModelContext(null, _tm.NamespaceUris, _as);
    }
    [TestMethod]
    public void CreateUAModelContextModelNamespaceUrisNull()
    {
      UANodeSet _tm = TestData.CreateNodeSetModel();
      AddressSpaceContext _as = new AddressSpaceContext(x => { });
      UAModelContext _mc = new UAModelContext(_tm.Aliases, null, _as);
    }
    [TestMethod]
    [ExpectedException(typeof(System.ArgumentNullException))]
    public void CreateUAModelContextAddressSpaceContextNull()
    {
      UANodeSet _tm = TestData.CreateNodeSetModel();
      UAModelContext _mc = new UAModelContext(_tm.Aliases, _tm.NamespaceUris, null);
      Assert.IsNotNull(_mc);
      Assert.IsNull(_mc.GetAddressSpaceContext);
    }
    [TestMethod]
    public void CreateUAModelContext()
    { 
      UANodeSet _tm = TestData.CreateNodeSetModel();
      AddressSpaceContext _as = new AddressSpaceContext(x => { });
      UAModelContext _mc = new UAModelContext(_tm.Aliases, _tm.NamespaceUris, _as);
      Assert.IsNotNull(_mc);
      Assert.IsNull(_mc.GetAddressSpaceContext);
    }
  }
}
