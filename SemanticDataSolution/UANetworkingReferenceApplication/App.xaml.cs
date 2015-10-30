﻿
using System;
using System.Collections.Generic;
using System.Windows;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    /// <summary>
    /// Handles the Startup event of the App control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="StartupEventArgs"/> instance containing the event data.</param>
    private void App_Startup(object sender, StartupEventArgs e)
    {
      GalaSoft.MvvmLight.Threading.DispatcherHelper.Initialize();
      // Open a window
      MainWindow window = new MainWindow();
      MainWindowViewModel _cbf = (MainWindowViewModel)window.DataContext;
      Consumer.ConsumerDataManagementSetup.CreateDevice(_cbf, x => m_DisposableCollection.Add(x));
      Producer.OPCUAServerProducerSimulator.CreateDevice(x => m_DisposableCollection.Add(x), y => m_Log.Add(y), _cbf);
      window.Show();
    }
    /// <summary>
    /// Handles the Exit event of the App control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="ExitEventArgs"/> instance containing the event data.</param>
    private void App_Exit(object sender, ExitEventArgs e)
    {
      foreach (IDisposable _toDispose in m_DisposableCollection)
        _toDispose.Dispose();
      ReferenceApplication.Properties.Settings.Default.Save();
    }
    private List<IDisposable> m_DisposableCollection = new List<IDisposable>();
    private List<string> m_Log = new List<string>();

  }
}
