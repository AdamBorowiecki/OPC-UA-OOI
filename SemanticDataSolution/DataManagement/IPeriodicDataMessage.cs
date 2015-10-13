﻿
using System;
using UAOOI.SemanticData.DataManagement.DataRepository;

namespace UAOOI.SemanticData.DataManagement
{

  /// <summary>
  /// Class Message - placeholder to implement the message send over the wire.
  /// </summary>
  public interface IPeriodicDataMessage
  {

    /// <summary>
    /// Updates my values - implementation .
    /// </summary>
    /// <param name="update">The update.</param>
    void UpdateMyValues(Func<int, IConsumerBinding> update);
    /// <summary>
    /// Check if the message destination is the data set described by the <paramref name="dataId"/> of type <see cref="ISemanticData"/>.
    /// </summary>
    /// <param name="dataId">The data identifier <see cref="ISemanticData"/>.</param>
    /// <returns><c>true</c> if <paramref name="dataId"/> is the destination of the message, <c>false</c> otherwise.</returns>
    bool IAmDestination(ISemanticData dataId);

  }
}
