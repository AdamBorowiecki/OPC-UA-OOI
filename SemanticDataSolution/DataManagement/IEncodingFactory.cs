﻿using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement
{
  /// <summary>
  /// Interface IEncodingFactory - provides functionality to lookup a dictionary containing value converters.
  /// It is expected that the encoding/decoding functionality is provided outside of this library
  /// The interface is used for late binding to inject dependency on the external library. 
  /// </summary>
  public interface IEncodingFactory
  {

    /// <summary>
    /// Updates the value converter.
    /// </summary>
    /// <param name="binding">An object responsible transfer the value between the message and ultimated destination in the repository.</param>
    /// <param name="repositoryGroup">The repository group.</param>
    /// <param name="sourceEncoding">The source encoding.</param>
    void UpdateValueConverter(IBinding binding, string repositoryGroup, BuiltInType sourceEncoding);

  }
}
